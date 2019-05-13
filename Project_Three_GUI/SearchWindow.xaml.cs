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
            QueryData();
        }

        private void QueryData()
        {
            WorkerCountText.Text += students.Where(s => s.stundetType == "Student Worker").Count();
            AthleteCountText.Text += students.Where(s => s.stundetType == "Student Athlete").Count();
            ScholarshipCountText.Text += students.Where(s => s.stundetType == "Scholarship Student").Count();
            FirstFloorText.Text += students.Where(s=>s.floorNumber.ToString() == "1").Count();
            SecondFloorText.Text += students.Where(s => s.floorNumber.ToString() == "2").Count();
            ThirdFloorText.Text += students.Where(s => s.floorNumber.ToString() == "3").Count();
            FouthFloorText.Text += students.Where(s => s.floorNumber.ToString() == "4").Count();
            FifthFlorrText.Text += students.Where(s => s.floorNumber.ToString() == "5").Count();
            SixthFloorText.Text += students.Where(s => s.floorNumber.ToString() == "6").Count();
            SeventhFloorText.Text += students.Where(s => s.floorNumber.ToString() == "7").Count();
            EighthFloorText.Text += students.Where(s => s.floorNumber.ToString() == "8").Count();

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
        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            StudentTable.SelectedValue = null;
        }
        private void StudentTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StudentTable.SelectedItems.Count > 0)
            {
                removeRes.IsEnabled = true;
            }
            else
            {
                removeRes.IsEnabled = false;
            }
        }

        private void StudentTable_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }

        private void RemoveRes_Click(object sender, RoutedEventArgs e)
        {
            students.Remove((StudentResident)StudentTable.SelectedItem);
            SaveData();
        }

        private void SaveData()
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string jsonData = JsonConvert.SerializeObject(this.students, jsonSerializerSettings);

            System.IO.File.WriteAllText(@"../../StudentData.json", jsonData);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<StudentResident> search = new List<StudentResident>();
            if (IDSearch.IsChecked == true)
            {
                search = students.Where(s=>s.idNum.ToLower().Contains(SearchText.Text.ToLower())).Select(s=>s).ToList();
            }
            else if (NameSearch.IsChecked == true)
            {
                search = students.Where(s => s.lastName.ToLower().Contains(SearchText.Text.ToLower()) || s.firstName.ToLower().Contains(SearchText.Text.ToLower())).Select(s => s).ToList();
            }
            else
            {
                StudentTable.ItemsSource = students;
                return;
            }

            StudentTable.ItemsSource = search;
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<StudentResident> search = new List<StudentResident>();
            if(IDSearch == null && NameSearch == null)
            {
                return;
            }
            if (IDSearch.IsChecked == true)
            {
                search = students.Where(s => s.idNum.ToLower().Contains(SearchText.Text.ToLower())).Select(s => s).ToList();
            }
            else if (NameSearch.IsChecked == true)
            {
                search = students.Where(s => s.lastName.ToLower().Contains(SearchText.Text.ToLower()) || s.firstName.ToLower().Contains(SearchText.Text.ToLower())).Select(s => s).ToList();
            }
            else
            {
                StudentTable.ItemsSource = students;
                return;
            }

            StudentTable.ItemsSource = search;
        }

        private void SearchText_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchText.Text = "";
            SearchText.FontStyle = FontStyles.Normal;
            SearchText.Foreground = Brushes.Black;
        }

        private void SearchText_LostFocus(object sender, RoutedEventArgs e)
        {
            if(SearchText.Text == "")
            {
                SearchText.Text = "Search";
                SearchText.FontStyle = FontStyles.Italic;
                SearchText.Foreground = Brushes.LightGray;
            }
        }
    }
}
