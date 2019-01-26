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
    [Table("tbl_users_time")]
    public class tbluserstime
    {
        [Key]
        [Column("`tid`", Order = 1)]
        public string tid
        { get; set; }

        [Column("`mobile`")]
        public string Mobile
        { get; set; }

        [Column("`crtdate`")]
        public DateTime crtdate
        { get; set; }

        [Column("`romateip`")]
        public string RomateIp
        { get; set; }

    }
}
