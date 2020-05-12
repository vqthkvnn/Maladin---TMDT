using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.Areas.API.Models;
namespace Maladin.Areas.API.DAO
{

    public class MessageDAO
    {
        TMDT_Maladin db = null;
        public MessageDAO() => db = new TMDT_Maladin();
        public List<FromToModel> getAllUserSendOrRevice(string user)
        {

            string sql = "SELECT FROM_ACC, TO_ACC FROM dbo.MESSAGE_SEND_TO WHERE FROM_ACC = '" + user + "' or TO_ACC='" + user
                + "' GROUP BY FROM_ACC,TO_ACC";
            var data = db.Database.SqlQuery<FromToModel>(sql)
                .Select(b => new FromToModel
                {
                    FROM_ACC = b.FROM_ACC,
                    TO_ACC = b.TO_ACC
                }).ToList();
            return data;

        }
        public int InsertMessage(MESSAGE_SEND_TO entity)
        {
            try
            {
                db.MESSAGE_SEND_TO.Add(entity);
                db.SaveChanges();
                return entity.ID_MESSAGE;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        public List<ChatModel> getAllLastMessage(string user)
        {
            var listSendRevice = getAllUserSendOrRevice(user);
            List<ChatModel> listRes = new List<ChatModel>();
            List<MESSAGE_SEND_TO> ListMax = new List<MESSAGE_SEND_TO>();
            foreach(var i in listSendRevice)
            {
                ListMax.Add(db.MESSAGE_SEND_TO.Where(x => x.FROM_ACC == i.FROM_ACC && x.TO_ACC == i.TO_ACC).
                    OrderByDescending(x => x.ID_MESSAGE).FirstOrDefault());
            }
            List<MESSAGE_SEND_TO> ListSort = ListMax.OrderByDescending(x => x.ID_MESSAGE).ToList();
            ChatModel model = new ChatModel();
            model.id = ListSort[0].ID_MESSAGE;
            model.content = ListSort[0].CONTEN_MESSAGE;
            model.from = ListSort[0].FROM_ACC;
            model.to = ListSort[0].TO_ACC;
            model.dateSend = ListSort[0].DATA_SEND_MESSAGE;
            model.messNoneRead = db.MESSAGE_SEND_TO
                .Where(x => x.FROM_ACC == model.from && x.TO_ACC == model.to && x.IS_READ ==false)
                .Count()+
                db.MESSAGE_SEND_TO
                .Where(x=>x.FROM_ACC == model.to && x.TO_ACC == model.from && x.IS_READ == false)
                .Count();
            if (model.from == user)
            {
                model.avtPath = db.INFOMATION_ACCOUNT.Where(x => x.USER_ACC == model.to && 
                (x.ID_TYPE_ACC == "CTV" || x.ID_TYPE_ACC == "ADMIN")).SingleOrDefault()
                .AVT_ACC;
                model.Name = db.INFOMATION_ACCOUNT.SingleOrDefault(x => x.USER_ACC == model.to
                && (x.ID_TYPE_ACC == "CTV" || x.ID_TYPE_ACC == "ADMIN")).NAME_INFO;
                model.UserName = model.to;
            }
            else
            {
                model.avtPath = db.INFOMATION_ACCOUNT.Where(x => x.USER_ACC == model.from && 
                (x.ID_TYPE_ACC == "CTV"|| x.ID_TYPE_ACC == "ADMIN")).SingleOrDefault()
                .AVT_ACC;
                model.Name = db.INFOMATION_ACCOUNT.SingleOrDefault(x => x.USER_ACC == model.from
                && (x.ID_TYPE_ACC == "CTV" || x.ID_TYPE_ACC == "ADMIN")).NAME_INFO;
                model.UserName = model.from;
            }
            
            listRes.Add(model);
            foreach(var i in ListSort)
            {
                int count = 0;
                foreach(var j in listRes)
                {
                    if (i.FROM_ACC == j.to && i.TO_ACC == j.from)
                    {
                        count++;
                    }
                    if (i.FROM_ACC == j.from && i.TO_ACC == j.to)
                    {
                        count++;
                    }
                }
                if (count==0)
                {
                    ChatModel model2 = new ChatModel();
                    model2.id = i.ID_MESSAGE;
                    model2.content = i.CONTEN_MESSAGE;
                    model2.from = i.FROM_ACC;
                    model2.to = i.TO_ACC;
                    model2.dateSend = i.DATA_SEND_MESSAGE;
                    model2.messNoneRead = db.MESSAGE_SEND_TO
                        .Where(x => x.FROM_ACC == model2.from && x.TO_ACC == model2.to && x.IS_READ == false)
                        .Count() +
                        db.MESSAGE_SEND_TO
                        .Where(x => x.FROM_ACC == model2.to && x.TO_ACC == model2.from && x.IS_READ == false)
                        .Count();
                    if (model2.from == user)
                    {
                        model2.avtPath = db.INFOMATION_ACCOUNT.Where(x => x.USER_ACC == model2.to &&
                        (x.ID_TYPE_ACC == "CTV" || x.ID_TYPE_ACC == "ADMIN")).SingleOrDefault()
                        .AVT_ACC;
                        model2.Name = db.INFOMATION_ACCOUNT.SingleOrDefault(x => x.USER_ACC == model2.to
                    && (x.ID_TYPE_ACC == "CTV" || x.ID_TYPE_ACC == "ADMIN")).NAME_INFO;
                        model2.UserName = model2.to;
                    }
                    else
                    {
                        model2.avtPath = db.INFOMATION_ACCOUNT.Where(x => x.USER_ACC == model2.from &&
                        (x.ID_TYPE_ACC == "CTV" || x.ID_TYPE_ACC == "ADMIN")).SingleOrDefault()
                        .AVT_ACC;
                        model2.Name = db.INFOMATION_ACCOUNT.SingleOrDefault(x => x.USER_ACC == model2.to
                        && (x.ID_TYPE_ACC == "CTV" || x.ID_TYPE_ACC == "ADMIN")).NAME_INFO;
                        model2.UserName = model2.from;
                    }
                    listRes.Add(model2);
                }
            }
            return listRes;
        }
        public int CountNewMessage(string user, string userTo)
        {
            return db.MESSAGE_SEND_TO.Where(x => (x.FROM_ACC == user && x.TO_ACC == userTo)
            || (x.TO_ACC == user && x.FROM_ACC == user)).Where(x=>x.IS_READ == false).Count();
        }
        public List<MessageModel> GetNewMessage(string user, string userTo)
        {
            string sql = "SELECT ID_MESSAGE as IdMess, CONTEN_MESSAGE as Content, FROM_ACC as FromID, " +
                "TO_ACC as ToID, DATA_SEND_MESSAGE as DateSend, IS_READ as IsRead " +
                "FROM dbo.MESSAGE_SEND_TO WHERE (FROM_ACC = '" + user + "' OR TO_ACC='" + user + "') AND (FROM_ACC='" +
                userTo + "' OR TO_ACC='" + userTo + "') AND IS_READ = 0 ORDER BY ID_MESSAGE DESC";
            var data = db.Database.SqlQuery<MessageModel>(sql)
                .Select(b => new MessageModel
                {
                    Content = b.Content,
                    DateSend = b.DateSend,
                    FromID = b.FromID,
                    IdMess = b.IdMess,
                    IsRead = b.IsRead,
                    ToID = b.ToID

                }).ToList();
            return data;
        }
        public bool UpdateIsRead(int id)
        {
            try
            {
                var res = db.MESSAGE_SEND_TO.SingleOrDefault(x => x.ID_MESSAGE == id);
                db.MESSAGE_SEND_TO.Attach(res);
                res.IS_READ = true;
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