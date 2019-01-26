using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_myhistory实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_myhistory")]
    public class tblmyhistory
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`userid`")]
        public int? Userid
        { get;set; }

        [Column("`playerid`")]
        public int? Playerid
        { get;set; }

        [Column("`playerno`")]
        public string Playerno
        { get;set; }

        [Column("`eventid`")]
        public int? Eventid
        { get;set; }

        [Column("`lineid`")]
        public int? Lineid
        { get;set; }

        [Column("`teamid`")]
        public int? Teamid
        { get;set; }

        [Column("`usertime`")]
        public string Usertime
        { get;set; }

        [Column("`eventresult`")]
        public string Eventresult
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

    }
}
