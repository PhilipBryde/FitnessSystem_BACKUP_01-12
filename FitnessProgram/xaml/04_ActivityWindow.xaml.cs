using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Text; // Nødvendig for StringBuilder

namespace FitnessProgram
{
    public partial class ActivityWindow : Window
    {
        private readonly Fitness fitness; // Shared fitness system
        private readonly Member member;   // Logged in user
        public int newActCount = 0; //Variabel der sørger for at der ikke kan laves for mange nye aktiviteter

        public ActivityWindow(Fitness fitness, Member member)
        {
            InitializeComponent();
            // Tilføjer null-checks for robusthed (baseret på tidligere korrektioner)
            this.fitness = fitness;
            this.member = member;

            ShowActivity(); //Viser aktiviteter med medlemmer
            ApplyRoleRestrictions(); // fjerner admin controls if not admin
            ApplyRoleRestrictions1(); // Member leave or join activity
            UpdateAllCapacities(); // updater så man kan se hvor mange er på en aktivitet

            // Sørger for at fjerne ID/Køn så medlemer ikke kan se hinandens info
            FormatTextForNonAdmin();
        }


        // Hide admin-only controls
        private void ApplyRoleRestrictions()
        {
            if (member.role.ToLower() != "admin")
            {
                // Bruger null-check logik for at undgå fejl hvis elementer mangler i XAML
                if (DeleteMemberButton != null) DeleteMemberButton.Visibility = Visibility.Collapsed;
                if (CreateActivity != null) CreateActivity.Visibility = Visibility.Collapsed;
                if (EnterActivity != null) EnterActivity.Visibility = Visibility.Collapsed;
                if (EnterMember != null) EnterMember.Visibility = Visibility.Collapsed;
            }
        }

        // Sidney Kode
        private void ShowActivity() //Oprettelse af aktiviteter
        {
            List<string> localMembers = fitness.MemberFromFile(); //Får listen over medlemmer fra Fitness-klassen
            List<string> localActivities = fitness.ActivityFromFile(); //Får listen over aktiviteter fra Fitness-klassen

            if (Yoga != null) // Sikkerhedscheck for TextBlock
            {
                Yoga.Text = localActivities[0].ToUpper() + Environment.NewLine +
                            localMembers[1] + Environment.NewLine +
                            localMembers[3] + Environment.NewLine +
                            localMembers[8] + Environment.NewLine +
                            localMembers[11] + Environment.NewLine +
                            localMembers[13]; //TextBlock hvor der er manuelt er lagt visse medlemmer ind ved brug af indexer fra listen
            }

            if (Boxing != null)
            {
                Boxing.Text = localActivities[1].ToUpper() + Environment.NewLine +
                              localMembers[1] + Environment.NewLine +
                              localMembers[4] + Environment.NewLine +
                              localMembers[7];
            }

            if (Spinning != null)
            {
                Spinning.Text = localActivities[2].ToUpper() + Environment.NewLine +
                                localMembers[0] + Environment.NewLine +
                                localMembers[2] + Environment.NewLine +
                                localMembers[9] + Environment.NewLine +
                                localMembers[10];
            }

            if (Pilates != null)
            {
                Pilates.Text = localActivities[3].ToUpper();
            }

            if (Crossfit != null)
            {
                Crossfit.Text = localActivities[4].ToUpper() + Environment.NewLine +
                    localMembers[3] + Environment.NewLine +
                    localMembers[8] + Environment.NewLine +
                    localMembers[9] + Environment.NewLine +
                    localMembers[13];
            }

            if (localActivities.Count > 5 && extraAct != null)
            {
                extraAct.Text = localActivities[5].ToUpper(); //Hvis der er kommet flere aktiviteter laves de her
            }

            if (localActivities.Count > 6 && extraAct1 != null)
            {
                extraAct1.Text = localActivities[6].ToUpper();
            }
            
        }

