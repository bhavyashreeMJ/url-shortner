using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using url_shortner.Models;

namespace url_shortner
{
    public partial class Default : System.Web.UI.Page
    {
        DataTable dtListOfURl = new DataTable();
        int dtcounter;

        protected void Page_Load(object sender, EventArgs e)
        {
            dtListOfURl.Columns.Add("OriginUrl");
            dtListOfURl.Columns.Add("ShortLink");
            dtListOfURl.Columns.Add("ClickCount");

            if (!IsPostBack)
            {
                ViewState["Number"] = 0;
                ViewState["table"] = dtListOfURl;
                dtListOfURl = Functions.GetAllURL();
            }

            if (dtListOfURl != null)
            {
                if (dtListOfURl.Rows.Count > 0)
                {
                    grdURL.DataSource = dtListOfURl;
                    grdURL.DataBind();
                }
            }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        private void BindGrid(string shortURL, string LongURL)
        {

            try
            {

                dtListOfURl = (DataTable)ViewState["table"];
                dtcounter = Convert.ToInt32(ViewState["Number"]) + 1;
                ViewState["Number"] = dtcounter;
                DataRow dtrow = dtListOfURl.NewRow();
                dtrow[0] = dtcounter;
                dtrow["OriginUrl"] = LongURL;
                dtrow["ShortLink"] = shortURL;
                dtrow["ClickCount"] = dtcounter;
                dtListOfURl.Rows.Add(dtrow);
                dtListOfURl = Functions.GetAllURL();
                ViewState["table"] = dtListOfURl;
                grdURL.DataSource = dtListOfURl;
                grdURL.DataBind();


            }

            catch (Exception e)
            {
                Response.Redirect("~/");
                throw e;

            }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            if (Functions.isValidUrl(url.Value))
            {
                String originUrl = Functions.CheckURL(url.Value);
                if (originUrl != null)
                {
                    data_place.InnerHtml = "<div class='alert alert-danger'>url already entered</div>";
                    url.Value = null;
                }
                else
                {

                    String ShortLink = Functions.AddNewLinkToDB(url.Value);
                    String HostName = Request.Url.Host;
                    String domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

                    ShortLink = domainName + "/" + ShortLink;
                    BindGrid(ShortLink, url.Value);

                    url.Value = null;
                    data_place.InnerText = null;
                }

            }
            else
            {
                data_place.InnerHtml = "<div class='alert alert-danger'>try with a valid url</div>";
                url.Value = null;
            }


        }

        protected void btn_Refresh(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }
    }
}