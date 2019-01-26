using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * usertask实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_resultdetail")]
    public class tblresultdetail
    {
        [Key]
        [Column("detail_id", Order = 1)]
        public string detailid
        { get; set; }

        [Column("teamid")]
        public string teamid
        { get; set; }

        [Column("`match_id`")]
        public string match_id
        { get; set; }

        [Column("`userno`")]
        public string userno
        { get; set; }

        [Column("`teamno`")]
        public string teamno
        { get; set; }

        [Column("`starttime`")]
        public DateTime? starttime
        { get; set; }

        [Column("`settime`")]
        public DateTime? settime
        { get; set; }

        [Column("`createtime`")]
        public DateTime? createtime
        { get; set; }

        [Column("`timeline`")]
        public string timeline
        { get; set; }
    }
}
