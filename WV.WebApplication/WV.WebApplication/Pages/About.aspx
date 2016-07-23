<%@  Page Title="Visión Mundial - About" Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WV.WebApplication.Pages.About" MasterPageFile="~/WV.Master" ClientIDMode="Static"%>

<asp:Content  ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript" src="/Content/assets/js/jquery-1.10.2.js"></script>
  </asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <form id="form1" runat="server">
    <div id="pagename" runat="server" class="hidden">Acerca_de</div>
    Just used for testing purposes
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Test" />
    </form>
    </asp:Content>
