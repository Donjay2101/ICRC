using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Infrastructure
{
    public class UnitOfWork:IUnitOfWork
    {
        public readonly IDbFactory dbFactory;

        private ICRCEntities dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public ICRCEntities Dbcontext
        {
            get { return dbContext ?? (dbContext = new ICRCEntities()); }
        }

        public void Commit()
        {
            Dbcontext.Commit();
        }
    }
}
