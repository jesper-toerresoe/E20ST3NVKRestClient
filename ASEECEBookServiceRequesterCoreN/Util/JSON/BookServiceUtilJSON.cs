using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASEECEBookServiceModel.Models.JSON;
using DALRESTFulUtilCore.HttpClientJSON;

namespace ASEECEBookServiceRequester.Util.JSON
{
    public class BookServiceUtilJSON
    {
        private string portnumber, hostname, servicepath;
        private string fullservicepath;


        public BookServiceUtilJSON(string hname, string portno, string serpath)
        {
            portnumber = portno;
            if (portno.Equals(""))
                hostname = "https://" + hname + "/";
            else
                hostname = "https://" + hname + ":" + portno + "/";

            servicepath = serpath + "/";
            fullservicepath = hostname + servicepath;
        }


        /*
         * Book metoder HER KOMMER DIN KODE
         */

        //public List<Book> getSimpleBooks()
        //{

        //  //Her kommer din kode

        //}

        //public Book DeleteBook(Book book) //Sletter et  Book objekt på BookService Web API
        //{
        //    //Her kommer din kode 
        //}
        //public BookCollection GetBooks()
        //{
        //    //Her kommer din kode 
        //}

        /*
         * Her kommer alle de følgende CRUD service metoder for Book og Author
         */

        /*
     * Author metoder 
     */

    }
}
