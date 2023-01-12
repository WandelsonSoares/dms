using System;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections;


namespace Email
{

    public class csEmail
    {
        public void Enviar (string emailDestinatario, string emailCriador, string assunto, string mensagem)
        {
            try
            {
                //crio objeto responsável pela mensagem de email
                MailMessage objEmail = new MailMessage();

                //rementente do email
                objEmail.From = new MailAddress("naoresponda@dms.comau.com", "DMS - Comau");

                //email para resposta(quando o destinatário receber e clicar em responder, vai para:)
                //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

                //destinatário(s) do email(s). Obs. pode ser mais de um, pra isso basta repetir a linha
                //abaixo com outro endereço
                objEmail.To.Add(emailDestinatario);

                //com cópia
                objEmail.CC.Add(emailCriador);

                //para enviar com cópia oculta utilize a linha abaixo:
                //objEmail.Bcc.Add("oculto@provedor.com.br");

                //prioridade do email
                objEmail.Priority = MailPriority.Normal;

                //utilize true pra ativar html no conteúdo do email, ou false, para somente texto
                objEmail.IsBodyHtml = true;

                //Assunto do email
                objEmail.Subject = assunto;

                //corpo do email a ser enviado
                objEmail.Body = mensagem;

                //codificação do assunto do email para que os caracteres acentuados serem reconhecidos.
                objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

                //codificação do corpo do email para que os caracteres acentuados serem reconhecidos.
                objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                //cria o objeto responsável pelo envio do email
                SmtpClient objSmtp = new SmtpClient();

                //endereço do servidor SMTP(para mais detalhes leia abaixo do código)
                objSmtp.Host = "187.0.245.100";

                //para envio de email autenticado, coloque login e senha de seu servidor de email
                //para detalhes leia abaixo do código
                //objSmtp.Credentials = new NetworkCredential("login", "senha");

                //envia o email
                objSmtp.Send(objEmail);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Enviar2(ArrayList emailDestinatario, string emailCriador, string assunto, string mensagem)
        {
            try
            {
                //crio objeto responsável pela mensagem de email
                MailMessage objEmail = new MailMessage();

                //rementente do email
                objEmail.From = new MailAddress("naoresponda@dms.comau.com", "DMS - Comau");

                //email para resposta(quando o destinatário receber e clicar em responder, vai para:)
                //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

                //destinatário(s) do email(s). Obs. pode ser mais de um, pra isso basta repetir a linha
                //abaixo com outro endereço
                foreach (string email in emailDestinatario)
                {
                    objEmail.To.Add(email);
                }

                //com cópia
                objEmail.CC.Add(emailCriador);

                //para enviar com cópia oculta utilize a linha abaixo:
                //objEmail.Bcc.Add("wandelson.soares@comau.com");

                //prioridade do email
                objEmail.Priority = MailPriority.Normal;

                //utilize true pra ativar html no conteúdo do email, ou false, para somente texto
                objEmail.IsBodyHtml = true;

                //Assunto do email
                objEmail.Subject = assunto;

                //corpo do email a ser enviado
                objEmail.Body = mensagem;

                //codificação do assunto do email para que os caracteres acentuados serem reconhecidos.
                objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

                //codificação do corpo do email para que os caracteres acentuados serem reconhecidos.
                objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                //cria o objeto responsável pelo envio do email
                SmtpClient objSmtp = new SmtpClient();

                //endereço do servidor SMTP(para mais detalhes leia abaixo do código)
                objSmtp.Host = "187.0.245.100";

                //para envio de email autenticado, coloque login e senha de seu servidor de email
                //para detalhes leia abaixo do código
                //objSmtp.Credentials = new NetworkCredential("login", "senha");

                //envia o email
                objSmtp.Send(objEmail);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}