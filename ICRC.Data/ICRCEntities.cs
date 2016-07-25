using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data
{
    public class ICRCEntities:DbContext
    {
        public ICRCEntities():base("IRCREntities")
        {

        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public System.Data.Entity.DbSet<ICRC.Model.CertifiedPersons> CertifiedPersons { get; set; } 
            public DbSet<Certifications> Certifications { get; set; }
        public DbSet<Certificates> Certificates { get; set; }
        public DbSet<EthicalVoilations> Voilations { get; set; }
        public DbSet<Reciprocities> Reciprocities { get; set; }
    }
}
