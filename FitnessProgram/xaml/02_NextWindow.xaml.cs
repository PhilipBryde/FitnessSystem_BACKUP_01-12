using System.Windows;

namespace FitnessProgram
{
    public partial class NextWindow : Window
    {
        private readonly Fitness _fitness;    // Reference to shared Fitness system
        private readonly Member? _member;     // Current logged-in member (nullable if needed)

        // Constructor for logged-in member
        public NextWindow(Member member, Fitness fitness)
        {
            InitializeComponent();
            _fitness = fitness;
            _member = member;

            WelcomeText.Text = $"Velkommen {member.name}!"; // Show welcome message
                                                            // SHOW ADMIN BUTTON ONLY IF ROLE IS ADMIN
            if (member.role.ToLower() == "admin")
            {
                MemberButton.Visibility = Visibility.Visible;
            }
        }

        // Optional constructor when navigating without a member
        public NextWindow(Fitness fitness)
        {
            InitializeComponent();
            _fitness = fitness;
        }

        // --- Button: Open Member Window --- Philip
        private void GoToMembers_Click(object sender, RoutedEventArgs e)
        {
            MemberWindow memberWindow = new MemberWindow(_fitness);
            memberWindow.Show();
            this.Close();
        }

        // --- Button: Open ActivityWindow --- Philip
        private void GoToActivity_Click(object sender, RoutedEventArgs e)
        {
            // Example: pass fitness and an example activity name
            ActivityWindow activity = new ActivityWindow();
            activity.Show();
            this.Close();
        }

        // --- Button: Go back to Main Menu --- Philip
        private void GoToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(_fitness);
            main.Show();
            this.Close();
        }
    }
}
