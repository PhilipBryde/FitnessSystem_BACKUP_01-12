using FitnessProgram;
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
    //Philip Kode 
    public partial class RegisterWindow : Window
    {
        private Fitness _fitness;

        public RegisterWindow(Fitness fitness)
        {
            InitializeComponent();
            _fitness = fitness;
        }

        // Håndterer klik på knappen 'Opret Bruger'.
        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            // Henter input fra tekstfelterne.
            string name = NameInput.Text;
            // Tager det første bogstav i Køn, ellers standard 'M'.
            char gender = GenderInput.Text.Length > 0 ? GenderInput.Text[0] : 'M';

            // Forsøger at konvertere Alder til et tal.
            if (!int.TryParse(AgeInput.Text, out int age))
            {
                // Viser fejlbesked, hvis Alder ikke er et tal.
                MessageBox.Show("Alder skal være et tal.");
                return;
            }
            // Opretter et nyt medlem via Fitness-systemet.
            Member newMember = _fitness.Register(name, gender, age);

            // Kalder metode for at gemme det nye medlem i filen.
            SaveMemberToFile(newMember);

            // Viser bekræftelse af oprettelse og login-information.
            MessageBox.Show($"Bruger oprettet! \nDit Brugernavn er {newMember.name} \nDit Adgangskode er {newMember.id}");

            this.Close();
        }

        private void SaveMemberToFile(Member member) //Metode der gemmer ny medlem i text filen, tager den nye medlem som input -- Sidney
        {
            string filePath = @"MemberList.txt"; //Gemmer stien til textfilen
            string m = $"ID: {member.id}, Navn: {member.name}, Køn: {member.gender}"; //Opretter ny string med medlemmets infomation
            File.AppendAllText(filePath, Environment.NewLine + m + Environment.NewLine); //Bliver gemt i filen

        }
    }
}