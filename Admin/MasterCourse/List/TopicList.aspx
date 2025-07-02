<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TopicList.aspx.cs" Inherits="SikshaNew.Admin.MasterCourse.List.TopicList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
    DataKeyNames="topic_id"
    DataSourceID="SqlDataSource1"
    CssClass="table table-bordered table-hover table-striped text-center align-middle"
    GridLines="None"
    HeaderStyle-BackColor="#2C3E50"
    HeaderStyle-ForeColor="White"
    HeaderStyle-Font-Bold="true"
    RowStyle-ForeColor="#212529"
    AlternatingRowStyle-BackColor="#f8f9fa">

    <Columns>
        <asp:CommandField ShowEditButton="True" HeaderText="Edit" />

        <asp:BoundField DataField="topic_id" HeaderText="ID" ReadOnly="True" InsertVisible="False" SortExpression="topic_id" />
        <asp:BoundField DataField="Topic" HeaderText="Topic" SortExpression="Topic" />
        <asp:BoundField DataField="Mastercourse" HeaderText="Mastercourse" SortExpression="Mastercourse" />
        <asp:BoundField DataField="subcourse" HeaderText="Subcourse" SortExpression="subcourse" />

        <asp:HyperLinkField DataTextField="video_url"
                            DataNavigateUrlFields="video_url"
                            HeaderText="Video"
                            Target="_blank"
                            DataNavigateUrlFormatString="{0}"
                            Text="Watch Video"
                            ItemStyle-CssClass="text-primary fw-semibold" />

        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
    </Columns>
</asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:dbconn %>" DeleteCommand="DELETE FROM [Topic] WHERE [topic_id] = @original_topic_id AND (([Topic] = @original_Topic) OR ([Topic] IS NULL AND @original_Topic IS NULL)) AND (([Mastercourse] = @original_Mastercourse) OR ([Mastercourse] IS NULL AND @original_Mastercourse IS NULL)) AND (([subcourse] = @original_subcourse) OR ([subcourse] IS NULL AND @original_subcourse IS NULL)) AND (([video_url] = @original_video_url) OR ([video_url] IS NULL AND @original_video_url IS NULL)) AND (([Status] = @original_Status) OR ([Status] IS NULL AND @original_Status IS NULL))" InsertCommand="INSERT INTO [Topic] ([Topic], [Mastercourse], [subcourse], [video_url], [Status]) VALUES (@Topic, @Mastercourse, @subcourse, @video_url, @Status)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [topic_id], [Topic], [Mastercourse], [subcourse], [video_url], [Status] FROM [Topic]" UpdateCommand="UPDATE [Topic] SET [Topic] = @Topic, [Mastercourse] = @Mastercourse, [subcourse] = @subcourse, [video_url] = @video_url, [Status] = @Status WHERE [topic_id] = @original_topic_id AND (([Topic] = @original_Topic) OR ([Topic] IS NULL AND @original_Topic IS NULL)) AND (([Mastercourse] = @original_Mastercourse) OR ([Mastercourse] IS NULL AND @original_Mastercourse IS NULL)) AND (([subcourse] = @original_subcourse) OR ([subcourse] IS NULL AND @original_subcourse IS NULL)) AND (([video_url] = @original_video_url) OR ([video_url] IS NULL AND @original_video_url IS NULL)) AND (([Status] = @original_Status) OR ([Status] IS NULL AND @original_Status IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_topic_id" Type="Int32" />
            <asp:Parameter Name="original_Topic" Type="String" />
            <asp:Parameter Name="original_Mastercourse" Type="String" />
            <asp:Parameter Name="original_subcourse" Type="String" />
            <asp:Parameter Name="original_video_url" Type="String" />
            <asp:Parameter Name="original_Status" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Topic" Type="String" />
            <asp:Parameter Name="Mastercourse" Type="String" />
            <asp:Parameter Name="subcourse" Type="String" />
            <asp:Parameter Name="video_url" Type="String" />
            <asp:Parameter Name="Status" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Topic" Type="String" />
            <asp:Parameter Name="Mastercourse" Type="String" />
            <asp:Parameter Name="subcourse" Type="String" />
            <asp:Parameter Name="video_url" Type="String" />
            <asp:Parameter Name="Status" Type="String" />
            <asp:Parameter Name="original_topic_id" Type="Int32" />
            <asp:Parameter Name="original_Topic" Type="String" />
            <asp:Parameter Name="original_Mastercourse" Type="String" />
            <asp:Parameter Name="original_subcourse" Type="String" />
            <asp:Parameter Name="original_video_url" Type="String" />
            <asp:Parameter Name="original_Status" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>



</asp:Content>
