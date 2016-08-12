﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    public class Certifications
    {
        public int ID { get; set; }
        [DisplayName ("IC&RC Acronym")]
        public int CertID { get; set; }
        [DisplayName("Certificate Number")]
        public int certificateNo { get; set; }
        [DisplayName("Issuing Board")]
        public int IssueBoard { get; set; }

        [DisplayName("Board's Certificate Number")]
        public string BoardCertificateNumber { get; set; }
        [DisplayName("Board's Certificate Accronym")]
        public int BoardCertificateAcronym { get; set; }
        [DisplayName("Certificate Issued Date")]
        public DateTime CertIssueDate { get; set; }
        [DisplayName("Expiration Date")]
        public DateTime RecertDate { get; set; }
        [DisplayName("Certificate Request Date")]
        public DateTime CertRequestDate { get; set; }
        [DisplayName("Certificate Request Fee")]
        public double CertRequestFee { get; set; }
        [DisplayName("Payment Number")]
        public string PaymentNumber { get; set; }
        [DisplayName("Payment Type")]
        public int PaymentType { get; set; }
        [DisplayName("Certificate Notes")]
        public string CertNotes { get; set; }
        [DisplayName("Add To Print Queues")]
        public bool AddToPrintQueues { get; set; }
        [DisplayName("Person")]
        public int PersonID{ get; set; }                                
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }

        [NotMapped]
        public string BoardCertificateAcronymName { get; set; }
        [NotMapped]
        public string IssueBoardAcronym { get; set; }
        [NotMapped]
        public string CertificateAcronym {get;set;}

        [NotMapped]
        public string PersonName { get; set; }
    }
}
