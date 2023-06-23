using Grpc.Core;
using Saludo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Saludo.SaludoService;

namespace Server
{
    internal class SaludosServiceImpl : SaludoServiceBase
    {
        public override Task<SaludoResponse> saludo(SaludoRequest request, ServerCallContext context)
        {
            string result = String.Format("Hola {0} desde C#",
                request.Nombre);

            return Task.FromResult(new SaludoResponse()
            {
                Result = result
            }); ;
        }
    }
}
