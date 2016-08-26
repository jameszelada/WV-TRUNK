<%@ Page Title="Visión Mundial - Actividad" Language="C#" AutoEventWireup="true" CodeBehind="screen_activity.aspx.cs" Inherits="WV.WebApplication.Pages.screen_activity" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
    <link href="<%# ResolveUrl("~/") %>Content/assets/js/dataTables/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/select.bootstrap.min.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/editor.dataTables.min.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/jquery-ui.min.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/fullcalendar.css" rel="stylesheet" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/assets/js/bootstrapValidator.js"></script>
    <script src="/Content/assets/js/dataTables/jquery.dataTables.min.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.bootstrap.min.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.bootstrap.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.select.min.js"></script>
    <script src="/Content/assets/js/jquery-ui.min.js"></script>
    <script src="/Content/assets/js/moment.min.js"></script>
    <script src="/Content/assets/js/fullcalendar.min.js"></script>
    <script src="/Content/assets/js/lang-all.js"></script>
    <script src="/Content/assets/js/screen_activity.js"></script>

</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">Actividades</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Registro de Actividades
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
                                    <table class="table table-striped table-bordered table-hover" id="dataTables-example" width="100%">
                                        <thead>
                                            <tr>
                                                <th>No</th>
                                                <th>Programa</th>
                                                <th>Estado</th>
                                                <th></th>
                                                <th></th>
                                              
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="page_details">
                                <div class="col-md-12">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group hidden in-controls">
                                                <label>Seleccione el Programa</label>
                                                <select id="cmbprograma" class="form-control">
                                                </select>
                                            </div>
                                            <div class="form-group txt-controls">
                                                <label>Programa</label>
                                                <p id="lbl_programa" class="form-control-static"></p>
                                            </div>
                                            <div class="col-md-1">
                                        </div>

                                            <div class="col-md-10">
                                                <div class="form-group hidden in-controls">
                                                    <label>Seleccione la(s) fechas de las actividades</label>
                                                    <div id="calendar" class="fc fc-ltr fc-unthemed">
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-horizontal">
                                                <button id="deleterow" class="form-control-static fa fa-minus-circle in-controls"></button>
                                            </div>
                                            <div id="table-responsiveactividades" class="table-responsive">
                                                <table class="table table-striped table-bordered table-hover" id="actividades" width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th></th>
                                                            <th>Codigo</th>
                                                            <th>Descripcion</th>
                                                            <th>Estado</th>
                                                            <th>Fecha</th>
                                                            <th>Observacion</th>
                                                            
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- From here all the controls get loaded either to display data or to edit --%>

                                   <button id="cancelpage" type="button" class="btn btn-default btn-sm hidden pull-rigth in-controls"><i class="fa fa-times "></i>Cancelar</button>
                                        <button id="savepage" type="button" class="btn btn-default btn-sm hidden pull-right in-controls"><i class="fa fa-floppy-o "></i>Guardar</button>
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
        <div class="modal fade" id="modalmessage" role="dialog" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Registrar Actividad</h4>
                    </div>
                    <div class="modal-body">
                        <form id="form1" class="form-horizontal" role="form">
                            <div class="form-group">
                                <label class="col-sm-2 control-label"
                                    for="in_codigo">
                                    Código</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control in-controls"
                                        id="in_codigo" name="in_codigo" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label"
                                    for="in_descripcion">
                                    Descripcion</label>
                                <div class="col-sm-10">
                                    <textarea cols="40" rows="3" class="form-control in-controls"
                                        id="in_descripcion" name="in_descripcion"></textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label"
                                    for="in_estado">
                                    Estado</label>
                                <div class="col-sm-10">
                                    <select class="form-control"
                                        id="in_estado">
                                         <option data-id-estado='A'>Activo</option>
                                                <option data-id-estado='I'>Inactivo</option>
                                                <option data-id-estado='S'>Suspendido</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label"
                                    for="in_estado">
                                    Fecha</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" disabled
                                        id="in_fecha" data-fecha="" />
                                </div>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control hidden" disabled data-fecha=""
                                        id="in_fechanumerica" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label"
                                    for="in_observacion">
                                    Observacion</label>
                                <div class="col-sm-10">
                                    <textarea cols="40" rows="3" class="form-control in-controls"
                                        id="in_observacion" name="in_observacion"></textarea>
                                </div>
                            </div>

                        </form>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default"
                            data-dismiss="modal">
                            Cerrar
                        </button>
                        <button id="addRecord" type="button" class="btn btn-primary">
                            Agregar
                        </button>
                    </div>
                </div>
            </div>
        </div>





    </div>
</asp:Content>

