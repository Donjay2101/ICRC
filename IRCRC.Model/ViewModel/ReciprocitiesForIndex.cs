﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model.ViewModel
{
    public class ReciprocitiesForIndex
    {
        public int ID { get; set; }
        public string PersonName { get; set; }
        public int Person { get; set; }
        public string OriginatingBoardName { get; set; }
        public string RequestedBoardName { get; set; }
        public string CertificateAcronym { get; set; }

        public bool Approved { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public double ReciprocityFee { get;set;}

        public string PaymentTypeName { get; set; }
        public string PaymentNumber { get; set; }
        public string Notes { get; set; }

        public bool Status { get; set; }

        public int OriginatingBoard { get; set; }
    }
}
