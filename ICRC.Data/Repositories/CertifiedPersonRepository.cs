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

        public IQueryable<CertifiedPersons> GetAlla(int pageIndex)
        {
            return DbContext.CertifiedPersons.AsQueryable();
            //return DbContext.Database.SqlQuery<CertifiedPersons>("exec sp_GetCertifiedPersons @pageindex", new SqlParameter("@pageindex", pageIndex)).ToList();
        }
    }

    public interface ICertifiedPersonRepository:IRepository<CertifiedPersons>
    {
         IQueryable<CertifiedPersons> GetAlla(int pageIndex);
    }
}
