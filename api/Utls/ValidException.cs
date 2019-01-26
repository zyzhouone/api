using System;

namespace Utls
{
    /// <summary>
    /// 自定义错误
    /// </summary>
    public class ValidException : Exception
    {
        public ValidException()
            : this(string.Empty)
        {
        }

        public ValidException(string message) :
            this("error", message)
        {
        }

        public ValidException(string name, string message)
            : base(message)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
