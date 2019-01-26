using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

/********************************************
 * tbl_company实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_company")]
    public class tblcompany
    {
        [Key]
        [Column("id", Order = 1)]
        public int? Id
        { get; set; }

        [Remote("CheckName", "Team", ErrorMessage = "该公司名称已经存在！")]
        [Required(ErrorMessage = "公司名称不能为空！")]
        [Column("`name`")]
        public string Name
        { get; set; }

        [Column("`area`")]
        public string Area
        { get; set; }

        [Column("`memo`")]
        public string Memo
        { get; set; }

        [Column("`contacts`")]
        public string Contacts
        { get; set; }

        [Column("`mobile`")]
        public string Moblie
        { get; set; }

        [Column("`status`")]
        public int? Status
        { get; set; }

    }
}
