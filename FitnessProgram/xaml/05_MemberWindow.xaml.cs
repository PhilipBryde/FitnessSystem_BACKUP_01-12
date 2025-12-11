using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

//Sidney Kode 
namespace FitnessProgram
{
    public partial class MemberWindow : Window
    {
        //private readonly Fitness _fitness; // shared Fitness system
        private readonly Fitness fitness; // Shared fitness system
        private readonly Member member;   // Logged in user
        public List<string> _localList; // Lokal liste
        public MemberWindow(Fitness fitness, Member member)
        {
            InitializeComponent();
            //_fitness = fitness;
            this.fitness = fitness;
            this.member = member;
            this._localList = fitness.MemberFromFile().ToList(); //Lokal liste får medlemmer fra Fitness-klassen
            ShowMembers(); // Kalder ShowMembers for at vise medlemmerne
        }

        private void ShowMembers() //Metode der viser alle medlemmerne fra textfilen i en string -- Sidney
        {
   
            StringBuilder allMembers = new StringBuilder(); //Opretter ny StringBuilder

            for (int i = 0; i < _localList.Count; i++) //En forløkke der kører så længe i er mindre end antallet af medlemmer i listen, inkrementerer i hvert loop
            {
                allMembers.AppendLine(_localList[i]);  //Hver medlem/index bliver sat i vores StringBuilder som ny linje
            }
            MemberBlock.Text = allMembers.ToString(); //Sætter dem sammen i en stor string i hukommelsen
        }

        private void RemoveMember() //Metode der fjerner medlem via dens index i listen, Gamle version fjernede via medlemmets ID -- Sidney
        {
            if (int.TryParse(EnterMember.Text, out int memberID)) //Input konverteres til en int
            {
                int memberIndex = memberID - 1; //Minus 1 da ID 1 = 0
                if (memberIndex >= 0 && memberIndex < _localList.Count)
                {
                    _localList.RemoveAt(memberIndex); //Fjerner medlem ved index
                    //File.WriteAllLines(@"MemberList.txt", localList); Kan fjerne medlem permanent fra textfilen
                    ShowMembers(); //Reloader vinduet
                    MessageBox.Show($"{memberID} er blevet slettet!");
                }
                else
                {
                    MessageBox.Show($"{memberID} findes ikke, prøv igen"); //Hvis indexet ikke findes
                }
            }
            else
            {
                MessageBox.Show("Indtast venligst et tal"); //Hvis der ikke er blevet tastet et tal ind
            }
        }

        private void RemoveMember_Click(object sender, RoutedEventArgs e) //Knap der kalder på RemoveMember() metoden -- Sidney
        {
            RemoveMember();
        }

        private void GoToNextWindow_Click(object sender, RoutedEventArgs e) //Knap der vender tilbage til menuen -- Sidney
        {
            NextWindow next = new NextWindow(member, fitness);
            next.Show();
            this.Close();
        }
    }
}
