using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Core.IServices;
using StaffManagement.Core.Model;
using StaffManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace StaffManagement.Controllers
{
    public class StudentContoller : Controller
    {

        
        #region StaffLogin
        public ActionResult MainPage()
        {
            return View();
        }
        public IActionResult StaffLogin()
        {

            return View();
        }

        [HttpPost]
        public IActionResult StaffLogin(StaffLogin loginDetails)
        {

            if (loginDetails.StaffName != null && loginDetails.Password != null)
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/Loginpage");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Json = new StringContent(JsonSerializer.Serialize(loginDetails), System.Text.Encoding.UTF8, "application/json");
                    var tmp = client.PostAsync(client.BaseAddress, Json).Result;
                    var result = tmp.Content.ReadAsAsync<bool>().Result;
                    if (result == true)
                    {
                        HttpContext.Session.SetString("token", loginDetails.StaffName);
                        TempData["login"] = "Login SucessFully";
                        return RedirectToAction("DisplayStudentDetails");
                    }
                    else
                    {
                        ViewBag.Message = "Please enter the correct username and password";

                        return View("StaffLogin");

                    }

                }
               

            }
            else
            {
                return RedirectToAction("StaffLogin");
            }
            
        }
        #endregion

        #region StudentLogin

      
        public IActionResult StudentLogin()
        {

            return View();
        }

        [HttpPost]
        public IActionResult StudentLogin(StudentDetails loginDetails)
        {

            if (loginDetails.RollNumber != null && loginDetails.Password != null)
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/StudentLoginpage");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Json = new StringContent(JsonSerializer.Serialize(loginDetails), System.Text.Encoding.UTF8, "application/json");
                    var tmp = client.PostAsync(client.BaseAddress, Json).Result;
                    var result = tmp.Content.ReadAsAsync<bool>().Result;
                    if (result == true)
                    {
                        HttpContext.Session.SetString("studenttoken", loginDetails.RollNumber);
                        TempData["login"] = "Login SuccessFully";
                        return RedirectToAction("StudentMarkView",loginDetails);
                        
                    }
                    else
                    {
                        ViewBag.Message = "Please enter the correct username and password";

                        return View("StudentLogin");

                    }

                }

            }
            return View();
        }
        #endregion

        #region StudentMarkView

        

        public ActionResult StudentMarkView(StudentDetails StudentList)
        {
            if (HttpContext.Session.GetString("studenttoken") != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/GetStudentMarkDetails");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var ItemJson = new StringContent(JsonSerializer.Serialize(StudentList), System.Text.Encoding.UTF8, "application/json");
                    var tmp = client.PostAsync(client.BaseAddress, ItemJson).Result;
                    var result = tmp.Content.ReadAsAsync<List<StudentMarkDetails>>().Result;


                    return View(result);

                }
            }
            else
            {
                return RedirectToAction("StudentLogin");
            }
          
        }
        #endregion

        #region CreatingStudentDetail
        public IActionResult AddStudentDetails()
        {
            StudentDetails studentEntry = new StudentDetails();
            return View(studentEntry);
        }
        [HttpPost]
        public IActionResult AddStudentsDetails(StudentDetails studentEntry)
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                using (HttpClient client = new HttpClient())
                {



                    client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/AddStudentPersonalDetails");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var ItemJson = new StringContent(JsonSerializer.Serialize(studentEntry), System.Text.Encoding.UTF8, "application/json");
                    var tmp = client.PostAsync(client.BaseAddress, ItemJson).Result;
                    var result = tmp.Content.ReadAsAsync<bool>().Result;



                    if (result == true)
                    {

                        TempData["Message"] = "Student Details Added Sucessfully";
                    }
                    else
                    {
                        TempData["Message"] = "Student Details Edited Sucessfully";
                    }
                    return RedirectToAction("DisplayStudentDetails");

                }
            }
            else
            {
                return RedirectToAction("StaffLogin");
            }
            //_testService.AddBook(bookDetails);

        }

        #endregion

        #region GetStudentDetails
        public ActionResult DisplayStudentDetails()
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/GetStudentPersonalDetails");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var tmp = client.GetAsync(client.BaseAddress).Result;
                    var result = tmp.Content.ReadAsAsync<List<StudentDetails>>().Result;


                    return View(result);

                }
            }
            else
            {
                return RedirectToAction("StaffLogin");

            }

        }
        #endregion

        #region EditStudentDetails

        
        public IActionResult EditStudentDetails(int id)
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var tmp = client.GetAsync("EditStudent?id=" + id).Result;
                    var result = tmp.Content.ReadAsAsync<StudentDetails>().Result;

                    return View("AddStudentDetails", result);
                }
            }
            else
            {
                return RedirectToAction("StaffLogin");
            }

        }

        #endregion

        #region StudentDeleteDetails
        public IActionResult StudentDeleteDetails(int id)
        {
            if (HttpContext.Session.GetString("token") != null)
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/DeleteStudent");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var tmp = client.DeleteAsync("?id=" + id).Result;
                }
                //_testService.DeleteBook(id);

                return RedirectToAction("DisplayStudentDetails");
            }
            else
            {
                return RedirectToAction("StaffLogin");
            }
        }
        #endregion

        #region ExcelFileUpload
       
        [HttpPost]
        public ActionResult Upload(IFormFile FormFile)
        {
            if (FormFile != null)
            {
                FileUpload fileupload = new FileUpload();

                string filename = Path.GetFileNameWithoutExtension(FormFile.FileName);
                string extension = Path.GetExtension(FormFile.FileName);
                fileupload.Filename = filename + extension;
                using (var stream = new MemoryStream())
                {
                    FormFile.CopyToAsync(stream);
                    fileupload.filebyte = stream.ToArray();
                }
                fileupload.contenttype = FormFile.ContentType;

                if (fileupload.Filename.EndsWith(".xls") || fileupload.Filename.EndsWith(".xlsx"))
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/FileUpload");
                        var Posttask = client.PostAsJsonAsync(client.BaseAddress, fileupload);
                        Posttask.Wait();
                        var checkresult = Posttask.Result;
                        
                        if (checkresult.IsSuccessStatusCode)
                        {
                            return RedirectToAction("AllStudentMark");
                        }
                        else if (checkresult.ReasonPhrase.Equals("Expectation Failed"))
                        {
                            TempData["ExcelNotify"] = "Some of the student Is not in student detail please update student detail first...";
                            return RedirectToAction("DisplayStudentDetails");
                        }

                        else if (checkresult.ReasonPhrase.Equals("Conflict"))
                        {
                            TempData["ExcelNotify"] = "Please check Excel file..Column should be not null...";
                            return RedirectToAction("DisplayStudentDetails");
                        }
                        else if (checkresult.ReasonPhrase.Equals("Not Found"))
                        {
                            TempData["ExcelNotify"] = "connection To API failed...";
                            return RedirectToAction("DisplayStudentDetails");
                        }
                    }

                }
                TempData["ExcelNotify"] = "select Excel File only";
                return RedirectToAction("DisplayStudentDetails");
            }
            TempData["ExcelNotify"] = "Please select file to upload";
            return RedirectToAction("DisplayStudentDetails");
            //if (FormFile != null)
            //{

            ////    using (HttpClient client = new HttpClient())
            //    {
            //        FileUpload fileupload = new FileUpload();

            //        string filename = Path.GetFileNameWithoutExtension(FormFile.FileName);
            //        string extension = Path.GetExtension(FormFile.FileName);
            //        fileupload.Filename = filename + extension;
            //        using (var stream = new MemoryStream())
            //        {
            //            FormFile.CopyToAsync(stream);
            //            fileupload.filebyte = stream.ToArray();
            //        }
            //        fileupload.contenttype = FormFile.ContentType;


            //        if (fileupload.Filename.EndsWith(".xls") || fileupload.Filename.EndsWith(".xlsx"))
            //        {
            //            client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/FileUpload");
            //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //            var ItemJson = new StringContent(JsonSerializer.Serialize(fileupload), System.Text.Encoding.UTF8, "application/json");
            //            var tmp = client.PostAsync(client.BaseAddress, ItemJson).Result;
            //            var result = tmp.Content.ReadAsAsync<bool>().Result;

            //            if (result == true)
            //            {

            //                TempData["Message"] = "Book Details Added Sucessfully";
            //            }
            //            else
            //            {
            //                TempData["Message"] = "Book Details Edited Sucessfully";
            //            }
            //            return RedirectToAction("DisplayStudentDetails");

            //        }
            //    }
            //}
            ////_testService.AddBook(
            ////_testService.ImportExcelFile(FormFile);
            //return View();
           
        }
        #endregion

        #region StudentMarkforstaffView

        
        public ActionResult AllStudentMark()
        {
            if (HttpContext.Session.GetString("studenttoken") != null)
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/GetAllStudentMarkDetails");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var tmp = client.GetAsync(client.BaseAddress).Result;
                    var result = tmp.Content.ReadAsAsync<List<StudentMarkDetails>>().Result;


                    return View(result);
                }
            }
            else
            {
                return RedirectToAction("StudentLogin");
            }
        }
        #endregion

        #region DeleteStudentMark

        
        public ActionResult DeleteStudentMark(int id)

        {

            if (HttpContext.Session.GetString("token") != null)
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:20481/api/StudentApi/DeleteStudentMark");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var tmp = client.DeleteAsync("?id=" + id).Result;
                }
                //_testService.DeleteBook(id);
            
            return RedirectToAction("AllStudentMark");
            }
            else
            {
                return RedirectToAction("StaffLogin");
            }

        }
        #endregion

        #region Logout

        
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Mainpage");
        }
        #endregion
    }

}

