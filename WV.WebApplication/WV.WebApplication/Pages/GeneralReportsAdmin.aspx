<%@ Page Title="Visión Mundial - Reportes Administracion Sistema" Language="C#" AutoEventWireup="true" CodeBehind="GeneralReportsAdmin.aspx.cs" Inherits="WV.WebApplication.Pages.GeneralReportsAdmin" MasterPageFile="~/WV.Master" ClientIDMode="Static"%>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	
	<script src="/Content/assets/js/reportsadmin.js"></script>


</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
	<%--<style type="text/css">
		.btn-primary:hover, .btn-primary:focus, .btn-primary:active, .btn-primary.active, .open .dropdown-toggle.btn-primary {
			background-color: #7db831;
		}
	</style>--%>
	<div runat="server" id="pagename" class="hidden">Reportes_Administracion</div>

	<div id="contenido" class="panel-body">
		<div class="row">
			<div class="col-md-12">
				<div class="panel panel-default">
					<div class="panel-heading">
						Reportes - Administracion
					</div>
					<div class="panel-body">
						<div class="row">

							<div class="col-md-12" style="padding: 0px;">
								<div class="content-header content-header-media" style="background-color: #09192a; height: 130px; padding-left: 0px;">
									<!-- Remove inline styles when have background image. -->
									<div class="header-section">
										<h1 style="color:#999;">Reportes - Administracion de Sistema<br>
											<%--<small >Realice Eliminacion Masiva de Registros. <strong><span id="attendance_date"></span>Realizar Cuidadosamente!</strong></small>--%></h1>
									</div>
									<!-- For best results use an image with a resolution of 2560x248 pixels (You can also use a blurred image with ratio 10:1 - eg: 1000x100 pixels - it will adjust and look great!) -->
									<!-- <img src="/images/proui-2.0/placeholders/headers/profile_header.jpg" alt="header image" class="animation-pulseSlow"> -->
								</div>

							</div>
						</div>
						<br />
						<div class="row">

							<div class="row text-center pad-top">
								<div class="col-md-4 col-sm-4">
									<div class="panel panel-primary">
										<div class="panel-heading">
											Usuarios
										</div>
										<div class="panel-body">
											<h4>
												<div class="panel panel-primary text-left no-boder bg-color-blue">
													<div class="panel-body">
														<a href="javascript:void(0)">
															<i class="fa fa-file-text-o fa-3x"></i>
														</a>

														<strong>Lista de Usuarios</strong>

													</div>

												</div>
											</h4>
											<div class="form-inline">
												<select id="cmbusuarios" class="form-control">
													
												</select>
												<a href="javascript:void(0)" id="btnreporteusuario"  class="btn btn-primary pull-right">Generar</a>
											</div>
										</div>
									</div>
								</div>

								<div class="col-md-4 col-sm-4">
									<div class="panel panel-info">
										<div class="panel-heading">
										  Roles
										</div>
										<div class="panel-body">
											<h4>
												<div class="panel panel-primary text-left no-boder bg-color-blue">
													<div class="panel-body">
														<a href="javascript:void(0)">
															<i class="fa fa-file-text-o fa-3x"></i>
														</a>

														<strong>Lista de Roles</strong>

													</div>

												</div>
											</h4>
											 <a href="/Handlers/GeneralReporstAdmin.ashx?method=getrolesreport" id="btnreporteroles"  class="btn btn-primary pull-right">Generar</a>

										</div>
									</div>
								</div>

								<div class="col-md-4 col-sm-4">
									<div class="panel panel-success">
										<div class="panel-heading">
											Opciones
										</div>
										<div class="panel-body">
											<h4>
												<div class="panel panel-primary text-left no-boder bg-color-green">
													<div class="panel-body">
														<a href="javascript:void(0)">
															<i class="fa fa-file-text-o fa-3x"></i>
														</a>

														<strong>Roles y Opciones</strong>

													</div>

												</div>
											</h4>
											<div class="form-inline">
												<select id="cmbroles" class="form-control">
													
												</select>
												<a href="javascript:void(0)" id="btnreporterol"  class="btn btn-primary pull-right">Generar</a>
											</div>
										   
											<%--<button type="button" class="btn btn-primary pull-right" data-toggle="modal" data-target="#modal-configure_daily_attendance_sheet"><i class="fa fa-cog"></i>Configurar</button>--%>

										</div>
									</div>
								</div>
							</div>

						   <%-- <div class="row text-center pad-top">
								
							</div>--%>

						   

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