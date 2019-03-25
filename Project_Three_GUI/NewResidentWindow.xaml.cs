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
        List<StudentResident> students;
        public NewResidentWindow()
        {
            InitializeComponent();
            students = LoadData();
            List<string> types = new List<string> { "Student Worker", "Student Athlete", "Scholarship Student" };
            this.studentTypeBox.ItemsSource = types;
            this.floorNumebrBox.ItemsSource = new List<int> { 1,2,3};
            this.roomNumberBox.ItemsSource = new List<int> { 1, 2, 3 };
        }

        private List<StudentResident> LoadData()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<StudentResident>>(File.ReadAllText(@"StudentData.json"));
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
            SetButton();
        }

        private void AddResidentButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Made it");
            if (studentTypeBox.SelectedItem.ToString() == "Student Worker")
            {
                this.students.Add(new StudentWorker(students.Count().ToString(), this.firstNameBox.Text, this.lastNameBox.Text, (int)this.roomNumberBox.SelectedItem, (int)this.floorNumebrBox.SelectedItem, Convert.ToInt32(this.MonthlyHoursBox.Text)));
            }
            else if (studentTypeBox.SelectedItem.ToString() == "Student Athlete")
            {
                this.students.Add(new StudentAthlete(students.Count().ToString(), this.firstNameBox.Text, this.lastNameBox.Text, (int)this.roomNumberBox.SelectedItem, (int)this.floorNumebrBox.SelectedItem));
            }
            else if (studentTypeBox.SelectedItem.ToString() == "Scholarship Student")
            {
                this.students.Add(new ScholarshipStudent("0", this.firstNameBox.Text, this.lastNameBox.Text, Convert.ToInt32(this.roomNumberBox.SelectedItem.ToString()), Convert.ToInt32(this.floorNumebrBox.SelectedItem.ToString())));
            }
            SaveData();
        }

        private void SaveData()
        {
            string jsonData = JsonConvert.SerializeObject(students);
            
            System.IO.File.WriteAllText(@"StudentData.json", jsonData);
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
            SetButton();
        }

        private void LastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetButton();
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
