<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeFile="CourseList.aspx.cs" Inherits="SikshaNew.Admin.MasterCourse.List.CourseList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        CssClass="table table-bordered table-hover table-striped text-center align-middle"
        DataKeyNames="course_id"
        DataSourceID="SqlDataSource1"
        GridLines="None"
        HeaderStyle-BackColor="#343a40"
        HeaderStyle-ForeColor="White"
        HeaderStyle-Font-Bold="true"
        RowStyle-ForeColor="#212529"
        AlternatingRowStyle-BackColor="#f8f9fa">

        <AlternatingRowStyle BackColor="#F8F9FA" />

        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="course_id" HeaderText="course_id" InsertVisible="False" ReadOnly="True" SortExpression="course_id" />
            <asp:BoundField DataField="course_name" HeaderText="course_name" SortExpression="course_name" />
            <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />

            <asp:ImageField DataImageUrlField="course_thumbnail" HeaderText="Thumbnail"
                DataImageUrlFormatString="~/Admin/MasterCourse/MasterThumbnail/{0}"
                ControlStyle-Width="120px" ControlStyle-Height="80px"
                AlternateText="No image found">
                <ControlStyle Height="80px" Width="120px" />
            </asp:ImageField>
        </Columns>

        <HeaderStyle BackColor="#343A40" Font-Bold="True" ForeColor="White" />
        <RowStyle ForeColor="#212529" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConflictDetection="CompareAllValues"
        ConnectionString="<%$ ConnectionStrings:dbconn %>" 
        OldValuesParameterFormatString="original_{0}"
        SelectCommand="SELECT [course_id], [course_name], [status], [course_thumbnail] FROM [Mastercourse]" 
        InsertCommand="INSERT INTO [Mastercourse] ([course_name], [status], [course_thumbnail]) VALUES (@course_name, @status, @course_thumbnail)" 
        UpdateCommand="UPDATE [Mastercourse] SET [course_name] = @course_name, [status] = @status, [course_thumbnail] = @course_thumbnail WHERE [course_id] = @original_course_id AND [course_name] = @original_course_name AND [status] = @original_status AND (([course_thumbnail] = @original_course_thumbnail) OR ([course_thumbnail] IS NULL AND @original_course_thumbnail IS NULL))" 
        DeleteCommand="DELETE FROM [Mastercourse] WHERE [course_id] = @original_course_id AND [course_name] = @original_course_name AND [status] = @original_status AND (([course_thumbnail] = @original_course_thumbnail) OR ([course_thumbnail] IS NULL AND @original_course_thumbnail IS NULL))">

        <DeleteParameters>
            <asp:Parameter Name="original_course_id" Type="Int32" />
            <asp:Parameter Name="original_course_name" Type="String" />
            <asp:Parameter Name="original_status" Type="String" />
            <asp:Parameter Name="original_course_thumbnail" Type="String" />
        </DeleteParameters>

        <InsertParameters>
            <asp:Parameter Name="course_name" Type="String" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="course_thumbnail" Type="String" />
        </InsertParameters>

        <UpdateParameters>
            <asp:Parameter Name="course_name" Type="String" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="course_thumbnail" Type="String" />
            <asp:Parameter Name="original_course_id" Type="Int32" />
            <asp:Parameter Name="original_course_name" Type="String" />
            <asp:Parameter Name="original_status" Type="String" />
            <asp:Parameter Name="original_course_thumbnail" Type="String" />
        </UpdateParameters>

    </asp:SqlDataSource>

</asp:Content>