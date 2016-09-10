using ICRC.Data.Infrastructure;
using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public IEnumerable<CertifiedPersons> GetAll(int pageIndex)
        {
            return DbContext.Database.SqlQuery<CertifiedPersons>("exec sp_GetCertifiedPersons @pageindex", new SqlParameter("@pageindex", pageIndex)).ToList();
        }
    }

    public interface ICertifiedPersonRepository:IRepository<CertifiedPersons>
    {
         IEnumerable<CertifiedPersons> GetAll(int pageIndex);
    }
}
