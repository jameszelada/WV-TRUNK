<%@ Page Title="Visión Mundial - Home" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WV.WebApplication.Pages.Default" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <%-- <script src="/Content/assets/js/default.js"></script>--%>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="row text-center pad-top">

                <div class="col-md-4 col-sm-4">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            Inscripcion de Beneficiarios
                        </div>
                        <div class="panel-body">
                            
                                <div class="panel panel-primary text-center no-boder bg-color-green">
                                    <div class="panel-body">
                                         <a href="Registration">
                                        <i class="fa fa-file-text-o fa-5x"></i>
                                         </a>
                                    </div>
                                    
                                </div>
                            
                        </div>
                        <div class="panel-footer">
                            Registro de Informacion de Beneficiarios
                        </div>
                    </div>
                </div>

                <div class="col-md-4 col-sm-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Control de Asistencia
                        </div>
                        <div class="panel-body">
                            <div class="panel panel-primary text-center no-boder bg-color-blue">
                                    <div class="panel-body">
                                         <a href="AttendanceTracking">
                                        <i class="fa fa-columns fa-5x"></i>
                                      </a>
                                    </div>
                                    
                                </div>
                        </div>
                        <div class="panel-footer">
                            Registro de Asistencia a Actividades
                        </div>
                    </div>
                </div>

                <div class="col-md-4 col-sm-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Control de Actividades
                        </div>
                        <div class="panel-body">
                            <div class="panel panel-primary text-center no-boder bg-color-blue">
                                    <div class="panel-body">
                                         <a href="Activity">
                                        <i class="fa fa-clock-o fa-5x"></i>
                                        </a>
                                    </div>
                                    
                                </div>
                        </div>
                        <div class="panel-footer">
                            Creación de Eventos (Actividades)
                        </div>
                    </div>
                </div>


            </div>

       
            <div class="row text-center pad-top">

                <div class="col-md-4 col-sm-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Asignacion de Recursos Humanos
                        </div>
                        <div class="panel-body">
                            <div class="panel panel-primary text-center no-boder bg-color-blue">
                                    <div class="panel-body">
                                         <a href="AssignRRHH">
                                        <i class="fa fa-users fa-5x"></i>
                                        </a>
                                    </div>
                                    
                                </div>
                        </div>
                        <div class="panel-footer">
                            Aisgnacion de Personal a Proyectos
                        </div>
                    </div>
                </div>

                <div class="col-md-4 col-sm-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Control de Personal
                        </div>
                        <div class="panel-body">
                        <div class="panel panel-primary text-center no-boder bg-color-blue">
                                    <div class="panel-body">
                                        <a href="ControlStaff">
                                        <i class="fa fa-table fa-5x"></i>
                                        </a>
                                    </div>
                                    
                                </div>
                        </div>
                        <div class="panel-footer">
                           Control de Bitacoras y Plan Semanal
                        </div>
                    </div>
                </div>

                <div class="col-md-4 col-sm-4">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            Reportes
                        </div>
                        <div class="panel-body">
                            <div class="panel panel-primary text-center no-boder bg-color-green">
                                    <div class="panel-body">
                                         <a href="#">
                                        <i class="fa fa-bar-chart-o fa-5x"></i>
                                        </a>
                                    </div>
                                    
                                </div>
                        </div>
                        <div class="panel-footer">
                            Informacion de Proyectos y Programas
                        </div>
                    </div>
                </div>


            </div>

        </div>






    </div>

</asp:Content>

