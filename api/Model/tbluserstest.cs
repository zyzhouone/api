using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

/********************************************
 * tbl_users实体类
 * 
 * *****************************************/
namespace Model
{
    public class tbluserstest
    {
        public string matchuserid
        { get; set; }

        public string userid
        { get; set; }

        public string Name
        { get; set; }

        public int? Playerid
        { get;set; }

        public string Mobile
        { get;set; }

        [Column("`passwd`")]
        public string Passwd
        { get;set; }

        [Column("`sexy`")]
        public string sexy
        { get; set; }

        [Column("`cardtype`")]
        public string cardtype
        { get; set; }

        [Column("`cardno`")]
        public string cardno
        { get; set; }

        [Column("`mono`")]
        public string mono
        { get; set; }

        [Column("`birthday`")]
        public DateTime? birthday
        { get; set; }

        [Column("`last_time`")]
        public DateTime? Last_Time
        { get;set; }

        [Column("`status`")]
        public int? Status
        { get;set; }

        private string devtoken = "-";
        [Column("`devicetoken`")]
        public string DeviceToken
        { 
            get { return devtoken; }
            set { devtoken = value; }
        }

        [Column("`isupt`")]
        public string Isupt
        { get; set; }

        [Column("`type`")]
        public string Type
        { get; set; }


        [Column("`ismod`")]
        public string Ismod
        { get; set; }

        [Column("`modtime`")]
        public DateTime? Modtime
        { get; set; }
    }
}
