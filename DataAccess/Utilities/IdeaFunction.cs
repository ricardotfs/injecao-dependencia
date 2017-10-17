using Chilkat;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataAccess.Utilities
{
    public class IdeaFunction : IIdeaFunction
    {
        public TipoPrioridade ConvertPrioridade(string prioridade)
        {
            if (prioridade == null)
            {
                return TipoPrioridade.Normal;
            }

            switch (prioridade.ToUpper())
            {
                case "LOW":
                    return TipoPrioridade.Baixa;
                case "NORMAL":
                    return TipoPrioridade.Normal;
                case "HIGH":
                    return TipoPrioridade.Alta;
                default:
                    return TipoPrioridade.Normal;
            }
        }

        public Cliente TratarRemetente(Email email)
        {
            Debug.WriteLine("Tratando dos Remetentes do e-mail.");

            var replayToValido = false; //conta.UsaReplyTo != 0 && !string.IsNullOrEmpty(email.ReplyTo);

            var address = (replayToValido)
                            ? email.ReplyTo.Replace("'", string.Empty).Replace("\"", string.Empty)
                            : email.FromAddress.Replace("'", string.Empty).Replace("\"", string.Empty);

            var email1      = Util.LimparEndEmail(address);
            var emailValido = Util.IsValid(email1);


            if (!emailValido && replayToValido)
                Debug.WriteLine($"E-mail configurado no replyTo não é valido: {address}");
                //NovaAuditoria(emailIdea, $"E-mail configurado no replyTo não é valido: {address}");

            Debug.WriteLine($"Remetente do e-mail - {email1}");

            return new Cliente
            {
                Nome = email.FromName.Replace("'", string.Empty).Replace("\"", string.Empty),
                Email = !emailValido && replayToValido
                            ? email.FromAddress.Replace("'", string.Empty).Replace("\"", string.Empty)
                            : email1,
            };
        }

        public List<Cliente> TratarDestinatarios(Email email)
        {
            Debug.WriteLine("Tratando dos destinatários do e-mail.");

            var dests = new List<Cliente>();

            if (email.NumTo.Equals(0))
            {
                return dests;
            }

            for (var x = 0; x < email.NumTo; x++)
            {
                dests.Add(new Cliente
                {
                    Email   = email.GetTo(x).Replace("'", string.Empty).Replace("\"", string.Empty),
                    Nome    = email.GetToName(x).Replace("'", string.Empty).Replace("\"", string.Empty)
                });
            }

            Debug.WriteLine("Total de destinatários do e-mail - {0}", dests.Count);

            return dests;
        }

        public List<Cliente> TratarDestinatariosCC(Email email)
        {
            Debug.WriteLine("Tratando dos destinatários (CC) do e-mail.");
            var dests = new List<Cliente>();

            if (email.NumCC.Equals(0))
            {
                return dests;
            }

            for (var x = 0; x < email.NumCC; x++)
            {
                dests.Add(new Cliente
                {
                    Email   = email.GetCC(x).Replace("'", string.Empty).Replace("\"", string.Empty),
                    Nome    = email.GetCcName(x).Replace("'", string.Empty).Replace("\"", string.Empty)
                });
            }

            Debug.WriteLine("Total de destinatários (CC) do e-mail - {0}", dests.Count);

            return dests;
        }

        public List<Cliente> TratarDestinatariosCCo(Email email)
        {
            Debug.WriteLine("Tratando dos destinatários (CCO) do e-mail.");
            var dests = new List<Cliente>();

            if (email.NumBcc.Equals(0))
            {
                return dests;
            }

            for (var x = 0; x < email.NumBcc; x++)
            {
                dests.Add(new Cliente
                {
                    Email   = email.GetBcc(x).Replace("'", string.Empty).Replace("\"", string.Empty),
                    Nome    = email.GetBccName(x).Replace("'", string.Empty).Replace("\"", string.Empty)
                });
            }

            Debug.WriteLine("Total de destinatários (CCO) do e-mail - {0}", dests.Count);

            return dests;
        }

        public string RetornaNome(string assunto)
        {
            if (string.IsNullOrEmpty(assunto)) { return string.Empty; }

            var singles = new[] { @"/", @"\", @":", @"*", @"?", @"<", @">", @"|", @",", @" " };

            assunto = singles.Aggregate(assunto, (current, s) => current.Replace(s, "_"));
            return assunto;
        }
    }
}
