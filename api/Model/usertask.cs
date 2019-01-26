using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * usertask实体类
 * 
 * *****************************************/
namespace Model
{

    public class usertask
    {
        public string matchuserid
        { get; set; }

        public string lines_id
        { get; set; }

        public string teamname
        { get; set; }

        public string linename
        { get; set; }

        public string nickname
        { get; set; }

        public string match_name
        { get; set; }


        public string isdown
        { get; set; }

        public string teamno
        { get; set; }

        public string tasklogo
        { get; set; }

        public string logopic
        { get; set; }

        public string date4
        { get; set; }

        public int? leader
        { get; set; }
    }
}
