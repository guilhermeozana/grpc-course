using Basics;
using Grpc.Core;
using System.Threading.Tasks;

namespace FirstGrpc.Services
{
    public interface IFirstService
    {
        Task<Response> Unary(Request request, ServerCallContext context);

        Task<Response> ClientStream(IAsyncStreamReader<Request> requestStream, ServerCallContext context);

        Task ServerStream(Request request, IServerStreamWriter<Response> responseStream, ServerCallContext context);

        Task BiDirectionalStream(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Response> responseStream, ServerCallContext context);
    }
}