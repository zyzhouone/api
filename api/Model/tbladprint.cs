using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_prop实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_ad_print")]
    public class tbladprint
    {

        [Key]
        [Column("adid", Order = 1)]
        public string Adid
        { get; set; }

        [Column("title")]
        public string Title
        { get; set; }

        [Column("`img`")]
        public string Img
        { get; set; }

        [Column("`url`")]
        public string Url
        { get; set; }

        [Column("`status`")]
        public string Status
        { get; set; }
    }
}
