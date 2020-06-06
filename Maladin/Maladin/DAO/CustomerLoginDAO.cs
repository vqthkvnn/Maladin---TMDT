using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;
using Maladin.Models;
using Maladin.Areas.API.Models;

namespace Maladin.DAO
{
    public class CustomerLoginDAO
    {
        TMDT_Maladin db = null;
        Maladin.Models.EF_MORE.FV_Maladin dbFV = null;
        public CustomerLoginDAO()
        {
            db = new TMDT_Maladin();
            dbFV = new Maladin.Models.EF_MORE.FV_Maladin();
        }
        public int Login(string user, string password)
        {
            var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
            if (res != null)
            {
                if (res.PASSWORD_ACC == password)
                {
                    if (res.IS_ACTIVE_ACC == false)
                    {
                        return 2;
                    }
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
        public string getNameUser(string user)
        {
            return db.INFOMATION_ACCOUNT.Where(x => x.USER_ACC == user).Where(x => x.ID_TYPE_ACC == "CT").SingleOrDefault().NAME_INFO;
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
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT, dbo.PRODUCT_IMAGE WHERE PRODUCT.ID_PRODUCT = PD.ID_PRODUCT AND PRODUCT.ID_PRODUCT = PRODUCT_IMAGE.ID_PRODUCT) AS pathImg, AP.USER_ACC as UserBy " +
                "FROM dbo.ACC_PRODUCT as AP, dbo.WATCHED_PRODUCT, dbo.PRODUCT AS PD WHERE AP.ID_ACC_PRODUCT = WATCHED_PRODUCT.ID_ACC_PRODUCT " +
                "AND PD.ID_PRODUCT = AP.ID_PRODUCT AND WATCHED_PRODUCT.USER_ACC = '" + user+ "' and CART_COUNT>0";
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
        public int getCoin(string user)
        {
            return (db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user).COINT_ACC ?? 0 );
        }
        public IPagedList<FavoriteModel> getAllFVR(string user, int page)
        {
            string sql = "SELECT ACC_PRODUCT.ID_ACC_PRODUCT as ID , NAME_PRODUCT as NameProduct, AMOUNT as PriceGoc, SALE_PERCENT as SalePricent, SALE_MONEY as SaleMoney, PD.DESCRIBE_PRODUCT as ContentP," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE where ID_PRODUCT = PD.ID_PRODUCT) AS PathIamge " +
                " FROM dbo.FAVORITE_PRODUCT, dbo.PRODUCT AS PD, dbo.ACC_PRODUCT " +
                "WHERE ACC_PRODUCT.ID_ACC_PRODUCT = FAVORITE_PRODUCT.ID_ACC_PRODUCT AND ACC_PRODUCT.ID_PRODUCT = PD.ID_PRODUCT " +
                "AND FAVORITE_PRODUCT.USER_ACC = '" + user + "'";
            var data = db.Database.SqlQuery<FavoriteModel>(sql)
                .Select(b => new FavoriteModel
                {
                    ID = b.ID,
                    NameProduct = b.NameProduct,
                    PathIamge = b.PathIamge,
                    PriceGoc = b.PriceGoc,
                    SaleMoney = b.SaleMoney,
                    SalePricent = b.SalePricent,
                    ContentP = b.ContentP
                }).ToList().ToPagedList(page, 10);
            return data;
        }
        public bool RemoveFavorite(string user, string id)
        {
            try
            {
                var res = dbFV.FAVORITE_PRODUCT.SingleOrDefault(x => x.USER_ACC == user && x.ID_ACC_PRODUCT == id);
                dbFV.FAVORITE_PRODUCT.Attach(res);
                dbFV.FAVORITE_PRODUCT.Remove(res);
                dbFV.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public IPagedList<FavoriteModel> getAllWatched(string user, int page)
        {
            string sql = "SELECT ACC_PRODUCT.ID_ACC_PRODUCT as ID , NAME_PRODUCT as NameProduct, AMOUNT as PriceGoc, SALE_PERCENT as SalePricent, SALE_MONEY as SaleMoney, PD.DESCRIBE_PRODUCT as ContentP," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE where ID_PRODUCT = PD.ID_PRODUCT) AS PathIamge " +
                " FROM dbo.WATCHED_PRODUCT, dbo.PRODUCT AS PD, dbo.ACC_PRODUCT " +
                "WHERE ACC_PRODUCT.ID_ACC_PRODUCT = WATCHED_PRODUCT.ID_ACC_PRODUCT AND ACC_PRODUCT.ID_PRODUCT = PD.ID_PRODUCT " +
                "AND WATCHED_PRODUCT.USER_ACC = '" + user + "'";
            var data = db.Database.SqlQuery<FavoriteModel>(sql)
                .Select(b => new FavoriteModel
                {
                    ID = b.ID,
                    NameProduct = b.NameProduct,
                    PathIamge = b.PathIamge,
                    PriceGoc = b.PriceGoc,
                    SaleMoney = b.SaleMoney,
                    SalePricent = b.SalePricent,
                    ContentP = b.ContentP
                }).ToList().ToPagedList(page, 10);
            return data;
        }
        public IPagedList<OrderModel> getAllOrder(string user, int page)
        {
            string sql = "SELECT ID_ODER as ID, DATE_ODER as DateOrder, ID_TYPE_ODER as TypeOrder, SUM_PRICE_ODER as SumPrice, STATUS_ODER as StatusOrder FROM dbo.ODER WHERE USER_ACC = '" +user+
                "'";
            var data = db.Database.SqlQuery<OrderModel>(sql)
                .Select(b => new OrderModel {
                ID = b.ID,
                DateOrder = b.DateOrder, 
                StatusOrder = b.StatusOrder,
                SumPrice = b.SumPrice,
                TypeOrder = b.TypeOrder
                }).ToList().ToPagedList(page, 10);
            return data;
        }
        public int CountListOrder(string user)
        {
            return db.ODERs.Where(x => x.USER_ACC == user).Count();
        }
        public int CountListWatch(string user)
        {
            return db.WATCHED_PRODUCT.Where(x => x.USER_ACC == user).Count();
        }
        public int CountListFVR(string user)
        {
            return dbFV.FAVORITE_PRODUCT.Where(x => x.USER_ACC == user).Count();
        }
        public bool CancelOrder(string user, string ido)
        {
            try
            {
                var res = db.ODERs.SingleOrDefault(x => x.ID_ODER == ido);
                db.ODERs.Attach(res);
                res.STATUS_ODER = -1;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public OrderDetailModel GetOrderDetailModel(string ido)
        {
            string sql = "SELECT ID_ODER as IDOrder, DATE_ODER as DateOrder, STATUS_ODER as status, AC.ID_ACC_PRODUCT as IDProduct, NAME_INFO as NameGuest, " +
                "ADRESS_INFO as AdressGuest, PHONE_INFO as PhoneGuest, ID_TYPE_ODER as TypeOrder, NAME_PRODUCT as NameProduct, SUM_PRICE_ODER as SumPrice," +
                "COUNT_ODER as CountOrder, AC.USER_ACC as UserSell, CAST(AMOUNT*COUNT_ODER*(100-SALE_PERCENT)/100 AS INT) AS SalePrice," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE AC.ID_PRODUCT = ID_PRODUCT) AS ImagePath  " +
                "FROM dbo.ACC_PRODUCT AS AC, dbo.INFOMATION_ACCOUNT, dbo.PRODUCT,dbo.ODER  " +
                "WHERE AC.ID_ACC_PRODUCT = ODER.ID_ACC_PRODUCT AND AC.ID_PRODUCT = PRODUCT.ID_PRODUCT " +
                " AND ID_TYPE_ACC = 'CT' AND ID_ODER = '" +ido+
                "' AND INFOMATION_ACCOUNT.USER_ACC = ODER.USER_ACC";
            var data = db.Database.SqlQuery<OrderDetailModel>(sql)
                .Select(b => new OrderDetailModel {
                IDOrder = b.IDOrder,
                AdressGuest =b.AdressGuest,
                CountOrder =b.CountOrder,
                DateOrder = b.DateOrder,
                IDProduct = b.IDProduct,
                NameGuest = b.NameGuest,
                NameProduct = b.NameProduct,
                PhoneGuest = b.PhoneGuest,
                SalePrice = b.SalePrice,
                status = b.status,
                SumPrice = b.SumPrice,
                TypeOrder = b.TypeOrder,
                UserSell = b.UserSell,
                ImagePath = b.ImagePath
                }).SingleOrDefault();
            return data;
        }
        public AccountModel getinfo(string user)
        {
            var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);

            string sql = "SELECT ID_INFO AS id, NAME_INFO AS name, EMAIL_INFO AS email, INFOMATION_ACCOUNT.ID_TYPE_ACC AS idType, COINT_ACC AS coint, " +
                "DATE_CREATE_ACC AS dateCreate,  AVT_ACC AS avt, CMND_INFO AS cmnd, BIRTH_INFO AS birth, SEX_INFO AS gt, ADRESS_INFO AS adr, " +
                "PHONE_INFO AS phone, NOTE_INFO AS note " +
                "FROM dbo.ACCOUNT, dbo.INFOMATION_ACCOUNT " +
                "WHERE ACCOUNT.USER_ACC = INFOMATION_ACCOUNT.USER_ACC AND ACCOUNT.USER_ACC ='" + user +
                "' AND INFOMATION_ACCOUNT.ID_TYPE_ACC ='" + "CT" +
                "'";
            var data = db.Database.SqlQuery<AccountModel>(sql)
                .Select(b => new AccountModel
                {
                    id = b.id,
                    name = b.name,
                    email = b.email,
                    idType = b.idType,
                    dateCreate = b.dateCreate,
                    coint = b.coint,
                    cmnd = b.cmnd,
                    birth = b.birth,
                    adr = b.adr,
                    avt = b.avt,
                    gt = b.gt,
                    note = b.note,
                    phone = b.phone
                });
            if (data == null)
            {
                return new AccountModel();
            }
            return data.FirstOrDefault();
        }

    }
}