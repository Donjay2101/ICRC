using ICRC.Data.Infrastructure;
using ICRC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{
    public class CertificationsRepository:RepositoryBase<Certifications>,ICertificationsRepository
    {

        public CertificationsRepository(IDbFactory dbFactory) : base(dbFactory) { }
        
        public override IEnumerable<Certifications> GetAll()
        {
            var data = dbContext.Certifications
                .Join(dbContext.Certificates, c => c.CertID, x => x.ID, (c, x) => new { Certifications = c, Certificate = x })
                .Select(x => new Certifications
                {
                    CertID = x.Certifications.CertID,
                    CertiFicateAccronym = x.Certificate.Name,
                    certificateNo = x.Certifications.certificateNo,
                    CreatedAt = x.Certifications.CreatedAt,
                    CreatedBy = x.Certifications.CreatedBy,
                    ID = x.Certifications.ID,
                    IssuedDate = x.Certifications.IssuedDate,
                    ModifiedAt = x.Certifications.ModifiedAt,
                    ModifiedBy = x.Certifications.ModifiedBy,
                    PersondID = x.Certifications.PersondID,
                    RecertDate = x.Certifications.RecertDate,
                    StartDate = x.Certifications.StartDate
                }).ToList();
            return data;
        }

        public IEnumerable<Certifications> GetCertificationsByPersonID(int ID)
        {            
            return GetAll().Where(x => x.PersondID == ID);
        }

    }

    public interface ICertificationsRepository:IRepository<Certifications>
    {
        IEnumerable<Certifications> GetCertificationsByPersonID(int ID);
        IEnumerable<Certifications> GetAll();


    }
}
