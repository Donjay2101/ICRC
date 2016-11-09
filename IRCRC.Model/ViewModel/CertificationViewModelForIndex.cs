using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model.ViewModel
{
    public class CertificationViewModelForIndex
    {
        public int ID { get; set; }
        public int CertID { get; set; }
        public int CertificateNumber { get; set; }
        public DateTime? CertIssueDate { get; set; }
        public DateTime? RecertDate { get; set; }
        public int PersonID { get; set;}

        public int IssueBoard { get; set; }

        public bool AddToPrintQueues { get; set; }

        public string CertificateAcronym { get; set; }
        public string PersonName { get; set; }
    }
}
