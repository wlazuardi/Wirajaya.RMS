using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Diagnostics;

namespace WirajayaRMS.Web
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class PrintReport : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = HttpContext.Current.Request;
            string processURI = request.QueryString["processURI"];
            string args = string.Format("\"{0}\" - ", "http://localhost:5955/Transaksi/RequestReport.aspx?no=YNi6AYFISyYV7Rq1OEn7aPuibBNb0172a%2fAlo2pYz5I%3d&mid=eRjG7a9eImlIvwqxt4v2bg%3d%3d");
            var startInfo = new ProcessStartInfo(processURI, args)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            var proc = new Process { StartInfo = startInfo };
            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();
            byte[] buffer = proc.StandardOutput.CurrentEncoding.GetBytes(output);
            proc.WaitForExit();
            proc.Close();
            context.Response.ContentType = "application/pdf";
            context.Response.BinaryWrite(buffer);
            context.Response.End();
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