        // Sikrer at almindelige brugere kun ser navne i de viste TextBlocks -- Philip
        private void FormatTextForNonAdmin()
        {
            // Kun nødvendigt at køre for almindelige medlemmer
            if (member.role.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                return; // Admin skal se de fulde detaljer
            }

            // TextBlock references skal have null-check, da de er XAML-elementer
            TextBlock?[] activityBlocks = { Yoga, Boxing, Spinning, Pilates, Crossfit };

            foreach (var block in activityBlocks)
            {
                if (block == null) continue;

                List<string> lines = block.Text
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                if (lines.Count > 0)
                {
                    string activityName = lines[0]; // Behold altid aktivitetens navn

                    // Brug StringBuilder for effektiv linjesammenføjning
                    var sb = new StringBuilder();
                    sb.AppendLine(activityName);

                    for (int i = 1; i < lines.Count; i++)
                    {
                        string line = lines[i];
                        string memberName = line;

                        // Hvis linjen indeholder "ID:" eller "Køn:" betyder det, at den blev skrevet af en Admin 
                        if (line.Contains("Navn:", StringComparison.OrdinalIgnoreCase))
                        {
                            // Dette er det detaljerede ADMIN format. Trækker kun navnet ud.
                            int nameStart = line.IndexOf("Navn:", StringComparison.OrdinalIgnoreCase);
                            int nameEnd = line.IndexOf("Køn:", StringComparison.OrdinalIgnoreCase);

                            if (nameStart != -1)
                            {
                                int start = nameStart + "Navn:".Length;

                                if (nameEnd != -1 && nameEnd > start)
                                {
                                    // Substring fra "Navn:" til "Køn:"
                                    memberName = line.Substring(start, nameEnd - start).Trim();
                                }
                                else
                                {
                                    // Substring fra "Navn:" til slutningen af linjen
                                    memberName = line.Substring(start).Trim();
                                }
                            }
                        }
                        // Hvis linjen ikke indeholder ID/Køn, er det allerede kun navnet

                        if (!string.IsNullOrWhiteSpace(memberName))
                        {
                            sb.AppendLine(memberName);
                        }
                    }

                    block.Text = sb.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                }
            }
        }

        // Metode der fjerner medlem hvis man er admin - Sidney
        private void RemoveMemberFromActivity()
        {
            if (!int.TryParse(EnterActivity?.Text, out int activityIndex))
            {
                MessageBox.Show("Indtast aktivitet 1-5"); // Fejlbesked hvis der ikke kan konverteres til en int
                return;
            }
            
            if (!int.TryParse(EnterMember?.Text, out int memberId))
            {
                MessageBox.Show("Indtast gyldigt medlem ID"); // Fejlbesked hvis der ikke kan konverteres til en int
                return;
            }

            int memberIndex = memberId - 1; //Minus 1 da ID 1 = 0

            List<string> localMembers = fitness.MemberFromFile(); //Får listen af medlemmer fra Fitness-klassen

            if (memberIndex < 0 || memberIndex >= localMembers.Count)
            {
                MessageBox.Show("Medlem findes ikke!"); //Fejl besked hvis index ikke findes i listen
                return;
            }

            string memberName = localMembers[memberIndex]; //Tager hele stringen fra indexen

            TextBlock? target = activityIndex switch
            {
                1 => Yoga,
                2 => Boxing,
                3 => Spinning,
                4 => Pilates,
                5 => Crossfit,
                6 => extraAct,
                7 => extraAct1,
            };

            if (target == null) return;

            List<string> lines = target.Text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToList(); //Split linjer i TextBlocken til en liste af strings

            bool removed = false;

            for (int i = 1; i < lines.Count; i++)
            {
                // Tjekker om en a linjerne er i List<string> lines
                if (lines[i].Contains(memberName, StringComparison.OrdinalIgnoreCase))
                {
                    lines.RemoveAt(i); //Fjerner ved index
                    removed = true; //bool sættes til true og stopper løkken
                    break;
                }
            }

            if (!removed)
            {
                MessageBox.Show("Medlem er ikke i denne aktivitet."); //Hvis medlem ikke er i aktiviteten
                return;
            }

            target.Text = string.Join(Environment.NewLine, lines); //Opdaterer TextBlock
            MessageBox.Show($"Fjernede {memberName} fra aktiviteten.");

            UpdateAllCapacities(); //Opdaterer kapacitetsvisningen
        }

