<%@ Page Title="Visión Mundial - Asignar Recursos Humanos" Language="C#" AutoEventWireup="true" CodeBehind="AssignRRHH.aspx.cs" Inherits="WV.WebApplication.Pages.AssignRRHH" MasterPageFile="~/WV.Master" ClientIDMode="Static"%>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
    <%--<link href="<%# ResolveUrl("~/") %>Content/assets/css/logbook.css" rel="stylesheet" />--%>
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
    <script src="/Content/assets/js/assignrrhh.js"></script>
    <script src="/Content/assets/js/jquery-ui.min.js"></script>


</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">Asignar_RRHH</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Asignar Recurso Humano
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <br />
                        </div>
                        <div class="row">
                            <br />
                        </div>
                        <div class="row">
                           <br />
                        </div>

                        <div class="row">
                            
                            <div class="col-md-10">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-7">
                                   <div class="form-horizontal">
                                            <label>Seleccione el proyecto</label>
                                            <select id="cmbproyecto" class="form-control">
                                            </select>
                                        </div>
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            
                        </div>

                        <div class="row">
                           <br />
                        </div>
                        <div class="row">
                           <br />
                        </div>
                        <div class="row">
                           <br />
                        </div>
                        <div class="row">
                            
                            <div class="form-horizontal col-md-12">
                                <button id="addRow" class="form-control-static fa fa-plus-circle"></button>
                                <button id="deleterow" class="form-control-static fa fa-minus-circle"></button>
                            </div>
                            
                        </div>
                        <div class="row">
                           <br />
                        </div>
                        <div class="row">
                           <br />
                        </div>
                        <div class="row">
                            
                            <div class="form-horizontal col-md-12">
                                
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" id="example" width="100%">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Nombre</th>
                                                    <th>Telefono</th>
                                                    <th>Puesto</th>
                                                </tr>
                                            </thead>

                                        </table>
                                </div>
                            </div>
                            
                        </div>
                        <div class="row">
                            <br />
                        </div>
                        <div class="row">
                            <br />
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                 <button id="savepage" data-disable="" type="button" class="btn btn-default in-controls"><i class=" fa fa-floppy-o "></i>Guardar</button>
                           <button id="cancelpage" type="button" class="btn btn-default in-controls" data-toggle="modal" data-target="#modalmessage"><i class=" fa fa-times "></i>Cancelar</button>
                            </div>
                          
                            <div class="col-md-4">
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
                    <h4 class="modal-title">Cancelar Operacion</h4>
                </div>
                <div class="modal-body">
                    <p>¿Desea cancelar la operación?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                    <button id="pagebtndelete" type="button" class="btn btn-default" data-dismiss="modal">Si</button>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
