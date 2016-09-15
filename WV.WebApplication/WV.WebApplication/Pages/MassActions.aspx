<%@ Page Title="Visión Mundial - Acciones de Mantenimiento" Language="C#" AutoEventWireup="true" CodeBehind="MassActions.aspx.cs" Inherits="WV.WebApplication.Pages.MassActions" MasterPageFile="~/WV.Master" ClientIDMode="Static"%>

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
    <script src="/Content/assets/js/massactions.js"></script>


</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        .btn-primary:hover, .btn-primary:focus, .btn-primary:active, .btn-primary.active, .open .dropdown-toggle.btn-primary {
            background-color: #7db831;
        }
    </style>
    <div runat="server" id="pagename" class="hidden">Acciones_Mantenimiento</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Acciones de Mantenimiento
                    </div>
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-12">
                                <div class="content-header content-header-media" style="background-color: #09192a; height: 145px;">
                                    <!-- Remove inline styles when have background image. -->
                                    <div class="header-section" style="padding :10px;">
                                        <h1 style="color:#999;">Acciones de Mantenimiento<br>
                                            <small >Seleccione los registros que desee eliminar <strong><span id="attendance_date"><br></span>Realizar Cuidadosamente!</strong></small></h1>
                                    </div>
                                    <!-- For best results use an image with a resolution of 2560x248 pixels (You can also use a blurred image with ratio 10:1 - eg: 1000x100 pixels - it will adjust and look great!) -->
                                    <!-- <img src="/images/proui-2.0/placeholders/headers/profile_header.jpg" alt="header image" class="animation-pulseSlow"> -->
                                </div>

                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-7">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a href="#roles" data-toggle="tab">Roles</a>
                                            </li>
                                            <li class=""><a href="#beneficiarios" data-toggle="tab">Beneficiarios</a>
                                            </li>
                                            <li class=""><a href="#proyectos" data-toggle="tab">Proyectos</a>
                                            </li>
                                            <li class=""><a href="#programas" data-toggle="tab">Programas</a>
                                            </li>
                                           
                                        </ul>
                                        <form id="form1">
                                            <div class="tab-content">
                                                <div class="tab-pane fade active in" id="roles">


                                                    <div id="table-responsive" class="table-responsive">
                                                        <table class="table table-striped table-bordered" id="dataTables-roles" width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th><input name="select_all" value="1" id="example-select-all" type="checkbox"></th>
                                                                    <th>Nombre de Rol</th>
                                                                    


                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>

                                                    <div class="col-sm-2 default-row-spacer">
                                                        <div class="dropdown">
                                                            <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                                                                Acciones
                                                         <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu">
                                                                <li><a id="btneliminarrol">Eliminar</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>


                                                </div>

                                                <div class="tab-pane fade" id="beneficiarios">
                                                    <div id="table-responsive1" class="table-responsive">
                                                        <table class="table table-striped table-bordered" id="dataTables-beneficiarios"  width="100%">
                                                            <thead>
                                                                <tr>
                                                                  
                                                                    <th><input name="select_all" value="1" id="example1-select-all" type="checkbox"></th>
                                                                    <th>Nombre de Beneficiario</th>


                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>

                                                    <div class="col-sm-2 default-row-spacer">
                                                        <div class="dropdown">
                                                            <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                                                                Acciones
                                                         <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu">
                                                                <li><a id="btneliminabeneficiario">Eliminar</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                    
                                                </div>

                                                <div class="tab-pane fade" id="proyectos">
                                                    <div id="table-responsive2" class="table-responsive">
                                                        <table class="table table-striped table-bordered" id="dataTables-proyectos"  width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th><input name="select_all2" value="1" id="example2-select-all" type="checkbox"></th>
                                                                    <th>Proyecto</th>
                                                                    


                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>
                                                    <div class="col-sm-2 default-row-spacer">
                                                        <div class="dropdown">
                                                            <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                                                                Acciones
                                                         <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu">
                                                                <li><a id="btneliminarproyecto">Eliminar</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                    
                                                </div>

                                                <div class="tab-pane fade" id="programas">
                                                
                                                    <div id="table-responsive3" class="table-responsive">
                                                        <table class="table table-striped table-bordered" id="dataTables-programas"  width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th><input name="select_all3" value="1" id="example3-select-all" type="checkbox"></th>
                                                                    <th>Programa</th>
                                                                   


                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>
                                                    <div class="col-sm-2 default-row-spacer">
                                                        <div class="dropdown">
                                                            <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                                                                Acciones
                                                         <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu">
                                                                <li><a id="btneliminarprograma">Eliminar</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                
                                                </div>

                                                
                                            </div>
                                            
                                        </form>
                                    </div>

                                </div>
                                </div>

                            </div>
                            <div class="col-md-3">
                                


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