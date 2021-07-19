
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using StaffManagement.Core.IRepository;
using StaffManagement.Core.IServices;
using StaffManagement.Core.Model;
using StaffManagement.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Resources.Repository
{
    public class StudentRepository : IStudentRepository
    {
        #region CreatingStudentDetail


        public bool AddStudentDetail(StudentDetails studentEntry)
        {
            if (studentEntry != null)
            {
                using (SampletestContext studentEntity = new SampletestContext())
                {
                    Student_Personal_Details studentPersonalEntry = new Student_Personal_Details();
                    if (studentEntry.StudentId == 0)
                    {
                        studentPersonalEntry.First_Name = studentEntry.FirstName;
                        studentPersonalEntry.Last_Name = studentEntry.LastName;
                        studentPersonalEntry.Gender = studentEntry.Gender;
                        studentPersonalEntry.Father_First_Name = studentEntry.FatherFirstName;
                        studentPersonalEntry.Father_Last_Name = studentEntry.FatherLastName;
                        studentPersonalEntry.Mother_First_Name = studentEntry.MotherFirstName;
                        studentPersonalEntry.Mother_Last_Name = studentEntry.MotherLastName;
                        studentPersonalEntry.Date_Of_Birth = studentEntry.DateOfBirth;
                        studentPersonalEntry.Email_Id = studentEntry.Email;
                        studentPersonalEntry.Student_Phone_No = studentEntry.StudentPhoneNumber;
                        studentPersonalEntry.Father_Phone_No = studentEntry.FatherPhoneNumber;
                        studentPersonalEntry.Father_Occupation = studentEntry.FatherOccupation;
                        studentPersonalEntry.Roll_No = studentEntry.RollNumber;
                        studentPersonalEntry.Password = studentEntry.Password;
                        studentEntity.Add(studentPersonalEntry);
                        studentEntity.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var studentValue = studentEntity.Student_Personal_Details.Where(x => x.Student_Id == studentEntry.StudentId && x.Is_Deleted == false).SingleOrDefault();
                        studentValue.First_Name = studentEntry.FirstName;
                        studentValue.Last_Name = studentEntry.LastName;
                        studentValue.Mother_First_Name = studentEntry.MotherFirstName;
                        studentValue.Mother_Last_Name = studentEntry.MotherLastName;
                        studentValue.Father_First_Name = studentEntry.FatherFirstName;
                        studentValue.Father_Last_Name = studentEntry.FatherLastName;
                        studentValue.Date_Of_Birth = studentEntry.DateOfBirth;
                        studentValue.Email_Id = studentEntry.Email;
                        studentValue.Student_Phone_No = studentEntry.StudentPhoneNumber;
                        studentValue.Father_Phone_No = studentEntry.FatherPhoneNumber;
                        studentValue.Gender = studentEntry.Gender;
                        studentValue.Father_Occupation = studentEntry.FatherOccupation;



                        studentValue.Updated_time_stamp = DateTime.Now;

                        studentEntity.SaveChanges();
                    }

                }
            }
            return false;
        }
        #endregion

        #region DeleteStudent


        public bool DeleteStudent(int id)
        {
            using (SampletestContext DeleteEntities = new SampletestContext())
            {
                var deleteValue = DeleteEntities.Student_Personal_Details.Where(x => x.Student_Id == id && x.Is_Deleted == false).SingleOrDefault();
                if (deleteValue != null)
                {
                    deleteValue.Is_Deleted = true;
                    deleteValue.Updated_time_stamp = DateTime.Now;
                    DeleteEntities.SaveChanges();
                    var Value = DeleteEntities.Student_Mark_Information.Where(x => x.Roll_No == deleteValue.Roll_No && x.Is_Deleted == false).SingleOrDefault();
                    Value.Is_Deleted = true;
                    Value.Updated_time_stamp = DateTime.Now;
                    DeleteEntities.SaveChanges();
                    return true;
                }

            }
            return false;
        }
        #endregion

        #region EditStudentDetails
        public StudentDetails EditStudentDetails(int id)
        {
            using (SampletestContext editEntity = new SampletestContext())
            {
                StudentDetails studentEditDetails = new StudentDetails();

                var editValues = editEntity.Student_Personal_Details.Where(x => x.Student_Id == id && x.Is_Deleted == false).SingleOrDefault();
                if (editValues != null)
                {
                    studentEditDetails.StudentId = editValues.Student_Id;
                    studentEditDetails.FirstName = editValues.First_Name;
                    studentEditDetails.LastName = editValues.Last_Name;
                    studentEditDetails.Gender = editValues.Gender;
                    studentEditDetails.FatherFirstName = editValues.Father_First_Name;
                    studentEditDetails.FatherLastName = editValues.Father_Last_Name;
                    studentEditDetails.MotherFirstName = editValues.Mother_First_Name;
                    studentEditDetails.MotherLastName = editValues.Mother_Last_Name;
                    studentEditDetails.DateOfBirth = editValues.Date_Of_Birth;
                    studentEditDetails.Email = editValues.Email_Id;
                    studentEditDetails.FatherOccupation = editValues.Father_Occupation;
                    studentEditDetails.FatherPhoneNumber = editValues.Father_Phone_No;
                    studentEditDetails.StudentPhoneNumber = editValues.Student_Phone_No;
                    studentEditDetails.RollNumber = editValues.Roll_No;
                    studentEditDetails.Password = editValues.Password;

                }
                return studentEditDetails;
            }
        }

        #endregion

        #region GetStudentPersonalDetailList


        public List<StudentDetails> GetStudentList()
        {
            List<StudentDetails> studentList = new List<StudentDetails>();
            using (SampletestContext studentDisplayEntity = new SampletestContext())
            {
                var displayValue = studentDisplayEntity.Student_Personal_Details.Where(x => x.Is_Deleted == false).ToList();
                if (displayValue != null)
                {
                    foreach (var studentValues in displayValue)
                    {
                        StudentDetails getStudentDetails = new StudentDetails();
                        getStudentDetails.StudentId = studentValues.Student_Id;
                        getStudentDetails.FirstName = studentValues.First_Name;
                        getStudentDetails.LastName = studentValues.Last_Name;
                        getStudentDetails.Gender = studentValues.Gender;
                        getStudentDetails.FatherFirstName = studentValues.Father_Last_Name;
                        getStudentDetails.FatherLastName = studentValues.Father_Last_Name;
                        getStudentDetails.MotherFirstName = studentValues.Mother_First_Name;
                        getStudentDetails.MotherLastName = studentValues.Mother_Last_Name;
                        getStudentDetails.DateOfBirth = studentValues.Date_Of_Birth;
                        getStudentDetails.Email = studentValues.Email_Id;
                        getStudentDetails.StudentPhoneNumber = studentValues.Student_Phone_No;
                        getStudentDetails.FatherPhoneNumber = studentValues.Father_Phone_No;
                        getStudentDetails.FatherOccupation = studentValues.Father_Occupation;
                        getStudentDetails.RollNumber = studentValues.Roll_No;
                        getStudentDetails.Password = studentValues.Password;

                        studentList.Add(getStudentDetails);
                    }
                }
            }
            return studentList;
        }


        #endregion

        #region StaffLogin


        public bool Login(StaffLogin loginDetails)
        {
            if (loginDetails != null)
            {
                using (SampletestContext loginEntity = new SampletestContext())
                {
                    var staffLoginValues = loginEntity.Staff_Login.Where(x => x.UserName == loginDetails.StaffName && x.Password == loginDetails.Password && x.Is_Deleted == false).SingleOrDefault();
                    if (staffLoginValues != null)
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        #endregion

        #region FileUpload

        public void ImportFileUpload(List<ErorrValidation> list)
        {
            SampletestContext newsave = new SampletestContext();

            foreach (var a in list)
            {
                Student_Mark_Information newdata = new Student_Mark_Information();
                var checkforexist = newsave.Student_Mark_Information.Where(m => m.Roll_No == a.Roll_No).SingleOrDefault();
                if (checkforexist != null)
                {
                    checkforexist.Roll_No = a.Roll_No;
                    checkforexist.Name = a.Name;
                    checkforexist.Tamil = Convert.ToInt32(a.Tamil);
                    checkforexist.English = Convert.ToInt32(a.English);
                    checkforexist.Maths = Convert.ToInt32(a.Maths);
                    checkforexist.Science = Convert.ToInt32(a.Science);
                    checkforexist.Social = Convert.ToInt32(a.Social);
                    checkforexist.Total = Convert.ToInt32(a.Total);
                    checkforexist.Average = Convert.ToDouble(a.Average);

                    newsave.SaveChanges();
                }
                else
                {


                    var checkfordetail = newsave.Student_Mark_Information.Where(m => m.Roll_No == a.Roll_No).SingleOrDefault();
                    if (checkfordetail == null)
                    {
                        newdata.Roll_No = a.Roll_No;
                        newdata.Name = a.Name;
                        newdata.Tamil = Convert.ToInt32(a.Tamil);
                        newdata.English = Convert.ToInt32(a.English);
                        newdata.Maths = Convert.ToInt32(a.Maths);
                        newdata.Science = Convert.ToInt32(a.Science);
                        newdata.Social = Convert.ToInt32(a.Social);
                        newdata.Total = Convert.ToInt32(a.Total);
                        newdata.Average = Convert.ToDouble(a.Average);
                        newsave.Student_Mark_Information.Add(newdata);
                        newsave.SaveChanges();
                    }
                }





            }


        }







        #endregion

        #region StudentLogin


        public bool StudentLogin(StudentDetails loginDetails)
        {
            if (loginDetails != null)
            {
                using (SampletestContext loginEntity = new SampletestContext())
                {
                    var staffLoginValues = loginEntity.Student_Personal_Details.Where(x => x.Roll_No == loginDetails.RollNumber && x.Password == loginDetails.Password && x.Is_Deleted == false).SingleOrDefault();
                    if (staffLoginValues != null)
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        #endregion

        #region GetSingleMarkList


        public List<StudentMarkDetails> GetStudentMarkList(StudentDetails StudentList)
        {
            List<StudentMarkDetails> studentMarkList = new List<StudentMarkDetails>();
            using (SampletestContext markDisplayEntity = new SampletestContext())
            {
                var displayValue = markDisplayEntity.Student_Mark_Information.Where(x => x.Roll_No == StudentList.RollNumber).ToList();
                if (displayValue != null)
                {
                    foreach (var studentMarkValues in displayValue)
                    {
                        StudentMarkDetails getStudentDetails = new StudentMarkDetails();
                        getStudentDetails.Roll_No = studentMarkValues.Roll_No;
                        getStudentDetails.Name = studentMarkValues.Name;
                        getStudentDetails.Tamil = studentMarkValues.Tamil;
                        getStudentDetails.English = studentMarkValues.English;
                        getStudentDetails.Maths = studentMarkValues.Maths;
                        getStudentDetails.Science = studentMarkValues.Science;
                        getStudentDetails.Social = studentMarkValues.Social;
                        getStudentDetails.Total = studentMarkValues.Total;
                        getStudentDetails.Average = studentMarkValues.Average;


                        studentMarkList.Add(getStudentDetails);
                    }
                }
            }
            return studentMarkList;
        }
        #endregion

        #region GetallstudentMarkfordispaly


        public List<StudentMarkDetails> AllStudentMarkList()
        {
            List<StudentMarkDetails> studentMarkList = new List<StudentMarkDetails>();
            using (SampletestContext markDisplayEntity = new SampletestContext())
            {
                var displayValue = markDisplayEntity.Student_Mark_Information.Where(x => x.Is_Deleted == false).ToList();
                if (displayValue != null)
                {
                    foreach (var studentMarkValues in displayValue)
                    {
                        StudentMarkDetails getStudentDetails = new StudentMarkDetails();
                        getStudentDetails.MarkId = studentMarkValues.Student_Mark_Id;
                        getStudentDetails.Roll_No = studentMarkValues.Roll_No;
                        getStudentDetails.Name = studentMarkValues.Name;
                        getStudentDetails.Tamil = studentMarkValues.Tamil;
                        getStudentDetails.English = studentMarkValues.English;
                        getStudentDetails.Maths = studentMarkValues.Maths;
                        getStudentDetails.Science = studentMarkValues.Science;
                        getStudentDetails.Social = studentMarkValues.Social;
                        getStudentDetails.Total = studentMarkValues.Total;
                        getStudentDetails.Average = studentMarkValues.Average;


                        studentMarkList.Add(getStudentDetails);
                    }
                }
            }
            return studentMarkList;

        }
        #endregion

        #region DeleteMark


        public bool DeleteMark(int id)
        {
            using (SampletestContext DeleteEntities = new SampletestContext())
            {
                var deleteValue = DeleteEntities.Student_Mark_Information.Where(x => x.Student_Mark_Id == id && x.Is_Deleted == false).SingleOrDefault();
                if (deleteValue != null)
                {
                    deleteValue.Is_Deleted = true;
                    deleteValue.Updated_time_stamp = DateTime.Now;
                    DeleteEntities.SaveChanges();
                    return true;
                }

            }
            return false;
        }
        #endregion

        public bool SendEmail(StudentDetails studentInfo)
        {
            using (SampletestContext studentEntity = new SampletestContext())
            {
                var emailValue = studentEntity.Student_Personal_Details.Where(x => x.Student_Id == studentInfo.StudentId).SingleOrDefault();
                if (emailValue != null)
                {
                    emailValue.Subject = studentInfo.Subject;
                    emailValue.DateTime = studentInfo.DateTime;
                    emailValue.Updated_time_stamp = DateTime.Now;

                    studentEntity.SaveChanges();
                }
                return true;
            }

        }
    }
}



