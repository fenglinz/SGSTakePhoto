using System.Collections.ObjectModel;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class Response
    {
        public bool Success { get; set; }

        public string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Success = false;
                }
                _errorMessage = value;
            }
        }

        public Response()
        {
            Success = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response
    {
        public T Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseSet<T> : Response
    {
        public ObservableCollection<T> Datas { get; set; }
    }
}
