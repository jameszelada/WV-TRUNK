<%@ Page Title="Visión Mundial - Plan Semanal" Language="C#" AutoEventWireup="true" CodeBehind="WeeklyPlan.aspx.cs" Inherits="WV.WebApplication.Pages.WeeklyPlan" MasterPageFile="~/WV.Master" ClientIDMode="Static"%>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/logbook.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/js/dataTables/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/select.bootstrap.min.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/editor.dataTables.min.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/jquery-ui.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/assets/js/bootstrapValidator.js"></script>
    <%--<script src="/Content/assets/js/dataTables/jquery.dataTables.js"></script>--%>
    <script src="/Content/assets/js/dataTables/jquery.dataTables.min.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.bootstrap.min.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.bootstrap.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.select.min.js"></script>
    <script src="/Content/assets/js/weeklyplan.js"></script>
    <script src="/Content/assets/js/jquery-ui.min.js"></script>


</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">Plan_Semanal</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Plan Semanal
                    </div>
                    <div class="panel-body">
                        <div id="wrapperinside" class="">

                            <!-- Sidebar -->
                            <!-- Sidebar -->
                            <div id="sidebar-wrapper" class="">
                                <ul id="sidebar_menu" class="sidebar-nav">
                                    <li class="sidebar-brand"><a id="menu-toggle" href="#">Semanas<span id="main_icon" class="fa fa-times"></span></a></li>
                                </ul>
                                <%-- Probablemente haya que actualizar el id del sidebar --%>
                                <ul id="logbookdates" class="sidebar-nav" id="sidebar">
                                </ul>
                            </div>

                            <!-- Page content -->
                            <div id="page-content-wrapper">
                                <!-- Keep all page content within the page-content inset div! -->
                                <div class="page-content inset">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="btn-toolbar" role="toolbar" aria-label="Toolbar with button groups">
                                                <div class="btn-group btn-group-lg" role="group">
                                                    <button id="btnshowside" type="button" class="btn btn-secondary fa fa-search"></button>
                                                    <button id="action-add" type="button" class="btn btn-secondary fa fa-plus"></button>

                                                </div>
                                                <div class="btn-group btn-group-lg" role="group">
                                                    <button id="action-edit" type="button" class="btn btn-secondary btn-sm detail fa fa-pencil">Editar</button>
                                                    <button id="action-cancel" type="button" class="btn btn-secondary btn-sm detail fa fa-times">Cancelar</button>
                                                    <button id="action-save" type="button" class="btn btn-secondary btn-sm detail fa fa-floppy-o">Guardar</button>
                                                    <button id="action-delete" type="button" class="btn btn-secondary btn-sm detail fa fa-times" data-toggle='modal' data-target='#modalmessage'>Eliminar</button>
                                                </div>

                                            </div>

                                            <div style="margin-top: 40px" class="row">
                                                <div class="col-md-8">
                                                    <div class="form-horizontal margin-bottom">
                                                        <div class="form-group">
                                                            <label>Nombre:</label>
                                                            <label id="lbl_nombre" class="form-control-static"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-horizontal margin-bottom">

                                                        <div class="form-group">
                                                            <label>Semana:</label>
                                                            <input type="text" class="form-control-static input-sm" id="datepicker">
                                                             <label id="rangofechas"></label>
                                                            <input type="hidden" id="fechainicio"/>
                                                            <input type="hidden" id="fechafinal"/>
                                                        </div>
                                                    </div>


                                                    <div class="form-horizontal">
                                                        <button id="addRow" class="form-control-static fa fa-plus-circle"></button>

                                                        <button id="deleterow" class="form-control-static fa fa-minus-circle"></button>
                                                    </div>

                                                </div>

                                                <div class="col-md-2">
                                                    <p id="screenmode" class="hidden"></p>
                                                    <p id="idperson" runat="server" class="hidden"></p>
                                                </div>

                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <table class="table table-striped table-bordered table-hover" id="example">
                                                            <thead>
                                                                <tr>
                                                                    <th></th>
                                                                    <th>Actividad</th>
                                                                    <th>Observacion</th>
                                                                    <th>Recursos</th>
                                                                </tr>
                                                            </thead>

                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>

                </div>



            </div>
        </div>
    </div>

    <input id="pagetodelete" class="hidden" type="text" />
    <input id="pagetoedit" class="hidden" type="text" />
    <div class="modal fade" id="modalmessage" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Eliminar Plan Semanal</h4>
                </div>
                <div class="modal-body">
                    <p>¿Está seguro de eliminar el registro seleccionado?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                    <button id="pagebtndelete" type="button" class="btn btn-default" data-dismiss="modal">Si</button>
                </div>
            </div>
        </div>
    </div>


</asp:Content>