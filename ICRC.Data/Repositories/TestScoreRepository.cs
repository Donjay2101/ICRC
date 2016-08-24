using ICRC.Data.Infrastructure;
using ICRC.Model;
using IRCRC.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{


    public class TestScoreRepository : RepositoryBase<TestScore>, ITestScoreRepository
    {
        public TestScoreRepository(IDbFactory dbFactory) : base(dbFactory) { }

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

        public IEnumerable<TestScoreViewModel> GetDataByFirstAndLastName(TestScoreViewModel model)
        {
        var data= DbContext.Database.SqlQuery<TestScoreViewModel>("sp_GetAllByFirstandLastName @Firstname, @Lastname , @address1", new SqlParameter("@Firstname", model.FirstName), new SqlParameter("@Lastname",model.LastName), new SqlParameter("@Address1",model.Address1)).ToList();
            return data;
        }


        public void UpdateScores(TestScore model)
        {
            DbContext.Database.ExecuteSqlCommand("sp_udpateScores @Exam,@ExamDate,@Score,@Status,@TestingCompany,@Board,@Id", new SqlParameter("@Exam", model.Exam),
                new SqlParameter("@ExamDate", model.ExamDate),
                new SqlParameter("@Score", model.Score)
                , new SqlParameter("@Status", model.Status),
                new SqlParameter("@TestingCompany", model.TestingCompany),
                new SqlParameter("@Board", model.Board),
                new SqlParameter("@ID", model.ID));

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

        IEnumerable<TestScoreViewModel> GetDistinctTestScores();

        IEnumerable<TestScoreViewModel> GetLastNames(int num);

        IEnumerable<TestScoreViewModel> GetFirstNames(string name);

        IEnumerable<TestScoreViewModel> GetDataByFirstAndLastName(TestScoreViewModel model);
        void UpdateScores(TestScore model);
    }
    
}
