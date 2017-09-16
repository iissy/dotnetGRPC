using Grpc.Core;
using Hello;
using HelloService;
using System.Threading.Tasks;
using Topshelf;

namespace MicroServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<HelloGrpcSrv>(s =>
                {
                    s.ConstructUsing(name => new HelloGrpcSrv());
                    s.WhenStarted(async tc => await tc.Start());
                    s.WhenStopped(async tc => await tc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("hello");
                x.SetDisplayName("hello");
                x.SetServiceName("hello");
            });
        }
    }

    public class HelloGrpcSrv
    {
        private Server server;

        public async Task Start()
        {
            server = new Server
            {
                Services = { HelloSrv.BindService(new HelloServiceImpl()) },
                Ports = { new ServerPort("192.168.1.76", 50088, ServerCredentials.Insecure) }
            };

            await Task.Run(() => {
                server.Start();
            });
        }

        public async Task Stop()
        {
            await server.ShutdownAsync();
        }
    }
}