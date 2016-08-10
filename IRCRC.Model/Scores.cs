using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    public class Scores
    {
        public int ID { get; set; }
        public int CertifiedPersonId { get; set; }
        public DateTime ExamDate { get; set; }
        public bool Status { get; set; }
        public int ScaledScore { get; set; }
        public int TestingCompany { get; set; }
        public int Board { get; set; }
    }
}
