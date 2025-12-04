using System.Windows;

namespace FitnessProgram

    // Philip Kode 
{
    public partial class MainWindow : Window
    {
        private readonly Fitness _fitness; // shared Fitness system

        // Default constructor (for designer)
        public MainWindow()
        {
            InitializeComponent();
            _fitness = new Fitness(); // new system if not passed
        }

        // Constructor that accepts existing Fitness object
        public MainWindow(Fitness fitness)
        {
            InitializeComponent();
            _fitness = fitness;
        }

        // --- LOGIN BUTTON --- Philip
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameInput.Text.Trim();
            string password = PasswordInput.Password.Trim();

            // Prevent empty login fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Udfyld både brugernavn og kodeord.");
                return;
            }

            // Call authentication in Fitness controller
            Member loggedIn = _fitness.Authenticate(username, password);

            // Incorrect login
            if (loggedIn == null)
            {
                MessageBox.Show("Forkert brugernavn eller kodeord.");
                return;
            }

            // LOGIN SUCCESS: Open NextWindow
            NextWindow next = new NextWindow(loggedIn, _fitness);
            next.Show();

            this.Close();
        }

        // --- REGISTER BUTTON --- Philip
        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            // Pass same Fitness system so new members are stored correctly
            RegisterWindow reg = new RegisterWindow(_fitness);
            reg.ShowDialog();
        }
    }
}
