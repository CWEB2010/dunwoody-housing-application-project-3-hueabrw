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
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Collections.ObjectModel;

namespace Project_Three_GUI
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        ObservableCollection<StudentResident> students;
        public SearchWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var jsonSerializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                string jsonData = File.ReadAllText(@"../../StudentData.json");

                students = JsonConvert.DeserializeObject<ObservableCollection<StudentResident>>(jsonData, jsonSerializerSettings);
                
                PopulateTable();
                
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            

        }

        private void PopulateTable()
        {
            StudentTable.ItemsSource = students;
        }

        private void NewResidentButton_Click(object sender, RoutedEventArgs e)
        {
            NewResidentWindow newResidentWindow = new NewResidentWindow();
            newResidentWindow.Show();
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            return;
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
