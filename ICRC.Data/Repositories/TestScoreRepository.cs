using ICRC.Data.Infrastructure;
using ICRC.Model;
using IRCRC.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{


    public class TestScoreRepository : RepositoryBase<TestScore>, ITestScoreRepository
    {
        public TestScoreRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<TestScore> GetTestScoreByPerson(string name)
        {
            return DbContext.TestScores.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name));
        }

        public IEnumerable<TestScoreViewModel> GetDistinctTestScores()
        {
            var data = DbContext.Database.SqlQuery<TestScoreViewModel>("sp_GetTestScores").ToList();

            return data;
        }



    }

    public interface ITestScoreRepository : IRepository<TestScore>
    {
        IEnumerable<TestScore> GetTestScoreByPerson(string name);

        IEnumerable<TestScoreViewModel> GetDistinctTestScores();
    }
    
}
