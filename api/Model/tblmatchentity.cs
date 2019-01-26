using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_matchentity实体类
 * 
 * *****************************************/
namespace Model
{
    public class tblmatchentity
    {
        public string Matchuserid
        { get; set; }

        public string Match_Id
        { get; set; }

        public string Match_name
        { get; set; }

        public string Content
        { get; set; }

        public string Area1
        { get; set; }

        public string Area2
        { get; set; }

        public DateTime? Date1
        { get; set; }

        public DateTime? Date2
        { get; set; }

        public DateTime? Date3
        { get; set; }

        public DateTime? Date4
        { get; set; }

        public string Pic1
        { get; set; }

        public string Pic2
        { get; set; }

        public string Logopic
        { get; set; }

        public string Status
        { get; set; }

        public string Teamid
        { get; set; }

        public string Teamname
        { get; set; }

        public string Lineno
        { get; set; }

        public string Nickname
        { get; set; }

        public string Mobile
        { get; set; }

        public string Password
        { get; set; }

        public string mstatus
        { get; set; }
        
        public string Mono
        { get; set; }

        public int? Sexy
        { get; set; }

        public int? Paystatus
        { get; set; }

        public string Cardno
        { get; set; }

        public string Cardtype
        { get; set; }

        public int? Age
        { get; set; }

        public string LeaderM
        { get; set; }

        public int? Leader
        { get; set; }

        public DateTime? Createtime
        { get; set; }

        public string Passwd
        { get; set; }

        public int? Pay
        { get; set; }

        public int? Teamno
        { get; set; }

        public string Teamno_T
        { get; set; }


        public int? Teamstatus
        { get; set; }

        public string Pnov
        { get; set; }

        public string userid
        { get; set; }

        public string LeaderName
        { get; set; }
    }
}
