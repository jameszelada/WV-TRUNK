<%@ Page Title="Visión Mundial - Personal" Language="C#" AutoEventWireup="true" CodeBehind="screen_staff.aspx.cs" Inherits="WV.WebApplication.Pages.screen_staff" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/assets/js/bootstrapValidator.js"></script>
     <script src="/Content/assets/js/screen_staff.js"></script>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">Personal</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Personal
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
                                            <label for="in_nombre" class="control-label">Nombres</label>
                                            <input type="text" id="in_nombre" name="in_nombre" class="form-control" placeholder="Ingrese el nombre">
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Nombres</label>
                                            <p id="lbl_nombre" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls">
                                            <label for="in_apellido" class="control-label">Apellidos</label>
                                            <input type="text" id="in_apellido" name="in_apellido" class="form-control" placeholder="Ingrese el apellido">
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Apellidos</label>
                                            <p id="lbl_apellido" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls">
                                            <label for="in_dui" class="control-label">Numero de DUI</label>
                                            <input type="text" id="in_dui" name="in_dui" class="form-control" placeholder="Ingrese el numero de dui" title="Dui" pattern="^\d{8}-\d" >
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Numero de DUI</label>
                                            <p id="lbl_dui" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls">
                                            <label for="in_sexo" class="control-label">Sexo</label>
                                            <div class="radio">
                                                <label class="radio-inline">
                                                    <input name="optionsRadios" id="radio_masculino" value="option1" checked="" type="radio">Masculino
                                                </label>
                                            </div>
                                            <div class="radio">
                                                <label class="radio-inline">
                                                    <input name="optionsRadios" id="radio_femenino" value="option2" type="radio">Femenino
                                                </label>
                                            </div>
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Sexo</label>
                                            <p id="lbl_sexo" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls">
                                            <label for="in_fecha_nacimiento" class="control-label">Fecha de Nacimiento</label>
                                            <input type="date" id="in_fecha_nacimiento" name="in_fecha_nacimiento" class="form-control" placeholder="Formato : dd/mm/yyyy">
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Fecha de Nacimiento</label>
                                            <p id="lbl_fecha_nacimiento" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls">
                                            <label for="in_email" class="control-label">Correo Electronico</label>
                                            <input type="text" id="in_email" name="in_email" class="form-control" placeholder="Ingrese el correo electronico">
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Correo Electronico</label>
                                            <p id="lbl_email" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls">
                                            <label for="in_direccion" class="control-label">Dirección</label>
                                            <input type="text" id="in_direccion" name="in_direccion" class="form-control" placeholder="Ingrese la direccion">
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Direccion</label>
                                            <p id="lbl_direccion" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls">
                                            <label for="in_telefono" class="control-label">Telefono</label>
                                            <input type="text" id="in_telefono" name="in_telefono" class="form-control" placeholder="Ingrese el numero de telefono">
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Telefono</label>
                                            <p id="lbl_telefono" class="form-control-static"></p>
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
                        <h4 class="modal-title">Eliminar Personal</h4>
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
                        <h4 class="modal-title">Eliminar Personal</h4>
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
