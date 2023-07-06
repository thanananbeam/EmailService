namespace EmailService.Model
{
    public class ResponseModel
    {
        public int status { get; set; }
        public string message{ get; set; }
        public object data { get; set; }
    }
}
