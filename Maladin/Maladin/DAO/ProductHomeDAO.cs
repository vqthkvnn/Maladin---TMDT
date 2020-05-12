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
        public ProductHomeDAO()
        {
            dbContext = new TMDT_Maladin();
        }
        public List<ItemProductModel> getListProduct(int page, string idType)
        {
            string sql = "SELECT ID_ACC_PRODUCT AS ID, NAME_PRODUCT as NameProduct, PRICE_PRODUCT as PriceGoc, SALE_PERCENT as SalePricent, " +
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
                var account = dbContext.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user || x.IS_ACTIVE_ACC == true);
                if (account == null)
                {
                    return -2;
                }
                if (res == null)
                {
                    return 0;
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
                    return 1;
                }
                else{
                    if (check.CART_COUNT >0)
                    {
                        return -1;
                    }
                    else
                    {
                        dbContext.WATCHED_PRODUCT.Attach(check);
                        check.CART_COUNT = 1;
                        dbContext.SaveChanges();
                        return 1;
                    }
                }
            }
            catch(Exception e)
            {
                return -3;
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


    }
}