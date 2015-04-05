<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="WirajayaRMS.Web.ForgotPassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<!DOCTYPE html>
<html class="bg-black">
    <head id="Head1" runat="server">
        <meta charset="UTF-8">
        <title>Forgot Password :: Wirajaya Recruitment Management System</title>
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
        <form id="form1" runat="server">
            <asp:Panel ID="pnlSearchEmail" runat="server" class="form-box">
                <div class="header">Forgot Password</div>
                <div class="body bg-gray">
                    <div class="form-group">
                        We will send the instruction to reset your password via e-mail. Please fill your e-mail address.
                    </div>
                    <div class="form-group">
                        <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="E-mail address required" Display="Dynamic" ValidationGroup="SearchUser"></asp:RequiredFieldValidator>                        
                        <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Invalid e-mail format" Display="Dynamic" 
                                ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ValidationGroup="SearchUser"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter your e-mail address"></asp:TextBox>
                    </div>
                </div>
                <div class="footer">                                                          
                    <asp:Button ID="btnNext" CssClass="btn bg-olive btn-block" Text="Next" runat="server" ValidationGroup="SearchUser" OnClick="btnNext_Click"/>                    
                    <asp:LinkButton ID="lbBack" CssClass="btn bg-olive btn-block" Text="Back" runat="server" PostBackUrl="~/Login.aspx"></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlSendInstruction" runat="server" class="form-box" Visible="false">
                <div class="header">Forgot Password</div>
                <div class="body bg-gray">
                    <div class="form-group">
                        Search result
                    </div>
                    <div class="form-group">
                        <asp:Literal ID="litSearchResult" runat="server"></asp:Literal>
                    </div>
                    <div class="form-group">
                        <asp:RadioButtonList ID="rblUser" runat="server"/>
                        <asp:CustomValidator ID="cvUser" runat="server" ValidationGroup="SendInstruction"
                                    ClientValidationFunction="ValidateUser" ErrorMessage="Please choose one user"
                                    Display="Dynamic">
                                </asp:CustomValidator>

                                <script type="text/javascript">
                                    // javascript to add to your aspx page
                                    function ValidateUser(source, args) {
                                        args.IsValid = false;
                                        $('#<%=rblUser.ClientID%> input[type=radio]').each(function(i, v) {
                                            if ($(v).prop('checked')) {
                                                args.IsValid = true;
                                                return false;
                                            }
                                        });
                                        return;
                                    }
                                </script>
                    </div>
                    <div class="form-group">
                        <asp:RequiredFieldValidator ID="reqCaptcha" runat="server" ControlToValidate="txtCapthca" ErrorMessage="Verification code required" Display="Dynamic" ValidationGroup="SendInstruction"></asp:RequiredFieldValidator>                        
                        <cc1:CaptchaControl ID="ccVerification" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                            CaptchaHeight="60" CaptchaWidth="200" CaptchaLineNoise="Low" CaptchaMinTimeout="5" 
                                            CaptchaMaxTimeout="240" FontColor = "#529E00" />
                        <asp:TextBox ID="txtCapthca" runat="server" CssClass="form-control" Placeholder="Enter the shown code above"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <div class="footer">                                                          
                    <asp:Button ID="btnSendInstruction" CssClass="btn bg-olive btn-block" Text="Send Instruction" runat="server" ValidationGroup="SendInstruction" OnClick="btnSendInstruction_Click"/>
                    <asp:Button ID="btnBack" CssClass="btn bg-olive btn-block" Text="Back" runat="server" OnClick="btnBack_Click"/>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlSuccess" runat="server" class="form-box" Visible="false">
                <div class="header">Forgot Password</div>
                <div class="body bg-gray">
                    <div class="form-group">
                        Reset password instruction was successfully sent to your e-mail
                    </div>
                </div>
                <div class="footer">                                                                              
                    <asp:LinkButton CssClass="btn bg-olive btn-block" Text="Back to Login Page" runat="server" PostBackUrl="~/Login.aspx"></asp:LinkButton>
                </div>
            </asp:Panel>
        </form>


        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="../../js/bootstrap.min.js" type="text/javascript"></script>        

    </body>
</html>