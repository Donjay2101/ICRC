using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IC_RC.ViewModels
{
    public class CertificationViewModel
    {
        public int ID { get; set; }
        public int CertID { get; set; }
        public int certificateNo { get; set; }
        public int IssueBoard { get; set; }
        public string BoardCertificateNumber { get; set; }
        public int BoardCertificateAcronym { get; set; }
        public DateTime CertIssueDate { get; set; }
        public DateTime RecertDate { get; set; }
        public DateTime CertRequestDate { get; set; }
        public double CertRequestFee { get; set; }
        public string PaymentNumber { get; set; }
        public int PaymentType { get; set; }
        public string CertNotes { get; set; }
        public bool AddToPrintQueues { get; set; }
        public int PersonID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }

       
        public string BoardCertificateAcronymName { get; set; }
       
        public string IssueBoardAcronym { get; set; }
       
        public string CertificateAcronym { get; set; }

               public string PersonName { get; set; }
    }
}