        private void ApplyRoleRestrictions1()
        {
            if (member.role.ToLower() == "admin") //Hide Bruger funktioner fra Admin -- philip
            {
                // Admin knapper
                if (DeleteMemberButton != null) DeleteMemberButton.Visibility = Visibility.Visible;
                if (CreateActivity != null) CreateActivity.Visibility = Visibility.Visible;
                if (EnterActivity != null) EnterActivity.Visibility = Visibility.Visible;
                if (EnterMember != null) EnterMember.Visibility = Visibility.Visible;

                // Skjul user buttons FOR ADMIN
                if (JoinButton != null) JoinButton.Visibility = Visibility.Collapsed;
                if (LeaveButton != null) LeaveButton.Visibility = Visibility.Collapsed;
                if (TypeActivityIn != null) TypeActivityIn.Visibility = Visibility.Collapsed;
                if (MeldDigTilHold != null) MeldDigTilHold.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Skjul admin-only controls FOR USER
                if (DeleteMemberButton != null) DeleteMemberButton.Visibility = Visibility.Collapsed;
                if (CreateActivity != null) CreateActivity.Visibility = Visibility.Collapsed;
                if (EnterActivity != null) EnterActivity.Visibility = Visibility.Collapsed;
                if (EnterMember != null) EnterMember.Visibility = Visibility.Collapsed;
                if (NewActivity != null) NewActivity.Visibility = Visibility.Collapsed;
                if (IndtastMedlemsID != null) IndtastMedlemsID.Visibility = Visibility.Collapsed;
                if (IndtastNummeretPåHoldet != null) IndtastNummeretPåHoldet.Visibility = Visibility.Collapsed;
                if (IndtastNavnetOpret != null) IndtastNavnetOpret.Visibility = Visibility.Collapsed;


                // Vis user buttons
                if (JoinButton != null) JoinButton.Visibility = Visibility.Visible;
                if (LeaveButton != null) LeaveButton.Visibility = Visibility.Visible;
            }
        }

