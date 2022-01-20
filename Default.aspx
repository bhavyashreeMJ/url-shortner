<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="url_shortner.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>short link</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.13.0/css/all.min.css" integrity="sha256-h20CPZ0QyXlBuAw7A+KluUYx/3pK+c7lYEpqLTlxjYQ=" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">

    <style>
        input[type="url"]:focus, button:focus, .btn:focus, .btn:active {
            border-color: none;
            box-shadow: none;
            outline: 0 none;
        }

        .container {
            width: 50%;
            margin-top: 120px;
        }
        .td{

        }
        .capatcha-skip {
            background: #fff;
            padding: 20px;
            border-radius: 10px;
            border-top: 5px solid #3498DB;
        }

        .gridstyle {
            width: 300px;
            height: 300px;
            display: grid;
            grid-template-columns: 1fr minmax(auto, 20%);
        }

            .body {
              margin: 40px;
            }

            .wrapper {
              display: grid;
              grid-template-columns: 100px 100px 100px;
              grid-gap: 10px;
              background-color: #fff;
              color: #444;
              text-align:left;
              white-space:pre-wrap;
              display:grid;
              grid-template-columns: repeat(auto-fill, minmax(100px, 1fr)); 
              grid-gap: 1em;
              padding: 5px 5px 5px 5px;

            }

            .box {
              background-color: #444;
              color: #fff;
              border-radius: 5px;
              padding: 20px;
              font-size: 150%;
            }

</style>

</head>



<body>
    <form id="form1" runat="server" >
        <div class="container text-center">
            <h1>Shorten Your URL</h1>
            <%--</br>--%>
	
            <div class="input-group">
                <input type="url" id="url" class="form-control rounded-0" placeholder="http://example.com/example" style="padding: 12px" runat="server" />
                <span class="input-group-btn" style="width: 15%">
                    <asp:LinkButton ID="btnClick" Text="Shorten URL" class="btn btn-primary form-control rounded-0" runat="server" OnClick="btn_Click">
	        	</asp:LinkButton>
                </span>
                <span>
                    <asp:LinkButton ID="btnRefresh" Text="Refresh" class="btn btn-primary form-control rounded-0" runat="server" OnClick="btn_Refresh">
	        	</asp:LinkButton>
                </span>

            </div>
            </br> 
		 </br>
            <div style="align-items:center" >
                <asp:GridView ID="grdURL" runat="server" style="inline-size:fit-content "
                    class="table table-bordered table-condensed table-responsive table-hover" 
                    AutoGenerateColumns="false">

                    <Columns >
                        <asp:HyperLinkField HeaderText="Long URL" DataTextField="OriginUrl" />
                        <asp:HyperLinkField HeaderText="Short URL" DataTextField="ShortLink"
                            DataNavigateUrlFields="ShortLink" NavigateUrl="Eval('ShortLink')" />
                        <asp:BoundField HeaderText="Count" DataField="ClickCount" />
                    </Columns>
                </asp:GridView>
            </div>

            </br>
	
            <div id="data_place" style="margin-top: 15px;" runat="server"></div>
        </div>
    </form>
</body>
</html>
