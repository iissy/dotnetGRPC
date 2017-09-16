using Grpc.Core;
using System;
using Hello;
using System.Threading;
using System.Threading.Tasks;

namespace HelloService
{
    public class HelloServiceImpl : HelloSrv.HelloSrvBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine(request.Name);
            HelloReply response = new HelloReply();
            response.Message = request.Name + ", Hello world!";
            return Task.FromResult(response);
        }
    }
}