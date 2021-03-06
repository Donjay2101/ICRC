﻿using ICRC.Data.Infrastructure;
using ICRC.Model;
using IRCRC.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{


    public class TestScoreRepository : RepositoryBase<TestScore>, ITestScoreRepository
    {
        public TestScoreRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public void UploadCSV(string FilePath)
        {
            DataTable dt = new DataTable();

            Type type = typeof(TestScore);
            var properties = type.GetProperties();
            foreach(PropertyInfo prop in properties)
            {
                
        
                if(prop.Name!="ID" && prop.Name!="PreviousFirstName" && prop.Name!= "PreviousLastName" && prop.Name != "PreviousAddress1")
                {
                    dt.Columns.Add(new DataColumn(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType));
                }
                
            }

            string CSVData=File.ReadAllText(FilePath);
            int count = 0;
            foreach (string row in CSVData.Split('\n'))
            {
                if(count!=0)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;
                        foreach (string cell in row.Split(','))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }
                count++;                
            }

            string conn = ConfigurationManager.ConnectionStrings["IRCREntities"].ConnectionString;

            SqlBulkCopy bulkinsert = new SqlBulkCopy(conn);
            bulkinsert.DestinationTableName = "dbo.TestScores";
            bulkinsert.WriteToServer(dt);

            File.Delete(FilePath);
        }



        public IEnumerable<TestScore> GetScoresForIndex(string lastname,string firstname,string middlename,string emailaddress,string address1,string address2,string exam,string status)
        {
            return DbContext.Database.SqlQuery<TestScore>("spGetTestSocreForIndex @LastName,@FirstName,@middleName,@emailAddress,@address1,@address2,@exam,@status",
                new SqlParameter("@LastName",lastname),
                new SqlParameter("@firstName",firstname),
                new SqlParameter("@middleName",middlename),
                new SqlParameter("@emailAddress",emailaddress),
                new SqlParameter("@address1",address1),
                new SqlParameter("@address2",address2),
                new SqlParameter("@exam",exam),
                new SqlParameter("@status", status)).AsQueryable();
        }
        public IEnumerable<TestScoreViewModel> GetTestScoreByPerson(string name)
        {
            return DbContext.Database.SqlQuery<TestScoreViewModel>("sp_SearchLastName @value", new SqlParameter("@value", name));
        }

        public IEnumerable<TestScoreViewModel> GetDistinctTestScores()
        {
            var data = DbContext.Database.SqlQuery<TestScoreViewModel>("sp_GetTestScores").ToList();

            return data;
        }

        public IEnumerable<TestScoreViewModel> GetLastNames(int num)
        {
            var data = DbContext.Database.SqlQuery<TestScoreViewModel>("sp_GetLastNames @page",new SqlParameter("@page",num)).ToList();

            return data;
        }


        public IEnumerable<TestScoreViewModel> GetFirstNames(string name)
        {
            var data = DbContext.Database.SqlQuery<TestScoreViewModel>("sp_GetFirstNames @name", new SqlParameter("@name",name)).ToList();

            return data;
        }

        public IEnumerable<TestScoreViewModel> GetDataByFirstAndLastName(string firstname,string lastname,string address1)
        {
                var data= DbContext.Database.SqlQuery<TestScoreViewModel>("sp_GetAllByFirstandLastName @Firstname, @Lastname , @address1", 
                    new SqlParameter("@Firstname", firstname??(object)DBNull.Value), 
                    new SqlParameter("@Lastname",lastname ?? (object)DBNull.Value), 
                    new SqlParameter("@Address1",address1 ?? (object)DBNull.Value)).AsQueryable();
                    return data;
        }


        public void UpdateScores(TestScore model)
        {
            DbContext.Database.ExecuteSqlCommand("sp_udpateScores @Exam,@ExamDate,@Score,@Status,@TestingCompany,@Board,@Id,@firstname,@lastname,@address1", new SqlParameter("@Exam", model.Exam),
                new SqlParameter("@ExamDate", model.ExamDate),
                new SqlParameter("@Score", model.Score)
                , new SqlParameter("@Status", model.Status),
                new SqlParameter("@TestingCompany", model.TestingCompany),
                new SqlParameter("@Board", model.Board),
                new SqlParameter("@ID", model.ID),
                new SqlParameter("@firstname", model.FirstName??(object)DBNull.Value),
                new SqlParameter("@lastname",model.LastName ?? (object)DBNull.Value),
                new SqlParameter("@address1", model.Address1 ?? (object)DBNull.Value)
                );

            //DbContext.Database.SqlQuery(null,"sp_udpateScores @Eaxm,@ExamDate,@Status,@TestingCompany,@Board,@Id", new SqlParameter("@Exam", model.Exam),
            //    new SqlParameter("@ExamDate", model.ExamDate)
            //    , new SqlParameter("@Status", model.Status),
            //    new SqlParameter("@TestingCompany", model.TestingCompany),
            //    new SqlParameter("@Board", model.Board),
            //    new SqlParameter("@ID", model.ID));
        }

        public override void Update(TestScore model)
        {
            DbContext.Database.ExecuteSqlCommand("sp_udpateTestScoreInformation @LastName,@FirstName,@MiddleName,@Address1,@Address2,@EmailAddress,@City,@state,@ZipCode,@ZipPlus,@PreviousFname,@PreviousLName,@previousAddress1",
                new SqlParameter("@LastName", model.LastName??""),
                new SqlParameter("@FirstName", model.FirstName==null?"": model.FirstName)
                , new SqlParameter("@MiddleName", model.MiddleName??""),
                new SqlParameter("@Address1", model.Address1??""),
                new SqlParameter("@Address2", model.Address2??""),
                new SqlParameter("@EmailAddress", model.EmailAddress??""),
                new SqlParameter("@City", model.City??""),
                new SqlParameter("@State", model.State??""),
                new SqlParameter("@ZipCode", model.ZipCode??""),
                new SqlParameter("@ZipPlus", model.ZipPlus??""),
                new SqlParameter("@PreviousFName", model.PreviousFirstName),
                new SqlParameter("@PreviousLName", model.PreviousLastName),
                new SqlParameter("@PreviousAddress1", model.PreviousAddress1)
                );
        }

    }

    public interface ITestScoreRepository : IRepository<TestScore>
    {
        IEnumerable<TestScoreViewModel> GetTestScoreByPerson(string name);

        IEnumerable<TestScore> GetScoresForIndex(string lastname, string firstname, string middlename, string emailaddress, string address1, string address2, string exam, string status);
        IEnumerable<TestScoreViewModel> GetDistinctTestScores();

        IEnumerable<TestScoreViewModel> GetLastNames(int num);

        IEnumerable<TestScoreViewModel> GetFirstNames(string name);

        IEnumerable<TestScoreViewModel> GetDataByFirstAndLastName(string firstname,string lastname,string address1);


        void UploadCSV(string FilePath);
        void UpdateScores(TestScore model);
    }
    
}
