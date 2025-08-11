using Grpc.Core;
using Grpc.Core.Interceptors;

namespace FirstGrpc.Interceptors;

public class ServerLoggingInterceptor : Interceptor
{
    private readonly ILogger<ServerLoggingInterceptor> _logger;
    
    public ServerLoggingInterceptor(ILogger<ServerLoggingInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            _logger.LogInformation("Server is executing UnaryServer method");
            
            return await continuation(request, context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error thrown by {context.Method}");
            throw;
        }
    }
}