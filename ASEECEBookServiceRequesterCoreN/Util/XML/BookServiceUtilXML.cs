using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASEECEBookServiceModel.Models.XML;
using DALRESTFulUtilCore.HttpClientJSON;



namespace ASEECEBookServiceRequester.Util.XML
{
    public class BookServiceUtilXML
    {
        private string portnumber, hostname, servicepath;
        private string fullservicepath;


        public BookServiceUtilXML(string hname, string portno, string serpath)
        {
            portnumber = portno;
            if (portno.Equals(""))
                hostname = "http://" + hname + "/";
            else
                hostname = "http://" + hname + ":" + portno + "/";

            servicepath = serpath + "/";
            fullservicepath = hostname + servicepath;
        }
        //Her kommer din kode!!
    }
}
