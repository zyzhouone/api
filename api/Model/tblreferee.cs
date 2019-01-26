using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_referee实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_referee")]
    public class tblreferee
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`refno`")]
        public string Refno
        { get;set; }

        [Column("`nickname`")]
        public string Nickname
        { get;set; }

        [Column("`realname`")]
        public string Realname
        { get;set; }

        [Column("`mobile`")]
        public string Mobile
        { get;set; }

        [Column("`cardno`")]
        public string Cardno
        { get;set; }

        [Column("`email`")]
        public string Email
        { get;set; }

        [Column("`times`")]
        public string Times
        { get;set; }

        [Column("`rating`")]
        public int? Rating
        { get;set; }

        [Column("`creater`")]
        public int? Creater
        { get;set; }

        [Column("`createtime`")]
        public string Createtime
        { get;set; }

        [Column("`pointer`")]
        public int? Pointer
        { get;set; }

    }
}