        //Medlemer tilmelder sig aktivitet -- Philip & Sidney
        private void JoinActivity_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TypeActivityIn?.Text, out int activityIndex))
            {
                MessageBox.Show("Indtast et gyldigt aktivitetsnummer."); //Fejlbesked hvis der ikke kan konverteres til int
                return;
            }

            TextBlock? target = activityIndex switch // Switch case; f.eks. input 4 går ind i Pilates TextBlock
            {
                1 => Yoga,
                2 => Boxing,
                3 => Spinning,
                4 => Pilates,
                5 => Crossfit,
                6 => extraAct,
                7 => extraAct1,
                _ => null,
            };

            if (target == null)
            {
                MessageBox.Show("Indtast et gyldigt aktivitetsnummer"); //Fejlbesked hvis et forkert nummer bliver tastet ind (TextBlock findes ikke)
                return;
            }

            // ** FORMATERING BASERET PÅ ROLLE **
            string displayMember;
            if (member.role.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                // Admin ser det fulde format (som det var før)
                displayMember = $"ID: {member.id} Navn: {member.name} Køn: {member.gender}";
            }
            else
            {
                // Almindeligt medlem ser KUN navnet (som ønsket)
                displayMember = member.name;
            }

            // Split lines så teksten er mere læsbar
            List<string> lines = target.Text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            // Første linje er altid aktivitetens navn
            int currentCount = lines.Count - 1;

            // Tjek om brugeren allerede står på holdet
            if (lines.Any(line => line.Contains(member.name, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Du er allerede tilmeldt dette hold.");
                return;
            }

            // Fuldt hold på 5 tjek
            if (currentCount >= 5)
            {
                MessageBox.Show("Dette hold er fyldt. (5/5)");
                return;
            }

            // Tilføj medlem
            target.Text += Environment.NewLine + displayMember;
            // Skiftet fra CustomMessageBox til MessageBox.Show
            MessageBox.Show($"Du er nu tilmeldt {target.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).First()}."); // Brug den faktiske aktivitet

            UpdateAllCapacities(); //Opdater kapacitetsvisningen
        }

        //Medlemer melder sig af aktivitet - Philip
        private void LeaveActivity_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TypeActivityIn?.Text, out int activityIndex)) 
            {
                MessageBox.Show("Indtast et gyldigt aktivitetsnummer 1-5."); //Fejlbesked hvis der ikke kan konverteres til int
                return;
            }

            TextBlock? target = activityIndex switch // Switch case; f.eks. input 4 går ind i Pilates TextBlock
            {
                1 => Yoga,
                2 => Boxing,
                3 => Spinning,
                4 => Pilates,
                5 => Crossfit,
                6 => extraAct,
                7 => extraAct1,
                _ => null
            };

            if (target == null)
            {
                MessageBox.Show(" "); //Fejlbesked hvis et forkert nummer bliver tastet ind (TextBlock findes ikke)
            }

            // Denne linje er kun til identifikation i metoden, men den fulde streng er ikke nødvendig for fjernelse takket være 'Contains'
            string memberLineToRemove;
            if (member.role.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                memberLineToRemove = $"ID: {member.id} Navn: {member.name} Køn: {member.gender}";
            }
            else
            {
                memberLineToRemove = member.name;
            }

            // Split linjer og sætter dem i en liste af strings
            List<string> lines = target.Text
            .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();

            // Find og fjern den linje, der matcher navnet/den fulde linje. Starter ved 1 da 0 er aktivitetsnavn
            bool removed = false;
            for (int i = 1; i < lines.Count; i++)
            {
                // Tjek for en match. Vi bruger member.name, da den enten matcher det fulde admin-format eller det simple navn.
                if (lines[i].Contains(member.name, StringComparison.OrdinalIgnoreCase))
                {
                    lines.RemoveAt(i); //Fjerner ved index
                    removed = true;
                    break; //Stopper løkken og går ud
                }
            }


            if (!removed)
            {
                MessageBox.Show("Du er ikke tilmeldt dette hold."); //Besked hvis man ikke er på aktiviteten
                return;
            }

            target.Text = string.Join(Environment.NewLine, lines); //Opdaterer TextBlocken
            MessageBox.Show("Du er nu frameldt.");

            UpdateAllCapacities(); //Opdater kapacitetsvisningen
        }

        //Vis antal meldemer tilmeldt aktivitet, tager 2 TextBlock som input -- Philip 
        private void UpdateCapacity(TextBlock activityText, TextBlock countText)
        {
            int maxCapacity = 5; // Max kapacitet sat til 5

            var lines = activityText.Text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries); //Split linjer til en array af strings

            int count = lines.Length - 1; // minus aktivitets navn

            countText.Text = $"{count}/{maxCapacity} tilmeldt"; 
        }
        private void UpdateAllCapacities()
        {
            // Disse TextBlock referencer (YogaCount, BoxingCount, etc.) antages at eksistere i XAML
            // og er nødvendige for at køre.
            if (YogaCount != null) UpdateCapacity(Yoga, YogaCount);
            if (BoxingCount != null) UpdateCapacity(Boxing, BoxingCount);
            if (SpinningCount != null) UpdateCapacity(Spinning, SpinningCount);
            if (PilatesCount != null) UpdateCapacity(Pilates, PilatesCount);
            if (CrossfitCount != null) UpdateCapacity(Crossfit, CrossfitCount);
        }


        // DELETE BUTTON HANDLER
        private void DeleteMember_Click(object sender, RoutedEventArgs e) //Knap der kalder RemoveMemberFromActivity metoden
        {
            RemoveMemberFromActivity(); 
        }

    
        private void CreateActivity_Click(object sender, RoutedEventArgs e) //Knap der opretter ny aktivitet -- Sidney
        {
            string input = NewActivity.Text.Trim(); //Brugerens input bliver sat i ind string der bruges senere

            if (string.IsNullOrEmpty(input)) //if statement der kører så længe brugeren ikke skriver noget i TextBoxen
            {
                MessageBox.Show("Indtast venligst et navn til aktivitetet");
                return; //Stopper her og går tilbage
            }

            if(newActCount >= 2)
            {
                MessageBox.Show("Der kan ikke oprettes flere aktiviteter ligenu"); //Hvis der er blevet oprettet 2 nye aktiviteter allerede
                return;
            }
            
            if (ActivityGrid != null)
            {
                newActCount++; //Inkerementerer varaiblen hver gang en ny aktivitet laves
                fitness.SaveActivityToFile(input); //Gemmer den nye aktivitet i text filen via en metode i Fitness klassen, giver det nye navn som input
                ShowActivity(); //Reloader vinduet
                MessageBox.Show($"Aktivitet {input} oprettet");
            }
        }

        private void GoToNextWindow_Click(object sender, RoutedEventArgs e) //Knap der vender tilbage til hovedmenuen -- Sidney
        {
            NextWindow next = new NextWindow(member, fitness);
            next.Show();
            this.Close();
        }

    }
}