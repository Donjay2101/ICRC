using ICRC.Data.Infrastructure;
using ICRC.Model;
using ICRC.Model.ViewModel;
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
        public IQueryable<ReciprocitiesForIndex> GetByPersonID(int ID)
        {

            return GetReciprocitiesForIndex().Where(x => x.Person == ID).AsQueryable();
            //var data = DbContext.Database.SqlQuery<ReciprocitiesViewModel>("exec sp_GetReciprocities @PersonID", new SqlParameter("@PersonID", ID))
            //  .Select(x => new Reciprocities
            //  {
            //      ApprovalDate = x.ApprovalDate,
            //      CreatedAt = x.CreatedAt,
            //      CreatedBy = x.CreatedBy,
            //      DateofEntry = x.DateofEntry,
            //      ICRCCertID = x.ICRCCertID,
            //      ID = x.ID,
            //      ModifiedAt = x.ModifiedAt,
            //      ModifiedBy = x.ModifiedBy,
            //      Notes = x.Notes,
            //      OriginatingBoard = x.OriginatingBoard,
            //      PaymentNumber = x.PaymentNumber,
            //      PaymentType = x.PaymentType,
            //      PersonID = x.PersonID,
            //      RecprocityFee = x.RecprocityFee,
            //      RequestedBoard = x.RequestedBoard,
            //      Status = x.Status,
            //      CertificationAcronym = x.CertificationAcronym,
            //      OrginiatingBoardName = x.OrginiatingBoardName,
            //      PaymentTypeName = x.PaymentTypeName,
            //      RequestedBoardName = x.RequestedBoardName
            //  }).AsQueryable();


            //return data;
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
                            RequestedBoardName = x.RequestedBoardName,
                            PersonName=x.PersonName                           
                        }).AsQueryable();

                     
                      

                     return data;
        }

        public override Reciprocities GetByID(int ID)
        {
           return  DbContext.Database.SqlQuery<ReciprocitiesViewModel>("exec sp_GetReciprocitiesbyID @id", new SqlParameter("@ID",ID)).Select(x => new Reciprocities
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
                RequestedBoardName = x.RequestedBoardName,
                PersonName = x.PersonName
            }).FirstOrDefault();
        }


        public IEnumerable<ReciprocitiesForIndex> GetReciprocitiesForIndex(string person="",string oboard="",string rboard="",string certifcate="",string number="",string notes="")
        {
            return DbContext.Database.SqlQuery<ReciprocitiesForIndex>("sp_GetReciprocitiesForIndex @person,@Oboard,@Rboard,@certificateAcronym,@paymentNumber,@notes",
                new SqlParameter("@person",person),
                new SqlParameter("@oboard",oboard),
                new SqlParameter("@rboard",rboard),
                new SqlParameter("@certificateAcronym",certifcate),
                new SqlParameter("@paymentNumber",number),
                new SqlParameter("@notes",notes)).AsQueryable();
        }
    }

    public interface IReciproctiesRepository:IRepository<Reciprocities>
    {
        IQueryable<ReciprocitiesForIndex> GetByPersonID(int ID);

        IEnumerable<ReciprocitiesForIndex> GetReciprocitiesForIndex(string person, string oboard, string rboard, string certifcate, string number, string notes);
    }
}
