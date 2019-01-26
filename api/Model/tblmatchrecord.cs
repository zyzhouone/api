using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

/********************************************
 * tbl_match_record实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_match_record")]
    public class tblmatchrecord
    {
        [Key]
        [Column("`recordid`", Order = 1)]
        public string recordid
        { get; set; }

        [Column("`matchuserid`")]
        public string matchuserid
        { get; set; }

        [Column("`teamid`")]
        public string teamid
        { get; set; }

        [Column("`typ`")]
        public string typ
        { get; set; }

        [Column("`upt_by`")]
        public string upt_by
        { get; set; }

        [Column("`pointid`")]
        public string pointid
        { get; set; }

        [Column("`pointtime`")]
        public DateTime? pointtime
        { get; set; }

        [Column("`createtime`")]
        public DateTime? createtime
        { get; set; }

        [Column("`status`")]
        public string status
        { get; set; }
    }
}
