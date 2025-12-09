using System.Windows;

namespace FitnessProgram
{
    public partial class NextWindow : Window
    {
        private readonly Fitness fitness;   
        private readonly Member? member;     

        // Constructor for logged-in member
        public NextWindow(Member member, Fitness fitness)
        {
            InitializeComponent();
            this.fitness = fitness;
            this.member = member;
            WelcomeText.Text = $"Velkommen {member.name}!"; // Viser velkomst beskeden med brugerens navn

            // Viser medlems knappen hvis man er logget ind som admin
            if (member.role.ToLower() == "admin")
            {
                MemberButton.Visibility = Visibility.Visible;
            }
        }

        // Optional constructor når man navigere uden member 
        public NextWindow(Fitness fitness)
        {
            InitializeComponent();
            this.fitness = fitness;
        }

        // --- Button: Open Member Window --- Philip
        private void GoToMembers_Click(object sender, RoutedEventArgs e)
        {
            MemberWindow memberWindow = new MemberWindow(fitness, member);
            memberWindow.Show();
            this.Close();
        }

        // --- Button: Open ActivityWindow --- Philip
        private void GoToActivity_Click(object sender, RoutedEventArgs e)
        {
            ActivityWindow activity = new ActivityWindow(fitness, member);
            activity.Show();
            Application.Current.MainWindow = activity;
            this.Close();
        }

        // --- Button: Go back to Main Menu --- Philip
        private void GoToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(fitness);
            main.Show();
            this.Close();
        }
    }
}