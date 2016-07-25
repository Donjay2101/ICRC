using ICRC.Data.Infrastructure;
using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{
    public class ReciprocitiesRepository:RepositoryBase<Reciprocities>,IReciproctiesRepository
    {
        public ReciprocitiesRepository(IDbFactory dbFactory):base(dbFactory)
        {
        
        }
        public IEnumerable<Reciprocities> GetByPersonID(int ID)
        {
            return dbContext.Reciprocities.Where(x => x.PersonID == ID);
        }
    }

    public interface IReciproctiesRepository:IRepository<Reciprocities>
    {
        IEnumerable<Reciprocities> GetByPersonID(int ID);
    }
}
