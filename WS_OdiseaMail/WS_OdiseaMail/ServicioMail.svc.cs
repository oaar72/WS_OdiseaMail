using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WS_OdiseaMail.Data;
using System.Web.Services;
using System.Configuration;

namespace WS_OdiseaMail
{
    public class Service1 : IServicioMail
    {
        [WebMethod]
        public string sendMail(string destinatario, string asunto, string mensaje)
        {
            string msgError = "";

            string smtp_server      = "";
            string smtp_user        = "";
            string smtp_pass        = "";
            string smtp_puertossl   = "";
            string smtp_ssl         = "";
            string remitenteMAIL    = "";

            try
            {
                smtp_server      = ConfigurationManager.AppSettings["smtp_server"].ToString();
                smtp_user        = ConfigurationManager.AppSettings["smtp_user"].ToString();
                smtp_pass        = ConfigurationManager.AppSettings["smtp_pass"].ToString();
                smtp_puertossl   = ConfigurationManager.AppSettings["smtp_sslport"].ToString();
                smtp_ssl         = ConfigurationManager.AppSettings["RequiereSSL"].ToString();
                remitenteMAIL    = ConfigurationManager.AppSettings["remitenteMAIL"].ToString();
            }
            catch(Exception e)
            {
                msgError = "Error en la configuración del servicio.";
            }

            if (msgError.Equals(""))
            {
                Mail mail = new Mail(smtp_server, 
                                     smtp_pass, 
                                     smtp_user, 
                                     remitenteMAIL, 
                                     bool.Parse(smtp_ssl), 
                                     smtp_puertossl,
                                     destinatario,
                                     null, 
                                     asunto, 
                                     mensaje);

                msgError = mail.enviar();
            }
            return msgError;
        }
    }
}
