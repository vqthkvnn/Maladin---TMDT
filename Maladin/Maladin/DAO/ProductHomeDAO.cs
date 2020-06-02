using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;
using Maladin.Models;
namespace Maladin.DAO
{
    public class ProductHomeDAO
    {
        TMDT_Maladin dbContext = null;
        Maladin.Models.EF_MORE.FV_Maladin dbFV = null;
        public ProductHomeDAO()
        {
            dbContext = new TMDT_Maladin();
            dbFV = new Models.EF_MORE.FV_Maladin();
        }
        public List<ItemProductModel> getListProduct(int page, string idType)
        {
            string sql = "SELECT TOP 5 ID_ACC_PRODUCT AS ID, NAME_PRODUCT as NameProduct, AMOUNT as PriceGoc, SALE_PERCENT as SalePricent, " +
                "SALE_MONEY as SaleMoney, RATING_PRODUCT as Rating," +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE AP.ID_ACC_PRODUCT = ID_ACC_PRODUCT) AS CountComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE P.ID_PRODUCT = ID_PRODUCT) AS PathIamge  " +
                "FROM dbo.PRODUCT AS P, dbo.ACC_PRODUCT AS AP, dbo.TYPE_PRODUCT  " +
                "WHERE AP.ID_PRODUCT = P.ID_PRODUCT AND P.ID_TYPE_PRODUCT = TYPE_PRODUCT.ID_TYPE_PRODUCT AND TYPE_PRODUCT.ID_TYPE_PRODUCT ='" +
                idType+"'";
            var data = dbContext.Database.SqlQuery<ItemProductModel>(sql)
                .Select(b => new ItemProductModel
                {
                    ID = b.ID,
                    NameProduct = b.NameProduct,
                    PriceGoc = b.PriceGoc,
                    SalePricent = b.SalePricent,
                    SaleMoney = b.SaleMoney,
                    Rating = b.Rating,
                    CountComment = b.CountComment,
                    PathIamge = b.PathIamge
                }).ToList();
            return data;
        }
        public List<ItemProductModel> searchBy(string q, string type, int page)
        {
            string sql = "";
            if (type == "NEW")
            {
                sql = "SELECT PR.ID as ID, PR.NameProduct as NameProduct, PR.PriceGoc as PriceGoc, PR.SalePricent as SalePricent, " +
                "PR.SaleMoney as SaleMoney, PR.Rating as Rating, PR.CountComment as CountComment, PR.PathIamge as PathIamge " +
                "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY AP.DATE_START_SELL DESC) AS RowNum, ID_ACC_PRODUCT AS ID, NAME_PRODUCT as NameProduct, AP.AMOUNT as PriceGoc, SALE_PERCENT AS SalePricent, SALE_MONEY as SaleMoney, RATING_PRODUCT as Rating," +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE AP.ID_ACC_PRODUCT = ID_ACC_PRODUCT) AS CountComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE P.ID_PRODUCT = ID_PRODUCT) AS PathIamge, AP.DATE_START_SELL, AP.SELL_COUNT " +
                "FROM dbo.PRODUCT AS P, dbo.ACC_PRODUCT AS AP WHERE AP.ID_PRODUCT = P.ID_PRODUCT AND P.NAME_PRODUCT LIKE N'%" + q +
                "%') AS PR WHERE PR.RowNum>" + Convert.ToString(16 * (page - 1)) +
                " AND PR.RowNum<=" + Convert.ToString(16 * page) +
                " ORDER BY PR.RowNum ASC";
            }
            else if (type == "MAXSELL")
            {
                sql = "SELECT PR.ID as ID, PR.NameProduct as NameProduct, PR.PriceGoc as PriceGoc, PR.SalePricent as SalePricent, " +
                "PR.SaleMoney as SaleMoney, PR.Rating as Rating, PR.CountComment as CountComment, PR.PathIamge as PathIamge " +
                "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY AP.SELL_COUNT DESC) AS RowNum, ID_ACC_PRODUCT AS ID, NAME_PRODUCT as NameProduct, AP.AMOUNT as PriceGoc, SALE_PERCENT AS SalePricent, SALE_MONEY as SaleMoney, RATING_PRODUCT as Rating," +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE AP.ID_ACC_PRODUCT = ID_ACC_PRODUCT) AS CountComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE P.ID_PRODUCT = ID_PRODUCT) AS PathIamge, AP.DATE_START_SELL, AP.SELL_COUNT " +
                "FROM dbo.PRODUCT AS P, dbo.ACC_PRODUCT AS AP WHERE AP.ID_PRODUCT = P.ID_PRODUCT AND P.NAME_PRODUCT LIKE N'%" + q +
                "%') AS PR WHERE PR.RowNum>" + Convert.ToString(16 * (page - 1)) +
                " AND PR.RowNum<=" + Convert.ToString(16 * page) +
                " ORDER BY PR.RowNum ASC";
            }
            else if (type == "MAXSALE")
            {
                sql = "SELECT PR.ID as ID, PR.NameProduct as NameProduct, PR.PriceGoc as PriceGoc, PR.SalePricent as SalePricent, " +
                "PR.SaleMoney as SaleMoney, PR.Rating as Rating, PR.CountComment as CountComment, PR.PathIamge as PathIamge " +
                "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY AP.SALE_PERCENT DESC) AS RowNum, ID_ACC_PRODUCT AS ID, NAME_PRODUCT as NameProduct, AP.AMOUNT as PriceGoc, SALE_PERCENT AS SalePricent, SALE_MONEY as SaleMoney, RATING_PRODUCT as Rating," +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE AP.ID_ACC_PRODUCT = ID_ACC_PRODUCT) AS CountComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE P.ID_PRODUCT = ID_PRODUCT) AS PathIamge, AP.DATE_START_SELL, AP.SELL_COUNT " +
                "FROM dbo.PRODUCT AS P, dbo.ACC_PRODUCT AS AP WHERE AP.ID_PRODUCT = P.ID_PRODUCT AND P.NAME_PRODUCT LIKE N'%" + q +
                "%') AS PR WHERE PR.RowNum>" + Convert.ToString(16 * (page - 1)) +
                " AND PR.RowNum<=" + Convert.ToString(16 * page) +
                " ORDER BY PR.RowNum ASC";
            }
            else if (type == "MINP")
            {
                sql = "SELECT PR.ID as ID, PR.NameProduct as NameProduct, PR.PriceGoc as PriceGoc, PR.SalePricent as SalePricent, " +
                "PR.SaleMoney as SaleMoney, PR.Rating as Rating, PR.CountComment as CountComment, PR.PathIamge as PathIamge " +
                "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY AP.AMOUNT ASC) AS RowNum, ID_ACC_PRODUCT AS ID, NAME_PRODUCT as NameProduct, AP.AMOUNT as PriceGoc, SALE_PERCENT AS SalePricent, SALE_MONEY as SaleMoney, RATING_PRODUCT as Rating," +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE AP.ID_ACC_PRODUCT = ID_ACC_PRODUCT) AS CountComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE P.ID_PRODUCT = ID_PRODUCT) AS PathIamge, AP.DATE_START_SELL, AP.SELL_COUNT " +
                "FROM dbo.PRODUCT AS P, dbo.ACC_PRODUCT AS AP WHERE AP.ID_PRODUCT = P.ID_PRODUCT AND P.NAME_PRODUCT LIKE N'%" + q +
                "%') AS PR WHERE PR.RowNum>" + Convert.ToString(16 * (page - 1)) +
                " AND PR.RowNum<=" + Convert.ToString(16 * page) +
                " ORDER BY PR.RowNum ASC";
            }
            else if (type == "MAXP")
            {
                sql = "SELECT PR.ID as ID, PR.NameProduct as NameProduct, PR.PriceGoc as PriceGoc, PR.SalePricent as SalePricent, " +
                "PR.SaleMoney as SaleMoney, PR.Rating as Rating, PR.CountComment as CountComment, PR.PathIamge as PathIamge " +
                "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY AP.AMOUNT DESC) AS RowNum, ID_ACC_PRODUCT AS ID, NAME_PRODUCT as NameProduct, AP.AMOUNT as PriceGoc, SALE_PERCENT AS SalePricent, SALE_MONEY as SaleMoney, RATING_PRODUCT as Rating," +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE AP.ID_ACC_PRODUCT = ID_ACC_PRODUCT) AS CountComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE P.ID_PRODUCT = ID_PRODUCT) AS PathIamge, AP.DATE_START_SELL, AP.SELL_COUNT " +
                "FROM dbo.PRODUCT AS P, dbo.ACC_PRODUCT AS AP WHERE AP.ID_PRODUCT = P.ID_PRODUCT AND P.NAME_PRODUCT LIKE N'%" + q +
                "%') AS PR WHERE PR.RowNum>" + Convert.ToString(16 * (page - 1)) +
                " AND PR.RowNum<=" + Convert.ToString(16 * page) +
                " ORDER BY PR.RowNum ASC";
            }
            else
            {
                sql = "SELECT PR.ID as ID, PR.NameProduct as NameProduct, PR.PriceGoc as PriceGoc, PR.SalePricent as SalePricent, " +
                "PR.SaleMoney as SaleMoney, PR.Rating as Rating, PR.CountComment as CountComment, PR.PathIamge as PathIamge " +
                "FROM (SELECT ROW_NUMBER() OVER ( ORDER BY AP.SALE_PERCENT DESC) AS RowNum, ID_ACC_PRODUCT AS ID, NAME_PRODUCT as NameProduct, AP.AMOUNT as PriceGoc, SALE_PERCENT AS SalePricent, SALE_MONEY as SaleMoney, RATING_PRODUCT as Rating," +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE AP.ID_ACC_PRODUCT = ID_ACC_PRODUCT) AS CountComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE P.ID_PRODUCT = ID_PRODUCT) AS PathIamge, AP.DATE_START_SELL, AP.SELL_COUNT " +
                "FROM dbo.PRODUCT AS P, dbo.ACC_PRODUCT AS AP WHERE AP.ID_PRODUCT = P.ID_PRODUCT AND P.NAME_PRODUCT LIKE N'%" + q +
                "%') AS PR WHERE PR.RowNum>" + Convert.ToString(16 * (page - 1)) +
                " AND PR.RowNum<=" + Convert.ToString(16 * page) +
                " ORDER BY PR.RowNum ASC";
            }
            var data = dbContext.Database.SqlQuery<ItemProductModel>(sql)
                .Select(b => new ItemProductModel
                {
                    ID = b.ID,
                    NameProduct = b.NameProduct,
                    PriceGoc = b.PriceGoc,
                    SalePricent = b.SalePricent,
                    SaleMoney = b.SaleMoney,
                    Rating = b.Rating,
                    CountComment = b.CountComment,
                    PathIamge = b.PathIamge
                }).ToList();
            return data;

        }
        public List<SearchProductModel> getSearchByPage(int page, string q)
        {
            string key;
            if (q == null)
            {
                key = "";
            }
            else
            {
                key = q;
            }
            string sql = "SELECT  RowConstrainedResult.ID_ACC_PRODUCT, RowConstrainedResult.ID_PRODUCT, RowConstrainedResult.NAME_PRODUCT, " +
                "RowConstrainedResult.AMOUNT,RowConstrainedResult.SALE_PERCENT,RowConstrainedResult.SALE_MONEY, " +
                "RowConstrainedResult.DATE_START_SELL, RowConstrainedResult.DATE_END_SELL, " +
                "RowConstrainedResult.TOTAL_COUNT,RowConstrainedResult.SELL_COUNT, RowConstrainedResult.RATING_PRODUCT " +
                "FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY SALE_PERCENT ) AS RowNum, " +
                "ID_ACC_PRODUCT, ACC_PRODUCT.ID_PRODUCT, ACC_PRODUCT.USER_ACC, AMOUNT, SALE_PERCENT, SALE_MONEY, " +
                "DATE_START_SELL, DATE_END_SELL,TOTAL_COUNT, SELL_COUNT, ACC_PRODUCT.IS_SELL, NAME_PRODUCT,RATING_PRODUCT " +
                "FROM dbo.ACC_PRODUCT, dbo.PRODUCT WHERE ACC_PRODUCT.ID_PRODUCT = PRODUCT.ID_PRODUCT ) AS RowConstrainedResult " +
                "WHERE RowConstrainedResult.NAME_PRODUCT LIKE '%" + key +
                "%' AND RowConstrainedResult.IS_SELL = 1 AND RowNum > " + Convert.ToString(15 * (page - 1)) +
                " AND RowNum <= " + Convert.ToString(15 * page) +
                " ORDER BY RowNum ";
            var data = dbContext.Database.SqlQuery<SearchProductModel>(sql)
                .Select(b => new SearchProductModel { 
                dateEnd = b.dateEnd,
                    dateStart = b.dateStart,
                    idLo = b.idLo,
                    idproduct = b.idproduct,
                    name = b.name,
                    price = b.price,
                    rating = b.rating,
                    saleMoney = b.saleMoney,
                    salePerce = b.salePerce,
                    sell = b.sell,
                    total = b.total
                }).ToList();
            return data;
        }
        public int AddToCart(string user, string idproduct)
        {
            try
            {
                var res = dbContext.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == idproduct);
                var account = dbContext.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user && x.IS_ACTIVE_ACC == true);
                if (account == null)
                {
                    return -2; // tai khoan k ton tai
                }
                if (res == null)
                {
                    return 0; // san pham k ton tai
                }
                var check = dbContext.WATCHED_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == idproduct &&
                x.USER_ACC == user);
                if (check == null)
                {
                    WATCHED_PRODUCT wATCHED_PRODUCT = new WATCHED_PRODUCT();
                    wATCHED_PRODUCT.ID_ACC_PRODUCT = idproduct;
                    wATCHED_PRODUCT.USER_ACC = user;
                    wATCHED_PRODUCT.CART_COUNT = 1;
                    dbContext.WATCHED_PRODUCT.Add(wATCHED_PRODUCT);
                    dbContext.SaveChanges();
                    return 1; // them thanh cong
                }
                else{
                    if (check.CART_COUNT >0)
                    {
                        return -1; // San pham da co
                    }
                    else
                    {
                        dbContext.WATCHED_PRODUCT.Attach(check);
                        check.CART_COUNT = 1;
                        dbContext.SaveChanges();
                        return 1; // them thanh cong
                    }
                }
            }
            catch(Exception e)
            {
                return -3; // loi khacs
            }
        }
        public bool checkCOD(string idproduct)
        {
            var res = dbContext.TYPE_ODER_ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == idproduct
            && x.ID_TYPE_ODER == "TT");
            if (res == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
       
        public bool InsertToFavorite(string user, string idp)
        {
            try
            {
                WATCHED_PRODUCT wATCHED_ = new WATCHED_PRODUCT();
                wATCHED_.USER_ACC = user;
                wATCHED_.ID_ACC_PRODUCT = idp;
                wATCHED_.CART_COUNT = -1;
                dbContext.WATCHED_PRODUCT.Add(wATCHED_);
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public ProductHomeModel GetInformationProduct(string id)
        {
            string sql = "SELECT AC.ID_ACC_PRODUCT as id,PD.NAME_PRODUCT as name, PC.NAME_PRODUCER as producer, " +
                "AC.AMOUNT as priceG, AC.SALE_PERCENT as saleP, PD.DESCRIBE_PRODUCT as content," +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE ID_ACC_PRODUCT = AC.ID_ACC_PRODUCT) AS totalComment," +
                "PD.RATING_PRODUCT as  totalRating, AC.USER_ACC AS ctv " +
                "FROM dbo.PRODUCT AS PD, dbo.PRODUCER_INFO AS PC, dbo.ACC_PRODUCT AS AC " +
                "WHERE AC.ID_PRODUCT = PD.ID_PRODUCT AND PC.ID_PRODUCER = PD.ID_PRODUCER AND " +
                "ID_ACC_PRODUCT='"+id+"'";
            var data = dbContext.Database.SqlQuery<ProductHomeModel>(sql)
                .Select(b => new ProductHomeModel
                {
                    id = b.id,
                    name = b.name,
                    producer = b.producer,
                    content = b.content,
                    priceG = b.priceG,
                    saleP = b.saleP,
                    totalComment = b.totalComment,
                    totalRating = b.totalRating,
                    ctv = b.ctv


                }).SingleOrDefault();
            return data;

        }
        public List<PRODUCT_IMAGE> getTopImage(string id)
        {
            string idp = dbContext.ACC_PRODUCT.Where(x => x.ID_ACC_PRODUCT == id).SingleOrDefault().ID_PRODUCT;
            return dbContext.PRODUCT_IMAGE.Where(x => x.ID_PRODUCT == idp).Take(5).ToList();
        }
        public List<PRODUCT_ATT> getAllAttr(string id)
        {
            string idp = dbContext.ACC_PRODUCT.Where(x => x.ID_ACC_PRODUCT == id).SingleOrDefault().ID_PRODUCT;
            return dbContext.PRODUCT_ATT.Where(x => x.ID_PRODUCT == idp).ToList();
        }
        public List<GUEST_QUESTION> getAllQuest(string id)
        {

            return dbContext.GUEST_QUESTION.Where(x => x.ID_ACC_PRODUCT == id).ToList();
        }
        public List<ACCOUNT_COMMENT> getAllComment(string id)
        {
            return dbContext.ACCOUNT_COMMENT.Where(x => x.ID_ACC_PRODUCT == id).ToList();
        }
        public int countTopImage(string id)
        {
            string idp = dbContext.ACC_PRODUCT.Where(x => x.ID_ACC_PRODUCT == id).SingleOrDefault().ID_PRODUCT;
            return dbContext.PRODUCT_IMAGE.Where(x => x.ID_PRODUCT == idp).Take(5).Count();
        }
        public int countQuestion(string id)
        {
            return dbContext.GUEST_QUESTION.Where(x => x.ID_ACC_PRODUCT == id).Count();
        }
        public int countComment(string id)
        {
            return dbContext.ACCOUNT_COMMENT.Where(x => x.ID_ACC_PRODUCT == id).Count();
        }
        public bool AddToFavorite(string id, string user)
        {
            try
            {
                Maladin.Models.EF_MORE.FAVORITE_PRODUCT fAVORITE_ = new Maladin.Models.EF_MORE.FAVORITE_PRODUCT();
                fAVORITE_.ID_ACC_PRODUCT = id;
                fAVORITE_.USER_ACC = user;
                dbFV.FAVORITE_PRODUCT.Add(fAVORITE_);
                dbFV.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool AddToWatched(string id, string user)
        {
            try
            {
                WATCHED_PRODUCT wATCHED_PRODUCT = new WATCHED_PRODUCT();
                wATCHED_PRODUCT.ID_ACC_PRODUCT = id;
                wATCHED_PRODUCT.USER_ACC = user;
                wATCHED_PRODUCT.CART_COUNT = 0;
                dbContext.WATCHED_PRODUCT.Add(wATCHED_PRODUCT);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public int InsertComment(string user, string title, string content, int rating, string idp)
        {
            try
            {
                var res = dbContext.ACCOUNT_COMMENT.SingleOrDefault(x => x.USER_ACC == user && x.ID_ACC_PRODUCT == idp);
                if (res != null)
                {
                    return 0;
                }
                ACCOUNT_COMMENT aCCOUNT_COMMENT = new ACCOUNT_COMMENT();
                aCCOUNT_COMMENT.USER_ACC = user;
                aCCOUNT_COMMENT.ID_ACC_PRODUCT = idp;
                aCCOUNT_COMMENT.TITLE_COMMENT = title;
                aCCOUNT_COMMENT.CONTEN_COMMENT = content;
                aCCOUNT_COMMENT.RATING_COMMENT = rating;
                aCCOUNT_COMMENT.DATE_COMMENT = DateTime.Now;
                aCCOUNT_COMMENT.IS_READ = false;
                dbContext.ACCOUNT_COMMENT.Add(aCCOUNT_COMMENT);
                dbContext.SaveChanges();
                return 1;

            }catch(Exception e)
            {
                return -1;
            }
        }
        



    }
}