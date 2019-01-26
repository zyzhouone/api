using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * usertask实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_result")]
    public class tblresult
    {
        [Key]
        [Column("teamid", Order = 1)]
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

        [Column("`status`")]
        public string status
        { get; set; }
    }
}
