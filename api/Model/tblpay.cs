using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_pay实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_pay")]
    public class tblpay
    {
        [Key]
        [Column("id", Order = 1)]
        public int? Id
        { get; set; }

        [Column("`orderid`")]
        public string Orderid
        { get; set; }

        [Column("`trade_no`")]
        public string Trade_No
        { get; set; }

        [Column("`buyer_email`")]
        public string Buyer_Email
        { get; set; }

        [Column("`status`")]
        public string Status
        { get; set; }

        [Column("`createtime`")]
        public string Createtime
        { get; set; }

    }
}
