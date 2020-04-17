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
        public List<ACCOUNT> getAllAccounts()
        {
            return dbContext.ACCOUNTs.ToList<ACCOUNT>();
        }
        public INFOMATION_ACCOUNT getInfomationByAccount(string account)
        {
            return dbContext.INFOMATION_ACCOUNT.Where(x => x.USER_ACC == account).FirstOrDefault();
        }
        public bool Update(ACCOUNT entity)
        {
            try
            {
                var product = GetByNameAccount(entity.USER_ACC);
                dbContext.ACCOUNTs.Attach(product);
                product.PASSWORD_ACC = entity.PASSWORD_ACC;
                product.TYPE_ACCOUNT = entity.TYPE_ACCOUNT;
                product.IS_ACTIVE_ACC = entity.IS_ACTIVE_ACC;
                product.NOTI_ACC = entity.NOTI_ACC;
                product.COINT_ACC = entity.COINT_ACC;
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool Delete(string user)
        {
            try
            {
                var informationAcc = getInfomationByAccount(user);
                dbContext.INFOMATION_ACCOUNT.Attach(informationAcc);
                informationAcc.USER_ACC = "null";
                var product = GetByNameAccount(user);
                dbContext.ACCOUNTs.Remove(product);
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                
                return false;
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