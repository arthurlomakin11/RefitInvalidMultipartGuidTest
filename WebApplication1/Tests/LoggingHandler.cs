namespace WebApplication1.Tests;

public class LoggingHandler(HttpMessageHandler innerHandler) 
    : DelegatingHandler(innerHandler)
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        await request.Content!.ReadAsStringAsync(cancellationToken);
        
        return await base.SendAsync(request, cancellationToken);
    }
}