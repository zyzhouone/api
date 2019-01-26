using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_eventref实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_eventref")]
    public class tbleventref
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`eventid`")]
        public int? Eventid
        { get;set; }

        [Column("`lineid`")]
        public int? Lineid
        { get;set; }

        [Column("`pointid`")]
        public int? Pointid
        { get;set; }

        [Column("`refid`")]
        public int? Refid
        { get;set; }

        [Column("`creater`")]
        public int? Creater
        { get;set; }

        [Column("`create_at`")]
        public string Create_At
        { get;set; }

    }
}
