using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    public class Certifications
    {
        public int ID { get; set; }
        [DisplayName ("IC&RC credential Acronym")]
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
        public int PersonID{ get; set; }                                
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }

        [NotMapped]
        public string BoardCertificateAcronymName { get; set; }
        [NotMapped]
        public string IssueBoardAcronym { get; set; }
        [NotMapped]
        public string CertificateAcronym {get;set;}

        [NotMapped]
        public string PersonName { get; set; }
    }
}
