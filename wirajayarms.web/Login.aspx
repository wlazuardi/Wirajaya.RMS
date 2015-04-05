<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WirajayaRMS.Web.Login" %>
<!DOCTYPE html>
<html class="bg-black">
    <head runat="server">
        <meta charset="UTF-8">
        <title>Log in :: Wirajaya Recruitment Management System</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
        
        <link rel="icon" type="image/png" href="img/wirajaya_packindo.png" />
        
        <!-- bootstrap 3.0.2 -->
        <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- font Awesome -->
        <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Theme style -->
        <link href="css/AdminLTE.css" rel="stylesheet" type="text/css" />

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
    </head>
    <body class="bg-black">

        <div class="form-box" id="login-box">
            <div class="header">Sign In</div>
            <form id="form1" runat="server">
                <div class="body bg-gray">
                    <div class="form-group">
                        <asp:RequiredFieldValidator ID="reqUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username required" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblErrUsername" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Username"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password required" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblErrPassword" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Password"></asp:TextBox>
                    </div>
                </div>
                <div class="footer">                                                          
                    <asp:Button ID="btnLogin" CssClass="btn bg-olive btn-block" Text="Sign In" runat="server" OnClick="btnLogin_Click"/>
                    
                    <p><asp:HyperLink runat="server" NavigateUrl="~/ForgotPassword.aspx">I forgot my password</asp:HyperLink></p>
                </div>
            </form>

            <%--<div class="margin text-center">
                <span>Sign in using social networks</span>
                <br/>
                <button class="btn bg-light-blue btn-circle"><i class="fa fa-facebook"></i></button>
                <button class="btn bg-aqua btn-circle"><i class="fa fa-twitter"></i></button>
                <button class="btn bg-red btn-circle"><i class="fa fa-google-plus"></i></button>

            </div>--%>
        </div>


        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="../../js/bootstrap.min.js" type="text/javascript"></script>        

    </body>
</html>