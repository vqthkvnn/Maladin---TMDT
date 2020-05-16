using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.Models;
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
        public int Register(string user, string email)
        {
            var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
            if (res != null)
            {
                return -1; // đã tồn tại user
            }
            else
            {
                var emailres = db.ACCOUNTs.SingleOrDefault(x => x.EMAIL_INFO == email);
                if (emailres!=null)
                {
                    return -2; // da ton ta email
                }
                else
                {
                    return 0;
                }
                    
            }
        }
        public bool Create(ACCOUNT entity)
        {
            try
            {
                db.ACCOUNTs.Add(entity);
                db.SaveChanges();
                /*
                 * create infomation
                 */
                INFOMATION_ACCOUNT iNFOMATION_ACCOUNT = new INFOMATION_ACCOUNT();
                iNFOMATION_ACCOUNT.USER_ACC = entity.USER_ACC;
                iNFOMATION_ACCOUNT.ID_TYPE_ACC = "CT";
                iNFOMATION_ACCOUNT.ID_INFO = "CT" + entity.USER_ACC.Substring(0, 2) + 
                    Convert.ToString(db.INFOMATION_ACCOUNT.Count()+1);
                iNFOMATION_ACCOUNT.NAME_INFO = entity.USER_ACC;
                iNFOMATION_ACCOUNT.CMND_INFO = "null";
                iNFOMATION_ACCOUNT.AVT_ACC = "/public/image/avt/avtdefult.png";
                db.INFOMATION_ACCOUNT.Add(iNFOMATION_ACCOUNT);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public string getNameUser(string user, string type)
        {
            return db.INFOMATION_ACCOUNT.Where(x => x.USER_ACC == user).Where(x => x.ID_TYPE_ACC == type).SingleOrDefault().NAME_INFO;
        }
        public string getTypeUser(string user)
        {
            return db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user).ID_TYPE_ACC;
        }
        public INFOMATION_ACCOUNT getInformationByUser(string user)
        {
            return db.INFOMATION_ACCOUNT.SingleOrDefault(x => x.USER_ACC == user && x.ID_TYPE_ACC == "CT");
        }
        public ACCOUNT getAccountByUser(string user)
        {
            return db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
        }
        public string getTypeMax(string user)
        {
            return db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user).ID_TYPE_ACC;
        }
        public int getTotalCart(string user)
        {
            return db.WATCHED_PRODUCT.Where(x => x.USER_ACC == user).Where(x => x.CART_COUNT > 0).Count();
        }
        public List<CartProductModel> getAllCart(string user)
        {
            string sql = "SELECT AP.ID_ACC_PRODUCT as ID, NAME_PRODUCT as Name, AMOUNT as Price, " +
                "SALE_PERCENT as saleP, " +
                "SALE_MONEY as saleM, CART_COUNT as TotalCount," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT, dbo.PRODUCT_IMAGE) AS pathImg, AP.USER_ACC as UserBy " +
                "FROM dbo.ACC_PRODUCT as AP, dbo.WATCHED_PRODUCT, dbo.PRODUCT WHERE AP.ID_ACC_PRODUCT = WATCHED_PRODUCT.ID_ACC_PRODUCT " +
                "AND PRODUCT.ID_PRODUCT = AP.ID_PRODUCT AND WATCHED_PRODUCT.USER_ACC = '"+user+ "' and CART_COUNT>0";
            var data = db.Database.SqlQuery<CartProductModel>(sql)
                .Select(b => new CartProductModel
                {
                    ID = b.ID,
                    Name = b.Name,
                    Price = b.Price,
                    saleP = b.saleP,
                    saleM = b.saleM,
                    TotalCount = b.TotalCount,
                    pathImg = b.pathImg,
                    UserBy = b.UserBy
                }).ToList();
            return data;
        }
    }
}