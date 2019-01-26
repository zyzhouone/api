using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * usertask实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("teamresult")]
    public class teamresult
    {
        [Key]
        [Column("teamid",Order=1)]
        public string teamid
        { get;set; }

        [Column("`teamname`")]
        public string teamname
        { get; set; }

        [Column("`teamno`")]
        public string teamno
        { get; set; }

        [Column("`line_no`")]
        public string line_no
        { get; set; }

        [Column("`match_id`")]
        public string match_id
        { get;set; }

        [Column("`match_name`")]
        public string match_name
        { get;set; }
        
    }
}
