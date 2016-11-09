using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model.ViewModel
{
    public class CPViewModelForIndex
    {
        public int ID { get; set; }
        public string Suffix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public int CurrentBoardID { get; set; }
        public string BoardAcronym { get; set; }
    }
}
