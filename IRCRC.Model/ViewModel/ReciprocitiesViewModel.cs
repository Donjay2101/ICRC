using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRCRC.Model.ViewModel
{
    public class ReciprocitiesViewModel
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

        
        public string OrginiatingBoardName { get; set; }
        
        public string RequestedBoardName { get; set; }
        
        public string CertificationAcronym { get; set; }
        
        public string PaymentTypeName { get; set; }
    }
}
