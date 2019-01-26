using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_matchentity实体类
 * 
 * *****************************************/
namespace Model
{
    public class tblmatchdetailentity
    {
        public string Match_name
        { get; set; }
        
        public string Teamname
        { get; set; }

        public string Teamstatus
        { get; set; }

        public string Matchstatus
        { get; set; }

        public int? leader
        { get; set; }

        public string Lineno
        { get; set; }

        public string Teamno_t
        { get; set; }

        public string Nickname
        { get; set; }

        public string Mobile
        { get; set; }
        
        public DateTime? MatchDate
        { get; set; }
    }
}
