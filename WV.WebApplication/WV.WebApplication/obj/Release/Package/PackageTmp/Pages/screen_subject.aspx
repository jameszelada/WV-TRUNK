<%@ Page Title="Visión Mundial - Materia" Language="C#" AutoEventWireup="true" CodeBehind="screen_subject.aspx.cs" Inherits="WV.WebApplication.Pages.screen_subject" MasterPageFile="~/WV.Master" ClientIDMode="Static"%>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
     <link href="<%# ResolveUrl("~/") %>Content/assets/js/dataTables/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/jquery-ui.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/assets/js/bootstrapValidator.js"></script>
    <script src="/Content/assets/js/dataTables/jquery.dataTables.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.bootstrap.js"></script>
     <script src="/Content/assets/js/screen_subject.js"></script>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">Materia</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                       Materias
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
                                 <div id="table-responsive" class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                        <tr>
                                            <th>No</th>
                                            <th>Nombre de Materia</th>
                                            <th>Grado</th>
                                            <th>Año</th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            </div>

                            <div class="tab-pane fade" id="page_details">
                                <div class="col-md-6 col-sm-6">
                                    <br />
                                    <form id="form1">

                                        <div class="form-group hidden in-controls">
                                            <label for="in_nombre_materia" class="control-label">Nombre de la Materia</label>
                                            <input type="text" id="in_nombre_materia" name="in_nombre_materia" class="form-control" placeholder="">
                                            <%-- <div class="help-block with-errors"></div>--%>
                                        </div>
                                        <div class="form-group txt-controls">
                                            <label>Nombre de la materia</label>
                                            <p id="lbl_nombre_materia" class="form-control-static"></p>
                                        </div>

                                         <div class="form-group hidden in-controls">
                                                        <label for="in_anio" class="control-label">Año</label>
                                                        <select id="cmbanio" class="form-control">
                                                            <option></option>
                                                            <option>2016</option>
                                                            <option>2015</option>
                                                            <option>2014</option>
                                                            <option>2013</option>
                                                            <option>2012</option>
                                                            <option>2011</option>
                                                            <option>2010</option>
                                                            <option>2009</option>
                                                            <option>2008</option>
                                                            <option>2007</option>
                                                            <option>2006</option>
                                                            <option>2005</option>
                                                            <option>2004</option>
                                                            <option>2003</option>
                                                            <option>2002</option>  
                                                        </select>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Año</label>
                                                        <p id="lbl_anio" class="form-control-static"></p>
                                                    </div>

                                        <div class="form-group hidden in-controls">
                                                        <label for="in_grado" class="control-label">Grado:</label>
                                                        <select id="cmbgrado" class="form-control">
                                                            <option></option>
                                                            <option>9no Grado</option>
                                                            <option>8vo Grado</option>
                                                            <option>7mo Grado</option>
                                                            <option>6to Grado</option>
                                                            <option>5to Grado</option>
                                                            <option>4to Grado</option>
                                                            <option>3er Grado</option>
                                                            <option>2do Grado</option>
                                                            <option>1er Grado</option>
                                                            
                                                        </select>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Grado:</label>
                                                        <p id="lbl_grado" class="form-control-static"></p>
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
                        <h4 class="modal-title">Eliminar Materia</h4>
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
                        <h4 class="modal-title">Eliminar Tipo de Puesto</h4>
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

