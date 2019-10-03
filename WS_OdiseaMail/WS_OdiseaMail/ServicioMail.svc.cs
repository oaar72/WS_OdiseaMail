using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;

namespace WS_OdiseaMail
{
    public class Service1 : IServicioMail
    {
        [WebMethod]
        public string sendMail(string destinatario, string asunto, string mensaje)
        {
            string msgError = "";



            return msgError;
        }
    }
}
