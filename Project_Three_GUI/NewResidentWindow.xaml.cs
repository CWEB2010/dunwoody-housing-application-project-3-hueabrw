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
using System.Collections.ObjectModel;

namespace Project_Three_GUI
{
    /// <summary>
    /// Interaction logic for NewResidentWindow.xaml
    /// </summary>
    public partial class NewResidentWindow : Window
    {
        ObservableCollection<StudentResident> students;
        List<int[]> floorNumberList;
        List<int[]> roomNumberList;
        public NewResidentWindow()
        {
            InitializeComponent();
            students = LoadData();
            List<string> types = new List<string> { "Student Worker", "Student Athlete", "Scholarship Student" };
            this.studentTypeBox.ItemsSource = types;
            floorNumberList =  new List<int[]> { new int[] {1,2,3}
                                                ,new int[] {4,5,6}
                                                ,new int[] {7,8} };
            roomNumberList = new List<int[]> { new int[] {1,2,3,4,5,6,7,8,9,10}
                                                ,new int[] {1,2,3,4,5,6,7,8,9,10}
                                                ,new int[] {1,2,3,4,5,6,7,8,9,10}};
        }

        private ObservableCollection<StudentResident> LoadData()
        {
            try
            {
                var jsonSerializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                string jsonData = File.ReadAllText(@"../../StudentData.json");
                return JsonConvert.DeserializeObject<ObservableCollection<StudentResident>>(jsonData,jsonSerializerSettings);
            }
            catch(Exception e)
            {
                MessageBox.Show("Creating new list of students");
                return new ObservableCollection<StudentResident>();
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
            if(this.studentTypeBox.SelectedItem.ToString() == "Student Worker")
            {
                this.MonthlyHoursBox.IsEnabled = true;
                this.MonthlyHoursText.Foreground = Brushes.Black;
            }
            else
            {
                this.MonthlyHoursBox.IsEnabled = false;
                this.MonthlyHoursText.Foreground = Brushes.Gray;
            }

            floorNumebrBox.ItemsSource = floorNumberList[studentTypeBox.SelectedIndex];
            roomNumberBox.ItemsSource = roomNumberList[studentTypeBox.SelectedIndex];

            floorNumebrBox.IsEnabled = true;
            roomNumberBox.IsEnabled = true;

            SetButton();
        }

        private void AddResidentButton_Click(object sender, RoutedEventArgs e)
        {
            int currentID;
            try
            {
                currentID = students.Count() + 1;
            }
            catch
            {
                currentID = 1;
            }

            if (studentTypeBox.SelectedItem.ToString() == "Student Worker")
            {
                this.students.Add(new StudentWorker(currentID.ToString("D4"), this.firstNameBox.Text, this.lastNameBox.Text, Convert.ToInt32(this.roomNumberBox.SelectedItem.ToString()), Convert.ToInt32(this.floorNumebrBox.SelectedItem.ToString()), Convert.ToInt32(this.MonthlyHoursBox.Text)));
            }
            else if (studentTypeBox.SelectedItem.ToString() == "Student Athlete")
            {
                this.students.Add(new StudentAthlete(currentID.ToString("D4"), this.firstNameBox.Text, this.lastNameBox.Text, Convert.ToInt32(this.roomNumberBox.SelectedItem.ToString()), Convert.ToInt32(this.floorNumebrBox.SelectedItem.ToString())));
            }
            else if (studentTypeBox.SelectedItem.ToString() == "Scholarship Student")
            {
                this.students.Add(new ScholarshipStudent(currentID.ToString("D4"), this.firstNameBox.Text, this.lastNameBox.Text, Convert.ToInt32(this.roomNumberBox.SelectedItem.ToString()), Convert.ToInt32(this.floorNumebrBox.SelectedItem.ToString())));
            }
            SaveData();
        }

        private void SaveData()
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string jsonData = JsonConvert.SerializeObject(this.students,jsonSerializerSettings);

            System.IO.File.WriteAllText(@"../../StudentData.json", jsonData);

            SelectionWindow selectionWindow = new SelectionWindow();
            selectionWindow.Show();
            this.Close();
        }

        private void SetButton()
        {
            if(studentTypeBox.SelectedItem.ToString() == "Student Worker")
            {
                AddResidentButton.IsEnabled = (studentTypeBox.SelectedItem != null && firstNameBox.Text != "" && lastNameBox.Text != "" && floorNumebrBox.SelectedItem != null && roomNumberBox.SelectedItem != null && MonthlyHoursBox.Text != "");
            }
            else
            {
                AddResidentButton.IsEnabled = (studentTypeBox.SelectedItem != null && firstNameBox.Text != "" && lastNameBox.Text != "" && floorNumebrBox.SelectedItem != null && roomNumberBox.SelectedItem != null);
            }
        }

        private void FirstNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SetButton();
            }
            catch
            {
                return;
            }
        }

        private void LastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SetButton();
            }
            catch
            {
                return;
            }
        }

        private void FloorNumebrBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetButton();
        }

        private void RoomNumberBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetButton();
        }

        private void MonthlyHoursBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetButton();
        }
    }
}
