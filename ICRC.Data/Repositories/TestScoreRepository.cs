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

    }

    public interface ITestScoreRepository : IRepository<TestScore>
    {
        IEnumerable<TestScoreViewModel> GetTestScoreByPerson(string name);

        IEnumerable<TestScoreViewModel> GetDistinctTestScores();

        IEnumerable<TestScoreViewModel> GetLastNames(int num);

        IEnumerable<TestScoreViewModel> GetFirstNames(string name);

        IEnumerable<TestScoreViewModel> GetDataByFirstAndLastName(TestScoreViewModel model);
    }
    
}
