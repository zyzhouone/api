using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * usermatch实体类,用户读取用户比赛信息
 * 
 * *****************************************/
namespace Model
{
    [Table("usermatch")]
    public class usermatch
    {
        [Key]
        [Column("matchuserid",Order=1)]
        public string matchuserid
        { get;set; }

        [Column("`match_id`")]
        public string match_id
        { get; set; }

        [Column("`match_name`")]
        public string match_name
        { get; set; }

        [Column("`teamid`")]
        public string teamid
        { get; set; }

        [Column("`teamname`")]
        public string teamname
        { get; set; }

        [Column("`lines_id`")]
        public string lines_id
        { get;set; }

        [Column("`linename`")]
        public string linenam
        { get; set; }

        [Column("`eventid`")]
        public int eventid
        { get; set; }

        [Column("`status`")]
        public string status
        { get;set; }

    }
}
