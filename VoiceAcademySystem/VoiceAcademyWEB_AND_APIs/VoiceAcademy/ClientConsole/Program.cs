using Grpc.Core;
using Saludo;
using VerificationCode;

namespace Cliente
{
    class Program
    {
        const string target = "127.0.0.1:50051";

        static async Task Main(string[] args)
        {
            Channel channel = new Channel("localhost", 50051, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("Cliente conectado OK");
            });

            var client = new SaludoService.SaludoServiceClient(channel);
            saludar(client);

            var client2 = new VerificationCodeService.VerificationCodeServiceClient(channel);
            SendVerificationCode(client2);

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }

        public static void saludar(SaludoService.SaludoServiceClient client)
        {
            var request = new SaludoRequest()
            {
                Nombre = "Dan"
            };

            var response = client.saludo(request);

            Console.WriteLine(response.Result);
        }

        public static void SendVerificationCode(VerificationCodeService.VerificationCodeServiceClient client)
        {
            var request = new VerificationCodeRequest()
            {
                Email = "dansegura8863@gmail.com",
                Code = "4128"
            };

            var response = client.verificationCode(request);
            if (response.Result)
            {
                Console.WriteLine(response.Result + "El codigo de verificacion ha sido enviado con exito");
            }
            else
            {
                Console.WriteLine(response.Result + "Ha ocurrido un error al enviar el codigo de verificacion, intentelo mas tarde");
            }
        }
    }
}