<%@ Page Title="Visión Mundial - Configuración de Contenido" Language="C#" AutoEventWireup="true" CodeBehind="screen_role_configuration.aspx.cs" Inherits="WV.WebApplication.Pages.screen_role_configuration" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">

    <%--<script src="/Content/assets/js/bootstrapValidator.js"></script>--%>
    <script src="/Content/assets/js/screen_role_configuration.js"></script>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="pagename" runat="server" class="hidden">Recursos</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Roles
                    </div>
                    <div class="panel-body">
                        <div id="menuroles" class="btn-group-vertical">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-9 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Opciones
                    </div>
                    <div class="panel-body">
                        <div class="form-group txt-controls">
                            <label>Rol</label>
                            <p id="lbl_rol" class="form-control-static"></p>
                        </div>
                        <div class="form-group txt-controls">
                            <label>Descripcion</label>
                            <p id="lbl_descripcion" class="form-control-static"></p>
                        </div>

                        <button id="btnagregaropcion" href="#myModal" data-toggle="modal" type="button" class="btn btn-secondary btn-sm disabled">Agregar Opción</button>

                        <table class="table table-striped" id="tblopcionesenrol" >
                                           

                        </table>
                        <input id="roletosave" type="text" />

                        <div class="modal fade" id="myModal" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                        <h3 class="modal-title">Opciones del sistema</h3>
                                    </div>
                                    <div class="modal-body">
                                        <h5 class="text-left">Seleccione las opciones que desee agregar al rol</h5>
                                        <table class="table table-striped" id="tblopciones" data-click-to-select="true">
                                           

                                        </table>
                                    </div>
                                    <div class="modal-footer">
                                        <button id="btnguardaropciones" type="button" class="btn btn-primary" data-dismiss="modal">Guardar Cambios</button>
                                    </div>

                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>
                        <!-- /.modal -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
