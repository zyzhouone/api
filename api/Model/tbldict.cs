using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_dict实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_dict")]
    public class tbldict
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`dictid`")]
        public int? Dictid
        { get;set; }

        [Column("`code`")]
        public string Code
        { get;set; }

        [Column("`name`")]
        public string Name
        { get;set; }

        [Column("`memo`")]
        public string Memo
        { get;set; }

    }
}
