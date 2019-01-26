using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_mypoints实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_mypoints")]
    public class tblmypoints
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`teamid`")]
        public int? Teamid
        { get;set; }

        [Column("`userid`")]
        public int? Userid
        { get;set; }

        [Column("`pointid`")]
        public int? Pointid
        { get;set; }

        [Column("`lineid`")]
        public int? Lineid
        { get;set; }

        [Column("`eventid`")]
        public int? Eventid
        { get;set; }

        [Column("`sort`")]
        public int? Sort
        { get;set; }

        [Column("`startpoint`")]
        public int? Startpoint
        { get;set; }

        [Column("`endpoint`")]
        public int? Endpoint
        { get;set; }

        [Column("`creater`")]
        public string Creater
        { get;set; }

        [Column("`status`")]
        public int? Status
        { get;set; }

        [Column("`lon`")]
        public string Lon
        { get;set; }

        [Column("`lat`")]
        public string Lat
        { get;set; }

        [Column("`updatetime`")]
        public string Updatetime
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

    }
}
