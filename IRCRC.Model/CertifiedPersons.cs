using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    public class CertifiedPersons
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Providence { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public bool EthicalVoilation { get; set; }        
        public int CurrentBoardID { get; set; }
        public int OtherBoardID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
