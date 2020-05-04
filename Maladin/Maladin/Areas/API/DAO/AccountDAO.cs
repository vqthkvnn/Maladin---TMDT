﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;
using Maladin.Areas.API.Models;

namespace Maladin.Areas.API.DAO
{
    public class AccountDAO
    {
        TMDT_Maladin db = null;
        public AccountDAO()
        {
            db = new TMDT_Maladin();
        }
        public int Insert(ACCOUNT entity)
        {
            try
            {
                var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == entity.USER_ACC);
                var resEmail = db.ACCOUNTs.SingleOrDefault(x => x.EMAIL_INFO == entity.EMAIL_INFO);
                if (res != null)
                {
                    return 0; // tai khoan da ton tai
                }
                if (resEmail!=null)
                {
                    return -2; // email da ton tai
                }
                db.ACCOUNTs.Add(entity);
                
                // auto create Infomation
                var count = db.INFOMATION_ACCOUNT.Count();
                INFOMATION_ACCOUNT iNFOMATION_ACCOUNT = new INFOMATION_ACCOUNT();
                iNFOMATION_ACCOUNT.ID_INFO = Convert.ToString(count+1);
                iNFOMATION_ACCOUNT.ADRESS_INFO = "";
                iNFOMATION_ACCOUNT.AVT_ACC = "/public/image/avt/avtdefult.png";
                iNFOMATION_ACCOUNT.CMND_INFO = "000000000000";
                iNFOMATION_ACCOUNT.NAME_INFO = entity.USER_ACC; 
                iNFOMATION_ACCOUNT.SEX_INFO = true;
                iNFOMATION_ACCOUNT.PHONE_INFO = "";
                iNFOMATION_ACCOUNT.NOTE_INFO = "";
                iNFOMATION_ACCOUNT.BIRTH_INFO = Convert.ToDateTime("01-01-2020");
                iNFOMATION_ACCOUNT.USER_ACC = entity.USER_ACC;
                iNFOMATION_ACCOUNT.ID_TYPE_ACC = "CT"; // gan mac dinh la khach
                db.INFOMATION_ACCOUNT.Add(iNFOMATION_ACCOUNT);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        public int Login(string user, string password)
        {
            var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
            if (res != null)
            {
                if (res.PASSWORD_ACC == password)
                {
                    if (res.IS_ACTIVE_ACC == true)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2; // tai khoan dang bi khoa
                    }
                    
                }
                else
                {
                    return -1; // sai password
                }
            }
            else
            {
                return 0; // tai khoan khong ton tai
            }
        }
        public AccountModel getinfo(string user)
        {
            var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);

            string sql = "SELECT ID_INFO AS id, NAME_INFO AS name, EMAIL_INFO AS email, INFOMATION_ACCOUNT.ID_TYPE_ACC AS idType, COINT_ACC AS coint, " +
                "DATE_CREATE_ACC AS dateCreate,  AVT_ACC AS avt, CMND_INFO AS cmnd, BIRTH_INFO AS birth, SEX_INFO AS gt, ADRESS_INFO AS adr, " +
                "PHONE_INFO AS phone, NOTE_INFO AS note " +
                "FROM dbo.ACCOUNT, dbo.INFOMATION_ACCOUNT " +
                "WHERE ACCOUNT.USER_ACC = INFOMATION_ACCOUNT.USER_ACC AND ACCOUNT.USER_ACC ='"+user+
                "' AND INFOMATION_ACCOUNT.ID_TYPE_ACC ='" + res.ID_TYPE_ACC+
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
            if (data==null)
            {
                return new AccountModel();
            }
            return data.FirstOrDefault();
        }
        public ACCOUNT getAccount(string user)
        {
            return db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
        }
        public bool Delete(string user)
        {
            try
            {
                var res = getAccount(user);
                db.ACCOUNTs.Attach(res);
                res.IS_ACTIVE_ACC = false;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public INFOMATION_ACCOUNT getINFOMATION_ACCOUNT(string user, string type)
        {
            return db.INFOMATION_ACCOUNT.Where(x => x.ID_TYPE_ACC == type).Where(x => x.USER_ACC == user)
                .FirstOrDefault();
        }
        public bool Update(INFOMATION_ACCOUNT entity)
        {
            try
            {
                var obj = getINFOMATION_ACCOUNT(entity.USER_ACC, entity.ID_TYPE_ACC);
                db.INFOMATION_ACCOUNT.Attach(obj);
                obj.CMND_INFO = entity.CMND_INFO;
                obj.NAME_INFO = entity.NAME_INFO;
                obj.BIRTH_INFO = entity.BIRTH_INFO;
                obj.SEX_INFO = entity.SEX_INFO;
                obj.ADRESS_INFO = entity.ADRESS_INFO;
                obj.PHONE_INFO = entity.PHONE_INFO;
                obj.NOTE_INFO = entity.NOTE_INFO;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public int Changepass(string user, string oldpass, string newpass)
        {
            try
            {
                var res = getAccount(user);
                if (oldpass != res.PASSWORD_ACC)
                {
                    return -1; // pass cu k dung
                }
                db.ACCOUNTs.Attach(res);
                res.PASSWORD_ACC = newpass;
                db.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        public List<NofiAccountModel> getNotiBy(int page)
        {
            string sql = "SELECT  NOTI.ID_NOTI as id, NOTI.TITLE_NOTI as titel, NOTI.CONTENT_NOTI as content, " +
                "NOTI.DATE_NOTI as dateTime, " +
                "NOTI.IMAGE_TYPE_NOTI as ImagePath, NOTI.IS_CHECK_NOTI as isRead FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY DATE_NOTI ) AS RowNum, " +
                "NOTIFICATION_.ID_NOTI, TITLE_NOTI, CONTENT_NOTI, DATE_NOTI, IMAGE_TYPE_NOTI,IS_CHECK_NOTI " +
                "FROM dbo.NOTI_ACC, dbo.NOTIFICATION_, dbo.TYPE_NOTIFICATION " +
                "WHERE NOTI_ACC.ID_NOTI = NOTIFICATION_.ID_NOTI AND NOTIFICATION_.ID_TYPE_NOTI = TYPE_NOTIFICATION.ID_TYPE_NOTI) AS NOTI " +
                "WHERE RowNum > 0 AND RowNum <= 10 ORDER BY RowNum ASC";
            var data = db.Database.SqlQuery<NofiAccountModel>(sql)
                .Select(b => new NofiAccountModel {
                id = b.id,
                content=b.content,
                dateTime=b.dateTime,
                ImagePath=b.ImagePath,
                isRead=b.isRead,
                titel=b.titel
                }).ToList();
            return data;
        }
    }
}