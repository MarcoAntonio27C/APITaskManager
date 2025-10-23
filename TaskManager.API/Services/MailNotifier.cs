using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using TaskManager.API.Models;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace TaskManager.API.Services
{
    public class MailNotifier : INotifier
    {
        private readonly EmailServerOptions _emailServerOptions;

        public MailNotifier(IOptions<EmailServerOptions> options)
        {
            _emailServerOptions = options.Value;
        }

        public bool Send(Message<object> message)
        {
            try
            {
                string datosSerializados = "No hay detalles adicionales.";
                if (message.Data != null)
                {
                    datosSerializados = JsonSerializer.Serialize(message.Data,
                        new JsonSerializerOptions { WriteIndented = true });
                }

                string cuerpoEmail = $"Hola,\n\n" +
                                     $"La notificación sobre '{message.Subject}' ha sido generada.\n\n" +
                                     $"--- Detalles del Contenido ---\n" +
                                     $"{datosSerializados}\n\n" +
                                     $"Saludos,\nEl equipo de TaskManager.";


                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_emailServerOptions.SenderEmail));
                email.To.Add(MailboxAddress.Parse(message.Addressee));
                email.Subject = message.Subject;

                email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = cuerpoEmail
                };

                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect(_emailServerOptions.Host, _emailServerOptions.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    smtp.Authenticate(_emailServerOptions.SenderEmail, _emailServerOptions.ApiKey);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo a {message.Addressee}: {ex.Message}");
                return false;
            }
        }

    }
}
