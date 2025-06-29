<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddMCQ.aspx.cs" Inherits="SikshaNew.Admin.AddMCQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
    
     <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" />
     <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" />
     <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" />
     
    
     <asp:Button ID="Button1" runat="server" Text="Add Question" OnClick="Button1_Click" />
     <br /><br />

  
     <asp:Repeater ID="Repeater1" runat="server">
         <ItemTemplate>
             <table border="1" style="margin-bottom: 10px;">
                 <tr><td colspan="2"><b>Question #<%# Container.ItemIndex + 1 %></b></td></tr>
                 <tr><td>Question:</td>
                     <td><asp:TextBox ID="TextBox1" runat="server" Width="400" Text='<%# Eval("Question") %>' /></td></tr>
                 <tr><td>Option A:</td>
                     <td><asp:TextBox ID="TextBox2" runat="server" Width="300" Text='<%# Eval("OptionA") %>' /></td></tr>
                 <tr><td>Option B:</td>
                     <td><asp:TextBox ID="TextBox3" runat="server" Width="300" Text='<%# Eval("OptionB") %>' /></td></tr>
                 <tr><td>Option C:</td>
                     <td><asp:TextBox ID="TextBox4" runat="server" Width="300" Text='<%# Eval("OptionC") %>' /></td></tr>
                 <tr><td>Option D:</td>
                     <td><asp:TextBox ID="TextBox5" runat="server" Width="300" Text='<%# Eval("OptionD") %>' /></td></tr>
                 <tr><td>Correct Answer:</td>
                     <td>
                         <asp:DropDownList ID="DropDownList4" runat="server">
                             <asp:ListItem Text="Select" Value="" />
                             <asp:ListItem Text="A" Value="A" />
                             <asp:ListItem Text="B" Value="B" />
                             <asp:ListItem Text="C" Value="C" />
                             <asp:ListItem Text="D" Value="D" />
                         </asp:DropDownList>
                     </td></tr>
                 <tr><td colspan="2" align="right">
                     <asp:LinkButton ID="LinkButton1" runat="server" CommandName="DeleteRow" CommandArgument='<%# Container.ItemIndex %>' Text="Delete" ForeColor="Red" />
                 </td></tr>
             </table>
         </ItemTemplate>
     </asp:Repeater>
     <asp:Button ID="Button2" runat="server" Text="Save All" OnClick="Button2_Click" />
     <br /><br />
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" 
       HeaderStyle-BackColor="#d3d3d3" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" />

 </div>

</asp:Content>
