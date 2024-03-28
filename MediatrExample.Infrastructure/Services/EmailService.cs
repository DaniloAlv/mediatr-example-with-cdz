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
            sb.AppendLine("<p>Bem vindo Cavaleiro de Atena!</p>");
            sb.AppendLine("<p>A deusa da justiça requisita o seu poder para lutar ao lado dela.</p>");

            sb.Append($"<br/><p>{detalhes.Nome}, espero que seus longos anos de treinamento em {detalhes.LocalDeTreinamento} ");
            sb.AppendLine($"tenham te transformado em um poderoso cavaleiro de {detalhes.Armadura} capaz de combater as forças do mal que estão por vir.");
            sb.AppendLine($"Temos certeza que com a proteção da sua constelação de {detalhes.Constelacao} nada irá te intimidar!");
            sb.AppendLine($"Use seu {detalhes.GolpePrincipal} para vencermos de uma vez por todas as tropas de Hades!</p>");

            sb.AppendLine("<br /><p>Esteja pronto, pois a próxima Guerra Santa se aproxima!!!</p>");

            return sb.ToString();
        }
    }
}
