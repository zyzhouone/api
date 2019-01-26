using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_likes实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_likes")]
    public class tbllikes
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`eventid`")]
        public int? Eventid
        { get;set; }

        [Column("`userid`")]
        public int? Userid
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

    }
}
