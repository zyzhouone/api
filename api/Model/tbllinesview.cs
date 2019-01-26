using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_lines实体类
 * 
 * *****************************************/
namespace Model
{
    public class tbllinesview
    {
        public string Lines_id
        { get; set; }

        public string Match_id
        { get; set; }

        public string Line_id
        { get; set; }

        public string Line_no
        { get; set; }

        public string Linename
        { get; set; }

        public int? Status
        { get; set; }

        public int? Playercount
        { get; set; }

        public int? Paycount
        { get; set; }

        public string Content
        { get; set; }

        public string Url
        { get; set; }

        public string Summary
        { get; set; }

        public int? Pointscount
        { get; set; }

        public int? Condition_Sex
        { get; set; }

        public int? Condition_Age
        { get; set; }

        public int? Condition_Subline
        { get; set; }

        public DateTime? Createtime
        { get; set; }

        public string Price
        { get; set; }

        public string Notice
        { get; set; }
    }
}
