using Microsoft.AspNetCore.Http;
using StaffManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Core.IRepository
{
    public interface ITestRepository
    {

        public bool Login(StaffLogin loginDetails);
        public bool AddStudentDetail(StudentDetails studentEntry);
        public List<StudentDetails> GetStudentList();
        public StudentDetails EditStudentDetails(int id);
        public bool DeleteStudent(int id);
        public void ImportFileUpload(FileUpload fileupload);
        public List<StudentMarkDetails> GetStudentMarkList(StudentDetails StudentList);
        public bool StudentLogin(StudentDetails loginDetails);
    }
}
