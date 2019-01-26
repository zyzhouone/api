using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_events实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_events")]
    public class tblevents
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

        [Column("`headimg`")]
        public string Headimg
        { get;set; }

        [Column("`starttime`")]
        public string Starttime
        { get;set; }

        [Column("`endtime`")]
        public string Endtime
        { get;set; }

        [Column("`startsigntime`")]
        public string Startsigntime
        { get;set; }

        [Column("`stopsigntime`")]
        public string Stopsigntime
        { get;set; }

        [Column("`players`")]
        public int? Players
        { get;set; }

        [Column("`expense`")]
        public string Expense
        { get;set; }

        [Column("`eventlocal`")]
        public string Eventlocal
        { get;set; }

        [Column("`traveling`")]
        public string Traveling
        { get;set; }

        [Column("`tips`")]
        public string Tips
        { get;set; }

        [Column("`likes`")]
        public int? Likes
        { get;set; }

        [Column("`status`")]
        public int? Status
        { get;set; }

        [Column("`master`")]
        public string Master
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

    }
}
