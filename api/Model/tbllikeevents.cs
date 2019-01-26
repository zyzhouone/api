using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_likeevents实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_likeevents")]
    public class tbllikeevents
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`userid`")]
        public int? Userid
        { get;set; }

        [Column("`eventid`")]
        public int? Eventid
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

    }
}
