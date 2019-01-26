using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * sys_user实体类
 * 
 * *****************************************/
namespace Model
{
    [Table("sys_user")]
    public class sysuser
    {
        [Key]
        [Column("UserID", Order = 1)]
        public string Userid
        { get; set; }

        [Column("`LoginCode`")]
        public string Logincode
        { get; set; }

        [Column("`UserName`")]
        public string Username
        { get; set; }

        [Column("`Password`")]
        public string Password
        { get; set; }

        [Column("`RoleType`")]
        public int? Roletype
        { get; set; }

        [Column("`Telphone`")]
        public string Telphone
        { get; set; }

        [Column("`Email`")]
        public string Email
        { get; set; }

        [Column("`LoginCount`")]
        public int? Logincount
        { get; set; }

        [Column("`LastLoginDate`")]
        public DateTime? Lastlogindate
        { get; set; }

        [Column("`Note`")]
        public string Note
        { get; set; }

        [Column("`DelFlag`")]
        public bool Delflag
        { get; set; }

        [Column("`CreatedBy`")]
        public string Createdby
        { get; set; }

        [Column("`CreatedDate`")]
        public DateTime? Createddate
        { get; set; }

        [Column("`ModifyBy`")]
        public string Modifyby
        { get; set; }

        [Column("`ModifyDate`")]
        public DateTime? Modifydate
        { get; set; }

        [NotMapped]
        public string NewPassword
        { get; set; }

        [NotMapped]
        public string ConfirmPassword
        { get; set; }
    }
}
