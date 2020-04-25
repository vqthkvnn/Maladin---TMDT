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
    }
}