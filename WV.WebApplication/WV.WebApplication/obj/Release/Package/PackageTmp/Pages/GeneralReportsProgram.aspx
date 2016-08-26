<%@ Page Title="Visión Mundial - Reportes Administracion Proyectos" Language="C#" AutoEventWireup="true" CodeBehind="GeneralReportsProgram.aspx.cs" Inherits="WV.WebApplication.Pages.GeneralReportsProgram" MasterPageFile="~/WV.Master" ClientIDMode="Static"%>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	
	<script src="/Content/assets/js/reportsprograms.js"></script>


</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
	<%--<style type="text/css">
		.btn-primary:hover, .btn-primary:focus, .btn-primary:active, .btn-primary.active, .open .dropdown-toggle.btn-primary {
			background-color: #7db831;
		}
	</style>--%>
	<div runat="server" id="pagename" class="hidden">Reportes_Programas</div>

	<div id="contenido" class="panel-body">
		<div class="row">
			<div class="col-md-12">
				<div class="panel panel-default">
					<div class="panel-heading">
						Reportes - Programas
					</div>
					<div class="panel-body">
						<div class="row">

							<div class="col-md-12" style="padding: 0px;">
								<div class="content-header content-header-media" style="background-color: #09192a; height: 130px; padding-left: 0px;">
									<!-- Remove inline styles when have background image. -->
									<div class="header-section">
										<h1 style="color:#999;">Reportes - Monitoreo de Programas<br>
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
						   <%--<div class="col-md-4 col-sm-4">
									<div class="panel panel-primary">
										<div class="panel-heading">
											Personal
										</div>
										<div class="panel-body">
											<h4>
												<div class="panel panel-primary text-left no-boder bg-color-blue">
													<div class="panel-body">
														<a href="javascript:void(0)">
															<i class="fa fa-file-text-o fa-3x"></i>
														</a>

														<strong>Bitacora Diaria</strong>

													</div>

												</div>
											</h4>
											<div class="form-inline">
												<select id="cmbpersonas" class="form-control">
													
												</select>
                                                <select id="cmbfechabitacora" class="form-control">
													
												</select>
												
											</div>
                                            <p><a href="javascript:void(0)" id="btnreportebitacora"  class="btn btn-primary pull-right">Generar</a></p>
										</div>
									</div>
								</div>

								<div class="col-md-4 col-sm-4">
									<div class="panel panel-info">
										<div class="panel-heading">
										  Personal
										</div>
										<div class="panel-body">
											<h4>
												<div class="panel panel-primary text-left no-boder bg-color-blue">
													<div class="panel-body">
														<a href="javascript:void(0)">
															<i class="fa fa-file-text-o fa-3x"></i>
														</a>

														<strong>Plan Semanal</strong>

													</div>

												</div>
											</h4>
                                            <div class="form-inline">
												<select id="cmbpersonasplan" class="form-control">
													
												</select>
                                                <select id="cmbfechaplan" class="form-control">
													
												</select>
												
											</div>
                                             <p><a href="javascript:void(0)" id="btnreporteplan"  class="btn btn-primary pull-right">Generar</a></p>
										</div>
									</div>
								</div>--%>

                           <%--     <div class="col-md-4 col-sm-4">
									<div class="panel panel-success">
										<div class="panel-heading">
											Tipos de Programas
										</div>
										<div class="panel-body">
											<h4>
												<div class="panel panel-primary text-left no-boder bg-color-green">
													<div class="panel-body">
														<a href="javascript:void(0)">
															<i class="fa fa-file-text-o fa-3x"></i>
														</a>

														<strong>Lista de Mecanismos</strong>

													</div>

												</div>
											</h4>

											<div class="form-inline">
												<a href="/Handlers/GeneralReportsProject.ashx?method=getjobreport" id="btnreportetipoprograma"  class="btn btn-primary pull-right">Generar</a>
											</div>
										   
											

										</div>
									</div>
								</div>--%>

                                <div class="col-md-4 col-sm-4">
									<div class="panel panel-primary">
										<div class="panel-heading">
											Programas
										</div>
										<div class="panel-body">
											<h4>
												<div class="panel panel-primary text-left no-boder bg-color-blue">
													<div class="panel-body">
														<a href="javascript:void(0)">
															<i class="fa fa-file-text-o fa-3x"></i>
														</a>

														<strong>Detalle de Programas</strong>

													</div>

												</div>
											</h4>
											<div class="form-inline">
												<select id="cmbprograma" class="form-control">
													
												</select>
												
											</div>
                                            <p><a href="javascript:void(0)" id="btnreporteprograma"  class="btn btn-primary pull-right">Generar</a></p>
										</div>
									</div>
								</div>

                                <div class="col-md-4 col-sm-4">
									<div class="panel panel-primary">
										<div class="panel-heading">
											Proyecto
										</div>
										<div class="panel-body">
											<h4>
												<div class="panel panel-primary text-left no-boder bg-color-blue">
													<div class="panel-body">
														<a href="javascript:void(0)">
															<i class="fa fa-file-text-o fa-3x"></i>
														</a>

														<strong>Programas en Proyecto</strong>

													</div>

												</div>
											</h4>
											<div class="form-inline">
												<select id="cmbproyecto" class="form-control">
													
												</select>
												
											</div>
                                            <p><a href="javascript:void(0)" id="btnreporteproyecto"  class="btn btn-primary pull-right">Generar</a></p>
										</div>
									</div>
								</div>

								
							</div>

						    <div class="row text-center pad-top">
								
                                <%--<div class="col-md-4 col-sm-4">
									<div class="panel panel-success">
										<div class="panel-heading">
											Proyecto
										</div>
										<div class="panel-body">
											<h4>
												<div class="panel panel-primary text-left no-boder bg-color-green">
													<div class="panel-body">
														<a href="javascript:void(0)">
															<i class="fa fa-file-text-o fa-3x"></i>
														</a>

														<strong>Detalle de Puestos</strong>

													</div>

												</div>
											</h4>

											<div class="form-inline">
												<a href="/Handlers/GeneralReportsProject.ashx?method=getjobreport" id="btnreportepuesto"  class="btn btn-primary pull-right">Generar</a>
											</div>
										   
											

										</div>
									</div>
								</div>--%>

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
