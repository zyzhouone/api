using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_lines实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_lines")]
    public class tbllines
    {
        [Key]
        [Column("lines_id")]
        public string Linesid
        { get; set; }

        [Column("`match_id`")]
        public string Matchid
        { get; set; }

        [Column("`line_id`")]
        public string Lineid
        { get; set; }

        [Column("`line_no`")]
        public string Lineno
        { get; set; }

        [Column("`linename`")]
        public string Linename
        { get; set; }

        [Column("`status`")]
        public int? Status
        { get; set; }

        [Column("`playercount`")]
        public int? Playercount
        { get; set; }

        [Column("`paycount`")]
        public int? Paycount
        { get; set; }

        [Column("`content`")]
        public string Content
        { get; set; }

        [Column("`url`")]
        public string Url
        { get; set; }

        [Column("`summary`")]
        public string Summary
        { get; set; }

        [Column("`pointscount`")]
        public int? Pointscount
        { get; set; }

        [Column("`condition_sex`")]
        public int? Condition_Sex
        { get; set; }

        [Column("`condition_age`")]
        public int? Condition_Age
        { get; set; }

        [Column("`condition_subline`")]
        public int? Condition_Subline
        { get; set; }

        [Column("`createtime`")]
        public DateTime? Createtime
        { get; set; }

        [Column("`price`")]
        public string Price
        { get; set; }

        [Column("`notice`")]
        public string Notice
        { get; set; }
    }
}
