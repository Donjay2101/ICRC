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

       

        public virtual int Commit()
        {
            return base.SaveChanges();
        }

        public DbSet<CertifiedPersons> CertifiedPersons { get; set; }
        public DbSet<Boards> Boards{ get; set; }
        public DbSet<Certifications> Certifications { get; set; }
        public DbSet<Certificates> Certificates { get; set; }
        public DbSet<StudentVoilations> StudentVoilations { get; set; }
        public DbSet<Reciprocities> Reciprocities { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Scores> Scores { get; set; }
        public DbSet<TestingCompany> TestingCompanies { get; set; }
        public DbSet<EthicalVoilation> EthicalVoilations { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

    }
}
