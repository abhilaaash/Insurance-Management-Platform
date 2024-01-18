using System.Text.Json;

namespace EInsuranceProject.Model
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        //public override string ToString()
        //{
        //    return JsonSerializer.Serialize(this); // convert ErrorDetails object to json object
        //}

    }
}
