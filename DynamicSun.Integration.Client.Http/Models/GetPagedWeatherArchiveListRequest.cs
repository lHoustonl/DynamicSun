namespace DynamicSun.Integration.Client.Http.Models;

public sealed class GetPagedAgentsListRequest : PageRequest
{
    public int Year { get; set; }
    public int Month { get; set; }
}