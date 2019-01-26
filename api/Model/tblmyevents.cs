using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_myevents实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_myevents")]
    public class tblmyevents
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`userid`")]
        public int? Userid
        { get;set; }

        [Column("`teamid`")]
        public int? Teamid
        { get;set; }

        [Column("`eventid`")]
        public int? Eventid
        { get;set; }

        [Column("`paystatus`")]
        public int? Paystatus
        { get;set; }

        [Column("`lineid`")]
        public int? Lineid
        { get;set; }

        [Column("`status`")]
        public int? Status
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

    }
}
