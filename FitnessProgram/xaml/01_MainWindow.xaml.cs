using System.Windows;

namespace FitnessProgram

    // Philip Kode 
{
    public partial class MainWindow : Window
    {
        private readonly Fitness _fitness; // Det delte Fitness system

        public MainWindow()
        {
            InitializeComponent();
            _fitness = new Fitness(); // new system if not passed
        }

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

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) // stopper tomme login forsøg
            {
                MessageBox.Show("Udfyld både brugernavn og kodeord.");
                return;
            }

            Member loggedIn = _fitness.Authenticate(username, password);

            // Forkert login besked
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
            RegisterWindow reg = new RegisterWindow(_fitness);
            reg.ShowDialog();
        }
    }
}
