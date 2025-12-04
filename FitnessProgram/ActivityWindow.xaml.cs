using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitnessProgram
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        Fitness fitness = new Fitness();
        public ActivityWindow()
        {
            InitializeComponent();
            ShowActivity();
            
        }
        
        public void ShowActivity()
        {
            List<string> localMembers = fitness.MemberFromFile();
            List<string> localActivities = fitness.ActivityFromFile();
            Yoga.Text = localActivities[0].ToUpper() + Environment.NewLine + localMembers[1] + Environment.NewLine + localMembers[3] + Environment.NewLine + localMembers[8] + Environment.NewLine + localMembers[11] + Environment.NewLine + localMembers[13];
            Boxing.Text = localActivities[1].ToUpper() + Environment.NewLine + localMembers[1] + Environment.NewLine + localMembers[4] + Environment.NewLine + localMembers[7];
            Spinning.Text = localActivities[2].ToUpper() + Environment.NewLine + localMembers[0] + Environment.NewLine + localMembers[2] + Environment.NewLine + localMembers[9] + Environment.NewLine + localMembers[10];
            Pilates.Text = localActivities[3].ToUpper();
            //Crossfit.Text
        }

        public void RemoveMember()
        {
            // Aktivitet: 1 = Yoga, 2 = Boxing osv.
            if (!int.TryParse(EnterActivity.Text, out int activityIndex))
            {
                MessageBox.Show("Indtast aktivitet 1-5 (1=Yoga, 2=Boxing, 3=Spinning, 4=Pilates, 5=Crossfit)");
                return;
            }

            // Medlem ID som brugeren ser (1, 2, 3 ... 14 osv.) → vi trækker 1 fra for at få rigtigt index
            if (!int.TryParse(EnterMember.Text, out int memberId) || memberId < 1)
            {
                MessageBox.Show("Indtast medlemets ID (f.eks. 14)");
                return;
            }

            int memberIndex = memberId - 1;  // HER: Brugeren skriver 14 → bliver index 13

            List<string> localMembers = fitness.MemberFromFile();

            // Tjek at medlemmet findes
            if (memberIndex < 0 || memberIndex >= localMembers.Count)
            {
                MessageBox.Show($"Medlem med ID {memberId} findes ikke!");
                return;
            }

            string memberName = localMembers[memberIndex];

            // Vælg TextBlock
            TextBlock target = activityIndex switch
            {
                1 => Yoga,
                2 => Boxing,
                3 => Spinning,
                4 => Pilates,
                5 => Crossfit,
                _ => null
            };

            if (target == null) return;

            var lines = target.Text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            bool removed = false;
            for (int i = 1; i < lines.Count; i++)  // Spring første linje over (aktivitetsnavn)
            {
                if (lines[i] == memberName)
                {
                    lines.RemoveAt(i);
                    removed = true;
                    break;
                }
            }

            if (!removed)
            {
                MessageBox.Show($"Medlem {memberId} ({memberName}) er ikke tilmeldt denne aktivitet!");
                return;
            }

            // Opdater visning
            target.Text = string.Join(Environment.NewLine, lines);

            // Ryd felter
            EnterActivity.Text = "";
            EnterMember.Text = "";

            MessageBox.Show($"Medlem {memberId} ({memberName}) fjernet fra aktivitet {activityIndex}!");
        }

        private void DeleteMemberButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveMember();
        }
    }
}
