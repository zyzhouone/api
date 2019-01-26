using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

/********************************************
 * tbl_signedon实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_signedon")]
    public class tblsignedon
    {
        [Key]
        [Column("`signid`", Order = 1)]
        public string signid
        { get; set; }

        [Column("`teamid`")]
        public string teamid
        { get; set; }

        [Column("`matchid`")]
        public string matchid
        { get; set; }

        [Column("`userid`")]
        public string Userid
        { get; set; }

        [Column("`dtime`")]
        public DateTime? dtime
        { get; set; }
    }
}
