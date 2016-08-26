<%@ Page Title="Visión Mundial - Roles" Language="C#" AutoEventWireup="true" CodeBehind="screen_role.aspx.cs" Inherits="WV.WebApplication.Pages.screen_role" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">

    <script src="/Content/assets/js/bootstrapValidator.js"></script>
    <script src="/Content/assets/js/screen_role.js"></script>

</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="pagename" runat="server" class="hidden">Roles</div>
    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Roles
                    </div>
                    <div class="panel-body">
                        <ul class="nav nav-pills">
                            <li class="active"><a id="tabtable" href="#roles_table" data-toggle="tab">Listar</a>
                            </li>
                            <li class=""><a id="tabdetails" href="#roles_details" data-toggle="tab">Nuevo</a>
                            </li>
                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane fade active in" id="roles_table">
                                <br />
                            </div>

                            <div class="tab-pane fade" id="roles_details">
                                <div class="col-md-6 col-sm-6">
                                    <br />
                                    <form id="form1">
                                        <div class="form-group hidden in-controls">
                                            <label for="in_rol" class="control-label">Rol</label>
                                            <input type="text" id="in_rol" name="in_rol" class="form-control" placeholder="Ingrese el nombre del Rol" >
                                          
                                            
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Rol</label>
                                            <p id="lbl_rol" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls has-feedback">
                                            <label for="in_descripcion">Descripcion</label>
                                            <input type="text" id="in_descripcion" name="in_descripcion" class="form-control" placeholder="Ingrese la descripción" >
                                          
                                            
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Descripcion</label>
                                            <p id="lbl_descripcion" class="form-control-static"></p>
                                        </div>
                                       
                                    </form>
                                    <%-- From here all the controls get loaded either to display data or to edit --%>

                                    <button id="saverole" data-disable="" type="button" class="btn btn-default hidden in-controls"><i class=" fa fa-floppy-o "></i>Guardar</button>
                                    <button id="cancelrole" type="button" class="btn btn-default hidden in-controls"><i class=" fa fa-times "></i>Cancelar</button>
                                    <input type="text" id="screenmode" class="hidden" />

                                </div>

                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
        <input id="roletodelete" class="hidden" type="text" />
        <input id="roletoedit" class="hidden" type="text" />
        <div class="modal fade" id="modalmessage" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Eliminar Rol</h4>
                    </div>
                    <div class="modal-body">
                        <p>¿Está seguro de eliminar al rol seleccionado?</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                        <button id="btndeleterole" type="button" class="btn btn-default" data-dismiss="modal">Si</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalsave" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Eliminar Rol</h4>
                    </div>
                    <div class="modal-body">
                        <p id="actionmessage">¿Está seguro de guardar los cambios?</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                        <button id="btnsaverole" type="button" class="btn btn-default" data-dismiss="modal">Si</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>