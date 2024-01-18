using System.Net;

namespace EInsuranceProject.Exceptions
{
    public class DataNotFoundExcpetion:Exception
    {
        public int StatusCode { get;}
        public string Message { get;}
        public DataNotFoundExcpetion(string  message):base(message)
        {
            Message = message;
            StatusCode=(int )HttpStatusCode.NotFound;
        }
    }
}
