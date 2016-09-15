<%@ Page Title="Visión Mundial - Control de asistencia" Language="C#" AutoEventWireup="true" CodeBehind="AttendanceTracking.aspx.cs" Inherits="WV.WebApplication.Pages.AttendanceTracking" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

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
    <script src="/Content/assets/js/moment.min.js"></script>
    <script src="/Content/assets/js/fullcalendar.min.js"></script>
    <script src="/Content/assets/js/lang-all.js"></script>
    <script src="/Content/assets/js/attendancetracking.js"></script>


</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        .btn-primary:hover, .btn-primary:focus, .btn-primary:active, .btn-primary.active, .open .dropdown-toggle.btn-primary {
            background-color: #7db831;
        }
    </style>
    <div runat="server" id="pagename" class="hidden">Control_Asistencia</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Control de Asistencia
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12">
                                <div class="content-header content-header-media" style="background-color: #09192a; height: 110px; padding:10px">
                                    <!-- Remove inline styles when have background image. -->
                                    <div class="header-section">
                                        <h1 style="color:#999;">Cuadro de Control de Asistencia<br>
                                            <small >Asistencia para el dia: <strong><span id="attendance_date"></span></strong></small></h1>
                                    </div>
                                    <!-- For best results use an image with a resolution of 2560x248 pixels (You can also use a blurred image with ratio 10:1 - eg: 1000x100 pixels - it will adjust and look great!) -->
                                    <!-- <img src="/images/proui-2.0/placeholders/headers/profile_header.jpg" alt="header image" class="animation-pulseSlow"> -->
                                </div>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-5">
                                <div id="calendar" class="fc fc-ltr fc-unthemed">
                                </div>

                            </div>
                            <div class="col-md-7">
                                <div class="col-sm-3">
                                    <h5>Asistencia para </h5>
                                </div>
                                <div class="col-sm-5">
                                    <select id="cmbprograma" class="form-control">
                                    </select>
                                </div>
                                <div class="col-sm-2 default-row-spacer">
                                    <div class="dropdown">
                                        <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                                            Marcar Todos
                                        <span class="caret"></span>
                                        </button>
                                        <ul class="dropdown-menu">
                                            <li><a id="btnpresente">Presente</a></li>
                                            <li><a id="btnausente">Ausente</a></li>
                                        </ul>
                                    </div>
                                </div>


                                <div id="table-responsive" class="table-responsive">
                                    <table class="table table-striped table-bordered" id="dataTables-example">
                                        <thead>
                                            <tr>
                                                <th>N°</th>
                                                <th>Nombre</th>
                                                <th></th>


                                            </tr>
                                        </thead>
                                    </table>
                                    <button id="savepage" type="button" class="btn btn-default btn-sm pull-right" disabled><i class="fa fa-floppy-o "></i>Guardar</button>
                                    

                                    <input type="text" id="screenmode" class="hidden" />
                                </div>


                            </div>



                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="backgroundCover" style="display: none; z-index: 1000000;"></div>
    <%--<input id="usertoassign" class="hidden" type="text" />
        <input id="roletoassign" class="hidden" type="text" />--%>
    <input id="idactividad" class="hidden" type="text" />
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
