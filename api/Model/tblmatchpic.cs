using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_match_pics实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_match_pics")]
    public class tblmatchpic
    {
        [Key]
        [Column("id",Order=1)]
        public string id
        { get; set; }

        [Column("`match_id`")]
        public string match_id
        { get; set; }

        [Column("`picture`")]
        public string picture
        { get; set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

    }
}
