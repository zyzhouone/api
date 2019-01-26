using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tbl_pointmgrdoc实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("tbl_pointmgrdoc")]
    public class tblpointmgrdoc
    {
        [Key]
        [Column("id",Order=1)]
        public int? Id
        { get;set; }

        [Column("`pointid`")]
        public string Pointid
        { get;set; }

        [Column("`pointmgrid`")]
        public int? Pointmgrid
        { get;set; }

        [Column("`local`")]
        public string Local
        { get;set; }

        [Column("`taskarea`")]
        public string Taskarea
        { get;set; }

        [Column("`refid`")]
        public string Refid
        { get;set; }

        [Column("`volunteer`")]
        public int? Volunteer
        { get;set; }

        [Column("`volunteer_src`")]
        public int? Volunteer_Src
        { get;set; }

        [Column("`p_pointid`")]
        public string P_Pointid
        { get;set; }

        [Column("`n_pointid`")]
        public string N_Pointid
        { get;set; }

        [Column("`creater`")]
        public int? Creater
        { get;set; }

        [Column("`create_at`")]
        public string Create_At
        { get;set; }

        [Column("`p_mobile`")]
        public string P_Mobile
        { get;set; }

        [Column("`n_mobile`")]
        public string N_Mobile
        { get;set; }

        [Column("`pointname`")]
        public string Pointname
        { get;set; }

        [Column("`contact`")]
        public string Contact
        { get;set; }

        [Column("`addr`")]
        public string Addr
        { get;set; }

        [Column("`mobile`")]
        public string Mobile
        { get;set; }

        [Column("`content`")]
        public string Content
        { get;set; }

    }
}
