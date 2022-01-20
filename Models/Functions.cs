using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace url_shortner.Models
{
    public class Functions
    {
        // Check long url is present or not
        public static String CheckURL(String URLLink)
        {
            var param = new Dictionary<string, object>() { { "@OriginUrl", URLLink } };
            DataSet ds = new dbconfig().querySelect("SELECT * FROM links WHERE OriginUrl=@OriginUrl", param);
            return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["OriginUrl"].ToString() : null;
        }

        //get all the url present 
        public static DataTable GetAllURL()
        {
            //var param = new Dictionary<string, object>() { { "@OriginUrl", URLLink } };
            DataSet ds = new dbconfig().querySelect("SELECT * FROM links ",null);
            return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0] : null;
        }

        //Get the long url
        public static String GetOriginUrl(String ShortLink){
        var param = new Dictionary<string, object>() { { "@ShortLink", ShortLink }};
        DataSet ds= new dbconfig().querySelect("SELECT * FROM links WHERE ShortLink=@ShortLink",param);
        return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["OriginUrl"].ToString() : null;
	  }

       public static int GetCount(string url)
        {
            var param = new Dictionary<string, object>() { { "@OriginUrl", url } };
            DataSet ds = new dbconfig().querySelect("SELECT ClickCount FROM links WHERE OriginUrl=@OriginUrl", param);
            int count=0;
            if (ds.Tables[0].Rows[0]["ClickCount"].ToString() != "0")
            {
                count = Convert.ToInt32(ds.Tables[0].Rows[0]["ClickCount"]);
            }
            return count;

        }

        public static void UpdateCount(String url,int count)
        {
            //String ShortLink = generateShortLink();
            var param = new Dictionary<string, object>() { { "@OriginUrl", url } };
            
            new dbconfig().queryUpdate("update links set ClickCount = " + count + " where OriginUrl =@OriginUrl" ,param);
            
        }

        //add new record to data base
        public static String AddNewLinkToDB(String url){
          String ShortLink=generateShortLink();
          var param = new Dictionary<string, object>() { { "@OriginUrl", url }, { "@ShortLink", ShortLink } };
          new dbconfig().queryUpdate("INSERT INTO links (OriginUrl,ShortLink) VALUES (@OriginUrl,@ShortLink)", param);
          return ShortLink;
	  }

      //generate random string with length=5 and verify that it's not exist in the DB
	  public static String generateShortLink(){
        String ShortLink= RandomString(5);
        while (GetOriginUrl(ShortLink) != null)
        {
            ShortLink = RandomString(5);	 
		}
        return ShortLink;
	   }

      //generate random string with specific lenght
       public static string RandomString(int length)
       {
          Random random = new Random();
          const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
          return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
       }

       //check if a string is a valid url c#
       public static bool isValidUrl(String uriName)
       {
           Uri uriResult;
           bool result = Uri.TryCreate(uriName, UriKind.Absolute, out uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
           return result;
       }
     }
}