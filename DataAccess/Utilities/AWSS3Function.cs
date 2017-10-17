using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace DataAccess.Utilities
{
    public class AWSS3Function : IAWSS3Function
    {
        readonly string _bucketNameBckEmail = ConfigurationManager.AppSettings["S3BucketBckEmail"];
        readonly string _bucketName         = ConfigurationManager.AppSettings["S3BucketMaster"];
        readonly string _acessKey           = ConfigurationManager.AppSettings["S3AccessKey"];
        readonly string _secretKey          = ConfigurationManager.AppSettings["S3SecretKey"];
        readonly string _host               = ConfigurationManager.AppSettings["S3Host"];
        readonly IdeaFunction IdeaFunction  = new IdeaFunction();


        public EmailIdea LoadEmlS3(String key)
        {
            EmailIdea emailIdea = new EmailIdea();

            try
            {
                using (Chilkat.Http http = new Chilkat.Http())
                {
                    try
                    {
                        bool success;

                        success = http.UnlockComponent("GVPCOMHttp_kmBYNa7xOH4u");
                        if (success != true)
                        {
                            Debug.WriteLine(http.LastErrorText);
                            return emailIdea;
                        }

                        http.AwsAccessKey = _acessKey;
                        http.AwsSecretKey = _secretKey;

                        int retval = http.S3_FileExists($"{_bucketNameBckEmail}", $"7048/{key}");
                        if (retval < 0)
                        {
                            Debug.WriteLine("Falha na verificação do arquivo no S3");
                            Debug.WriteLine(http.LastErrorText);
                            return emailIdea;
                        }

                        if (retval == 0)
                        {
                            Debug.WriteLine("O arquivo não existe no S3.");
                            return emailIdea;
                        }

                        byte[] res = http.S3_DownloadBytes($"{_bucketNameBckEmail}", $"7048/{key}");

                        /* ---- */
                        /* MIME */
                        /* ---- */

                        Chilkat.Mime mime = new Chilkat.Mime();

                        success = mime.UnlockComponent("GVPCOMSMIME_2Cwt37t36W8s");
                        if (success != true)
                        {
                            Console.WriteLine(mime.LastErrorText);
                            return emailIdea;
                        }

                        mime.LoadMimeBytes(res);

                        Debug.WriteLine("---- GetMime ----");
                        Debug.WriteLine(mime.GetMime());

                        /* ------ */
                        /* E-MAIL */
                        /* ------ */

                        Chilkat.Email email = new Chilkat.Email();

                        success = email.SetFromMimeBytes(res);

                        if (!success)
                        {
                            Debug.WriteLine("Não foi possivel converter o arquivo MIME.");
                            return emailIdea;
                        }

                        /* ----------- */
                        /* E-MAIL IDEA */
                        /* ----------- */
                        if (email.FromAddress == null)
                        {
                            Debug.WriteLine($"O E-Mail de From :: {email.From} / Assunto :: {email.Subject} / Data :: {email.LocalDate} não possui o Address");
                            return emailIdea;
                        }

                        emailIdea.GuidEmail         = email.GetHeaderField("MESSAGE-ID");
                        emailIdea.DataRecebimento   = email.LocalDate;
                        emailIdea.Remetente         = IdeaFunction.TratarRemetente(email);
                        emailIdea.Destinatario      = IdeaFunction.TratarDestinatarios(email);
                        emailIdea.Copia             = IdeaFunction.TratarDestinatarios(email);
                        emailIdea.CopiaOculta       = IdeaFunction.TratarDestinatarios(email);
                        emailIdea.Assunto           = (!string.IsNullOrWhiteSpace(email.Subject.Trim())) ? email.Subject.Trim().Replace("'", "''") : "[SEM ASSUNTO]";
                        emailIdea.Prioridade        = IdeaFunction.ConvertPrioridade(email.GetHeaderField("X-PRIORITY"));

                        emailIdea.Idc               = 2758;
                        emailIdea.Tipo              = 1;
                        emailIdea.IdEmailConta      = 0;
                        emailIdea.EmailConta        = email.FromAddress;

                        /* ---------------------------------------------- */
                        /* E-MAIL IDEA - ANEXOS / MENOS MESSAGENS ANEXADO */
                        /* ---------------------------------------------- */

                        var anexos              = new List<Anexo>();
                        var anexosRelacionados  = new List<AnexoRelacionado>();

                        Debug.WriteLine("Number of attachments = " + Convert.ToString(email.NumAttachments));

                        for (var i = 0; i < email.NumAttachments; i++)
                        {
                            var type = email.GetAttachmentContentType(i);

                            if (type.ToLower() == "message/rfc822")
                            {
                                continue;
                            }

                            var strNome = IdeaFunction.RetornaNome(email.GetAttachmentFilename(i));

                            var ext     = Path.GetExtension(strNome);
                            var name    = Path.GetFileNameWithoutExtension(strNome);

                            if (strNome.Length > 100)
                                strNome = $"{ name.Substring(0, 90)}...{ext}";

                            anexos.Add(new Anexo
                            {
                                Arquivo     = new MemoryStream(email.GetAttachmentData(i)),
                                Nome        = strNome,
                                Tamanho     = email.GetAttachmentSize(i)
                            });

                            Debug.WriteLine("---- Attachment " + Convert.ToString(i));

                            //  Examine the filename (if any)
                            Debug.WriteLine("filename: " + email.GetAttachmentFilename(i));
                            //  Examine the content-ID (if any)
                            Debug.WriteLine("Content-ID: " + email.GetAttachmentContentID(i));
                            //  Examine the content-type
                            Debug.WriteLine("Content-Type: " + email.GetAttachmentContentType(i));
                            //  Examine the content-disposition
                            Debug.WriteLine("Content-Disposition" + email.GetAttachmentHeader(i, "content-disposition"));
                            //  Examine the attachment size:
                            Debug.WriteLine("Size (in bytes) of the attachment: " + Convert.ToString(email.GetAttachmentSize(i)));
                        }

                        Debug.WriteLine("--");

                        /* ------------------------------- */
                        /* E-MAIL IDEA - ANEXOS / NO CORPO */
                        /* ------------------------------- */
                   
                        Debug.WriteLine("Number of related items = " + Convert.ToString(email.NumRelatedItems));

                        for (var i = 0; i < email.NumRelatedItems; i++)
                        {
                            anexosRelacionados.Add(new AnexoRelacionado
                            {
                                Index                   = i,
                                GetRelatedContentID     = email.GetRelatedContentID(i),
                                GetRelatedFilename      = email.GetRelatedFilename(i)
                            });

                            Debug.WriteLine("---- Related Item " + Convert.ToString(i));

                            //  Examine the filename (if any)
                            Debug.WriteLine("filename: " + email.GetRelatedFilename(i));
                            //  Examine the content-ID (if any)
                            Debug.WriteLine("Content-ID: " + email.GetRelatedContentID(i));
                            //  Examine the content-type
                            Debug.WriteLine("Content-Type: " + email.GetRelatedContentType(i));
                            //  Examine the content-location (if any)
                            Debug.WriteLine("Content-Location" + email.GetRelatedContentLocation(i));
                        }

                        Debug.WriteLine("--");

                        /* -------------------------------------------- */
                        /* E-MAIL IDEA - ANEXOS / COM MESSAGENS ANEXADO */
                        /* -------------------------------------------- */
                       
                        Debug.WriteLine("Number of attached messages = " + Convert.ToString(email.NumAttachedMessages));
                        for (var i = 0; i < email.NumAttachedMessages; i++)
                        {
                            var file        = email.GetAttachedMessage(i).GetAttachmentData(i);
                            var filename    = $"Mensagem_{i}.eml";

                            if (file.Length > 0)
                            {
                                anexos.Add(new Anexo
                                {
                                    Arquivo     = new MemoryStream(file),
                                    Nome        = filename,
                                    Tamanho     = file.Length
                                });
                            }

                            Debug.WriteLine("---- Attached message " + Convert.ToString(i));

                            //  Examine the attached email
                            Chilkat.Email em = email.GetAttachedMessage(i);
                            Debug.WriteLine("from: " + em.From);
                            Debug.WriteLine("subject: " + em.Subject);

                            i = i + 1;
                        }

                        Debug.WriteLine("--");


                        emailIdea.Anexos = anexos;
                        emailIdea.AnexosRelacionado = anexosRelacionados;

                        return emailIdea;

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Erro :: {ex.InnerException}");
                        return emailIdea;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro :: {ex.InnerException}");
                return emailIdea;
            }
        }
    }
}