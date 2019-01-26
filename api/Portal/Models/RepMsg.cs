namespace Portal
{
    public class RepMsg
    {
        /// <summary>
        /// 0:OK -1:Error 1:Other
        /// </summary>
        public int Code
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public object Data
        {
            get;
            set;
        }
    }
}