using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Core.IServices;
using StaffManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPortal.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentApi : ControllerBase
    {
        #region TestService

        
        private readonly ITestService _testService;

        public StudentApi(ITestService testService)
        {
            _testService = testService;
        }
        #endregion

        #region StaffLogin


        [HttpPost]
        public ActionResult Loginpage(StaffLogin login)
        {

            var logins = _testService.Login(login);
            if (logins == true)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }
        #endregion

        #region CreatingStudentPersonalDetails

       
        [HttpPost]
        public ActionResult AddStudentPersonalDetails(StudentDetails studentEntry)
        {
            var addStudent = _testService.AddStudentDetail(studentEntry);
            if (addStudent == true)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }
        #endregion

        #region GetStudentDetails

        
        [HttpGet]
        public ActionResult GetStudentPersonalDetails()
        {
            var getStudentList = _testService.GetStudentList();
            return Ok(getStudentList);
        }
        #endregion

        #region EditStudentDetails

        
        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            var getdetails = _testService.EditStudentDetails(id);
            
            return Ok(getdetails);
        }
        #endregion

        #region DeleteStudent

        
        [HttpDelete]
        public ActionResult DeleteStudent (int id)

        {
            _testService.DeleteStudent(id);
            return Ok();
        }
        #endregion

        #region FileUpload

        
        [HttpPost]
        public ActionResult FileUpload(FileUpload fileupload)
        {
            byte[] bytefile = fileupload.filebyte;
            var streama = new MemoryStream(bytefile);
            IFormFile file = new FormFile(streama, 0, bytefile.Length, "name", "fileName");
            fileupload.ExcelValues = file;
            _testService.ImportFileUpload(fileupload);
            return Ok();
        }
        #endregion

        #region StudentLogin

        
        [HttpPost]
        public ActionResult StudentLoginpage(StudentDetails loginDetails)
        {
            var logins = _testService.StudentLogin(loginDetails);
            if (logins == true)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
        #endregion

        #region GetMarkDetails

        
        [HttpPost]
       public ActionResult GetStudentMarkDetails(StudentDetails StudentList)
        {
            var getMarkList = _testService.GetStudentMarkList(StudentList);
            return Ok(getMarkList);
        }
        #endregion
    }
}
