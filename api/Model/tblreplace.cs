using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_replace实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_replace")]
    public class tblreplace
    {
        [Key]
        [Column("id",Order=1)]
        public string Id
        { get;set; }

        [Column("`match_id`")]
        public string Match_Id
        { get;set; }

        [Column("`teamid`")]
        public string Teamid
        { get;set; }

        [Column("`s_matchuserid`")]
        public string S_Matchuserid
        { get;set; }

        [Column("`s_userid`")]
        public string S_Userid
        { get;set; }

        [Column("`d_matchuserid`")]
        public string D_Matchuserid
        { get;set; }

        [Column("`d_userid`")]
        public string D_Userid
        { get;set; }

        [Column("`d_nickname`")]
        public string D_Nickname
        { get;set; }

        [Column("`d_mobile`")]
        public string D_Mobile
        { get;set; }

        [Column("`d_sexy`")]
        public int? D_Sexy
        { get;set; }

        [Column("`d_cardno`")]
        public string D_Cardno
        { get;set; }

        [Column("`d_cardtype`")]
        public string D_Cardtype
        { get;set; }

        [Column("`d_age`")]
        public int? D_Age
        { get;set; }

        [Column("`d_birthday`")]
        public DateTime? D_Birthday
        { get;set; }

        [Column("`s_agreetime`")]
        public DateTime? S_Agreetime
        { get;set; }

        [Column("`s_flag`")]
        public string S_Flag
        { get;set; }

        [Column("`d_agreetime`")]
        public DateTime? D_Agreetime
        { get;set; }

        [Column("`d_flag`")]
        public string D_Flag
        { get;set; }

        [Column("`createtime`")]
        public DateTime? Createtime
        { get;set; }

        [Column("`note`")]
        public string Note
        { get;set; }

    }
}
