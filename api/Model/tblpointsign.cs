using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_pointsign实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_pointsign")]
    public class tblpointsign
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`pointid`")]
        public int? Pointid
        { get;set; }

        [Column("`refid`")]
        public int? Refid
        { get;set; }

        [Column("`srcid`")]
        public int? Srcid
        { get;set; }

        [Column("`volunteercount`")]
        public int? Volunteercount
        { get;set; }

        [Column("`v_srcid`")]
        public int? V_Srcid
        { get;set; }

        [Column("`create_at`")]
        public string Create_At
        { get;set; }

        [Column("`creater`")]
        public int? Creater
        { get;set; }

    }
}
