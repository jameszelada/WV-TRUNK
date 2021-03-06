﻿<%@ Page Title="Visión Mundial - Asignacion de Materia" Language="C#" AutoEventWireup="true" CodeBehind="AssignSubject.aspx.cs" Inherits="WV.WebApplication.Pages.AssignSubject" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

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
    <script src="/Content/assets/js/assignsubject.js"></script>


</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        .btn-primary:hover, .btn-primary:focus, .btn-primary:active, .btn-primary.active, .open .dropdown-toggle.btn-primary {
            background-color: #7db831;
        }
    </style>
    <div runat="server" id="pagename" class="hidden">Asignar_Materia</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Asignacion de Materia
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12">
                                <div class="content-header content-header-media" style="background-color: #09192a; height: 110px; padding:10px">
                                    <!-- Remove inline styles when have background image. -->
                                    <div class="header-section">
                                        <h1 style="color:#999;">Programas CIC<br>
                                            <small >Asignacion de Materia <strong><span id="attendance_date"></span></strong></small></h1>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-2">
                                

                            </div>
                            <div class="col-md-7">
                                <div class="col-sm-2">
                                    <h5>Materia</h5>
                                </div>
                                <div class="col-sm-7">
                                    <select id="cmbmateria" class="form-control">
                                    </select>
                                </div>
                                <div class="col-sm-2 default-row-spacer">
                                   <button id="btnagregarbeneficiario" href="#myModal" data-toggle="modal" type="button" class="btn btn-secondary btn-sm ">Agregar</button>
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
    <%--<input id="usertoassign" class="hidden" type="text" />--%>
     <input id="subjecttoassign" class="hidden" type="text" />
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

    <div class="modal fade" id="myModal" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 class="modal-title">Beneficiarios CIC</h3>
                </div>
                <div class="modal-body">
                    <h5 class="text-left">Seleccione los beneficiarios que desee asignar a la materia</h5>
                    <div id="table-responsive2" class="table-responsive">
                        <table class="table table-striped table-bordered" id="tblbeneficiarios" data-click-to-select="true">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Nombre de Beneficiario</th>
                                </tr>
                            </thead>

                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnguardarasignacion" type="button" class="btn btn-primary" data-dismiss="modal">Guardar Cambios</button>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    </div>
</asp:Content>

