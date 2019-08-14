using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate
{
    /// <summary>
    /// 
    /// </summary>
    public class Response
    {
        public bool Success { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Success = false;
                }
                _message = value;
            }
        }
    }
}
