using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    public class Reciprocities
    {
        public int ID { get; set; }
        [DisplayName("Originating Board")]
        public int OriginatingBoard { get; set; }
        [DisplayName("Requested Board")]
        public int RequestedBoard { get; set; }
        [DisplayName("IC&RC Credential Acronym")]
        public int ICRCCertID { get; set; }
        [DisplayName("Date of Entry")]
        public DateTime DateofEntry { get; set; }
        public bool Status { get; set; }
        [DisplayName("Approval Date")]
        public DateTime ApprovalDate { get; set; }
        [DisplayName("Reciprocity Fee")]
        public double RecprocityFee { get; set; }
        [DisplayName("Payment Type")]
        public int PaymentType { get; set; }
        [DisplayName("Payment Number")]
        public string PaymentNumber { get; set; }
        public string Notes { get; set; }        
        [DisplayName("Person")]
        public int PersonID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }

        [NotMapped]
        public string OrginiatingBoardName { get; set; }
        [NotMapped]
        public string RequestedBoardName{ get; set; }
        [NotMapped]
        public string CertificationAcronym { get; set; }
        [NotMapped]
        public string PaymentTypeName { get; set; }
    }
}
