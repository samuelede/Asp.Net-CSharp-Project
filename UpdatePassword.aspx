<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="Elearning.UpdatePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:Button ID="btnUserAccount" runat="server" PostBackUrl="~/UserAccount.aspx" 
        style="z-index: 1; left: 104px; top: 15px; position: absolute" 
        Text="User Account" />
     
     <asp:Label ID="lblUpdatePassword" runat="server" 
            style="z-index: 1; left: 102px; top: 56px; position: absolute" 
            Text="Update Password" Font-Bold="True" Font-Size="Larger" 
            Font-Underline="True"></asp:Label>
    
     <asp:Label ID="lblCurrentPassword" runat="server" 
        style="z-index: 1; left: 53px; top: 111px; position: absolute" 
        Text="Current Password:"></asp:Label>
    <asp:Label ID="lblNewPassword" runat="server" 
        style="z-index: 1; left: 72px; top: 148px; position: absolute" 
        Text="New Password"></asp:Label>
  
    <asp:TextBox ID="txtCurrentPassword" runat="server" 
        style="z-index: 1; left: 184px; top: 111px; position: absolute" 
        TextMode="Password" OnTextChanged="txtCurrentPassword_TextChanged" AutoPostBack="true"></asp:TextBox>
    <asp:TextBox ID="txtNewPassword" runat="server" 
        style="z-index: 1; left: 184px; top: 149px; position: absolute" 
        TextMode="Password" OnTextChanged="txtNewPassword_TextChanged" AutoPostBack="true"></asp:TextBox>
    
    
    <asp:Label ID="lblConfirmPassword" runat="server" 
        style="z-index: 1; left: 50px; top: 186px; position: absolute" 
            Text="Confirm Password:"></asp:Label>
    <asp:TextBox ID="txtConfirmPassword" runat="server" 
        style="z-index: 1; left: 184px; top: 188px; position: absolute" 
            TextMode="Password" OnTextChanged="txtConfirmPassword_TextChanged" AutoPostBack="true"></asp:TextBox>
        
        
        </div>
    <asp:Button ID="btnUpdatePassword" runat="server" 
        onclick="btnUpdatePassword_Click" 
        style="z-index: 1; left: 183px; top: 230px; position: absolute" 
        Text="Update Password" />
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
             <ContentTemplate>
                   <p>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                            style="z-index: 1; left: 185px; top: 275px; position: absolute"></asp:Label>
                    </p>
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" 	DynamicLayout="true">
                    <ProgressTemplate>
                         <p style="z-index: 1; left: 185px; top: 230px; position: absolute">Loading...</p>
                   </ProgressTemplate>
                </asp:UpdateProgress>

             </ContentTemplate>
             <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btnUpdatePassword" EventName="Click"/>
                 <asp:AsyncPostBackTrigger ControlID="txtCurrentPassword" EventName="TextChanged" />
                 <asp:AsyncPostBackTrigger ControlID="txtNewPassword" EventName="TextChanged"/>
                 <asp:AsyncPostBackTrigger ControlID="txtConfirmPassword" EventName="TextChanged"/>
             </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
