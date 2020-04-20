using Maladin.Areas.Admin.Models;
using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.Admin.DAO
{
    public class AccountAdminDAO
    {
        TMDT_Maladin dbContext = null;
        public AccountAdminDAO()
        {
            dbContext = new TMDT_Maladin();

        }
        public bool Insert(ACCOUNT entity)
        {
            try
            {
                dbContext.ACCOUNTs.Add(entity);
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        
        public INFOMATION_ACCOUNT getInfoByAcc(string user)
        {
            return dbContext.INFOMATION_ACCOUNT.SingleOrDefault(x => x.USER_ACC == user);
        }
        public bool Delete(string user)
        {
            try
            {
                var informationAcc = getInfoByAcc(user);
                dbContext.INFOMATION_ACCOUNT.Attach(informationAcc);
                informationAcc.USER_ACC = "null";
                var product = dbContext.ACCOUNTs.Find(user);
                dbContext.ACCOUNTs.Remove(product);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        public ACCOUNT GetByNameAccount(string user)
        {
            return dbContext.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
        }
        public int CheckLogin(LoginAdminModel account)
        {
            var res = dbContext.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == account.UserAdmin);
            
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
                    if (res.PASSWORD_ACC == account.PasswordAdmin)
                    {
                        if (res.ID_TYPE_ACC == "ADMIN")
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