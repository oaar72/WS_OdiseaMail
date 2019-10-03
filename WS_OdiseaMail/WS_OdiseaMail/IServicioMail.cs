using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WS_OdiseaMail
{
    [ServiceContract]
    public interface IServicioMail
    {

        [OperationContract]
        string sendMail(string destinatario, string asunto, string mensaje);
    }
}
