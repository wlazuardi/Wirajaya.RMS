using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using WirajayaRMS.CrossCutting.Security;
using WirajayaRMS.CrossCutting.OptManagement;

namespace WirajayaRMS.Web
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DownloadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request.QueryString["name"] != null && request.QueryString["name"] != "")
            {
                string fileName = Rijndael.Decrypt(HttpUtility.UrlDecode(request.QueryString["name"]));
                HttpResponse response = HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                response.TransmitFile(SystemConfiguration.UploadDirectory + fileName);
                response.Flush();
                response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
