using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * migration实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("migration")]
    public class migration
    {
        [Key]
        [Column("version",Order=1)]
        public string Version
        { get;set; }

        [Column("`apply_time`")]
        public int? Apply_Time
        { get;set; }

    }
}
