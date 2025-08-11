using Grpc.Core;

namespace FirstGrpc.Tests.Unit.Helpers;

public class TestServerCallContext : ServerCallContext
{
    private readonly Metadata _requestHeaders;
    private readonly CancellationToken _cancellationToken;
    
    public TestServerCallContext(Metadata requestHeaders, Metadata requestHeadersCore, Metadata responseTrailersCore, AuthContext authContextCore, IDictionary<object, object> userStateCore, CancellationToken cancellationToken = default)
    {
        _requestHeaders = requestHeaders;
        _cancellationToken = cancellationToken;
    }

    public TestServerCallContext()
    {
    }

    protected override Task WriteResponseHeadersAsyncCore(Metadata responseHeaders)
    {
        throw new NotImplementedException();
    }

    protected override ContextPropagationToken CreatePropagationTokenCore(ContextPropagationOptions? options)
    {
        throw new NotImplementedException();
    }

    protected override string MethodCore => "MethodName";
    protected override string HostCore => "HostName";
    protected override string PeerCore =>  "PeerName";
    protected override DateTime DeadlineCore { get; }
    protected override Metadata RequestHeadersCore { get; }
    protected override CancellationToken CancellationTokenCore { get; }
    protected override Metadata ResponseTrailersCore { get; }
    protected override Status StatusCore { get; set; }
    protected override WriteOptions? WriteOptionsCore { get; set; }
    protected override AuthContext AuthContextCore { get; }
    protected override IDictionary<object, object> UserStateCore { get; }
    
    public static ServerCallContext Create() => new TestServerCallContext();

}