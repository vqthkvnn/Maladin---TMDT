using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.DAO
{
    public class CustomerLoginDAO
    {
        TMDT_Maladin db = null;
        public CustomerLoginDAO()
        {
            db = new TMDT_Maladin();
        }
        public int Login(string user, string password)
        {
            var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
            if (res != null)
            {
                if (res.PASSWORD_ACC == password)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        public bool Create(ACCOUNT entity)
        {
            try
            {
                db.ACCOUNTs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}