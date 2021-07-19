using Microsoft.AspNetCore.Http;
using StaffManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Core.IServices
{
   public interface IStudentService
    {
        public bool Login(StaffLogin loginDetails);
        public bool AddStudentDetail(StudentDetails studentEntry);
        public List<StudentDetails> GetStudentList();
        public StudentDetails EditStudentDetails(int id);
        public bool DeleteStudent(int id);
        public int ImportFileUpload(FileUpload fileupload);
        public bool StudentLogin(StudentDetails loginDetails);
        public List<StudentMarkDetails> GetStudentMarkList(StudentDetails StudentList);
        public List<StudentMarkDetails> AllStudentMarkList();
        public bool DeleteMark(int id);
        public bool SendEmail(StudentDetails studentInfo);
        public int Validate(List<ErorrValidation> list);
    }
}
