<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="Elearning.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Label ID="lblUsername" runat="server" Text="Username:" 
                style="z-index: 1; top: 88px; position: absolute; left: 97px; " width="64"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" 
                style="z-index: 1; left: 181px; top: 88px; position: absolute" 
                OnTextChanged="txtUsername_TextChanged" AutoPostBack="true"></asp:TextBox>
        
            <asp:Label ID="lblPassword" runat="server" Text="Password:" 
                style="z-index: 1; left: 97px; top: 129px; position: absolute"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" 
                style="z-index: 1; left: 181px; top: 128px; position: absolute" 
                TextMode="Password" OnTextChanged="txtPassword_TextChanged" AutoPostBack="true"></asp:TextBox>
        
            <asp:Label ID="lblLoginPortal" runat="server" Font-Bold="True" 
                Font-Size="Larger" style="z-index: 1; left: 97px; top: 40px; position: absolute" 
                Text="Login Portal" Font-Underline="True"></asp:Label>
            <p>     
                <asp:Button ID="btnLogin" runat="server" Text="Login" 
                    style="z-index: 1; left: 181px; top: 173px; position: absolute" 
                    onclick="btnLogin_Click" />      
            </p>
            <asp:Label ID="lblRegister" runat="server" 
                style="z-index: 1; left: 143px; top: 260px; position: absolute" 
                Text="Student not registered?" ForeColor="#33CC33"></asp:Label>
        
            <asp:Button ID="btnRegister" runat="server" 
            style="z-index: 1; left: 293px; top: 260px; position: absolute" 
            Text="Register" PostBackUrl="~/StudentRegistration.aspx" />
            <asp:Label ID="lblUpdateSuccess" runat="server" ForeColor="Green" 
                    style="z-index: 1; left: 143px; top: 218px; position: absolute"></asp:Label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" 	ChildrenAsTriggers="False" UpdateMode="Conditional">
             <ContentTemplate>                  
                <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                    style="z-index: 1; left: 143px; top: 218px; position: absolute"></asp:Label>
                 <asp:Label ID="lblUsernameError" runat="server" ForeColor="Red" 
                    style="z-index: 1; left: 353px; top: 88px; position: absolute"></asp:Label>
                 <asp:Label ID="lblPasswordError" runat="server" ForeColor="Red" 
                    style="z-index: 1; left: 353px; top: 128px; position: absolute"></asp:Label>
                <p>&nbsp;</p>
                
             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click"/>
                <asp:AsyncPostBackTrigger ControlID="txtUsername" EventName="TextChanged"/>
                <asp:AsyncPostBackTrigger ControlID="txtPassword" EventName="TextChanged"/>
             </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
