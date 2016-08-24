using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICRC.Model
{
    public class TestScore
    {
        public int ID { get; set; }
      public Nullable<int> TestingCompany { get; set; }
      public string Exam { get; set; }
        public string LastName { get;set;}
        public string FirstName { get;set;}
        public string MiddleName { get;set;}
        public string CandidateId { get;set;}
        public string EmailAddress { get;set;}
        public Nullable<DateTime> ExamDate { get;set;}
        public string Status { get;set;}
        public Nullable<double> Score { get;set;}
        public Nullable<double> Scale { get;set;}
        public string TestCode { get;set;}
        public string Form { get;set;}
        public Nullable<double> One {get;set;}
    public Nullable<double> Two { get;set;}
public Nullable<double> Three {get;set;}
      public Nullable<double>  Four {get;set;}
      public Nullable<double> Five {get;set;}
      public Nullable<double> Six {get;set;}
      public Nullable<double>  Seventh {get;set;}
      public Nullable<double>  Eight {get;set;}
      public Nullable<double>  Nine {get;set;}
      public Nullable<double> Ten {get;set;}
      public string Method { get;set;}
public Nullable<int> Board { get;set;}
public string Address1 { get;set;}
public string Address2 { get;set;}
public string City { get;set;}
public string State { get;set;}
public string ZipCode { get;set;}
public string ZipPlus { get;set;}


        [NotMapped]
        public string PreviousFirstName { get; set; }

        [NotMapped]
        public string PreviousLastName { get; set; }

        [NotMapped]
        public string PreviousAddress1{ get; set; }

    }
}
