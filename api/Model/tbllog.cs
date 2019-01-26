using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

/********************************************
 * tbllog实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_log")]
    public class tbllog
    {
        [Key]
        [Column("`tid`", Order = 1)]
        public string tid
        { get; set; }

        [Column("`createtime`")]
        public DateTime createtime
        { get; set; }

        /// <summary>
        /// 1.登录 2.注册 3.报名 4.支付 5.比赛
        /// </summary>
        [Column("`opttype`")]
        public int opttype
        { get; set; }

        [Column("`userid`")]
        public string userid
        { get; set; }

        [Column("`IP`")]
        public string IP
        { get; set; }

        [Column("`remark`")]
        public string remark
        { get; set; }

        /// <summary>
        /// 来源 1.web 2.app
        /// </summary>
        [Column("`source`")]
        public int source
        { get; set; }
    }
}
