<%@ Page Title="Visión Mundial - Proyecto" Language="C#" AutoEventWireup="true" CodeBehind="screen_project.aspx.cs" Inherits="WV.WebApplication.Pages.screen_project" MasterPageFile="~/WV.Master" ClientIDMode="Static"%>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/assets/js/bootstrapValidator.js"></script>
     <script src="/Content/assets/js/screen_project.js"></script>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">Proyectos</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Proyectos
                    </div>
                    <div class="panel-body">
                        <ul class="nav nav-pills">
                            <li class="active"><a id="tabtable" href="#page_table" data-toggle="tab">Listar</a>
                            </li>
                            <li class=""><a id="tabdetails" href="#page_details" data-toggle="tab">Nuevo</a>
                            </li>
                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane fade active in" id="page_table">
                                <br />
                            </div>

                            <div class="tab-pane fade" id="page_details">
                                <div class="col-md-6 col-sm-6">
                                    <br />
                                    <form id="form1">

                                        <div class="form-group hidden in-controls">
                                            <label for="in_codigo_proyecto" class="control-label">Codigo de Proyecto</label>
                                            <input type="text" id="in_codigo_proyecto" name="in_codigo_proyecto" class="form-control" placeholder="Ingrese el codigo del proyecto">
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Codigo de Proyecto</label>
                                            <p id="lbl_codigo_proyecto" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls">
                                            <label for="in_descripcion" class="control-label">Descripción del Proyecto</label>
                                            <textarea id="in_descripcion" name="in_descripcion" class="form-control" rows="4"></textarea>
                                            <%--<input type="text" id="in_apellido" name="in_apellido" class="form-control" placeholder="Ingrese el apellido">--%>
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Descripcion de Proyecto</label>
                                            <p id="lbl_descripcion" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls">
                                            <label>Estado del Proyecto</label>
                                            <select id="cmbestado" class="form-control">
                                                <option data-id-estado='A'>Activo</option>
                                                <option data-id-estado='I'>Inactivo</option>
                                                <option data-id-estado='S'>Suspendido</option>             
                                            </select>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Estado</label>
                                            <p id="lbl_estado" class="form-control-static"></p>
                                        </div>

                                    </form>
                                    <%-- From here all the controls get loaded either to display data or to edit --%>

                                    <button id="savepage" data-disable="" type="button" class="btn btn-default hidden in-controls"><i class=" fa fa-floppy-o "></i>Guardar</button>
                                    <button id="cancelpage" type="button" class="btn btn-default hidden in-controls"><i class=" fa fa-times "></i>Cancelar</button>
                                    <input type="text" id="screenmode" class="hidden" />

                                </div>

                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
        <%--<input id="usertoassign" class="hidden" type="text" />
        <input id="roletoassign" class="hidden" type="text" />--%>
        <input id="pagetodelete" class="hidden" type="text" />
        <input id="pagetoedit" class="hidden" type="text" />
        <div class="modal fade" id="modalmessage" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Eliminar Proyecto</h4>
                    </div>
                    <div class="modal-body">
                        <p>¿Está seguro de eliminar el registro seleccionado?</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                        <%--<button id="btndeleteuser" type="button" class="btn btn-default" data-dismiss="modal">Si</button>--%>
                        <button id="pagebtndelete" type="button" class="btn btn-default" data-dismiss="modal">Si</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalsave" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Eliminar Proyecto</h4>
                    </div>
                    <div class="modal-body">
                        <p id="actionmessage">¿Está seguro de guardar los cambios?</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                        <button id="pagebtnsave" type="button" class="btn btn-default" data-dismiss="modal">Si</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
