using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class dbconfig
{

    private SqlConnection con;
    public dbconfig()
    {
    }
    private void connect()
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        con.Open();
    }
    private void close()
    {
        con.Close();
    }
    public DataSet querySelect(String requete, Dictionary<string, object> param)
    {
        connect();
        SqlCommand sc = new SqlCommand(requete, con);
        if (param != null)
        {
            foreach (KeyValuePair<string, object> entry in param)
                sc.Parameters.AddWithValue(entry.Key, entry.Value);
        }
        SqlDataAdapter sda = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        close();
        return ds;
    }
    public void queryUpdate(String requete, Dictionary<string, object> param)
    {
        connect();
        SqlCommand sc = new SqlCommand(requete, con);
        if (param != null)
        {
            foreach (KeyValuePair<string, object> entry in param)
                sc.Parameters.AddWithValue(entry.Key, entry.Value);
        }
        sc.ExecuteNonQuery();
        close();
    }

    //public void update(string value, Dictionary<string, object> param)
    //{
    //    connect();
    //    SqlCommand sc = new SqlCommand(value, con);
    //    sc.ExecuteNonQuery();
    //    close();

    //}


}