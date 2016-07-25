using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Infrastructure
{
    public class DbFactory: Disposable,IDbFactory
    {
        ICRCEntities dbcontext;
        public ICRCEntities Init()
        {
            return dbcontext ?? (dbcontext = new ICRCEntities());
        }

        protected override void DisposeCore()
        {
            if(dbcontext!=null)
            {
                dbcontext.Dispose();
            }
        }
          
    }
}
