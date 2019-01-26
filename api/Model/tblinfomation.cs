using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

/********************************************
 * tbl_users实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_infomation")]
    public class tblinfomation
    {
        [Key]
        [Column("`infoid`", Order = 1)]
        public string Infoid
        { get; set; }

        [Column("`type`")]
        public string Type
        { get; set; }

        [Column("`createtime`")]
        public DateTime createtime
        { get; set; }

        [Column("`userid`")]
        public string Userid
        { get; set; }

        [Column("`mobile`")]
        public string Mobile
        { get; set; }

        [Column("`context`")]
        public string Context
        { get; set; }

        [Column("`status`")]
        public string Status
        { get; set; }

        [Column("`url`")]
        public string Url
        { get; set; }

        [Column("`note`")]
        public string Note
        { get; set; }

        [Column("`field1`")]
        public string Field1
        { get; set; }

        [Column("`field2`")]
        public string Field2
        { get; set; }
    }
}
