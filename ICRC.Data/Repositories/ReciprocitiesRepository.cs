using ICRC.Data.Infrastructure;
using ICRC.Model;
using IRCRC.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Data.Repositories
{
    public class ReciprocitiesRepository:RepositoryBase<Reciprocities>,IReciproctiesRepository
    {
        public ReciprocitiesRepository(IDbFactory dbFactory):base(dbFactory)
        {
        
        }
        public IEnumerable<Reciprocities> GetByPersonID(int ID)
        {

            var data = DbContext.Database.SqlQuery<ReciprocitiesViewModel>("exec sp_GetReciprocities @personID", new SqlParameter("@personID", ID))
              .Select(x => new Reciprocities
              {
                  ApprovalDate = x.ApprovalDate,
                  CreatedAt = x.CreatedAt,
                  CreatedBy = x.CreatedBy,
                  DateofEntry = x.DateofEntry,
                  ICRCCertID = x.ICRCCertID,
                  ID = x.ID,
                  ModifiedAt = x.ModifiedAt,
                  ModifiedBy = x.ModifiedBy,
                  Notes = x.Notes,
                  OriginatingBoard = x.OriginatingBoard,
                  PaymentNumber = x.PaymentNumber,
                  PaymentType = x.PaymentType,
                  PersonID = x.PersonID,
                  RecprocityFee = x.RecprocityFee,
                  RequestedBoard = x.RequestedBoard,
                  Status = x.Status,
                  CertificationAcronym = x.CertificationAcronym,
                  OrginiatingBoardName = x.OrginiatingBoardName,
                  PaymentTypeName = x.PaymentTypeName,
                  RequestedBoardName = x.RequestedBoardName
              }).ToList();


            return data;
        }

        public override IEnumerable<Reciprocities> GetAll()
        {

            //var data = (from re in DbContext.Reciprocities
            //            join ob in DbContext.Boards on re.OriginatingBoard equals ob.ID
            //            join cert in DbContext.Certificates on re.ICRCCertID equals cert.ID
            //            join type in DbContext.PaymentTypes on re.PaymentType equals type.ID
            //            join rb in DbContext.Boards on re.RequestedBoard equals rb.ID
            //            join CP in DbContext.CertifiedPersons on re.PersonID equals CP.ID
            //            select new 
            //            {
            //                ApprovalDate = re.ApprovalDate,
            //                CreatedAt = re.CreatedAt,
            //                CreatedBy = re.CreatedBy,
            //                DateofEntry = re.DateofEntry,
            //                ICRCCertID = re.ICRCCertID,
            //                ID = re.ID,
            //                ModifiedAt = re.ModifiedAt,
            //                ModifiedBy = re.ModifiedBy,
            //                Notes = re.Notes,
            //                OriginatingBoard = re.OriginatingBoard,
            //                PaymentNumber = re.PaymentNumber,
            //                PaymentType = re.PaymentType,                            
            //                PersonID = re.PersonID,
            //                RecprocityFee = re.RecprocityFee,
            //                RequestedBoard = re.RequestedBoard,
            //                Status = re.Status,
            //                CertificationAcronym = cert.Name,
            //                OrginiatingBoardName = ob.Acronym,
            //                PaymentTypeName = type.Name,
            //                RequestedBoardName = rb.Acronym
            //            }).ToList()
            
            var data=DbContext.Database.SqlQuery<ReciprocitiesViewModel>("exec sp_GetReciprocities")
            .Select(x=>new Reciprocities {
                            ApprovalDate = x.ApprovalDate,
                            CreatedAt = x.CreatedAt,
                            CreatedBy = x.CreatedBy,
                            DateofEntry = x.DateofEntry,
                            ICRCCertID = x.ICRCCertID,
                            ID = x.ID,
                            ModifiedAt = x.ModifiedAt,
                            ModifiedBy = x.ModifiedBy,
                            Notes = x.Notes,
                            OriginatingBoard =x.OriginatingBoard,
                            PaymentNumber = x.PaymentNumber,
                            PaymentType = x.PaymentType,
                            PersonID = x.PersonID,
                            RecprocityFee = x.RecprocityFee,
                            RequestedBoard = x.RequestedBoard,
                            Status = x.Status,
                            CertificationAcronym = x.CertificationAcronym,
                            OrginiatingBoardName = x.OrginiatingBoardName,
                            PaymentTypeName = x.PaymentTypeName,
                            RequestedBoardName = x.RequestedBoardName
                        }).ToList();

                     
                      

                     return data;
        }
    }

    public interface IReciproctiesRepository:IRepository<Reciprocities>
    {
        IEnumerable<Reciprocities> GetByPersonID(int ID);
    }
}
