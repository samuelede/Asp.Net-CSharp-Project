﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentRegistration.aspx.cs" Inherits="Elearning.WebForm1" %>

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
            <asp:Label ID="lblRegister" runat="server" 
                style="z-index: 1; left: 98px; top: 40px; position: absolute" 
                Text="Student Registration" Font-Bold="True" Font-Size="Larger" 
                Font-Underline="True"></asp:Label>
    
            <asp:Label ID="lblUsername" runat="server" 
                style="z-index: 1; left: 102px; top: 77px; position: absolute; height: 19px" 
                Text="Username:"></asp:Label>
            <asp:Label ID="lblPassword" runat="server" 
                style="z-index: 1; left: 103px; top: 111px; position: absolute" 
                Text="Password:"></asp:Label>
            <asp:Label ID="lblConfirmPassword" runat="server" 
                style="z-index: 1; left: 50px; top: 148px; position: absolute" 
                Text="Confirm Password:"></asp:Label>
            <asp:Label ID="lblCourse" runat="server" 
                style="z-index: 1; left: 119px; top: 266px; position: absolute" 
                Text="Course:"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" 
                style="z-index: 1; left: 184px; top: 76px; position: absolute" 
                OnTextChanged="txtUsername_TextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" 
                style="z-index: 1; left: 184px; top: 111px; position: absolute" 
                TextMode="Password" OnTextChanged="txtPassword_TextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:TextBox ID="txtConfirmPassword" runat="server" 
                style="z-index: 1; left: 184px; top: 149px; position: absolute" 
                TextMode="Password" OnTextChanged="txtConfirmPassword_TextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:TextBox ID="txtRealName" runat="server" 
                style="z-index: 1; left: 184px; top: 184px; position: absolute" 
                OnTextChanged="txtRealName_TextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:TextBox ID="txtEmailAddress" runat="server" 
                style="z-index: 1; left: 184px; top: 225px; position: absolute" 
                OnTextChanged="txtEmailAddress_TextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:Label ID="lblEmailAddress" runat="server" 
                style="z-index: 1; left: 75px; top: 226px; position: absolute" 
                Text="Email Address:"></asp:Label>    
            <asp:DropDownList ID="ddlCourses" runat="server" 
                style="z-index: 1; left: 184px; top: 267px; position: absolute;">
            </asp:DropDownList>    
            <asp:Label ID="lblRealName" runat="server" 
                style="z-index: 1; left: 100px; top: 186px; position: absolute" 
                Text="Full Name:"></asp:Label>
            <asp:Button ID="btnRegister" runat="server" 
                style="z-index: 1; left: 184px; top: 314px; position: absolute" 
                Text="Register" onclick="btnRegister_Click" />
            <asp:Button ID="btnLogin" runat="server"  
                style="z-index: 1; left: 284px; top: 314px; position: absolute" Text="Login?" PostBackUrl="~/UserLogin.aspx"></asp:Button>     
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" 	ChildrenAsTriggers="False" UpdateMode="Conditional">
             <ContentTemplate>
                  <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                    style="z-index: 1; left: 184px; top: 360px; position: absolute"></asp:Label>
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" 	DynamicLayout="true">
                    <ProgressTemplate>
                         <p style="z-index: 1; left: 184px; top: 350px; position: absolute">Loading...</p>
                   </ProgressTemplate>
                </asp:UpdateProgress>

             </ContentTemplate>
             <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btnRegister" EventName="Click"/>
                 <asp:AsyncPostBackTrigger ControlID="txtUsername" EventName="TextChanged"/>
                 <asp:AsyncPostBackTrigger ControlID="txtPassword" EventName="TextChanged"/>
                 <asp:AsyncPostBackTrigger ControlID="txtConfirmPassword" EventName="TextChanged"/>
                 <asp:AsyncPostBackTrigger ControlID="txtRealName" EventName="TextChanged"/>
                 <asp:AsyncPostBackTrigger ControlID="txtEmailAddress" EventName="TextChanged"/>
             </Triggers>
        </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
