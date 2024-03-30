using MailKit.Net.Smtp;
using MediatrExample.Domain.Services;
using MediatrExample.Domain.ViewModels;
using MediatrExample.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;

namespace MediatrExample.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }

        public async Task EnviarEmail(DetalhesCavaleiroParaEmail detalhes)
        {
            MimeMessage mailMessage = ConstroiCorpoEmail(detalhes);

            using SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect(_emailConfiguration.Host, _emailConfiguration.Port, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
            smtpClient.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
            await smtpClient.SendAsync(mailMessage);
            smtpClient.Disconnect(true);

            Console.WriteLine($"Email enviado com sucesso para {_emailConfiguration.ReceiverEmailAddress}");
        }

        private MimeMessage ConstroiCorpoEmail(DetalhesCavaleiroParaEmail detalhes)
        {
            MimeMessage mailMessage = new MimeMessage();
            mailMessage.From.Add(MailboxAddress.Parse(_emailConfiguration.Username));
            mailMessage.To.Add(MailboxAddress.Parse(_emailConfiguration.ReceiverEmailAddress));
            mailMessage.Subject = "Cavaleiro pronto para a Guerra Santa!";
            mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = ConstroiHtmlEmail(detalhes)
            };

            return mailMessage;
        }

        private string ConstroiHtmlEmail(DetalhesCavaleiroParaEmail detalhes)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div style='margin: 0 20px 20px 10px;'><h2>Bem vindo Cavaleiro de Atena!</h2>");
            sb.AppendLine("<h3><i>A deusa da justiça requisita o seu poder para lutar ao lado dela.</i></h3></div>");
            sb.AppendLine("<div style='width: 100%; height: 400px; display: inline-block;'>");
            sb.AppendLine($"<p><img align='left' src='{detalhes.ImagemAsBase64}' style='height: 400px; width: 300px; margin: 0 20px 20px 10px;'/ >");

            sb.Append($"{detalhes.Nome}, espero que seus longos anos de treinamento em {detalhes.LocalDeTreinamento} ");
            sb.AppendLine($"tenham te transformado em um poderoso cavaleiro de {detalhes.Armadura} capaz de combater as forças do mal que estão por vir.");
            sb.AppendLine($"Temos certeza que com a proteção da sua constelação de {detalhes.Constelacao} nada irá te intimidar!");
            sb.AppendLine($"Use seu {detalhes.GolpePrincipal} para vencermos de uma vez por todas as tropas de Hades!");

            sb.AppendLine("<br /><br /><b><i>Esteja pronto, pois a próxima Guerra Santa se aproxima!!!</i></b></p></div>");

            return sb.ToString();
        }
    }
}
