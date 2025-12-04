using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FitnessProgram
{
    public partial class MemberWindow : Window
    {
        private readonly Fitness _fitness; // shared Fitness system

        public MemberWindow(Fitness fitness)
        {
            InitializeComponent();
            _fitness = fitness;

            ShowMembers(); // Kalder ShowMembers for at vise medlemmerne
        }

        // --- Show all members in TextBlock ---
        public void ShowMembers()
        {
            /*List<Member> localList = _fitness.GetAllMembers();
            StringBuilder allMembers = new StringBuilder();

            for (int i = 0; i < localList.Count; i++)
            {
                Member member = localList[i];
                allMembers.AppendLine($"ID: {member.id} Navn: {member.name} Køn: {member.gender}");
            }

            MemberBlock.Text = allMembers.ToString();*/
            List<string> localList = _fitness.MemberFromFile();
            MemberBlock.Text = string.Join(Environment.NewLine, localList);
        }

        // --- Remove member by ID ---
        public void RemoveMember()
        {
            if (int.TryParse(EnterMember.Text, out int memberID))
            {
                Member member = _fitness.GetAllMembers().FirstOrDefault(m => m.id == memberID);

                if (member != null)
                {
                    _fitness.GetAllMembers().Remove(member);
                    ShowMembers(); // Refresh list
                    MessageBox.Show($"{memberID} slettet!");
                }
                else
                {
                    MessageBox.Show($"{memberID} ikke fundet, prøv igen");
                }
            }
            else
            {
                MessageBox.Show("Indtast venligst et tal");
            }
        }

        // --- Back button ---
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(_fitness);
            main.Show();
            this.Close();
        }

        // --- Delete member button ---
        private void DeleteMemberButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveMember();
        }
    }
}
