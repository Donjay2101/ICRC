using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    public class Certifications
    {
        public int ID { get; set; }
        public int certificateNo { get; set; }
        public int PersondID{ get; set; }
        public DateTime StartDate { get; set; }
        public DateTime IssuedDate{ get; set; }
        public DateTime RecertDate{ get; set; }
        public int CertID{ get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string CertiFicateAccronym {get;set;}
    }
}
