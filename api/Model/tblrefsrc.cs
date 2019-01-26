using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_refsrc实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_refsrc")]
    public class tblrefsrc
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`soruce`")]
        public string Soruce
        { get;set; }

        [Column("`creater`")]
        public int? Creater
        { get;set; }

    }
}
