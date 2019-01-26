using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_tlcomment实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_tlcomment")]
    public class tbltlcomment
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`tid`")]
        public int? Tid
        { get;set; }

        [Column("`content`")]
        public string Content
        { get;set; }

        [Column("`userid`")]
        public int? Userid
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

    }
}
