<%@ Page Title="Visión Mundial - No Autorizado" Language="C#" AutoEventWireup="true" CodeBehind="Unauthorized.aspx.cs" Inherits="WV.WebApplication.Pages.Error.Unauthorized" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     <div runat="server" id="pagename" class="hidden">Unauthorized</div>
    <div class="col-md-12">
        <h1 class="page-header">
            Acceso No Autorizado
        </h1>
    </div>
    <div class="col-md-6">
        <div class="alert alert-danger">
            <strong>Lo sentimos!</strong> 
            No tienes los permisos suficientes para acceder a esta página.
        </div>
    </div>
</asp:Content>
