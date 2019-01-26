using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_noticemsg实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_noticemsg")]
    public class tblnoticemsg
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`subject`")]
        public string Subject
        { get;set; }

        [Column("`content`")]
        public string Content
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

        [Column("`status`")]
        public int? Status
        { get;set; }

        [Column("`type`")]
        public int? Type
        { get;set; }

        [Column("`img`")]
        public string Img
        { get;set; }

    }
}
