<%@ Page Title="Visión Mundial - Configuración de Contenido" Language="C#" AutoEventWireup="true" CodeBehind="screen_role_configuration.aspx.cs" Inherits="WV.WebApplication.Pages.screen_role_configuration" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
    <link href="<%# ResolveUrl("~/") %>Content/assets/js/dataTables/dataTables.bootstrap.css" rel="stylesheet" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">

    <%--<script src="/Content/assets/js/bootstrapValidator.js"></script>--%>
    <script src="/Content/assets/js/dataTables/jquery.dataTables.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.bootstrap.js"></script>
    <script src="/Content/assets/js/bootstrap-checkbox.js"></script>
    <script src="/Content/assets/js/screen_role_configuration.js"></script>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style type="text/css">
    .btn-success:hover, .btn-success:focus, .btn-success:active, .btn-success.active, .open .dropdown-toggle.btn-success {
    color: #fff;
    background-color: #3276b1;
    border-color: none;
}
    </style>

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
                        <br />
                        <div id="table-responsive" class="table-responsive">
                            <table class="table table-striped table-bordered table-hover" id="tblopcionesenrol">
                                <thead>
                                    <tr>
                                        <th>N°</th>
                                        <th>Nombre de Opción</th>
                                        <th>Descripción</th>
                                        <th></th>
                                        <th></th>
                                    </tr>
                                </thead>

                            </table>
                        </div>
                        <input id="roletosave" class="hidden" type="text" />

                        <div class="modal fade" id="myModal" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                        <h3 class="modal-title">Opciones del sistema</h3>
                                    </div>
                                    <div class="modal-body">
                                        <h5 class="text-left">Seleccione las opciones que desee agregar al rol</h5>
                                         <div id="table-responsive2" class="table-responsive">
                                        <table class="table table-striped" id="tblopciones" data-click-to-select="true">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Nombre de Opción</th>
                                                    <th>Descripción</th>
                                                </tr>
                                            </thead>

                                        </table>
                                             </div>
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

                        <div class="modal fade" id="modalpermissions" role="dialog" data-keyboard="false" data-backdrop="static">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4  class="modal-title">Permisos de la opción <div id="nombreopcion"></div>
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-8">
                                                <div hidden id="identrecurso"></div>

                                                <div id="table-permissions" class="table-responsive">
                                                    <table class="table" id="tblpermisions">
                                                        <tbody>
                                                             <tr>
                                                                 
                                                                 <td><i class="fa fa-plus-circle fa-2x" aria-hidden="true"></i></td>
                                                                <td>Agregar</td>
                                                                <td> <input id="opcionagregar" type="checkbox" data-off-label="false" data-on-label="false" data-off-icon-cls="glyphicon-remove" data-on-icon-cls="glyphicon-ok"></td>
                                                            </tr>
                                                            <tr>
                                                                <td><i class="fa fa-pencil-square-o fa-2x" aria-hidden="true"></i></td>
                                                                <td>Editar</td>
                                                                <td><input id="opcioneditar" type="checkbox" data-off-label="false" data-on-label="false" data-off-icon-cls="glyphicon-remove" data-on-icon-cls="glyphicon-ok"></td>
                                                            </tr>
                                                            <tr>
                                                                <td><i class="fa fa-times fa-2x" aria-hidden="true"></i></td>
                                                                <td>Eliminar</td>
                                                                <td> <input id="opcioneliminar" type="checkbox" data-off-label="false" data-on-label="false" data-off-icon-cls="glyphicon-remove" data-on-icon-cls="glyphicon-ok"></td>
                                                            </tr>
                                                        </tbody>

                                                    </table>
                                                </div>

                                                


                                            </div>

                                            <div class="col-sm-2">
                                            </div>

                                        </div>

                                        <%--<p>¿Está seguro de eliminar el registro seleccionado?</p>--%>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-primary" data-dismiss="modal" id="btnguardarpermisos">Guardar Cambios</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
