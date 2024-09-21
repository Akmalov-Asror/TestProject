namespace EcommerceApi.V1.Commons.Exceptions;

public class EcommerceException : Exception
{
    public int Code { get; set; }
    public bool? Global { get; set; }
    public EcommerceException(int code,string message, bool? global = true) : base(message)
    {
        Code = code;
        Global = global;
    }
}
