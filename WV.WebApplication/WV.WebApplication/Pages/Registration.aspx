<%@ Page Title="Visión Mundial - Registro" Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="WV.WebApplication.Pages.Registration" MasterPageFile="~/WV.Master" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="MetaContent" runat="server">
    <link href="<%# ResolveUrl("~/") %>Content/assets/js/dataTables/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/select.bootstrap.min.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/editor.dataTables.min.css" rel="stylesheet" />
    <link href="<%# ResolveUrl("~/") %>Content/assets/css/jquery-ui.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/assets/js/bootstrapValidator.js"></script>
    <script src="/Content/assets/js/dataTables/jquery.dataTables.min.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.bootstrap.min.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.bootstrap.js"></script>
    <script src="/Content/assets/js/dataTables/dataTables.select.min.js"></script>
    <script src="/Content/assets/js/jquery-ui.min.js"></script>
    <script src="/Content/assets/js/registration.js"></script>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div runat="server" id="pagename" class="hidden">Inscripcion</div>

    <div id="contenido" class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Registro de Participantes
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal default-row-spacer">
                            <button id="nuevobeneficiario" class="btn btn-info btn-sm"><i class="fa fa-plus-square-o"></i>Nuevo</button>
                            <button id="editarbeneficiario" class="btn btn-primary btn-sm"><i class="fa fa-pencil-square-o"></i>Editar</button>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                    <table class="table table-striped table-bordered table-hover" id="example">
                                        <thead>
                                            <tr>
                                                <th>Participantes</th>
                                            </tr>
                                        </thead>
                                    </table>
                            </div>
                            <div class="col-md-8">

                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a href="#principal" data-toggle="tab">Información Principal</a>
                                            </li>
                                            <li class=""><a href="#adicional" data-toggle="tab">Adicional</a>
                                            </li>
                                            <li class=""><a href="#educacion" data-toggle="tab">Educación</a>
                                            </li>
                                            <li class=""><a href="#salud" data-toggle="tab">Salud</a>
                                            </li>
                                            <li class=""><a href="#compromiso" data-toggle="tab">Compromiso</a>
                                            </li>
                                        </ul>
                                        <form id="form1">
                                            <div class="tab-content">
                                                <div class="tab-pane fade active in" id="principal">

                                                    <div class="form-group hidden in-controls">
                                                        <label>Seleccione el Programa</label>
                                                        <select id="cmbprograma" class="form-control">
                                                        </select>
                                                    </div>
                                                    <div class="form-group txt-controls ">
                                                        <label>Programa</label>
                                                        <p id="lbl_programa" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="inp_nombre" class="control-label">Nombres:</label>
                                                        <input type="text" id="inp_nombre" name="inp_nombre" class="form-control">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Nombres:</label>
                                                        <p id="lblp_nombre" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="inp_apellido" class="control-label">Apellidos:</label>
                                                        <input type="text" id="inp_apellido" name="inp_apellido" class="form-control">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Apellidos:</label>
                                                        <p id="lblp_apellido" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        RC:
                                                        <input name="optionpatrocinio" type="radio" value="RC" >
                                                        NRC:
                                                        <input name="optionpatrocinio" type="radio" value="NRC">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group hidden in-controls">
                                                        <label for="inp_codigo" class="control-label">Codigo:</label>
                                                        <input type="text" id="inp_codigo" name="inp_codigo" class="form-control">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Codigo</label>
                                                        <p id="lblp_codigo" class="form-control-static"></p>
                                                    </div>


                                                    <div class="form-group hidden in-controls">
                                                        <label>Seleccione la edad año/mes:</label>

                                                        
                                                    </div>
                                                    <div class="form-horizontal hidden in-controls default-row-spacer">
                                                        Años
                                                        <select id="cmbanio" >
                                                            <option>0</option>
                                                            <option>1</option>
                                                             <option>2</option>
                                                             <option>3</option>
                                                             <option>4</option>
                                                             <option>5</option>
                                                             <option>6</option>
                                                             <option>7</option>
                                                             <option>8</option>
                                                             <option>9</option>
                                                             <option>10</option>
                                                             <option>11</option>
                                                             <option>12</option>
                                                             <option>13</option>
                                                             <option>14</option>
                                                             <option>15</option>
                                                             <option>16</option>
                                                             <option>17</option>
                                                             <option>18</option>
                                                             <option>19</option>
                                                             <option>20</option>
                                                             <option>21</option>
                                                        </select>
                                                        Meses
                                                        <select id="cmbmeses" >
                                                            <option>0</option>
                                                            <option>1</option>
                                                             <option>2</option>
                                                             <option>3</option>
                                                             <option>4</option>
                                                             <option>5</option>
                                                             <option>6</option>
                                                             <option>7</option>
                                                             <option>8</option>
                                                             <option>9</option>
                                                             <option>10</option>
                                                             <option>11</option>
                                                        </select>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Edad:    </label>
                                                        <p id="lblp_edad" class="form-control-static"></p>
                                                    </div>




                                                    <div class="form-group hidden in-controls">
                                                        <label>Género:</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiogenero" value="Masculino">Masculino</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiogenero" value="Femenino">Femenino</label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group txt-controls ">
                                                        <label>Género:</label>
                                                        <p id="lblp_genero" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="inp_dui" class="control-label">Documento Único de Identidad:</label>
                                                        <input type="text" id="inp_dui" name="inp_dui" class="form-control">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Documento Único de Identidad:</label>
                                                        <p id="lblp_dui" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="inp_direccion" class="control-label">Dirección:</label>
                                                        <input type="text" id="inp_direccion" name="inp_direccion" class="form-control">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Dirección:</label>
                                                        <p id="lblp_direccion" class="form-control-static"></p>
                                                    </div>


                                                </div>

                                                <div class="tab-pane fade" id="adicional">

                                                    <div class="form-group hidden in-controls">
                                                        <label for="ina_emergenciacontacto" class="control-label">En Caso de Emergencia llamar a:</label>
                                                        <input type="text" id="ina_emergenciacontacto" name="ina_emergenciacontacto" class="form-control">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>En Caso de Emergencia llamar a:</label>
                                                        <p id="lbla_emergenciacontacto" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="ina_emergenciacontacto" class="control-label">Numero de Emergencia:</label>
                                                        <input type="text" id="ina_emergencianumero" name="ina_emergencianumero" class="form-control">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Numero de Emergencia:</label>
                                                        <p id="lbla_emergencianumero" class="form-control-static"></p>
                                                    </div>
                                                    <div class="form-group hidden in-controls">
                                                        <label for="ina_registronacimiento" class="control-label">¿Posee Registro de Nacimiento?</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradioregistro" value="si">Si</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradioregistro" value="no">No</label>
                                                        </div>
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>¿Posee Registro de Nacimiento?</label>
                                                        <p id="lbla_registronacimiento" class="form-control-static"></p>
                                                    </div>
                                                </div>

                                                <div class="tab-pane fade" id="educacion">

                                                    <div class="form-group hidden in-controls">
                                                        <label for="ine_estudia" class="control-label">¿Se encuentra el Niño,Niña,Adolescente o Joven estudiando actualmente?</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradioestudia" value="si">Si</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradioestudia" value="no">No</label>
                                                        </div>

                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>¿Se encuentra el Niño,Niña,Adolescente o Joven estudiando actualmente?</label>
                                                        <p id="lble_estudia" class="form-control-static"></p>
                                                    </div>
                                                    <div class="form-group hidden in-controls">
                                                        <label for="ine_grado" class="control-label">Grado que Estudia:</label>
                                                        <select id="cmbgrado" class="form-control">
                                                            <option></option>
                                                            <option>3er Año Bachillerato</option>
                                                            <option>2do Año Bachillerato</option>
                                                            <option>1er Año Bachillerato</option>
                                                            <option>9no Grado</option>
                                                            <option>8vo Grado</option>
                                                            <option>7mo Grado</option>
                                                            <option>6to Grado</option>
                                                            <option>5to Grado</option>
                                                            <option>4to Grado</option>
                                                            <option>3er Grado</option>
                                                            <option>2do Grado</option>
                                                            <option>1er Grado</option>
                                                            <option>Parvularia</option>
                                                        </select>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>¿Grado Actual?</label>
                                                        <p id="lble_gradoactual" class="form-control-static"></p>
                                                    </div>
                                                    <div class="form-group hidden in-controls">
                                                        <label for="ine_motivo" class="control-label">Motivo:</label>
                                                        <input type="text" id="ine_motivo" name="ine_motivo" class="form-control">
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="ine_ultimogrado" class="control-label">¿Ultimo Grado Cursado?</label>
                                                        <select id="cmbultimogrado" class="form-control">
                                                            <option></option>
                                                            <option>3er Año Bachillerato</option>
                                                            <option>2do Año Bachillerato</option>
                                                            <option>1er Año Bachillerato</option>
                                                            <option>9no Grado</option>
                                                            <option>8vo Grado</option>
                                                            <option>7mo Grado</option>
                                                            <option>6to Grado</option>
                                                            <option>5to Grado</option>
                                                            <option>4to Grado</option>
                                                            <option>3er Grado</option>
                                                            <option>2do Grado</option>
                                                            <option>1er Grado</option>
                                                            <option>Parvularia</option>
                                                           
                                                        </select>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>¿Ultimo Grado Cursado?</label>
                                                        <p id="lble_ultimogrado" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="ine_ultimoaño" class="control-label">¿Ultimo año calendario que estudió?</label>
                                                        <select id="cmbultimoaño" class="form-control">
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
                                                            <option>2001</option>
                                                            <option>2000</option>
                                                            <option>1999</option>
                                                            <option>1998</option>
                                                            <option>1997</option>
                                                            <option>1996</option>
                                                            <option>1995</option>
                                                            <option>1994</option>
                                                            <option>1993</option>
                                                            <option>1992</option>
                                                            <option>1991</option>
                                                        </select>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>¿Ultimo año calendario que estudió?</label>
                                                        <p id="lble_ultimoaño" class="form-control-static"></p>
                                                    </div>
                                                    <div class="form-group hidden in-controls">
                                                        <label for="ine_centroescolar" class="control-label">Nombre del Centro Escolar:</label>
                                                        <input type="text" id="ine_centroescolar" name="ine_centroescolar" class="form-control">
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Nombre del Centro Escolar:</label>
                                                        <p id="lble_centroescolar" class="form-control-static"></p>
                                                    </div>
                                                    <div class="form-group hidden in-controls">
                                                        <label for="ine_turno" class="control-label">Turno en el que estudia el Niño,Niña,Adolescente o Joven:</label>
                                                        <div class="radio">
                                                        <label><input  name="optionradioturno" type="radio" value="Mañana" >Mañana</label>
                                             </div>
                                                        <div class="radio">
                                                        <label><input  name="optionradioturno" type="radio" value="Tarde" >Tarde</label>
                                                        </div>
                                                        <div class="radio">
                                                        <label><input  name="optionradioturno" type="radio" value="Otro">Otro</label>
                                                            </div>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Turno en el que estudia el Niño,Niña,Adolescente o Joven</label>
                                                        <p id="lble_turno" class="form-control-static"></p>
                                                    </div>
                                                </div>

                                                <div class="tab-pane fade" id="salud">
                                                
                                                    <div class="form-group hidden in-controls">
                                                        <label for="ins_estadosalud" class="control-label">Estado de Salud del niño/a:</label>
                                                        <div class="radio">
                                                        <label><input  name="radioestadosalud" type="radio" value="Satisfactoria">Satisfactoria</label>
                                                            </div>
                                                        <div class="radio">
                                                        <label><input  name="radioestadosalud" type="radio" value="No Satisfactoria" >No Satisfactoria</label> 
                                                            </div>
                                                        
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Estado de Salud del niño/a:</label>
                                                        <p id="lbls_estadosalud" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="ins_tarjeta" class="control-label">¿Tarjeta de Salud/ Crecimiento Verificada?</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiotarjeta" value="si">Si</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiotarjeta" value="no">No</label>
                                                        </div>
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>¿Tarjeta de Salud/ Crecimiento Verificada?</label>
                                                        <p id="lbls_tarjeta" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label class="control-label">¿Sigue la Curva de Crecimiento?</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiocurvacrecimiento" value="si">Si</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiocurvacrecimiento" value="no">No</label>
                                                        </div>
                                                       
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="ins_curva" class="control-label">Inmunización/Fecha:</label>
                                                       <input name="ins_curva" id="ins_curva" type="text" class="form-control-static input-sm">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Inmunización/Fecha:</label>
                                                        <p id="lbls_curva" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label class="control-label">¿Tiene las vacunas de acuerdo a su edad?</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiovacunas" value="si">Si</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiovacunas" value="no">No</label>
                                                        </div>
                                                       
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="ins_vacuna" class="control-label">Inmunización/Fecha:</label>
                                                       <input name="ins_vacuna" id="ins_vacuna" type="text" class="form-control-static input-sm">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Inmunización/Fecha:</label>
                                                        <p id="lbls_vacuna" class="form-control-static"></p>
                                                    </div>

                                                     
                                                    
                                                    <div class="form-group hidden in-controls">
                                                        <label class="control-label">¿Padece el Niño, Niña, Adolescente o Joven de alguna enfermedad crónica?</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiopadece" value="si">Si</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiopadece" value="no">No</label>
                                                        </div>
                                                       
                                                    </div>


                                                    <div class="form-group hidden in-controls">
                                                        <label for="ins_enfermedad" class="control-label">Enfermedad:</label>
                                                        <input type="text" id="ins_enfermedad" name="ins_enfermedad" class="form-control">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Enfermedad:</label>
                                                        <p id="lbls_enfermedad" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label class="control-label">¿Posee el Niño, Niña, Adolescente o Joven de alguna discapacidad?</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiodiscapacidad" value="si">Si</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradiodiscapacidad" value="no">No</label>
                                                        </div>
                                                       
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="ins_discapacidad" class="control-label">Discapacidad:</label>
                                                        <input type="text" id="ins_discapacidad" name="ins_discapacidad" class="form-control">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Discapacidad:</label>
                                                        <p id="lbls_discapacidad" class="form-control-static"></p>
                                                    </div>
                                                
                                                </div>

                                                <div class="tab-pane fade" id="compromiso">


                                                    <div class="form-group hidden in-controls">
                                                        <label for="inc_reconoce" class="control-label">¿El niño ó responsable reconocen su participacion en actividades de Vision Mundial?</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradioreconoce" value="si">Si</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradioreconoce" value="no">No</label>
                                                        </div>
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>¿El niño ó responsable reconocen su participacion en actividades de Vision Mundial?</label>
                                                        <p id="lblc_reconoce" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="inc_problema" class="control-label">¿Se observó algun problema respecto a la proteccion del niño?</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradioproblema" value="si">Si</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradioproblema" value="no">No</label>
                                                        </div>
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>¿Se observó algun problema respecto a la proteccion del niño?</label>
                                                        <p id="lblc_problema" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="inc_congrega" class="control-label">¿El niño(a) se congrega en alguna iglesia?</label>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradioiglesia" value="si">Si</label>
                                                        </div>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optradioiglesia" value="no">No</label>
                                                        </div>
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>¿El niño(a) se congrega en alguna iglesia?</label>
                                                        <p id="lblc_congrega" class="form-control-static"></p>
                                                    </div>


                                                    <div class="form-group hidden in-controls">
                                                        <label for="inc_iglesia" class="control-label">Nombre de Iglesia:</label>
                                                        <input type="text" id="inc_iglesia" name="inc_iglesia" class="form-control">
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Nombre de Iglesia:</label>
                                                        <p id="lblc_iglesia" class="form-control-static"></p>
                                                    </div>

                                                    <div class="form-group hidden in-controls">
                                                        <label for="inc_comentario" class="control-label">Comentarios:</label>
                                                        <textarea rows="4" cols="40" id="inc_comentario" name="inc_comentario" class="form-control"></textarea>
                                                        <%-- <div class="help-block with-errors"></div>--%>
                                                    </div>
                                                    <div class="form-group txt-controls">
                                                        <label>Comentarios:</label>
                                                        <p id="lblc_comentario" class="form-control-static"></p>
                                                    </div>
                                                </div>
                                            </div>
                                            <button id="savepage" data-disable="" type="button" class="btn btn-default btn-sm in-controls hidden"><i class=" fa fa-floppy-o "></i>Guardar</button>
                                            <button id="cancelpage" type="button" class="btn btn-default btn-sm in-controls hidden"><i class=" fa fa-times "></i>Cancelar</button>
                                             <%--<button id="editpage" type="button" class="btn btn-default btn-sm in-controls hidden"><i class=" fa fa-pencil-square "></i>Editar</button>--%>
                                            
                                            <input type="text" id="screenmode" class="hidden" />
                                        </form>
                                    </div>

                                </div>
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
