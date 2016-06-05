<%@ Page Title="Visión Mundial - Usuarios" Language="C#" AutoEventWireup="true" CodeBehind="screen_user.aspx.cs" Inherits="WV.WebApplication.Pages.screen_user" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <%--<script src="/Content/assets/js/jquery.validate.js"></script>--%>
    <%-- <script src="/Content/assets/js/validator.js"></script>--%>
    <script src="/Content/assets/js/bootstrapValidator.js"></script>
    <script src="/Content/assets/js/screen_user.js"></script>

</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">Usuarios</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Usuarios
                    </div>
                    <div class="panel-body">
                        <ul class="nav nav-pills">
                            <li class="active"><a id="tabtable" href="#users_table" data-toggle="tab">Listar</a>
                            </li>
                            <li class=""><a id="tabdetails" href="#user_details" data-toggle="tab">Nuevo</a>
                            </li>
                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane fade active in" id="users_table">
                                <br />
                            </div>

                            <div class="tab-pane fade" id="user_details">
                                <div class="col-md-6 col-sm-6">
                                    <br />
                                    <form id="form1">
                                        <div class="form-group hidden in-controls">
                                            <label for="in_username" class="control-label">Username</label>
                                            <input type="text" id="in_username" name="in_username" class="form-control" placeholder="Ingrese el Username">
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Username</label>
                                            <p id="lbl_username" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls has-feedback">
                                            <label for="in_nombre">Nombres</label>
                                            <input type="text" id="in_nombre" name="in_nombre" class="form-control" placeholder="Ingrese el Nombre">
                                            <%--  <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Nombres</label>
                                            <p id="lbl_nombre" class="form-control-static"></p>
                                        </div>
                                        <div class="form-group hidden in-controls has-feedback">
                                            <label for="in_apellido">Apellidos</label>
                                            <input type="text" id="in_apellido" name="in_apellido" class="form-control" placeholder="Ingrese sus Apellidos">
                                            <%--<div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Apellidos</label>
                                            <p id="lbl_apellido" class="form-control-static"></p>
                                        </div>
                                        <div class="form-group hidden in-controls has-feedback">
                                            <label>Contraseña</label>
                                            <input id="in_contrasenia" name="in_contrasenia" type="password" class="form-control" placeholder="Ingrese su contraseña">
                                            <%--<div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Contraseña</label>
                                            <p id="lbl_contrasenia" class="form-control-static"></p>
                                        </div>
                                        <div class="form-group hidden in-controls has-feedback">
                                            <label>Escriba de nuevo su Contraseña</label>
                                            <input id="in_contrasenia_re" name="in_contrasenia_re" type="password" class="form-control" placeholder="Rescriba su contraseña">
                                            <%--<div class="help-block with-errors"></div>--%>
                                        </div>

                                        <div class="form-group hidden in-controls has-feedback">
                                            <label for="in_email">Correo Electrónico</label>
                                            <input type="email" id="in_email" name="in_email" class="form-control" placeholder="Ingrese su correo electronico">
                                            <%--  <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Correo Electrónico</label>
                                            <p id="lbl_email" class="form-control-static"></p>
                                        </div>

                                    </form>
                                    <%-- From here all the controls get loaded either to display data or to edit --%>

                                    <button id="saveuser" data-disable="" type="button" class="btn btn-default hidden in-controls"><i class=" fa fa-floppy-o "></i>Guardar</button>
                                    <button id="canceluser" type="button" class="btn btn-default hidden in-controls"><i class=" fa fa-times "></i>Cancelar</button>
                                    <input type="text" id="screenmode" class="hidden" />

                                </div>

                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
        <input id="usertoassign" class="hidden" type="text" />
        <input id="roletoassign" class="hidden" type="text" />
        <input id="usertodelete" class="hidden" type="text" />
        <input id="usertoedit" class="hidden" type="text" />
        <div class="modal fade" id="modalmessage" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Eliminar Usuario</h4>
                    </div>
                    <div class="modal-body">
                        <p>¿Está seguro de eliminar al usuario seleccionado?</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                        <button id="btndeleteuser" type="button" class="btn btn-default" data-dismiss="modal">Si</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalsave" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Eliminar Usuario</h4>
                    </div>
                    <div class="modal-body">
                        <p id="actionmessage">¿Está seguro de guardar los cambios?</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                        <button id="btnsaveuser" type="button" class="btn btn-default" data-dismiss="modal">Si</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalrole" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Seleccionar Rol</h4>
                    </div>
                    <div class="modal-body">
                        <p id="actionmessagerol">Elija el Rol para asignar al usuario</p>
                        <div class="form-group">
                            <label>Roles de Usuario</label>
                            <select id="cmbroles" class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnassignrole" type="button" class="btn btn-default" data-dismiss="modal">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

