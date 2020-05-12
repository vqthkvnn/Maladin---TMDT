using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.Admin.DAO
{
    public class AccountDAO
    {
        /*
         * phần này liên quan đến quản lý tài khoản thông tin tài khoản, thông báo người dùng.
         */
        private TMDT_Maladin db = null;
        public AccountDAO()
        {
            db = new TMDT_Maladin();
        }
        public int NewAccount(ACCOUNT entity)
        {
            try
            {
                var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == entity.USER_ACC);
                var resEmail = db.ACCOUNTs.SingleOrDefault(x => x.EMAIL_INFO == entity.EMAIL_INFO);
                if (res != null)
                {
                    return 0; // tai khoan da ton tai
                }
                if (resEmail != null)
                {
                    return -2; // email da ton tai
                }
                db.ACCOUNTs.Add(entity);
                db.SaveChanges();
                /*
                 * create infomation
                 */
                INFOMATION_ACCOUNT iNFOMATION_ACCOUNT = new INFOMATION_ACCOUNT();
                iNFOMATION_ACCOUNT.USER_ACC = entity.USER_ACC;
                iNFOMATION_ACCOUNT.ID_TYPE_ACC = "CT";
                iNFOMATION_ACCOUNT.ID_INFO = "CT" + entity.USER_ACC.Substring(0, 2) +
                    Convert.ToString(db.INFOMATION_ACCOUNT.Count() + 1);
                iNFOMATION_ACCOUNT.NAME_INFO = entity.USER_ACC;
                iNFOMATION_ACCOUNT.CMND_INFO = "null";
                iNFOMATION_ACCOUNT.AVT_ACC = "/public/image/avt/avtdefult.png";
                db.INFOMATION_ACCOUNT.Add(iNFOMATION_ACCOUNT);
                db.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        public int Active(string user, bool action)
        {
            try
            {
                var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
                db.ACCOUNTs.Attach(res);
                res.IS_ACTIVE_ACC = action;
                db.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        public int Update(string user, string type, string name, string cmnd, string adr, string phone, string birth,
            string sex)
        {
            /*
             * update thông tin -> không cho phép update cái gì ngoài thông tin, đối với account chỉ có đổi pass
             * Sửa các thông tin như cmnd, name, địa chỉ, số điện thoại, ngày sinh, giới tính
             */
            try {
                if(type == null)
                {
                    type = "CT";
                }
                var res = db.INFOMATION_ACCOUNT.Where(x=>x.USER_ACC == user).Where(x=>x.ID_TYPE_ACC == type).SingleOrDefault();
                db.INFOMATION_ACCOUNT.Attach(res);

                if (cmnd != null)
                {
                    res.CMND_INFO = cmnd;
                }
                if (name != null)
                {
                    res.NAME_INFO = cmnd;
                }
                if (adr != null)
                {
                    res.ADRESS_INFO = cmnd;
                }
                if (phone != null)
                {
                    res.PHONE_INFO = cmnd;
                }
                if (sex != null)
                {
                    if (sex == "true")
                    {
                        res.SEX_INFO = true;
                    }
                    if (sex == "false")
                    {
                        res.SEX_INFO = false;
                    }
                }
                if (birth != null)
                {
                    DateTime date = Convert.ToDateTime(birth);
                    res.BIRTH_INFO = date;
                }
                return 1;

            }
            catch
            {
                return -1;
            }
        }
        public int CreateNoti(NOTIFICATION_ entity)
        {
            try
            {
                var count = db.NOTIFICATION_.Count();
                entity.ID_NOTI = count + 1;
                db.NOTIFICATION_.Add(entity);
                db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public int SetNoti(int id, string user)
        {
            try
            {
                var res = db.NOTIFICATION_.SingleOrDefault(x => x.ID_NOTI == id);
                NOTI_ACC nOTI_ACC = new NOTI_ACC();
                nOTI_ACC.ID_NOTI = res.ID_NOTI;
                nOTI_ACC.USER_ACC = user;
                db.NOTI_ACC.Add(nOTI_ACC);
                db.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        /*
         * phầm duyệt tài khoản xin lên quyền CTV
         * khi lên thì phải tạo bảng thông tin mới cho bảng đó
         * set quyền cho lên CTV
         * neu thanh cong thi thong bao, neu k thanh cong thi huy
         */
         public bool ApprovedCTV(string user, bool action, string userW)
        {
            try
            {
                APPROVED_USER_WAIT wAIT = new APPROVED_USER_WAIT();
                wAIT.USER_ACC = user;
                wAIT.DATE_APPROVED = DateTime.UtcNow;
                wAIT.ACTION_APPROVED = action;
                wAIT.ID_APPROVED_USER_WAIT = db.USES_WAIT.SingleOrDefault(x => x.USER_ACC_WANT == userW).ID_USER_WAIT;
                db.SaveChanges();
                /*
                 * tao thong bao
                 */
                NOTIFICATION_ nOTIFICATION_ = new NOTIFICATION_();
                nOTIFICATION_.ID_NOTI = db.NOTIFICATION_.Count() + 1;
                nOTIFICATION_.TITLE_NOTI = "Thông báo kết quả yêu cầu nâng cấp tài khoản";
                nOTIFICATION_.CONTENT_NOTI = "Thông báo kích hoạt tài khoản lên cộng tác viên:";
                if (action)
                {
                    nOTIFICATION_.CONTENT_NOTI += "Chấp Nhận";
                }
                else
                {
                    nOTIFICATION_.CONTENT_NOTI += "Hủy bỏ";
                }
                nOTIFICATION_.ID_TYPE_NOTI = 3;
                nOTIFICATION_.DATE_NOTI = DateTime.UtcNow;
                db.NOTIFICATION_.Add(nOTIFICATION_);
                db.SaveChanges();
                /*
                 * add cho tài khoản đang yêu cầu
                 */
                NOTI_ACC nOTI_ACC = new NOTI_ACC();
                nOTI_ACC.USER_ACC = userW;
                nOTI_ACC.ID_NOTI = nOTIFICATION_.ID_NOTI;
                db.NOTI_ACC.Add(nOTI_ACC);
                db.SaveChanges();
                return true;

            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}