using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_orders实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_orders")]
    public class tblorders
    {
        [Key]
        [Column("id", Order = 1)]
        public string Id
        { get; set; }

        [Column("`match_id`")]
        public string Match_Id
        { get; set; }

        [Column("`orderid`")]
        public string Orderid
        { get; set; }

        [Column("`teamid`")]
        public string Teamid
        { get; set; }

        [Column("`lineid`")]
        public string Lineid
        { get; set; }

        [Column("`userid`")]
        public string Userid
        { get; set; }

        [Column("`ordertotal`")]
        public string Ordertotal
        { get; set; }

        [Column("`status`")]
        public int? Status
        { get; set; }

        [Column("`createtime`")]
        public string Createtime
        { get; set; }

        [Column("`title`")]
        public string Title
        { get; set; }

        [Column("`paytime`")]
        public DateTime? Paytime
        { get; set; }
    }
}
