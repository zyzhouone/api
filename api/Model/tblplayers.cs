using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_players实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_players")]
    public class tblplayers
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`teamid`")]
        public string Teamid
        { get;set; }

        [Column("`playerno`")]
        public string Playerno
        { get;set; }

        [Column("`headimg`")]
        public string Headimg
        { get;set; }

        [Column("`company`")]
        public string Company
        { get;set; }

        [Column("`nickname`")]
        public string Nickname
        { get;set; }

        [Column("`realname`")]
        public string Realname
        { get;set; }

        [Column("`sexy`")]
        public int? Sexy
        { get;set; }

        [Column("`mobile`")]
        public string Mobile
        { get;set; }

        [Column("`cardtype`")]
        public string Cardtype
        { get;set; }

        [Column("`cardno`")]
        public string Cardno
        { get;set; }

        [Column("`birthday`")]
        public string Birthday
        { get;set; }

        [Column("`leader`")]
        public int? Leader
        { get;set; }

        [Column("`mono`")]
        public string Mono
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

        [Column("`founder`")]
        public int? Founder
        { get;set; }

    }
}
