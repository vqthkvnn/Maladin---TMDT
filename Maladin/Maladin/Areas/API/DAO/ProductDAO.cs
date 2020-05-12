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
        public ProductDAO() => db = new TMDT_Maladin();
        public List<ProductModel> getByNameAndPage(string page, string key)
        {
            string sql = "SELECT  RCR.ID_ACC_PRODUCT as id, RCR.NAME_PRODUCT as name, RCR.SALE_PERCENT as salePercent, " +
                "RCR.AMOUNT as price,RCR.RATING_PRODUCT as rating," +
                "RCR.TotalComment as totalComment, RCR.imagePath as imagePath  FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY SALE_PERCENT ) AS RowNum, " +
                "AP.ID_ACC_PRODUCT, AP.ID_PRODUCT, AP.USER_ACC, AP.AMOUNT, AP.SALE_PERCENT, AP.SALE_MONEY, AP.DATE_START_SELL, AP.DATE_END_SELL," +
                "AP.TOTAL_COUNT, AP.SELL_COUNT, AP.IS_SELL, NAME_PRODUCT,RATING_PRODUCT, " +
                "(SELECT COUNT(*) FROM dbo.ACCOUNT_COMMENT WHERE AP.ID_ACC_PRODUCT = ID_ACC_PRODUCT) AS TotalComment," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.PRODUCT_IMAGE WHERE AP.ID_PRODUCT = ID_PRODUCT) AS imagePath " +
                "FROM dbo.ACC_PRODUCT AS AP, dbo.PRODUCT  " +
                "WHERE AP.ID_PRODUCT = PRODUCT.ID_PRODUCT ) AS RCR " +
                "WHERE RCR.NAME_PRODUCT LIKE '%" + key+
                "%' AND RCR.IS_SELL = 1 AND RowNum > 0 AND RowNum <= 10 " +
                "ORDER BY RowNum ASC ";
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

    }
}