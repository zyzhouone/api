using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_subline实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_subline")]
    public class tblsubline
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`lineid`")]
        public int? Lineid
        { get;set; }

        [Column("`sublinename`")]
        public string Sublinename
        { get;set; }

    }
}
