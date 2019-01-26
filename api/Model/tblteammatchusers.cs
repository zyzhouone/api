using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_match_users实体类
 * 
 * *****************************************/
namespace Model
{
    public class tblteammatchusers
    {
        public string Matchuserid
        { get;set; }

        public string Match_Id
        { get;set; }

        public string Teamid
        { get;set; }

        public string Teamname
        { get;set; }

        public string Nickname
        { get;set; }

        public string Mobile
        { get;set; }

        public string Password
        { get;set; }

        public string Mono
        { get;set; }

        public int? Sexy
        { get;set; }

        public string Cardno
        { get;set; }

        public DateTime? birthday
        { get; set; }

        public string Cardtype
        { get;set; }

        public int? Age
        { get;set; }

        public int? Leader
        { get;set; }

        public DateTime? Createtime
        { get;set; }

        public string Userid
        { get; set; }

        public string Passwd
        { get;set; }

        public int? Pay
        { get;set; }

        public int? Teamno
        { get;set; }


        public string Teamno2
        { get; set; }

        public string Pnov
        { get;set; }

        public string Status
        { get; set; }

        public string Lineid
        { get; set; }

        public string LineName
        { get; set; }

        public int? LineType
        { get; set; }

        public int? teamstatus
        { get; set; }

        public int? TeamType
        { get; set; }

        public string isfore
        { get; set; }
    }
}
