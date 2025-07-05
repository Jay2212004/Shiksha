<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TopicList.aspx.cs" Inherits="SikshaNew.Admin.MasterCourse.List.TopicList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* GridView Header Styling */
        .grid-header th {
            background-color: #2C3E50 !important;
            color: white !important;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
        }

        /* GridView Row Styling */
        .grid-row td {
            vertical-align: middle;
            text-align: center;
            color: #212529;
        }

        .grid-alt-row td {
            background-color: #f8f9fa;
        }

        /* Edit Link Styling */
        .grid-edit-btn {
            color: #3498db !important;
            font-weight: 600;
            text-decoration: underline;
        }

        /* Hyperlink for Watch Video */
        .grid-video-link {
            color: #007bff;
            font-weight: 600;
        }

        /* Table Hover */
        .table-hover tbody tr:hover {
            background-color: #eaf2f8;
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="text-center mb-4">Topic List</h2>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        DataKeyNames="topic_id"
        DataSourceID="SqlDataSource1"
        CssClass="table table-bordered table-hover table-striped text-center align-middle"
        GridLines="None"
        HeaderStyle-CssClass="grid-header"
        RowStyle-CssClass="grid-row"
        AlternatingRowStyle-CssClass="grid-alt-row">

        <Columns>
            <asp:CommandField ShowEditButton="True" HeaderText="Edit" ButtonType="Link" ControlStyle-CssClass="grid-edit-btn" />

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
                ItemStyle-CssClass="grid-video-link" />

            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConflictDetection="CompareAllValues"
        ConnectionString="<%$ ConnectionStrings:dbconn %>"
        SelectCommand="SELECT [topic_id], [Topic], [Mastercourse], [subcourse], [video_url], [Status] FROM [Topic]"
        InsertCommand="INSERT INTO [Topic] ([Topic], [Mastercourse], [subcourse], [video_url], [Status]) VALUES (@Topic, @Mastercourse, @subcourse, @video_url, @Status)"
        UpdateCommand="UPDATE [Topic] SET [Topic] = @Topic, [Mastercourse] = @Mastercourse, [subcourse] = @subcourse, [video_url] = @video_url, [Status] = @Status WHERE [topic_id] = @original_topic_id AND (([Topic] = @original_Topic) OR ([Topic] IS NULL AND @original_Topic IS NULL)) AND (([Mastercourse] = @original_Mastercourse) OR ([Mastercourse] IS NULL AND @original_Mastercourse IS NULL)) AND (([subcourse] = @original_subcourse) OR ([subcourse] IS NULL AND @original_subcourse IS NULL)) AND (([video_url] = @original_video_url) OR ([video_url] IS NULL AND @original_video_url IS NULL)) AND (([Status] = @original_Status) OR ([Status] IS NULL AND @original_Status IS NULL))"
        DeleteCommand="DELETE FROM [Topic] WHERE [topic_id] = @original_topic_id AND (([Topic] = @original_Topic) OR ([Topic] IS NULL AND @original_Topic IS NULL)) AND (([Mastercourse] = @original_Mastercourse) OR ([Mastercourse] IS NULL AND @original_Mastercourse IS NULL)) AND (([subcourse] = @original_subcourse) OR ([subcourse] IS NULL AND @original_subcourse IS NULL)) AND (([video_url] = @original_video_url) OR ([video_url] IS NULL AND @original_video_url IS NULL)) AND (([Status] = @original_Status) OR ([Status] IS NULL AND @original_Status IS NULL))"
        OldValuesParameterFormatString="original_{0}">

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