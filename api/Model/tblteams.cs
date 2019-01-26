using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

/********************************************
 * tbl_teams实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_teams")]
    public class tblteams
    {
        [Key]
        [Column("`teamid`", Order = 1)]
        public string teamid
        { get; set; }

        [Column("`match_id`")]
        public string match_id
        { get; set; }

        [Column("`teamno`")]
        public string Teamno
        { get; set; }

        [Column("`teamname`")]
        public string Teamname
        { get; set; }

        [Column("`userid`")]
        public string Userid
        { get; set; }

        [Column("`company`")]
        public string Company
        { get; set; }

        [Column("`lineid`")]
        public string Lineid
        { get; set; }

        [Column("`linesid`")]
        public string Linesid
        { get; set; }

        [Column("`createtime`")]
        public DateTime? Createtime
        { get; set; }

        [Column("`eventid`")]
        public int Eventid
        { get; set; }

        [Column("`status`")]
        public int? Status
        { get; set; }

        [Column("`teamtype`")]
        public int? Teamtype
        { get; set; }

        [Column("`record`")]
        public string Record
        { get; set; }

        [Column("`type_`")]
        public string Type_
        { get; set; }

    }
}
