using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_tmpuserinfo实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_tmpuserinfo")]
    public class tbltmpuserinfo
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`teamid`")]
        public string Teamid
        { get;set; }

        [Column("`teamname`")]
        public string Teamname
        { get;set; }

        [Column("`nickname`")]
        public string Nickname
        { get;set; }

        [Column("`mobile`")]
        public string Mobile
        { get;set; }

        [Column("`password`")]
        public string Password
        { get;set; }

        [Column("`mono`")]
        public string Mono
        { get;set; }

        [Column("`sexy`")]
        public int? Sexy
        { get;set; }

        [Column("`cardno`")]
        public string Cardno
        { get;set; }

        [Column("`cardtype`")]
        public string Cardtype
        { get;set; }

        [Column("`age`")]
        public int? Age
        { get;set; }

        [Column("`leader`")]
        public int? Leader
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

        [Column("`passwd`")]
        public string Passwd
        { get;set; }

        [Column("`pay`")]
        public int? Pay
        { get;set; }

        [Column("`teamno`")]
        public int? Teamno
        { get;set; }

        [Column("`pnov`")]
        public string Pnov
        { get;set; }

    }
}
