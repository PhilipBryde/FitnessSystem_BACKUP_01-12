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
            /*string filePath = @"ActivityList.txt";
            //string fileText = File.ReadAllText(filePath);
            if (File.Exists(filePath))
            {
                string[] activities = File.ReadAllLines(filePath);
                if (activities.Length > 0) Yoga.Text = activities[0];
                if (activities.Length > 1) Boxing.Text = activities[1];
                if (activities.Length > 2) Spinning.Text = activities[2];
                if (activities.Length > 3) Pilates.Text = activities[3];
                if (activities.Length > 4) Crossfit.Text = activities[4];
            }*/
            
        }


        private void DeleteActivityButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
