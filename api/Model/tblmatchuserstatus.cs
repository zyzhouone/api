using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/********************************************
 * tblmatchuserstatus实体类
 * 
 * *****************************************/
namespace Model
{
    public class tblmatchuserstatus
    {
        public string Teamid
        { get; set; }

        public string Teamname
        { get; set; }

        public string Status
        { get; set; }

        public string IsLeader
        { get; set; }

        public string MacthStatus
        { get; set; }

        public string Linesname
        { get; set; }

        public int TeamType
        { get; set; }


        public string TeamNo2
        { get; set; }
     }
}
