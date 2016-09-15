<%@ Page Title="Visión Mundial - Control de Notas" Language="C#" AutoEventWireup="true" CodeBehind="GradesControl.aspx.cs" Inherits="WV.WebApplication.Pages.GradesControl" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	<%-- <script src="/Content/assets/js/default.js"></script>--%>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
	<div runat="server" id="pagename" class="hidden">Administracion_Notas</div>
	<div id="contenido" class="panel-body">
		<div class="row">
            <div class="col-md-12" style="padding: 0px;">
								<div class="content-header content-header-media" style="background-color: #09192a; height: 110px; padding:10px; margin-bottom:20px;">
									<!-- Remove inline styles when have background image. -->
									<div class="header-section">
										<h1 style="color:#999;">Administración de Notas<br>
											<small >Mecanismo CIC.</small></h1>
									</div>
									<!-- For best results use an image with a resolution of 2560x248 pixels (You can also use a blurred image with ratio 10:1 - eg: 1000x100 pixels - it will adjust and look great!) -->
									<!-- <img src="/images/proui-2.0/placeholders/headers/profile_header.jpg" alt="header image" class="animation-pulseSlow"> -->
								</div>

							</div>
            <div class="col-md-2">
            </div>
			<div class="row text-center pad-top">

				<div class="col-md-4 col-sm-4">
					<div class="panel panel-success">
						<div class="panel-heading">
							Registro de Asignaturas
						</div>
						<div class="panel-body">
							
								<div class="panel panel-primary text-center no-boder bg-color-green">
									<div class="panel-body">
										 <a href="Subject">
										<i class="fa fa-file-text-o fa-5x"></i>
										 </a>
									</div>
									
								</div>
							
						</div>
						<div class="panel-footer">
							Asignaturas
						</div>
					</div>
				</div>

				<div class="col-md-4 col-sm-4">
					<div class="panel panel-primary">
						<div class="panel-heading">
							Control de Examenes
						</div>
						<div class="panel-body">
							<div class="panel panel-primary text-center no-boder bg-color-blue">
									<div class="panel-body">
										 <a href="Exam">
										<i class="fa fa-columns fa-5x"></i>
									  </a>
									</div>
									
								</div>
						</div>
						<div class="panel-footer">
							Examenes
						</div>
					</div>
				</div>

				


			</div>

            <div class="col-md-2">
            </div>
			<div class="row text-center pad-top">

				<div class="col-md-4 col-sm-4">
					<div class="panel panel-primary">
						<div class="panel-heading">
							Asignacion de Materias
						</div>
						<div class="panel-body">
							<div class="panel panel-primary text-center no-boder bg-color-blue">
									<div class="panel-body">
										 <a href="AssignSubject">
										<i class="fa fa-users fa-5x"></i>
										</a>
									</div>
									
								</div>
						</div>
						<div class="panel-footer">
							Asignacion Materias
						</div>
					</div>
				</div>

				<div class="col-md-4 col-sm-4">
					<div class="panel panel-primary">
						<div class="panel-heading">
							Control de Notas
						</div>
						<div class="panel-body">
						<div class="panel panel-primary text-center no-boder bg-color-blue">
									<div class="panel-body">
										<a href="RecordGrades">
										<i class="fa fa-table fa-5x"></i>
										</a>
									</div>
									
								</div>
						</div>
						<div class="panel-footer">
						   Registro de Calificaciones
						</div>
					</div>
				</div>

				


			</div>

		</div>






	</div>

</asp:Content>
