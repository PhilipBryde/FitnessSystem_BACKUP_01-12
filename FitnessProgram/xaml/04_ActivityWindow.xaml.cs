using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FitnessProgram
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private readonly Fitness fitness; // Shared fitness system
        private readonly Member member;   // Logged in user

        // --- Constructor: MUST match Option 1 ---
        public ActivityWindow(Fitness fitness, Member member)
        {
            InitializeComponent();
            this.fitness = fitness;
            this.member = member;

            ShowActivity();
            ApplyRoleRestrictions(); // Hide admin controls if not admin
        }

        // Hide admin-only controls
        private void ApplyRoleRestrictions()
        {
            if (member.role.ToLower() != "admin")
            {
                DeleteMemberButton.Visibility = Visibility.Collapsed;
                AddMemberButton.Visibility = Visibility.Collapsed;
                CreateActivity.Visibility = Visibility.Collapsed;
                EnterActivity.Visibility = Visibility.Collapsed;
                EnterMember.Visibility = Visibility.Collapsed;
            }
        }

        // Show all activities and members
        public void ShowActivity()
        {
            List<string> localMembers = fitness.MemberFromFile();
            List<string> localActivities = fitness.ActivityFromFile();

            Yoga.Text = localActivities[0].ToUpper() + Environment.NewLine +
                        localMembers[1] + Environment.NewLine +
                        localMembers[3] + Environment.NewLine +
                        localMembers[8] + Environment.NewLine +
                        localMembers[11] + Environment.NewLine +
                        localMembers[13];

            Boxing.Text = localActivities[1].ToUpper() + Environment.NewLine +
                          localMembers[1] + Environment.NewLine +
                          localMembers[4] + Environment.NewLine +
                          localMembers[7];

            Spinning.Text = localActivities[2].ToUpper() + Environment.NewLine +
                            localMembers[0] + Environment.NewLine +
                            localMembers[2] + Environment.NewLine +
                            localMembers[9] + Environment.NewLine +
                            localMembers[10];

            Pilates.Text = localActivities[3].ToUpper();
        }

        // Remove a member from an activity
        private void RemoveMemberFromActivity()
        {
            if (!int.TryParse(EnterActivity.Text, out int activityIndex))
            {
                MessageBox.Show("Indtast aktivitet 1-5");
                return;
            }

            if (!int.TryParse(EnterMember.Text, out int memberId))
            {
                MessageBox.Show("Indtast gyldigt medlem ID");
                return;
            }

            int memberIndex = memberId - 1;

            List<string> localMembers = fitness.MemberFromFile();

            if (memberIndex < 0 || memberIndex >= localMembers.Count)
            {
                MessageBox.Show("Medlem findes ikke!");
                return;
            }

            string memberName = localMembers[memberIndex];

            TextBlock? target = activityIndex switch
            {
                1 => Yoga,
                2 => Boxing,
                3 => Spinning,
                4 => Pilates,
                5 => Crossfit,
                _ => null
            };

            if (target == null) return;

            List<string> lines = target.Text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            bool removed = false;

            for (int i = 1; i < lines.Count; i++)
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
                MessageBox.Show("Medlem er ikke i denne aktivitet.");
                return;
            }

            target.Text = string.Join(Environment.NewLine, lines);
            MessageBox.Show($"Fjernede {memberName} fra aktiviteten.");
        }

        // DELETE BUTTON HANDLER
        private void DeleteActivityButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveMemberFromActivity();
        }

        // BACK BUTTON HANDLER
        private void GoToNextWindow_Click(object sender, RoutedEventArgs e)
        {
            NextWindow next = new NextWindow(member, fitness);
            next.Show();
            this.Close();
        }
    }
}
