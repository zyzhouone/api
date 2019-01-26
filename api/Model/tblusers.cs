using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

/********************************************
 * tbl_users实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_users")]
    public class tblusers
    {
        [Key]
        [Column("`userid`", Order = 1)]
        public string userid
        { get; set; }

        [Column("`name`")]
        public string Name
        { get; set; }

        [Column("`playerid`")]
        public int? Playerid
        { get;set; }

        //[Remote("CheckTel", "Member", ErrorMessage = "该电话号码已经存在！")]
        //[Required(ErrorMessage = "手机号码不能为空！")]
        //[RegularExpression(@"^(0|86|17951)?(13[0-9]|15[012356789]|17[0-9]|18[0-9]|14[57])[0-9]{8}$", ErrorMessage = "手机号码不合法！")]
        [Column("`mobile`")]
        public string Mobile
        { get;set; }

        [Required(ErrorMessage = "密码不能为空！")]
        //[RegularExpression(@"^\d{6}$", ErrorMessage = "密码不合法！6个数字")]
        [Column("`passwd`")]
        public string Passwd
        { get;set; }

        [Column("`sexy`")]
        public string sexy
        { get; set; }

        [Column("`cardtype`")]
        public string cardtype
        { get; set; }

        [Column("`cardno`")]
        public string cardno
        { get; set; }

        [Column("`mono`")]
        public string mono
        { get; set; }

        [Column("`birthday`")]
        public DateTime? birthday
        { get; set; }

        [Column("`last_time`")]
        public DateTime? Last_Time
        { get;set; }

        [Column("`status`")]
        public int? Status
        { get;set; }

        private string devtoken = "-";
        [Column("`devicetoken`")]
        public string DeviceToken
        { 
            get { return devtoken; }
            set { devtoken = value; }
        }

        [Column("`isupt`")]
        public string Isupt
        { get; set; }

        [Column("`type`")]
        public string Type
        { get; set; }


        [Column("`ismod`")]
        public string Ismod
        { get; set; }

        [Column("`modtime`")]
        public DateTime? Modtime
        { get; set; }
    }
}
