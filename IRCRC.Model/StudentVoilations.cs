using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    [Table("StudentVoilations")]
    public class StudentVoilations
    {
        public int ID { get; set; }
        public int Board { get; set; }
        public int EthicalVoilationId { get; set; }
        public DateTime Date { get; set; }
        public bool IsScanned { get; set; }
        public bool IsLetterSent { get; set; }
        public bool ISsharable { get; set;}
        public string Comments { get; set; }                
        public int personID { get; set; }
        public string Notes { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }

        [NotMapped]
        public string BoardName { get; set; }
        [NotMapped]
        public string PersonName { get; set; }
        [NotMapped]
        public string EthicalVoilation { get; set; }
    }
}
