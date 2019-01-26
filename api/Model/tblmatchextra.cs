using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_pay实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_match_extra")]
    public class tblmatchextra
    {
        [Key]
        [Column("id", Order = 1)]
        public string Id
        { get; set; }

        [Column("`updt`")]
        public DateTime? updt
        { get; set; }

        [Column("`extype`")]
        public string extype
        { get; set; }

        [Column("`teamid`")]
        public string teamid
        { get; set; }

        [Column("`info1`")]
        public string info1
        { get; set; }

        [Column("`info2`")]
        public string info2
        { get; set; }

        [Column("`info3`")]
        public string info3
        { get; set; }
    }
}
