using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace Project_Three_GUI
{
    /// <summary>
    /// Interaction logic for NewResidentWindow.xaml
    /// </summary>
    public partial class NewResidentWindow : Window
    {
        public NewResidentWindow()
        {
            InitializeComponent();
            List<ScholarshipStudent> students = LoadData();
            List<string> types = new List<string> { "Student Worker", "Student Athlete", "Scholarship Student" };
            this.studentTypeBox.ItemsSource = types;
        }

        private List<ScholarshipStudent> LoadData()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<ScholarshipStudent>>(File.ReadAllText(@"StudentData.json"));
            }
            catch(Exception e)
            {
                return null;
            }
        }

        private void NewResidentButton_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void StudentTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
