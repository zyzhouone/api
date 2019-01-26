using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * group实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("group")]
    public class group
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`name`")]
        public string Name
        { get;set; }

    }
}
