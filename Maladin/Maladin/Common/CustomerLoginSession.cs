using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Common
{
    public class CustomerLoginSession
    {
        public static string CUSTOMER_SESSION = "CUSTOMER_SESSION";
        public string CUSTOMER_USER { get; set; }
    }
}