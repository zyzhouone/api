using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_eventrank实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_eventrank")]
    public class tbleventrank
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`contentrank`")]
        public int? Contentrank
        { get;set; }

        [Column("`refereerank`")]
        public int? Refereerank
        { get;set; }

        [Column("`mono`")]
        public string Mono
        { get;set; }

        [Column("`pointid`")]
        public int? Pointid
        { get;set; }

        [Column("`eventid`")]
        public int? Eventid
        { get;set; }

        [Column("`userid`")]
        public int? Userid
        { get;set; }

        [Column("`create_at`")]
        public string Create_At
        { get;set; }

        [Column("`ranktype`")]
        public int? Ranktype
        { get;set; }

    }
}
