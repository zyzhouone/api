using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_timelinelikes实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_timelinelikes")]
    public class tbltimelinelikes
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`userid`")]
        public int? Userid
        { get;set; }

        [Column("`nickname`")]
        public string Nickname
        { get;set; }

        [Column("`timelineid`")]
        public int? Timelineid
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

    }
}
