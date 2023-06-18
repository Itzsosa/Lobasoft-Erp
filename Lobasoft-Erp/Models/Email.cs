using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;

namespace Lobasoft_Erp.Models
{
    public class Email
    {
        public void EnviarCorreoRegistro(LBS_Usuario user)
        {
            try
            {
                // Se crea una instancia del objeto MailMessage
                MailMessage email = new MailMessage();

                // Asunto
                email.Subject = "Datos de registro en LobaSoft ERP";

                // Destinatarios
                email.To.Add(new MailAddress("lobasofterp@gmail.com")); // Dirección del correo del administrador
                email.To.Add(new MailAddress(user.U_correo)); // Dirección de correo del usuario

                // Emisor del correo
                email.From = new MailAddress("lobasofterp@gmail.com");


                // Contenido del email
                string html = "<html>";
                html += "<body>";
                html += "<h1>Bienvenido a Lobasoft ERP </h1>";
                html += "Estimado(a) " + user.U_nombreUsuario + ",<br><br>";
                html += "A continuación se detallan los datos registrados:";
                html += "<ul>";
                html += "<li><b>Nombre:</b> " + user.U_nombreUsuario + "</li>";
                html += "<li><b>Correo:</b> " + user.U_correo + "</li>";
                html += "<li><b>Contraseña:</b> " + user.U_contrasena + "</li>";
                html += "</ul>";
                html += "<p><b>No responda este correo porque fue generado de forma automática.</b></p>";
                html += "</body>";
                html += "</html>";

                // Se indica el contenido en HTML
                email.IsBodyHtml = true;
                email.Body = html;

                // Configuración del protocolo de comunicación SMTP
                SmtpClient smtp = new SmtpClient();

                // Servidor de correo a utilizar
                smtp.Host = "smtp.gmail.com";

                // Puerto de comunicación
                smtp.Port = 587;

                // Indica si se utiliza SSL
                smtp.EnableSsl = true;

                // Se indican las credenciales de autenticación
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("lobasofterp@gmail.com", "btqdqkapcucxagsl");

                // Método para enviar el email
                smtp.Send(email);

                // Se liberan las instancias de los objetos
                email.Dispose();
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                // Se retorna el error en caso de que exista
                throw ex;
            }
        }





    }
}
