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
            
            var data =( from cr in DbContext.Certifications                      
                       join c in DbContext.Certificates on cr.CertID equals c.ID
                       join bc in DbContext.Boards on cr.BoardCertificateAcronym equals bc.ID
                       join ib in DbContext.Certificates on cr.IssueBoard equals ib.ID
                       join cp in DbContext.CertifiedPersons on cr.PersonID equals cp.ID
                       select new {
                           AddToPrintQueues=cr.AddToPrintQueues,
                           BoardCertificateAcronym=cr.BoardCertificateAcronym,
                           CertID=cr.CertID,
                           BoardCertificateNumber=cr.BoardCertificateNumber,
                           BoardCertificateAcronymName=bc.Acronym,
                           CertificateAcronym=c.Name,
                           certificateNo=cr.certificateNo,
                           CertIssueDate=cr.CertIssueDate,
                           CertNotes=cr.CertNotes,
                           CertRequestDate=cr.CertRequestDate,
                           CertRequestFee=cr.CertRequestFee,
                           CreatedAt=cr.CreatedAt,
                           CreatedBy=cr.CreatedBy,
                           ID=cr.ID,
                           IssueBoard=cr.IssueBoard,
                           IssueBoardAcronym=ib.Name,
                           ModifiedAt=cr.ModifiedAt,
                           ModifiedBy=cr.ModifiedBy,
                           PaymentNumber=cr.PaymentNumber,
                           PaymentType=cr.PaymentType,
                           PersonID=cr.PersonID,
                           PersonName=cp.FirstName+" "+cp.MiddleName+" "+cp.LastName,
                           RecertDate=cr.RecertDate                                                                              
                       }).ToList().Select(x=>new Certifications {
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
