<%@ Page Title="Visión Mundial - Programa" Language="C#" AutoEventWireup="true" CodeBehind="screen_program.aspx.cs" Inherits="WV.WebApplication.Pages.screen_program" MasterPageFile="~/WV.Master" ClientIDMode="Static"%>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
    <link href="<%# ResolveUrl("~/") %>Content/assets/js/dataTables/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/jquery-ui.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/assets/js/bootstrapValidator.js"></script>
    <script src="/Content/assets/js/dataTables/jquery.dataTables.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.bootstrap.js"></script>
    <script src="/Content/assets/js/jquery-ui.min.js"></script>
    <script src="/Content/assets/js/screen_program.js"></script>
    
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">Programa</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Programas
                    </div>
                    <div class="panel-body">
                        <ul class="nav nav-pills">
                            <li class="active"><a id="tabtable" href="#page_table" data-toggle="tab">Listar</a>
                            </li>
                            <li class=""><a id="tabdetails" href="#page_details" data-toggle="tab">Nuevo</a>
                            </li>
                        </ul>

                        <div class="tab-content">
                            <br />
                            <div class="tab-pane fade active in" id="page_table">
                                 <div id="table-responsive" class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                        <tr>
                                            <th>No</th>
                                            <th>Código</th>
                                            <th>Estado</th>
                                            <th>Tipo de Programa</th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                                <%--<br />--%>
                            </div>

                            <div class="tab-pane fade" id="page_details">
                                <div class="col-md-6 col-sm-6">
                                    <br />
                                    <form id="form1">

                                        <div class="form-group hidden in-controls">
                                            <label>Seleccione el Proyecto</label>
                                            <select id="cmbproyecto" class="form-control">
                                            </select>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Proyecto</label>
                                            <p id="lbl_proyecto" class="form-control-static"></p>
                                        </div>


                                        <div class="form-group hidden in-controls">
                                            <label>Seleccione el Tipo de Programa</label>
                                            <select id="cmbtipoprograma" class="form-control">
                                            </select>
                                        </div>
                                        <div class="form-group txt-controls ">
                                            <label>Tipo de Programa</label>
                                            <p id="lbl_tipoprograma" class="form-control-static"></p>
                                        </div>


                                        <div class="form-group hidden in-controls">
                                            <label>Seleccione la Comunidad</label>
                                            <select id="cmbcomunidad" class="form-control">
                                            </select>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Comunidad</label>
                                            <p id="lbl_comunidad" class="form-control-static"></p>
                                        </div>


                                        <div class="form-group hidden in-controls">
                                            <label for="in_codigo" class="control-label">Código del Programa</label>
                                            <input type="text" id="in_codigo" name="in_codigo" class="form-control" placeholder="Ingrese el código de programa">
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Codigo de Programa</label>
                                            <p id="lbl_codigo" class="form-control-static"></p>
                                        </div>

                                        <div class="form-group hidden in-controls">
                                            <label for="in_descripcion" class="control-label">Descripción del Programa</label>
                                            <textarea id="in_descripcion" name="in_descripcion" class="form-control" rows="4"></textarea>
                                            <%--<input type="text" id="in_apellido" name="in_apellido" class="form-control" placeholder="Ingrese el apellido">--%>
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Descripcion de Programa</label>
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



                                        <div class="form-horizontal margin-bottom hidden in-controls">

                                            <div class="form-group">
                                                <label for="datepickerinicio">Fecha Inicio:</label>
                                                <input name="datepickerinicio" id="datepickerinicio" type="text" class="form-control-static input-sm" >
                                            </div>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Fecha Inicio:</label>
                                            <p id="lbl_datepickerinicio" class="form-control-static"></p>
                                        </div>


                                        <div class="form-horizontal margin-bottom hidden in-controls">

                                            <div class="form-group">
                                                <label for="datepickerfinal">Fecha Final:</label>
                                                <input name="datepickerfinal" id="datepickerfinal" type="text" class="form-control-static input-sm">
                                            </div>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Fecha Final:</label>
                                            <p id="lbl_datepickerfinal" class="form-control-static"></p>
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

      

    </div>
</asp:Content>

