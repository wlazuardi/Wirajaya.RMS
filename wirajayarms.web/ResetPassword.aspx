<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="WirajayaRMS.Web.ResetPassword" %>

<!DOCTYPE html>
<html class="bg-black">
    <head id="Head1" runat="server">
        <meta charset="UTF-8">
        <title>Reset Password :: Wirajaya Recruitment Management System</title>
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
            <div class="header">Reset Password</div>
            <form id="form1" runat="server">
                <div class="body bg-gray">
                    <div class="form-group">
                        Hello, <asp:Literal ID="litUser" runat="server"></asp:Literal>. Please change your password and you'll be able to login with your new password
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"
                            Placeholder="Enter new password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            ValidationExpression="^.{6,}$" ErrorMessage="Password must be at least 6 characters" Display="Dynamic"
                            ValidationGroup="ChangePassword"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtConfNewPassword" runat="server" CssClass="form-control" TextMode="Password"
                                            Placeholder="Confirm password"></asp:TextBox>
                        <asp:CompareValidator ID="compConfNewPassword" runat="server" ControlToCompare="txtNewPassword"
                            ControlToValidate="txtConfNewPassword" ErrorMessage="Confirm password did not match" Display="Dynamic"
                            Operator="Equal" ValidationGroup="ChangePassword"></asp:CompareValidator>
                    </div>
                </div>
                <div class="footer">        
                    <asp:Label ID="lblError" ForeColor="Red" runat="server"></asp:Label>                                                  
                    <asp:Button ID="btnChangePass" CssClass="btn bg-olive btn-block" Text="Change Password" runat="server" OnClick="btnChangePass_Click"/>
                    <p><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx">Go to login page</asp:HyperLink></p>
                </div>
            </form>

            <%--<div class="margin text-center">
                <span>Sign in using social networks</span>
                <br/>nbutton>
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