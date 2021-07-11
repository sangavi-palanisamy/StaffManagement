using Microsoft.AspNetCore.Http;
using StaffManagement.Core.IRepository;
using StaffManagement.Core.IServices;
using StaffManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Service.Service
{
    public class TestService : ITestService
    {
        #region TestRepositoryInjection

        
        ITestRepository _testRepository;
        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        #endregion

        #region CreatingStudentDetail

        public bool AddStudentDetail(StudentDetails studentEntry)
        {
           return _testRepository.AddStudentDetail(studentEntry);
        }
        #endregion

        #region GetStudentList

        

        public List<StudentDetails> GetStudentList()
        {
            return _testRepository.GetStudentList();
        }
        #endregion

        #region StaffLogin

       
        public bool Login(StaffLogin loginDetails)
        {
            return _testRepository.Login(loginDetails);
        }
        #endregion

        #region EditStudent

        
        public StudentDetails EditStudentDetails(int id)
        {
            return _testRepository.EditStudentDetails(id);
        }
        #endregion

        #region DeletStudent

        
        public bool DeleteStudent(int id)
        {
            return _testRepository.DeleteStudent(id);
        }
        #endregion

        #region FileUpload

        
        public void ImportFileUpload(FileUpload fileupload)
        {
              _testRepository.ImportFileUpload(fileupload);
        }
        #endregion

        #region StudentLogin

        
        public bool StudentLogin(StudentDetails loginDetails)
        {
            return _testRepository.StudentLogin(loginDetails);
        }
        #endregion

        #region GetSingleStudentMarkList

        
        public List<StudentMarkDetails> GetStudentMarkList(StudentDetails StudentList)
        {
            return _testRepository.GetStudentMarkList(StudentList);
        }
        #endregion
    }
}
