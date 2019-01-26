using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_prop实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_prop")]
    public class tblprop
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`pointid`")]
        public int? Pointid
        { get;set; }

        [Column("`propname`")]
        public string Propname
        { get;set; }

        [Column("`propcount`")]
        public int? Propcount
        { get;set; }

        [Column("`sources`")]
        public string Sources
        { get;set; }

        [Column("`mono`")]
        public string Mono
        { get;set; }

        [Column("`creater`")]
        public int? Creater
        { get;set; }

        [Column("`create_at`")]
        public string Create_At
        { get;set; }

    }
}
