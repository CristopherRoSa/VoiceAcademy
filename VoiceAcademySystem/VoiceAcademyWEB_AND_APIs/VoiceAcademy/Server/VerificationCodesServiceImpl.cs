using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VerificationCode;
using static VerificationCode.VerificationCodeService;

namespace Server
{
    internal class VerificationCodesServiceImpl : VerificationCodeServiceBase
    {
        public override Task<VerificationCodeResponse> verificationCode(VerificationCodeRequest request, ServerCallContext context)
        {
            bool result = false;
            string smtpServer = "smtp-mail.outlook.com";
            int port = 587;
            string emailAddress = "voiceacademyuv@outlook.com";
            string password = "voiceAcademy@UV123";
            string addressee = request.Email;
            try
            {
                var mailMessage = new MailMessage(emailAddress, addressee, "Token de verificaion, VoiceAcademy", ("Este es tu token de verificaión: " + request.Code + "\n Por favor, si tu no reconoces este correo, haz caso omiso."))
                {
                    IsBodyHtml = true
                };
                var smtpClient = new SmtpClient(smtpServer)
                {
                    Port = port,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailAddress, password),
                    EnableSsl = true,
                };
                smtpClient.Send(mailMessage);
                result= true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error al enviar el correo electrónico " + ex.Message);
                result= false;
            }

            return Task.FromResult(new VerificationCodeResponse()
            {
                Result = result
            }); ;
        }
    }
}