using IC_RC.ViewModels;
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
        
        public override  IEnumerable<Certifications> GetAll()
        {

             var data = DbContext.Database.SqlQuery<CertificationViewModel>("exec SP_GetCertifications").ToList().Select(x => new Certifications
             {
                 AddToPrintQueues = x.AddToPrintQueues,
                 BoardCertificateAcronym = x.BoardCertificateAcronym,
                 CertID = x.CertID,
                 BoardCertificateNumber = x.BoardCertificateNumber,
                 BoardCertificateAcronymName = x.BoardCertificateAcronymName,
                 CertificateAcronym = x.CertificateAcronym,
                 certificateNo = x.certificateNo,
                 CertIssueDate = x.CertIssueDate,
                 CertNotes = x.CertNotes,
                 CertRequestDate = x.CertRequestDate,
                 CertRequestFee = x.CertRequestFee,
                 CreatedAt = x.CreatedAt,
                 CreatedBy = x.CreatedBy,
                 ID = x.ID,
                 IssueBoard = x.IssueBoard,
                 IssueBoardAcronym = x.IssueBoardAcronym,
                 ModifiedAt = x.ModifiedAt,
                 ModifiedBy = x.ModifiedBy,
                 PaymentNumber = x.PaymentNumber,
                 PaymentType = x.PaymentType,
                 PersonID = x.PersonID,
                 PersonName = x.PersonName,
                 RecertDate = x.RecertDate
             }).ToList();
            //var data = (from c in DbContext.Certificates
            //            join cr in DbContext.Certifications on c.ID equals cr.CertID
            //            into t
            //            from rt in t.DefaultIfEmpty()
            //            join bc in DbContext.Boards on rt.BoardCertificateAcronym equals bc.ID
            //            into t1
            //            from rt1 in t1.DefaultIfEmpty()
            //            join ib in DbContext.Boards on rt.IssueBoard equals ib.ID
            //            into t2
            //            from rt2 in t2.DefaultIfEmpty()
            //            join cp in DbContext.CertifiedPersons on rt.PersonID equals cp.ID                      
            //            select new
            //            {
            //                AddToPrintQueues = (bool?)rt.AddToPrintQueues,
            //                BoardCertificateAcronym = (int?)rt.BoardCertificateAcronym,
            //                CertID = (int?)rt.CertID,
            //                BoardCertificateNumber = rt.BoardCertificateNumber,
            //                BoardCertificateAcronymName = rt1.Acronym,
            //                CertificateAcronym = c.Name,
            //                certificateNo = rt.certificateNo,
            //                CertIssueDate = rt.CertIssueDate,
            //                CertNotes = rt.CertNotes,
            //                CertRequestDate = rt.CertRequestDate,
            //                CertRequestFee = rt.CertRequestFee,
            //                CreatedAt = rt.CreatedAt,
            //                CreatedBy = (int?)rt.CreatedBy,
            //                ID = rt.ID,
            //                IssueBoard = (int?)rt.IssueBoard,
            //                IssueBoardAcronym = rt2.Acronym,
            //                ModifiedAt = rt.ModifiedAt,
            //                ModifiedBy = rt.ModifiedBy,
            //                PaymentNumber = rt.PaymentNumber,
            //                PaymentType = rt.PaymentType,
            //                PersonID = (int?)rt.PersonID,
            //                PersonName = cp.FirstName + " " + cp.MiddleName + " " + cp.LastName,
            //                RecertDate = rt.RecertDate
            //            }).ToList().Select(x => new Certifications
            //            {
            //                AddToPrintQueues = x.AddToPrintQueues,
            //                BoardCertificateAcronym = x.BoardCertificateAcronym,
            //                CertID = x.CertID,
            //                BoardCertificateNumber = x.BoardCertificateNumber,
            //                BoardCertificateAcronymName = x.BoardCertificateAcronymName,
            //                CertificateAcronym = x.CertificateAcronym,
            //                certificateNo = x.certificateNo,
            //                CertIssueDate = x.CertIssueDate,
            //                CertNotes = x.CertNotes,
            //                CertRequestDate = x.CertRequestDate,
            //                CertRequestFee = x.CertRequestFee,
            //                CreatedAt = x.CreatedAt,
            //                CreatedBy = x.CreatedBy,
            //                ID = x.ID,
            //                IssueBoard = x.IssueBoard ?? x.IssueBoard.Value,
            //                IssueBoardAcronym = x.IssueBoardAcronym,
            //                ModifiedAt = x.ModifiedAt,
            //                ModifiedBy = x.ModifiedBy,
            //                PaymentNumber = x.PaymentNumber,
            //                PaymentType = x.PaymentType,
            //                PersonID = x.PersonID,
            //                PersonName = x.PersonName,
            //                RecertDate = x.RecertDate
            //            }).ToList();

            return data;
        }

        public IEnumerable<Certifications> GetCertificationsByPersonID(int ID)
        {
            
            return GetAll().Where(x => x.PersonID == ID);
        }

        public bool CheckNumber(int Number)
        {
            var data=DbContext.Certifications.Where(x => x.certificateNo == Number).ToList();
            if(data.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

    }

    public interface ICertificationsRepository:IRepository<Certifications>
    {
        IEnumerable<Certifications> GetCertificationsByPersonID(int ID);
        bool CheckNumber(int number);
        //IEnumerable<Certifications> GetAll();


    }
}
