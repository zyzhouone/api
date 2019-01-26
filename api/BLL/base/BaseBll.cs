using System.Linq;

using DAL;
using Model;

namespace BLL
{
    public class BaseBll
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert<T>(T entity) where T : class
        {
            using (var db = new BFdbContext())
            {
                return db.Insert<T>(entity);
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update<T>(T entity) where T : class
        {
            using (var db = new BFdbContext())
            {
                return db.Update<T>(entity);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete<T>(T entity) where T : class
        {
            using (var db = new BFdbContext())
            {
                return db.Delete<T>(entity);
            }
        }

        /// <summary>
        /// sql执行数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, params object[] parms)
        {
            using (var db = new BFdbContext())
            {
                return db.ExecuteSqlCommand(sql, parms);
            }
        }

        /// <summary>
        /// 获取自增长列id
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        protected int GetIncrement(BFdbContext db, string key)
        {
            return 0;// db.SqlQuery<sysid>(string.Format("SELECT NEXTVAL('{0}') AS ID", key)).FirstOrDefault().ID;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="type"> 1.登录 2.注册 3.报名 4.支付 5.比赛</param>
        /// <param name="userid"></param>
        /// <param name="ip"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public int OptLog(int type, string userid, string ip, string note)
        {
            tbllog log = new tbllog();
            log.createtime = System.DateTime.Now;
            log.IP = ip;
            log.opttype = type;
            log.remark = note;
            log.tid = System.Guid.NewGuid().ToString();
            log.userid = userid;
            log.source = 2;//来源 1.web 2.app
            using (var db = new BFdbContext())
            {
                return db.Insert<tbllog>(log);
            }

        }
    }
}
