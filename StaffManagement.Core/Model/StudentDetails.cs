using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Core.Model
{
    public class StudentDetails
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender  { get; set; }
        public string FatherFirstName { get; set; }
        public string FatherLastName { get; set; }
        public string MotherFirstName { get; set; }
        public string MotherLastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public long StudentPhoneNumber { get; set; }
        public long FatherPhoneNumber { get; set; }
        public string FatherOccupation { get; set; }
        public string RollNumber { get; set; }
        public string Password { get; set; }
            }
}
