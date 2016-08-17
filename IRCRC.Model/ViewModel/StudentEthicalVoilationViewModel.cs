﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRCRC.Model.ViewModel
{
    public class StudentEthicalViolationViewModel
    {
        public int ID { get; set; }
        
        public Nullable<int> Board { get; set; }
        
        public Nullable<int> EthicalViolationId { get; set; }
        
        public DateTime Date { get; set; }
        
        public bool IsScanned { get; set; }
        
        public bool IsLetterSent { get; set; }
        
        public bool ISsharable { get; set; }
        
        public string Comments { get; set; }
        
        public Nullable<int> personID { get; set; }
        public string Notes { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }

        
        public string BoardName { get; set; }
        
        public string PersonName { get; set; }
        
        public string EthicalViolation { get; set; }
    }
}
