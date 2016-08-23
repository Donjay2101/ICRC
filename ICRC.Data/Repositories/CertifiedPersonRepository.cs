using ICRC.Data.Infrastructure;
using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{
    public class CertifiedPersonRepository:RepositoryBase<CertifiedPersons>,ICertifiedPersonRepository
    {
        public CertifiedPersonRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public override IEnumerable<CertifiedPersons> GetAll()
        {
            return DbContext.CertifiedPersons.OrderBy(x => x.LastName).ToList();
        }
    }

    public interface ICertifiedPersonRepository:IRepository<CertifiedPersons>
    {

    }
}
