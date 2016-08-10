using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    public class Reciprocities
    {
        public int ID { get; set; }
        public int OriginatingBoard { get; set; }
        public int RequestedBoard { get; set; }
        public int ICRCCertID { get; set; }
        public DateTime DateofEntry { get; set; }
        public bool Status { get; set; }
        public DateTime ApprovalDate { get; set; }
        public double RecprocityFee { get; set; }
        public int PaymentType { get; set; }
        public string PaymentNumber { get; set; }
        public string Notes { get; set; }        
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
