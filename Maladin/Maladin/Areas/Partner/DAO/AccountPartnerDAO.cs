using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;
namespace Maladin.Areas.Partner.DAO
{
    public class AccountPartnerDAO
    {
        TMDT_Maladin dbContext = null;
        public AccountPartnerDAO()
        {
            dbContext = new TMDT_Maladin();
        }
        public string Insert(ACCOUNT entity)
        {
            try
            {
                dbContext.ACCOUNTs.Add(entity);
                dbContext.SaveChanges();
                return entity.USER_ACC;
            }
            catch(Exception e)
            {
                return "error";
            }
        }
        public ACCOUNT GetByNameAccount(string user_name)
        {
            return dbContext.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user_name);
        }
        public int CheckLogin(string user_name, string password)
        {
            var res = dbContext.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user_name);
            if (res == null)
            {
                return 0; // tài khoản không tồn tại
            }
            else
            {
                if (res.IS_ACTIVE_ACC == false)
                {
                    return -1; // tài khoản chưa được kích hoat
                }
                else
                {
                    if (res.PASSWORD_ACC == password)
                    {
                        if (res.ID_TYPE_ACC == "CTV")
                        {
                            return 1;
                        }
                        else
                        {
                            return -2; // tai khoan k duoc cap quyen
                        }
                    }
                    else
                    {
                        return -3; //sai pass
                    }
                    
                }
            }
        }
    }
}