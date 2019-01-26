using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_timelines实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_timelines")]
    public class tbltimelines
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`userid`")]
        public int? Userid
        { get;set; }

        [Column("`content`")]
        public string Content
        { get;set; }

        [Column("`likes`")]
        public int? Likes
        { get;set; }

        [Column("`comments`")]
        public int? Comments
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

    }
}
