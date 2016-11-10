using ICRC.Data.Infrastructure;
using ICRC.Model;
using ICRC.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IC_RC
{
    public class ShrdMaster
    {

        ICRC.Data.ICRCEntities db = new ICRC.Data.ICRCEntities();

        private static ShrdMaster _instance;


        public static ShrdMaster Instance
        {
            get
            {
                return _instance==null ?_instance = new ShrdMaster():_instance;
            }
        }



        public List<PaymentType> GetPaymentList()
        {
            List<PaymentType> list = new List<PaymentType>() { new PaymentType() { ID=1,Name="Credit Card"}, new PaymentType() { ID = 1, Name = "Money Order" }, new PaymentType() { ID = 1, Name = "Cheque" } };
            return list;
        }

        public string GetQueryString(string value)
        {
            if (HttpContext.Current.Request.QueryString[value] != null)
            {
                return HttpContext.Current.Request.QueryString[value];
            }
            return "";
        }

        


     
        public List<Status> Getstatus()
        {
            List<Status> list = new List<Status>()
            {
                new Status() { ID="Pass",Name="Pass"},
                new Status() { ID="Fail",Name="Fail"}
            };

            return list;
        }


        public int GetPageIndex()
        {
            int num = 1;
            if(HttpContext.Current.Request.QueryString["grid-page"]!=null)
            {
                int.TryParse(HttpContext.Current.Request.QueryString["grid-page"], out num);
            }

            return num;
        }
        public bool IsAdmin(string Username)
        {
            var data=db.Users.FirstOrDefault(x => x.Username == Username);

            var roles = db.UserRoles.Where(x => x.UserID == data.ID && x.RoleID==1);
            if(roles!=null && roles.Count()>0)
            {
                return true;
            }
            return false;
        }

        public Users  GetUser(string username)
        {
            return db.Users.FirstOrDefault(x => x.Username == username && x.Active==true);
        }


        public string GetReturnUrl(string defaultUrl)
        {
            //if (defaultUrl == null) throw new ArgumentNullException(nameof(defaultUrl));


            if (HttpContext.Current.Request.QueryString["ReturnUrl"] != null)
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                int index = url.IndexOf("returnUrl");
                string returnUrl = url.Substring(index, (url.Length - index));
                returnUrl = returnUrl.Replace("returnUrl=", "");
                defaultUrl = returnUrl;
                //Current.Request.QueryString[$"ReturnUrl"].ToString();
            }

           return defaultUrl;
        }

        //public List<CPViewModelForIndex> getPersons()
        //{

        //}


        public List<Suffix> GetSuffix()
        {
            List<Suffix> list = new List<Suffix>()
            {
                new Suffix() {ID="Mr.",Name="Mr." },
                new Suffix() {ID="Ms.",Name="Ms." },
                new Suffix() {ID="Mrs.",Name="Mrs." },

            };

            return list;
        }

        public  List<Fee> GetFees()
        {
            List<Fee> list = new List<IC_RC.Fee>()
            {
                new Fee() { ID=25.00,Name="$25.00"},
                new Fee() { ID=50.00,Name="$50.00"},
                new Fee() { ID=75.00,Name="$75.00"},
                new Fee() { ID=10.00,Name="$100.00"},
            };

            return list;
        }

}



    public class Fee
    {
        public double ID { get; set; }
        public string Name{get;set;}
    }
    public class Suffix
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class PaymentType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }


    public class Status
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}