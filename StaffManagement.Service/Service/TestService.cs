using LinqToExcel;
using Microsoft.AspNetCore.Http;
using StaffManagement.Core.IRepository;
using StaffManagement.Core.IServices;
using StaffManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
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


        public async void ImportFileUpload(FileUpload fileupload)
        {
            string filename = fileupload.Filename;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Root", "FileUpload", filename);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await fileupload.ExcelValues.CopyToAsync(fileStream);
            }


            //create directory "Uploads" if it doesn't exists


            if (fileupload != null)
            {
                if (fileupload.contenttype == "application/vnd.ms-excel" || fileupload.contenttype == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
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
                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Sheet1";
                    var excelFile = new ExcelQueryFactory(path);
                    var artistAlbums = from a in excelFile.Worksheet<StudentMarkDetails>(sheetName) select a;

                //    XmlSchemaSet schemas;
                //    schemas = new XmlSchemaSet();
                //    schemas.Add("http://www.deitel.com/booklist", schema + "\\StudentsSchema.xsd");

                //    settings = new XmlReaderSettings();
                //    settings.ProhibitDtd = false;
                //    settings.CheckCharacters = true;
                //    settings.ValidationType = ValidationType.DTD;
                //    settings.Schemas = schemas;
                //    settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
                //    Uri uri = new Uri(schema + "\\StudentDTD.dtd");
                //    settings.XmlResolver.ResolveUri(uri, null);

                //    XmlReader reader = XmlReader.Create(schema + "\\Students.xml", settings);
                //    while (reader.Read()) ;
                //    if (valid)
                //    {
                //        writertbox("Document SCHEMA is valid");
                //    }  // end if  
                //    valid = true;
                //    reader.Close();

                //    return false;
                //}
                //private void ValidateExcelData(string Path)
                //{
                //    XmlReaderSettings settings = new XmlReaderSettings();

                //    settings.ProhibitDtd = false;
                //    settings.DtdProcessing = DtdProcessing.Parse;
                //    settings.ValidationType = ValidationType.DTD;

                //    settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
                //    XmlReader reader = XmlReader.Create(Path + "\\Students.xml", settings);



                //    Parse the file.

                //    try
                //    {

                //        while (reader.Read()) ;

                //        if (valid)
                //        {
                //            writertbox("Document contains valid data");
                //        }  // end if  
                //    }
                //    catch
                //    {


                //    }
                //}
                //private void ValidationCallBack(Object sender, ValidationEventArgs args)
                //{
                //    Display the validation error.  This is only called on error
                //    bool m_Success = false; //Validation failed  
                //    valid = false; // validation failed  
                //    writertbox("Validation error: " + args.Message);
                //}
                _testRepository.ImportFileUpload(fileupload);
                }
            }
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


        }
    }
