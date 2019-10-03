using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WS_OdiseaMail.Data
{
    public class Mail
    {
        private string _SMTP;
        private string _contrasena;
        private string _usuario;
        private string _remitente;
        private bool _smtpRequiereSSL;
        private string _smtpPortSSL;
        private string[] _destinatario;
        private string[] _archivo;
        private string _asunto;
        private string _texto;
        private string msgError;

        public Mail()
        {
            _SMTP           = "";
            _contrasena     = "";
            _usuario        = "";
            _remitente      = "";
            _destinatario   = null;
            _archivo        = null;
            _asunto         = "";
            _texto          = "";
        }

        public Mail(string SMTP, string contrasena, string usuario, string remitente, bool smtpRequiereSSL, string smtpPortSSL, string destinatario, string[] archivo, string asunto, string texto)
        {
            char[] delimitador = new char[2];
            delimitador[0] = ',';
            delimitador[1] = ';';
            _SMTP = SMTP;
            _contrasena = contrasena;
            _usuario = usuario;
            _remitente = remitente;
            _smtpRequiereSSL = smtpRequiereSSL;
            _smtpPortSSL = smtpPortSSL;
            _destinatario = destinatario.Split(delimitador);
            _archivo = archivo;
            _asunto = asunto;
            _texto = texto;
        }

        public virtual string enviar()
        {
            string msgError = "";
            
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            correo.From = new System.Net.Mail.MailAddress(_remitente);

            foreach (string dest in _destinatario)
            {
                correo.To.Add(dest);
            }
            
            correo.Subject = _asunto;
            
            correo.Body = _texto;
            correo.IsBodyHtml = true;
            correo.Priority = System.Net.Mail.MailPriority.Normal;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = _SMTP;
            
            smtp.Credentials = new System.Net.NetworkCredential(_usuario, _contrasena);
            smtp.EnableSsl = _smtpRequiereSSL;
            smtp.Port = int.Parse(_smtpPortSSL);

            Attachment attach1;

            if (_archivo != null)
            {
                foreach (string arch in _archivo)
                {
                    try
                    {
                        attach1 = new Attachment(arch);
                        correo.Attachments.Add(attach1);
                    }
                    catch (Exception ex)
                    {
                        msgError += "Error al cargar alguno de los archivos. " + ex.StackTrace;
                        return msgError;
                    }
                }
            }

            try
            {
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                msgError = ex.Message + "  ---  " + ex.StackTrace;
            }

            return msgError;
        }

        public string SMTP
        {
            get
            {
                return _SMTP;
            }
            set
            {
                _SMTP = value;
            }
        }

        public string contrasena
        {
            get
            {
                return _contrasena;
            }
            set
            {
                _contrasena = value;
            }
        }

        public string usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
            }
        }

        public string remitente
        {
            get
            {
                return _remitente;
            }
            set
            {
                _remitente = value;
            }
        }

        public string[] destinatario
        {
            get
            {
                return _destinatario;
            }
            set
            {
                int i;
                _destinatario = value;
                for (i = 0; i <= _destinatario.Length - 1; i++)
                {
                    _destinatario[i] = _destinatario[i].Trim();
                }
            }
        }


        public string[] archivo
        {
            get
            {
                return _archivo;
            }
            set
            {
                _archivo = value;
            }
        }

        public string asunto
        {
            get
            {
                return _asunto;
            }
            set
            {
                _asunto = value;
            }
        }

        public string texto
        {
            get
            {
                return _texto;
            }
            set
            {
                _texto = value;
            }
        }

        public string validaCampoRequerido(string Texto, string Concepto)
        {
            msgError = "";

            if (string.IsNullOrEmpty(Texto))
            {
                msgError = "Se debe proporcionar " + Concepto + @".\n";
            }
            else if (Texto.Replace(" ", "") == "")
            {
                msgError = "Se debe proporcionar " + Concepto + @".\n";
            }
            return msgError;
        }
    }
}