using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IC_RC
{
    public class ShrdMaster
    {

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
    }



    public class PaymentType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}