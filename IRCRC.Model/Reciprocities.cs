using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    public class Reciprocities
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string Notes { get; set; }
        public DateTime DateOfEntry { get; set; }
        public int RequestedBoard{ get; set; }
        public int CertificateID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
