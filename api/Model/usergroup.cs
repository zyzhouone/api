using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * usergroup实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("usergroup")]
    public class usergroup
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`userid`")]
        public int? Userid
        { get;set; }

        [Column("`groupid`")]
        public int? Groupid
        { get;set; }

    }
}
