using System.Security.Cryptography;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Mail;

namespace VoiceAcademyAPI.Utility;

public static class Utilities
{
   
    public static byte[] HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return hashedBytes;//BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
    public static bool SendToken(string email, int code)
    {
        string smtpServer = "smtp-mail.outlook.com";
        int port = 587;
        string emailAddress = "voiceacademyuv@outlook.com";
        string password = "voiceAcademy@UV123";
        string addressee = email;
        var random = new Random();
        try
        {
            var mailMessage = new MailMessage(emailAddress, addressee, "Token de verificación, VoiceAcademy", ("Este es tu token de verificación: " + code + "\n Por favor, si no reconoces este correo, haz caso omiso."))
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
            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine("Se produjo un error al enviar el correo electrónico " + ex.Message);
            return false;
        }
    }

        public static void uploadSong()
    {
        string archivoAudio = "ruta/al/archivo/audio.mp3";
        string nombreArchivo = "nombre_del_archivo.mp3";
        string carpetaId = "id_de_la_carpeta_destino";

        string credentialsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Credentials", "credentials.json");

        UserCredential credential;
        using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                new[] { DriveService.Scope.Drive },
                "usuario",
                CancellationToken.None,
                new FileDataStore("ruta/a/tu/archivo/tokens.json", true)).Result;
        }

        // Crear el servicio de Google Drive
        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Nombre de tu aplicación"
        });

        // Subir archivo de audio
       /* var archivoMetadata = new File()
        {
            Name = nombreArchivo,
            Parents = new List<string> { carpetaId }
        };

        FilesResource.CreateMediaUpload request;
        using (var stream = new FileStream(archivoAudio, FileMode.Open))
        {
            request = service.Files.Create(
                archivoMetadata,
                stream,
                "audio/mpeg");
            request.Upload();
        }

        var archivoSubido = request.ResponseBody;

        Console.WriteLine("Archivo de audio subido: " + archivoSubido.Name);*/
    }
}