using Microsoft.AspNetCore.Components.Forms;
using Refit;

namespace DynamicSun.Client.Extensions;
public static class ConvertToStreamPartExtension
{
    public static StreamPart ConvertToStreamPart(this IBrowserFile file)
    {
        var stream = file.OpenReadStream(file.Size);
        var streamPart = new StreamPart(stream, file.Name, file.ContentType);
        return streamPart;
    }
}