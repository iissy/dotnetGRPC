using Grpc.Core;
using Hello;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MicroClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Parallel.For(1, 10, Run);
            watch.Stop();
            long ms = watch.ElapsedMilliseconds;
            Console.WriteLine($"用时 {ms}ms");
            Console.Read();
        }

        static void Run(int i)
        {
            HelloRequest request = new HelloRequest();
            request.Name = i.ToString();
            HelloReply reply = new HelloReply();
            reply = GrpcClient.SayHello(request);
            Console.WriteLine(reply.Message);
        }

        private static Lazy<HelloSrv.HelloSrvClient> Connection = new Lazy<HelloSrv.HelloSrvClient>(() =>
        {
            var channel = new Channel(string.Format("{0}:{1}", "172.16.89.139", 50088), ChannelCredentials.Insecure);
            HelloSrv.HelloSrvClient client = new HelloSrv.HelloSrvClient(channel);
            return client;
        });

        private static HelloSrv.HelloSrvClient GrpcClient
        {
            get
            {
                return Connection.Value;
            }
        }
    }
}