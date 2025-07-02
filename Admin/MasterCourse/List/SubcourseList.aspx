<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeFile="SubcourseList.aspx.cs" Inherits="SikshaNew.Admin.MasterCourse.List.SubcourseList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--   Sort :   <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
     <asp:ListItem>Low to High</asp:ListItem>
     <asp:ListItem>High to Low</asp:ListItem>
 </asp:DropDownList>--%>



    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        DataKeyNames="subcourse_id"
        DataSourceID="SqlDataSource1"
        CssClass="table table-bordered table-hover table-striped text-center align-middle"
        GridLines="None"
        HeaderStyle-BackColor="#343a40"
        HeaderStyle-ForeColor="White"
        HeaderStyle-Font-Bold="true"
        RowStyle-ForeColor="#212529"
        AlternatingRowStyle-BackColor="#f8f9fa">

        <AlternatingRowStyle BackColor="#F8F9FA"></AlternatingRowStyle>

        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="Mastercourse" HeaderText="Mastercourse" SortExpression="Mastercourse" />
            <asp:BoundField DataField="subcourse_id" HeaderText="subcourse_id" ReadOnly="True" InsertVisible="False" SortExpression="subcourse_id" />
            <asp:BoundField DataField="subcourse_name" HeaderText="subcourse_name" SortExpression="subcourse_name" />
            <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
            <asp:BoundField DataField="subcourses_price" HeaderText="price" SortExpression="price" />

            <asp:ImageField DataImageUrlField="subcourse_thumbnail" HeaderText="Thumbnail"
                DataImageUrlFormatString="~{0}"
                ControlStyle-Width="120px" ControlStyle-Height="80px"
                AlternateText="No image found" />

        </Columns>

        <HeaderStyle BackColor="#343A40" Font-Bold="True" ForeColor="White"></HeaderStyle>

        <RowStyle ForeColor="#212529"></RowStyle>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbconn %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [Mastercourse], [subcourse_id], [subcourse_name], [status], [subcourses_price], [subcourse_thumbnail] FROM [subcourses]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [subcourses] WHERE [subcourse_id] = @original_subcourse_id AND (([Mastercourse] = @original_Mastercourse) OR ([Mastercourse] IS NULL AND @original_Mastercourse IS NULL)) AND [subcourse_name] = @original_subcourse_name AND [status] = @original_status AND (([price] = @original_price) OR ([subcourses_price] IS NULL AND @original_price IS NULL)) AND (([subcourse_thumbnail] = @original_subcourse_thumbnail) OR ([subcourse_thumbnail] IS NULL AND @original_subcourse_thumbnail IS NULL))" InsertCommand="INSERT INTO [subcourses] ([Mastercourse], [subcourse_name], [status], [subcourses_price], [subcourse_thumbnail]) VALUES (@Mastercourse, @subcourse_name, @status, @price, @subcourse_thumbnail)" UpdateCommand="UPDATE [subcourses] SET [Mastercourse] = @Mastercourse, [subcourse_name] = @subcourse_name, [status] = @status, [subcourses_price] = @price, [subcourse_thumbnail] = @subcourse_thumbnail WHERE [subcourse_id] = @original_subcourse_id AND (([Mastercourse] = @original_Mastercourse) OR ([Mastercourse] IS NULL AND @original_Mastercourse IS NULL)) AND [subcourse_name] = @original_subcourse_name AND [status] = @original_status AND (([subcourses_price] = @original_price) OR ([subcourses_price] IS NULL AND @original_price IS NULL)) AND (([subcourse_thumbnail] = @original_subcourse_thumbnail) OR ([subcourse_thumbnail] IS NULL AND @original_subcourse_thumbnail IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_subcourse_id" Type="Int32" />
            <asp:Parameter Name="original_Mastercourse" Type="String" />
            <asp:Parameter Name="original_subcourse_name" Type="String" />
            <asp:Parameter Name="original_status" Type="String" />
            <asp:Parameter Name="original_price" Type="Decimal" />
            <asp:Parameter Name="original_subcourse_thumbnail" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Mastercourse" Type="String" />
            <asp:Parameter Name="subcourse_name" Type="String" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="subcourses_price" Type="Decimal" />
            <asp:Parameter Name="subcourse_thumbnail" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Mastercourse" Type="String" />
            <asp:Parameter Name="subcourse_name" Type="String" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="price" Type="Decimal" />
            <asp:Parameter Name="subcourse_thumbnail" Type="String" />
            <asp:Parameter Name="original_subcourse_id" Type="Int32" />
            <asp:Parameter Name="original_Mastercourse" Type="String" />
            <asp:Parameter Name="original_subcourse_name" Type="String" />
            <asp:Parameter Name="original_status" Type="String" />
            <asp:Parameter Name="original_price" Type="Decimal" />
            <asp:Parameter Name="original_subcourse_thumbnail" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>




</asp:Content>
