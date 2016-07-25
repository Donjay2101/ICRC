using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    public class EthicalVoilations
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string EthicalVoilation { get; set; }
        public string Comments { get; set; }
        public int personID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
