namespace ClientHubDatabase.Models;

public class GenericResponse<T>
{
    public bool Status { get; set; }
    public int StatusCode { get; set; }
    public string StatusMessage { get; set; }
    public T Data { get; set; }
}


public class GenericResponse
{
    public bool Status { get; set; }
    public int StatusCode { get; set; }
    public string StatusMessage { get; set; }
}