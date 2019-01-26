using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_myteamhistory实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_myteamhistory")]
    public class tblmyteamhistory
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`playerid`")]
        public int? Playerid
        { get;set; }

        [Column("`teamid`")]
        public int? Teamid
        { get;set; }

        [Column("`eventid`")]
        public int? Eventid
        { get;set; }

        [Column("`lineid`")]
        public int? Lineid
        { get;set; }

        [Column("`status`")]
        public int? Status
        { get;set; }

        [Column("`jointime`")]
        public string Jointime
        { get;set; }

    }
}
