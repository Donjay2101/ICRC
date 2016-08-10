using ICRC.Data.Infrastructure;
using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{

    public interface IEthicalVoilationsRepository:IRepository<EthicalVoilation>
    {

    }


    public class EthicalVoilationsRepository:RepositoryBase<EthicalVoilation>,IEthicalVoilationsRepository
    {
        public EthicalVoilationsRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }              
    }
}
