using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_timelineimgs实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_timelineimgs")]
    public class tbltimelineimgs
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`tid`")]
        public int? Tid
        { get;set; }

        [Column("`img`")]
        public string Img
        { get;set; }

    }
}
