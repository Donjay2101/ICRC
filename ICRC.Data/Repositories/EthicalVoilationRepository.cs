using ICRC.Data.Infrastructure;
using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{
    public class EthicalVoilationRepository:RepositoryBase<EthicalVoilations>, IEthicalVoilationRepository
    {

        public EthicalVoilationRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }

        public IEnumerable<EthicalVoilations> GetByPersonID(int ID)
        {
            return dbContext.Voilations.Where(x => x.personID == ID);
        }

    }

    public interface IEthicalVoilationRepository:IRepository<EthicalVoilations>
    {
        IEnumerable<EthicalVoilations> GetByPersonID(int ID);
    }
}
