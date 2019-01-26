using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

/********************************************
 * tbl_match实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_match")]
    public class tblmatch
    {
        [Key]
        [Column("match_id", Order = 1)]
        public string Match_id
        { get; set; }

        [Required(ErrorMessage = "请输入赛事名称"), StringLength(100, ErrorMessage = "长度不能超过100")]
        [Column("`match_name`")]
        public string Match_name
        { get; set; }

        [Required(ErrorMessage = "请输入赛事描述"), StringLength(100, ErrorMessage = "长度不能超过200")]
        [Column("`content`")]
        public string Content
        { get; set; }

        [Column("`area1`")]
        public string Area1
        { get; set; }

        [Column("`area2`")]
        public string Area2
        { get; set; }

        [Column("`date1`")]
        public DateTime? Date1
        { get; set; }

        [Column("`date2`")]
        public DateTime? Date2
        { get; set; }

        [Column("`date3`")]
        public DateTime? Date3
        { get; set; }

        [Column("`date4`")]
        public DateTime? Date4
        { get; set; }

        [Column("`pic1`")]
        public string Pic1
        { get; set; }

        [Column("`pic2`")]
        public string Pic2
        { get; set; }

        public string ispic
        { get; set; }

        public string logopic
        { get; set; }

        [Column("`status`")]
        public string Status
        { get; set; }

        [Column("`notice`")]
        public string Notice
        { get; set; }

        [Column("`sort`")]
        public string Sort
        { get; set; }
    }
}

