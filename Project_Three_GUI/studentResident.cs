using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Three_GUI
{
    abstract class StudentResident
    {
        protected string idNum;
        protected string firstName;
        protected string lastName;
        protected int roomNumber;
        protected int floorNumber;
        protected double rentFee;
        protected string stundetType;

    }

    class StudentWorker : StudentResident
    {

        public StudentWorker()
        {
            
        }
    }

}
