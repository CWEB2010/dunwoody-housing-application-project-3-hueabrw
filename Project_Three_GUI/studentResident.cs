using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Three_GUI
{
    abstract class StudentResident
    {
        public string idNum { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int[] floorRange { get; set; }
        public int roomNumber { get; set; }
        public int floorNumber { get; set; }
        public double rentFee { get; set; }
        public string stundetType { get; set; }

    }

    class StudentWorker : StudentResident
    {
        private double hoursWorked;
        public static double WAGE = 14.00;

        public StudentWorker(string idNum, string firstName, string lastName, int roomNumber, int floorNumber, int hoursWorked)
        {
            this.idNum = idNum;
            this.firstName = firstName;
            this.lastName = lastName;
            this.floorRange = new int[] { 1, 2, 3 };
            this.roomNumber = roomNumber;
            this.floorNumber = floorNumber;
            this.hoursWorked = hoursWorked;
            this.rentFee = calculateRent(hoursWorked);
            this.stundetType = "Student Worker";
        }

        private double calculateRent(int hoursWorked)
        {
            double fee = 1245 - ((hoursWorked * WAGE) / 2);
            return fee;
        }
    }

    class StudentAthlete : StudentResident
    {

        public StudentAthlete(string idNum, string firstName, string lastName, int roomNumber, int floorNumber)
        {
            this.idNum = idNum;
            this.firstName = firstName;
            this.lastName = lastName;
            this.floorRange = new int[] { 4, 5, 6 };
            this.roomNumber = roomNumber;
            this.floorNumber = floorNumber;
            this.rentFee = 1200;
            this.stundetType = "Student Athlete";
        }
        
    }

    class ScholarshipStudent : StudentResident
    {

        public ScholarshipStudent(string idNum, string firstName, string lastName, int roomNumber, int floorNumber)
        {
            this.idNum = idNum;
            this.firstName = firstName;
            this.lastName = lastName;
            this.floorRange = new int[] { 7, 8 };
            this.roomNumber = roomNumber;
            this.floorNumber = floorNumber;
            this.rentFee = 100;
            this.stundetType = "Scholarship Student";
        }

    }
    

}
