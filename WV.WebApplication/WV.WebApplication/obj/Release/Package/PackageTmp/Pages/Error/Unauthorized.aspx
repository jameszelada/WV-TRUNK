<%@ Page Title="Visión Mundial - No Autorizado" Language="C#" AutoEventWireup="true" CodeBehind="Unauthorized.aspx.cs" Inherits="WV.WebApplication.Pages.Error.Unauthorized" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">Unauthorized</div>
    <div class="col-md-12">
        <h1 class="page-header">Acceso No Autorizado
        </h1>
    </div>
    <div class="row">

        <div class="col-md-2"></div>

        <div class="col-md-8">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <div class="col-sm-2"></div>
                <i class="fa fa-exclamation-triangle fa-5x" aria-hidden="true"></i>
                <div class="col-sm-2"></div>
            </div>
            <div class="col-md-4"></div>



        </div>

        <div class="col-md-2"></div>


    </div>

    <div class="row">

        <div class="col-md-2"></div>

        <div class="col-md-8">


            <div class="alert alert-danger">
                <p class="text-center">
                    <strong>Lo sentimos!</strong>
                    No tiene los permisos suficientes para acceder a esta página.
                </p>
                <p class="text-center">
                    Contacte a su administrador.
                </p>

            </div>



        </div>

        <div class="col-md-2"></div>

    </div>
</asp:Content>
