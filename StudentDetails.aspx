<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDetails.aspx.cs" Inherits="Elearning.StudentDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Button ID="btnUserAccount" runat="server" PostBackUrl="~/UserAccount.aspx" 
        style="z-index: 1; left: 104px; top: 15px; position: absolute" 
        Text="User Account" />
    
    <div>
    
        <asp:Label ID="lblStudentDetails" runat="server" Font-Bold="True" 
            Font-Size="Larger" 
            style="z-index: 1; left: 100px; top: 48px; position: absolute" 
            Text="Student Details" Font-Underline="True"></asp:Label>
    
    </div>
    <asp:Label ID="lblCourse" runat="server" 
        style="z-index: 1; left: 102px; top: 90px; position: absolute" Text="Course:"></asp:Label>
    <asp:TextBox ID="txtCourse" runat="server" ReadOnly="True" 
        style="z-index: 1; left: 165px; top: 87px; position: absolute; width: 200px;"></asp:TextBox>
        
    
        
    <asp:Label ID="lblTutors" runat="server" 
        style="z-index: 1; left: 102px; top: 125px; position: absolute" 
        Text="Tutor(s) on your course:"></asp:Label>
        
    <asp:ListBox ID="lstTutors" runat="server"  
        style="z-index: 1; left: 102px; top: 159px; position: absolute; height: 72px; width: 180px">
    </asp:ListBox>
    
    <asp:Button ID="btnShowEmail" runat="server" 
        style="z-index: 1; left: 102px; top: 250px; position: absolute" 
        Text="Show Email" onclick="btnShowEmail_Click" AutoPostBack="true" />
    
        
    <asp:Label ID="lblModules" runat="server" 
        style="z-index: 1; left: 102px; top: 301px; position: absolute" 
        Text="Modules on your course:"></asp:Label>
    
    <div style="z-index: 1; left: 110px; top: 340px; position: absolute;">
    <asp:Repeater ID="rptModules" runat="server">
        <ItemTemplate>
        Module Code:
        <strong><%#Eval("ModuleCode") %></strong><br />
        Module Name:
        <strong><%#Eval("ModuleName") %></strong><br />
        </ItemTemplate>
        <SeparatorTemplate>
            <div style="width:300px;"><hr /></div>
        </SeparatorTemplate>
    </asp:Repeater>
               
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" 	ChildrenAsTriggers="False" UpdateMode="Conditional">
             <ContentTemplate>
                   <p>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" 
                            style="z-index: 1; left: 236px; top: 250px; position: absolute"></asp:Label>
                    </p>
                    <asp:Label ID="lblEmail" runat="server" Font-Italic="True" 
                        style="z-index: 1; left: 236px; top: 250px; position: absolute" 
                        Text="Email will appear here."></asp:Label>
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" 	DynamicLayout="true">
                    <ProgressTemplate>
                         <p style ="z-index: 1; left: 236px; top: 254px; position: absolute">Loading...</p>
                   </ProgressTemplate>
                </asp:UpdateProgress>

             </ContentTemplate>
             <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btnShowEmail" EventName="Click"/>
             </Triggers>
     </asp:UpdatePanel>
        
    <br />
    </form>
</body>
</html>
