
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StaffManagement.Service.Service
{
    public class StudentService : IStudentService
    {
        #region TestRepositoryInjection


        IStudentRepository _testRepository;
        public StudentService(IStudentRepository testRepository)
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


        public  int ImportFileUpload(FileUpload fileupload)
        {
            string filename = fileupload.Filename;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Root", "FileUpload", filename);

            using var fileStream = new FileStream(path, FileMode.Create);
            
                 fileupload.ExcelValues.CopyToAsync(fileStream);
            


            //create directory "Uploads" if it doesn't exists


            if (fileupload != null)
            {
                if (fileupload.ContentType == "application/vnd.ms-excel" || fileupload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    var connectionString = "";
                    if (fileupload.Filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", path);
                    }
                    else if (fileupload.Filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", path);
                    }
                    //*var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString) */;
                    //var ds = new DataSet();
                    //adapter.Fill(ds, "ExcelTable");
                    //DataTable dtable = ds.Tables["ExcelTable"];
                    //var value = dtable.Columns;
                    //string sheetName = "Sheet1";
                    //var excelFile = new ExcelQueryFactory(path);
                    //var artistAlbums = from a in excelFile.Worksheet<ExcelValidation>(sheetName) select a;
                    //ExcelValidation checkforexist = new ExcelValidation();
                   
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using var package = new ExcelPackage(fileStream);
                        var list = new List<ErorrValidation>();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        var rowcount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowcount; row++)
                        {
                            list.Add(new ErorrValidation
                            {
                                Roll_No = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                Name = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                Tamil = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                English = worksheet.Cells[row, 4].Value.ToString().Trim(),
                                Maths = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                Science = worksheet.Cells[row, 6].Value.ToString().Trim(),
                                Social = worksheet.Cells[row, 7].Value.ToString().Trim(),
                                Total = worksheet.Cells[row, 8].Value.ToString().Trim(),
                                Average = worksheet.Cells[row, 9].Value.ToString().Trim()
                            });
                        }
                        var value = Validate(list);
                        if (value == 200)
                        {
                            _testRepository.ImportFileUpload(list);
                            return value;
                        }
                        else
                        {
                            return value;
                        }


                    
                }

            }
            return 11;
        }
        #endregion

            #region Validate

      
        public int Validate(List<ErorrValidation> list)
        {
            for (int i = 0; i <= list.Count; i++)
            {
                foreach (var mark in list)
                {
                    if (mark.Name != null&&mark.Roll_No != null && mark.English != null && mark.Tamil != null && mark.Maths != null && mark.Science != null && mark.Social != null && mark.Total != null && mark.Average != null)
                    {
                        var regex = new Regex(@"[^a-zA-Z0-9\s]");
                        var regexs = new Regex(@"[^0-9\.\s]");
                        var Mark = new Regex(@"[^0-9\s]");
                        if (regex.IsMatch(mark.Roll_No)||(mark.Roll_No.Length!=8))
                        {
                            

                            return 1;
                        }
                        if (regex.IsMatch(mark.Name)||(mark.Name.Length<3||mark.Name.Length>50))
                        {
                            
                                return 2;
                        }
                        if (Mark.IsMatch(mark.English) || (Convert.ToInt32(mark.English) > 100) || (Convert.ToInt32(mark.English) < 0))
                        {
                            return 3;
                        }
                        if (Mark.IsMatch(mark.Tamil) || (Convert.ToInt32(mark.Tamil) > 100) || (Convert.ToInt32(mark.Tamil) < 0))
                        {
                            return 4;
                        }
                        if (Mark.IsMatch(mark.Maths) || (Convert.ToInt32(mark.Maths) > 100) || (Convert.ToInt32(mark.Maths) < 0))
                        {
                            return 5;
                        }
                        if (Mark.IsMatch(mark.Science) || (Convert.ToInt32(mark.Science) > 100) || (Convert.ToInt32(mark.Science) < 0))
                        {
                            return 6;
                        }
                        if (Mark.IsMatch(mark.Social) || (Convert.ToInt32(mark.Social) > 100) || (Convert.ToInt32(mark.Social) < 0))
                        {
                            return 7;
                        }
                        if (regexs.IsMatch(mark.Total) || (Convert.ToInt32(mark.Total) > 500) || (Convert.ToInt32(mark.Total) < 0))
                        {
                            return 8;
                        }
                        if (regexs.IsMatch(mark.Average) || (Convert.ToDouble(mark.Average) > 100) || (Convert.ToDouble(mark.Average) < 0))
                        {
                            return 9;
                        }
                    }
                    else
                    {
                        return 10;
                    }
                }
            }
            return 200;
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

            #region AllStudentMarkList


            public List<StudentMarkDetails> AllStudentMarkList()
            {
                return _testRepository.AllStudentMarkList();
            }
            #endregion

            #region DeleteMark


            public bool DeleteMark(int id)
            {
                return _testRepository.DeleteMark(id);
            }
        #endregion

        public bool SendEmail(StudentDetails studentInfo)
        {
            return _testRepository.SendEmail(studentInfo);

        }

    }
    }
