<%@ Page Title="Visión Mundial - Mapa del Sitio" Language="C#" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="WV.WebApplication.Pages.Misc.SiteMap" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>



<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">SiteMap</div>
    <div class="col-md-12">
        <h1 class="page-header">Mapa del Sitio
        </h1>
    </div>
    <div class="row">

        <div class="col-md-2"></div>

        <div class="col-md-8">
          
           
                <div class="clearfix row">
                    <div>
                        
                        <h2 id="pages">Paginas</h2>
                        <br />
                        <ul>
                            <li class="page_item "><a href="Home">Inicio</a></li>
                            <li class="page_item "><a href="About">Acerca de</a></li>
                            <li class="page_item "><a href="User">Usuarios</a></li>
                            <li class="page_item "><a href="Role">Roles</a></li>
                            <li class="page_item "><a href="Options">Configuración de Contenido</a></li>
                            <li class="page_item "><a href="Community">Comunidades</a></li>
                            <li class="page_item "><a href="ProgramType">Tipo de Programa</a></li>
                            <li class="page_item "><a href="Staff">Personal</a></li>
                            <li class="page_item "><a href="Project">Proyectos</a></li>
                            <li class="page_item "><a href="JobType">Tipo de Puesto</a></li>
                            <li class="page_item "><a href="Registration">Registro de Beneficiarios</a></li>
                            <li class="page_item "><a href="LogBook">Bitacora</a></li>
                            <li class="page_item "><a href="ControlStaff">Control de Personal</a></li>
                            <li class="page_item "><a href="WeeklyPlan">Plan Semanal</a></li>
                            <li class="page_item "><a href="Job">Puesto</a></li>
                            <li class="page_item "><a href="AssignRRHH">Asignar Recurso Humano</a></li>
                            <li class="page_item "><a href="Program">Programas</a></li>
                            <li class="page_item "><a href="Activity">Control de Actividades</a></li>
                            <li class="page_item "><a href="AttendanceTracking">Control de Asistencia</a></li>
                            <li class="page_item "><a href="MassActions">Acciones de Mantenimiento</a></li>
                            <li class="page_item "><a href="Subject">Materia</a></li>
                            <li class="page_item "><a href="Exam">Examenes</a></li>
                            <li class="page_item "><a href="AssignSubject">Asignar Materia</a></li>
                            <li class="page_item "><a href="RecordGrades">Control de Notas</a></li>
                            <li class="page_item "><a href="GradesControl">Administración de notas</a></li>
                            <li class="page_item "><a href="AdminReports">Reportes de Administración</a></li>
                            <li class="page_item "><a href="ProjectReports">Reportes de Proyectos</a></li>
                            <li class="page_item "><a href="ProgramReports">Reportes de Programas</a></li>
                            <li class="page_item "><a href="GradesReports">Reportes de Notas</a></li>
                        </ul>
                        
                    </div>
                </div>
           
            



        </div>

        <div class="col-md-2"></div>


    </div>
</asp:Content>
