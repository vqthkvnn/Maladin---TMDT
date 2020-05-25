using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;
using Maladin.Areas.API.Models;
namespace Maladin.Areas.API.DAO
{
    public class ProductDAO
    {
        TMDT_Maladin db = null;
        Maladin.Models.EF_MORE.FV_Maladin dbFV = null;
        public ProductDAO()
        {
            db = new TMDT_Maladin();
            dbFV = new Maladin.Models.EF_MORE.FV_Maladin();
        }
        public List<ProductModel> getByNameAndPage(int page, string key)
        {
            string sql = "SELECT  RCR.ID_ACC_PRODUCT as id, RCR.NAME_PRODUCT as name, RCR.SALE_PERCENT as salePercent, RCR.AMOUNT as price,RCR.RATING_PRODUCT as rating,RCR.TotalComment as totalComment, RCR.imagePath as imagePath  " +
                "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY SALE_PERCENT DESC) AS RowNum, " +
                "AP.ID_ACC_PRODUCT, AP.ID_PRODUCT, AP.USER_ACC, AP.AMOUNT, AP.SALE_PERCENT, AP.SALE_MONEY, AP.DATE_START_SELL, AP.DATE_END_SELL,AP.TOTAL_COUNT, AP.SELL_COUNT, AP.IS_SELL, NAME_PRODUCT,RATING_PRODUCT, " +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE AP.ID_ACC_PRODUCT = ID_ACC_PRODUCT) AS TotalComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE AP.ID_PRODUCT = ID_PRODUCT) AS imagePath " +
                "FROM dbo.ACC_PRODUCT AS AP, dbo.PRODUCT  WHERE AP.ID_PRODUCT = PRODUCT.ID_PRODUCT ) AS RCR " +
                "WHERE RCR.NAME_PRODUCT LIKE N'%" + key+
                "%' AND RCR.IS_SELL = 1 AND RowNum >" + Convert.ToString((page-1)*10)+
                " AND RowNum <= " + Convert.ToString(page  * 10) +
                " ORDER BY RowNum ASC ";
            var data = db.Database.SqlQuery<ProductModel>(sql)
                .Select(b => new ProductModel
                {
                    id = b.id,
                    name = b.name,
                    salePercent = b.salePercent,
                    price = b.price,
                    rating = b.rating,
                    totalComment=b.totalComment,
                    imagePath = b.imagePath
                   
                }).ToList();
            return data;
        }
        public List<ProductModel> getFavoriteProduct(string user, int page)
        {
            string sql = "SELECT RS.ID_ACC_PRODUCT as id, RS.NAME_PRODUCT as name, RS.SALE_PERCENT as salePercent, RS.AMOUNT as price, " +
                "RS.RATING_PRODUCT as rating, RS.totalComment as totalComment,RS.imagePath  as imagePath " +
                "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY PD.DATE_PRODUCT) AS RowNum, AP.ID_ACC_PRODUCT, PD.NAME_PRODUCT, AP.SALE_PERCENT, AP.AMOUNT, PD.RATING_PRODUCT," +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE ID_ACC_PRODUCT = FP.ID_ACC_PRODUCT) AS totalComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE AP.ID_PRODUCT = ID_PRODUCT) AS imagePath " +
                "FROM dbo.FAVORITE_PRODUCT AS FP, dbo.ACC_PRODUCT AS AP, dbo.PRODUCT AS PD WHERE AP.ID_ACC_PRODUCT = FP.ID_ACC_PRODUCT " +
                "AND AP.ID_PRODUCT = PD.ID_PRODUCT AND FP.USER_ACC ='"+user+ "') AS RS WHERE RS.RowNum>" +Convert.ToString((page-1)*10)+
                " AND RS.RowNum<=" + Convert.ToString((page) * 10)+
                " ORDER BY RS.RowNum ASC";
            var data = db.Database.SqlQuery<ProductModel>(sql)
                .Select(b => new ProductModel
                {
                    id = b.id,
                    name = b.name,
                    salePercent = b.salePercent,
                    price = b.price,
                    rating = b.rating,
                    totalComment = b.totalComment,
                    imagePath = b.imagePath

                }).ToList();
            return data;
        }
        public List<ProductModel> getWatchedProduct(string user, int page)
        {
            string sql = "SELECT RS.ID_ACC_PRODUCT as id, RS.NAME_PRODUCT as name, RS.SALE_PERCENT as salePercent, RS.AMOUNT as price, " +
                "RS.RATING_PRODUCT as rating, RS.totalComment as totalComment,RS.imagePath  as imagePath " +
                "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY PD.DATE_PRODUCT) AS RowNum, AP.ID_ACC_PRODUCT, PD.NAME_PRODUCT, AP.SALE_PERCENT, AP.AMOUNT, PD.RATING_PRODUCT," +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE ID_ACC_PRODUCT = FP.ID_ACC_PRODUCT) AS totalComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE AP.ID_PRODUCT = ID_PRODUCT) AS imagePath " +
                "FROM dbo.WATCHED_PRODUCT AS FP, dbo.ACC_PRODUCT AS AP, dbo.PRODUCT AS PD WHERE AP.ID_ACC_PRODUCT = FP.ID_ACC_PRODUCT " +
                "AND AP.ID_PRODUCT = PD.ID_PRODUCT AND FP.USER_ACC ='" + user + "') AS RS WHERE RS.RowNum>" + Convert.ToString((page - 1) * 10) +
                " AND RS.RowNum<=" + Convert.ToString((page) * 10) +
                " ORDER BY RS.RowNum ASC";
            var data = db.Database.SqlQuery<ProductModel>(sql)
                .Select(b => new ProductModel
                {
                    id = b.id,
                    name = b.name,
                    salePercent = b.salePercent,
                    price = b.price,
                    rating = b.rating,
                    totalComment = b.totalComment,
                    imagePath = b.imagePath

                }).ToList();
            return data;
        }
        public List<ProductAdsModel> getTopSale()
        {
            string sql = "SELECT TOP 6 ID_ACC_PRODUCT as id, NAME_PRODUCT as name, AMOUNT as price, SALE_PERCENT as saleP," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE ID_PRODUCT = AC.ID_PRODUCT) AS imagePath " +
                "FROM dbo.ACC_PRODUCT AS AC, dbo.PRODUCT AS PD " +
                "WHERE AC.ID_PRODUCT  = PD.ID_PRODUCT AND " +
                "(DATE_END_SELL >=GETDATE() OR DATE_END_SELL IS NULL) AND TOTAL_COUNT-SELL_COUNT>0 ORDER BY SALE_PERCENT DESC";
            var data = db.Database.SqlQuery<ProductAdsModel>(sql)
                .Select(b => new ProductAdsModel
                {
                    id = b.id,
                    name = b.name,
                    price = b.price,
                    saleP = b.saleP,
                    imagePath = b.imagePath
                }).ToList();
            return data;
        }
        public List<ProductAdsModel> getTopSell()
        {
            string sql = "SELECT TOP 6 ID_ACC_PRODUCT as id, NAME_PRODUCT as name, AMOUNT as price, SALE_PERCENT as saleP " +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE ID_PRODUCT = AC.ID_PRODUCT) AS imagePath " +
                "FROM dbo.ACC_PRODUCT AS AC, dbo.PRODUCT AS PD  " +
                "WHERE AC.ID_PRODUCT  = PD.ID_PRODUCT AND " +
                "TOTAL_COUNT-SELL_COUNT>0 ORDER BY SELL_COUNT DESC, DATE_START_SELL DESC";
            var data = db.Database.SqlQuery<ProductAdsModel>(sql)
                .Select(b => new ProductAdsModel
                {
                    id = b.id,
                    name = b.name,
                    price = b.price,
                    saleP = b.saleP,
                    imagePath = b.imagePath
                }).ToList();
            return data;
        }
        public List<ProductAdsModel> getTopNew()
        {
            string sql = "SELECT TOP 6 ID_ACC_PRODUCT as id, NAME_PRODUCT as name, AMOUNT as price, SALE_PERCENT as saleP," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE ID_PRODUCT = AC.ID_PRODUCT) AS imagePath " +
                "FROM dbo.ACC_PRODUCT AS AC, dbo.PRODUCT AS PD  " +
                "WHERE AC.ID_PRODUCT  = PD.ID_PRODUCT AND " +
                "TOTAL_COUNT-SELL_COUNT>0 ORDER BY DATE_START_SELL DESC, SALE_PERCENT DESC";
            var data = db.Database.SqlQuery<ProductAdsModel>(sql)
                .Select(b => new ProductAdsModel
                {
                    id = b.id,
                    name = b.name,
                    price = b.price,
                    saleP = b.saleP,
                    imagePath = b.imagePath
                }).ToList();
            return data;
        }
        public List<ProductAdsModel> getallForYour(int page)
        {
            string sql = "SELECT RS.ID_ACC_PRODUCT as id, RS.NAME_PRODUCT as name, RS.AMOUNT as price, RS.SALE_PERCENT as saleP, RS.imagePath AS imagePath FROM " +
                "(SELECT ROW_NUMBER() OVER (ORDER BY DATE_START_SELL DESC) AS RN,  ID_ACC_PRODUCT, NAME_PRODUCT, AMOUNT, SALE_PERCENT, AC.DATE_START_SELL," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE ID_PRODUCT = AC.ID_PRODUCT) AS imagePath " +
                "FROM dbo.ACC_PRODUCT AS AC, dbo.PRODUCT AS PD  WHERE AC.ID_PRODUCT  = PD.ID_PRODUCT AND " +
                "TOTAL_COUNT-SELL_COUNT>0) RS WHERE RS.RN >" + Convert.ToString((page-1) * 6) +
                " AND RS.RN<=" + Convert.ToString(page * 6);
            var data = db.Database.SqlQuery<ProductAdsModel>(sql)
                .Select(b => new ProductAdsModel
                {
                    id = b.id,
                    name = b.name,
                    price = b.price,
                    saleP = b.saleP,
                    imagePath = b.imagePath
                }).ToList();
            return data;
        }
        public ShopModel getInformationShop(string id)
        {
            var res = db.INFOMATION_ACCOUNT.SingleOrDefault(x => x.USER_ACC == id && x.ID_TYPE_ACC == "CTV");
            if (res == null)
            {
                return null;
            }
            else
            {
                string sql = "SELECT TOP 1 AP.USER_ACC as id, IA.NAME_INFO as name, IA.BIRTH_INFO as dateJoin , IA.AVT_ACC as imagePath," +
                    "(SELECT COUNT(*) FROM dbo.ACC_PRODUCT WHERE USER_ACC = AP.USER_ACC) AS numProduct," +
                    "(SELECT SUM(RATING_PRODUCT) FROM dbo.PRODUCT, dbo.ACC_PRODUCT WHERE ACC_PRODUCT.ID_PRODUCT = PRODUCT.ID_PRODUCT AND ACC_PRODUCT.USER_ACC = AP.USER_ACC) AS totalRating " +
                    "FROM dbo.INFOMATION_ACCOUNT AS IA, dbo.ACC_PRODUCT AP " +
                    "WHERE AP.USER_ACC = IA.USER_ACC AND AP.USER_ACC = '" + id +
                    "' AND ID_TYPE_ACC = 'CTV'";
                var data = db.Database.SqlQuery<ShopModel>(sql)
                    .Select(b => new ShopModel { 
                        id = b.id,
                        name = b.name,
                        dateJoin= b.dateJoin,
                        numProduct = b.numProduct,
                        totalRating = b.totalRating,
                        imagePath = b.imagePath
                    }).SingleOrDefault();
                return data;
            }
        }
        public List<ProductAdsModel> getallOfShop(int page, string id)
        {
            string sql = "SELECT RS.ID_ACC_PRODUCT as id, RS.NAME_PRODUCT as name, RS.AMOUNT as price, RS.SALE_PERCENT as saleP, RS.imagePath AS imagePath FROM " +
                "(SELECT ROW_NUMBER() OVER (ORDER BY DATE_START_SELL DESC) AS RN,  ID_ACC_PRODUCT, NAME_PRODUCT, AMOUNT, SALE_PERCENT, AC.DATE_START_SELL," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE ID_PRODUCT = AC.ID_PRODUCT) AS imagePath " +
                "FROM dbo.ACC_PRODUCT AS AC, dbo.PRODUCT AS PD  WHERE AC.ID_PRODUCT  = PD.ID_PRODUCT AND AC.USER_ACC = '" +id+
                "' and TOTAL_COUNT-SELL_COUNT>0) RS WHERE RS.RN >" + Convert.ToString((page - 1) * 6) +
                " AND RS.RN<=" + Convert.ToString(page * 6);
            var data = db.Database.SqlQuery<ProductAdsModel>(sql)
                .Select(b => new ProductAdsModel
                {
                    id = b.id,
                    name = b.name,
                    price = b.price,
                    saleP = b.saleP,
                    imagePath = b.imagePath
                }).ToList();
            return data;
        }
        public List<Tuple<string, string>> getAllAttrOfProduct(string id)
        {
            var value = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == id);
            List<Tuple<string, string>> all = new List<Tuple<string, string>>();
            var res = db.PRODUCT_ATT.Where(x => x.ID_PRODUCT == value.ID_PRODUCT).ToList();
            if (res.Count() ==0)
            {
                return new List<Tuple<string, string>>();
            }
            else
            {
                foreach(var i in res)
                {
                    all.Add(new Tuple<string, string>(i.KEY_ATT, i.VALUE_ATT));
                }
                return all;
            }
            
        }
        public ProductDetailModel getInformationOfProduct(string id)
        {
            string sql = "SELECT AP.ID_ACC_PRODUCT as ID, PD.NAME_PRODUCT as name,AP.AMOUNT as priceGoc, AP.SALE_PERCENT as saleP, PD.RATING_PRODUCT as ratting,PD.DESCRIBE_PRODUCT as contentProduct," +
                " (SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE ID_ACC_PRODUCT = AP.ID_ACC_PRODUCT) AS totalComment," +
                "(SELECT NAME_INFO FROM dbo.INFOMATION_ACCOUNT WHERE AP.USER_ACC =USER_ACC AND ID_TYPE_ACC = 'CTV') AS NameCTV " +
                "  FROM dbo.ACC_PRODUCT AS AP, dbo.PRODUCT AS PD WHERE AP.ID_PRODUCT = PD.ID_PRODUCT AND AP.ID_ACC_PRODUCT='" + id + "'";
            var data = db.Database.SqlQuery<ProductDetailModel>(sql)
                .Select(b => new ProductDetailModel {
                    ID = b.ID,
                    contentProduct= b.contentProduct,
                    name = b.name,
                    NameCTV = b.NameCTV,
                    priceGoc = b.priceGoc,
                    ratting = b.ratting,
                    saleP = b.saleP,
                    totalComment=b.totalComment
                }).SingleOrDefault();
            return data;
        }
        public int AddToCart(string user, string idproduct)
        {
            try
            {
                var res = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == idproduct);
                var account = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user && x.IS_ACTIVE_ACC == true);
                if (account == null)
                {
                    return -2; // tai khoan k ton tai
                }
                if (res == null)
                {
                    return 0; // san pham k ton tai
                }
                var check = db.WATCHED_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == idproduct &&
                x.USER_ACC == user);
                if (check == null)
                {
                    WATCHED_PRODUCT wATCHED_PRODUCT = new WATCHED_PRODUCT();
                    wATCHED_PRODUCT.ID_ACC_PRODUCT = idproduct;
                    wATCHED_PRODUCT.USER_ACC = user;
                    wATCHED_PRODUCT.CART_COUNT = 1;
                    db.WATCHED_PRODUCT.Add(wATCHED_PRODUCT);
                    db.SaveChanges();
                    return 1; // them thanh cong
                }
                else
                {
                    if (check.CART_COUNT > 0)
                    {
                        return -1; // San pham da co
                    }
                    else
                    {
                        db.WATCHED_PRODUCT.Attach(check);
                        check.CART_COUNT = 1;
                        db.SaveChanges();
                        return 1; // them thanh cong
                    }
                }
            }
            catch (Exception e)
            {
                return -3; // loi khacs
            }
        }
        public List<string> getAllImageOfProduct(string id)
        {
            var value = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == id);

            List<string> data = new List<string>();
            var res =  db.PRODUCT_IMAGE.Where(x => x.ID_PRODUCT == value.ID_PRODUCT).ToList();
            if (res == null)
            {
                return new List<string>();
            }
            else
            {
                foreach(var i in res)
                {
                    data.Add(i.IMAGE_PATH);
                }
                return data;
            }
        }
        public List<Tuple<string,string,string>> allQuestionForProduct(string id)
        {
            List<Tuple<string, string, string>> all = new List<Tuple<string, string, string>>();
            var res = db.GUEST_QUESTION.Where(x => x.ID_ACC_PRODUCT == id).Take(6).ToList();
            foreach(var i in res)
            {
                all.Add(new Tuple<string, string, string>(i.TITLE_QUESTION, i.CONTENT_QUESTION, i.DATE_QUESTION.Value.ToString("dd-MM-yyyy")));
            }
            return all;
        }
        public bool AddToFavorite(string id, string user)
        {
            try
            {
                Maladin.Models.EF_MORE.FAVORITE_PRODUCT fAVORITE_ = new Maladin.Models.EF_MORE.FAVORITE_PRODUCT();
                var res = dbFV.FAVORITE_PRODUCT.Where(x => x.ID_ACC_PRODUCT == id && x.USER_ACC == user).Count();
                if(res>0)
                {
                    return true;
                }
                else
                {
                    fAVORITE_.ID_ACC_PRODUCT = id;
                    fAVORITE_.USER_ACC = user;
                    dbFV.FAVORITE_PRODUCT.Add(fAVORITE_);
                    dbFV.SaveChanges();
                    return true;
                }
                
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool AddToWatched(string id, string user)
        {
            try
            {
                var res = db.WATCHED_PRODUCT.Where(x => x.ID_ACC_PRODUCT == id && x.USER_ACC == user).Count();
                if (res>0)
                {
                    return true;
                }
                else
                {
                    WATCHED_PRODUCT wATCHED_PRODUCT = new WATCHED_PRODUCT();
                    wATCHED_PRODUCT.ID_ACC_PRODUCT = id;
                    wATCHED_PRODUCT.USER_ACC = user;
                    wATCHED_PRODUCT.CART_COUNT = 0;
                    db.WATCHED_PRODUCT.Add(wATCHED_PRODUCT);
                    db.SaveChanges();
                    return true;
                }
                
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public int CountCart(string user)
        {
            return db.WATCHED_PRODUCT.Where(x => x.USER_ACC == user && x.CART_COUNT > 0).Count();
        }
        public bool DeleteFromCart(string user, string id)
        {
            var res = db.WATCHED_PRODUCT.SingleOrDefault(x => x.USER_ACC == user && x.ID_ACC_PRODUCT == id);
            if(res == null)
            {
                return false;
            }
            else
            {
                db.WATCHED_PRODUCT.Attach(res);
                res.CART_COUNT = 0;
                db.SaveChanges();
                return true;
            }
        }
        public bool UpdateCart(string user, string id, int count)
        {
            var res = db.WATCHED_PRODUCT.SingleOrDefault(x => x.USER_ACC == user && x.ID_ACC_PRODUCT == id);
            if (res == null)
            {
                return false;
            }
            else
            {
                db.WATCHED_PRODUCT.Attach(res);
                res.CART_COUNT = count;
                db.SaveChanges();
                return true;
            }
        }
        public int GetPriceFromCart(string user)
        {
            int count = 0;
            var res = db.WATCHED_PRODUCT.Where(x => x.USER_ACC == user && x.CART_COUNT > 0).ToList();
            foreach(var i in res)
            {
                var pr = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == i.ID_ACC_PRODUCT);
                count += (i.CART_COUNT ?? 0) * (pr.AMOUNT - pr.SALE_PERCENT * pr.AMOUNT / 100);
            }
            return count;
        }
        public Tuple<string, string, string, string, string> getInformationOfShop(string name)
        {
            var res = db.INFOMATION_ACCOUNT.SingleOrDefault(x => x.NAME_INFO == name && x.ID_TYPE_ACC == "CTV");
            var sql = "SELECT SUM(RS.NumProduct) AS totalStart FROM (SELECT ID_ACC_PRODUCT, SUM(RATING_PRODUCT) AS NumProduct FROM dbo.ACC_PRODUCT, dbo.PRODUCT " +
                "WHERE PRODUCT.ID_PRODUCT = ACC_PRODUCT.ID_PRODUCT AND ACC_PRODUCT.USER_ACC = 'banhang' GROUP BY(ID_ACC_PRODUCT)) AS RS";
            var count = db.ACC_PRODUCT.Where(x => x.USER_ACC == res.USER_ACC).Count();
            var total = db.Database.SqlQuery<String>(sql).ToArray();
            return new Tuple<string, string, string, string, string>(res.NAME_INFO, res.BIRTH_INFO.Value.ToString("dd-MM-yyyy"), Convert.ToString(count / Convert.ToInt32(total)),
                res.AVT_ACC, Convert.ToString(count));
        }
        public string GetIDShopOfIdp(string idp)
        {
            return db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == idp).USER_ACC;
        }
        public List<ProductAdsModel> getProductOfShop(string id, int page)
        {
            string sql = "SELECT RS.ID_ACC_PRODUCT as id, RS.NAME_PRODUCT as name, RS.AMOUNT as price, RS.SALE_PERCENT as saleP, RS.imagePath AS imagePath FROM " +
                "(SELECT ROW_NUMBER() OVER (ORDER BY DATE_START_SELL DESC) AS RN,  ID_ACC_PRODUCT, NAME_PRODUCT, AMOUNT, SALE_PERCENT, AC.DATE_START_SELL," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE ID_PRODUCT = AC.ID_PRODUCT) AS imagePath " +
                "FROM dbo.ACC_PRODUCT AS AC, dbo.PRODUCT AS PD  WHERE AC.ID_PRODUCT  = PD.ID_PRODUCT AND " +
                "TOTAL_COUNT-SELL_COUNT>0 and AC.USER_ACC ='" +id+"')"+
                ") RS WHERE RS.RN >" + Convert.ToString((page - 1) * 6) +
                " AND RS.RN<=" + Convert.ToString(page * 6);
            var data = db.Database.SqlQuery<ProductAdsModel>(sql)
                .Select(b => new ProductAdsModel
                {
                    id = b.id,
                    name = b.name,
                    price = b.price,
                    saleP = b.saleP,
                    imagePath = b.imagePath
                }).ToList();
            return data;
        }
        public List<Tuple<string, string, DateTime, string, int>> getAllComment(string idp)
        {
            List<Tuple<string, string, DateTime, string, int>> all = new List<Tuple<string, string, DateTime, string, int>>();
            var res = db.ACCOUNT_COMMENT.Where(x => x.ID_ACC_PRODUCT == idp).ToList();
            foreach(var i in res)
            {
                string scc = i.USER_ACC;
                var name = db.INFOMATION_ACCOUNT.SingleOrDefault(x => x.USER_ACC == scc && x.ID_TYPE_ACC == "CT");
                if (name == null)
                {
                    all.Add(new Tuple<string, string, DateTime, string, int>(i.TITLE_COMMENT, scc, i.DATE_COMMENT, i.CONTEN_COMMENT, (i.RATING_COMMENT ?? 1)));
                }
                else
                { all.Add(new Tuple<string, string, DateTime, string, int>(i.TITLE_COMMENT, name.NAME_INFO, i.DATE_COMMENT, i.CONTEN_COMMENT, (i.RATING_COMMENT ?? 1))); }
            }
            return all;
        }
    }
}