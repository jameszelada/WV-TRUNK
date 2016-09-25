using DataLayer;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using WV.WebApplication.Reports;
using WV.WebApplication.Utils;
using System.Web.UI.DataVisualization.Charting;
using System.Web.Helpers;


namespace WV.WebApplication.Handlers
{
    /// <summary>
    /// Summary description for GeneralReportsProgram
    /// </summary>
    public class GeneralReportsProgram : DataAccess, IHttpHandler, IRequiresSessionState
    {

        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Persona> _persona;
        IDataRepository<Proyecto> _proyecto;
        IDataRepository<Programa> _programa;
        IDataRepository<Comunidad> _comunidad;
        IDataRepository<Actividad> _actividad;
        IDataRepository<Beneficiario> _beneficiario;
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            switch (MethodName.ToLower())
            {
                case "getallprograms":
                    context.Response.Write(GetAllPrograms());
                    break;
                case "getallactivities":
                    context.Response.Write(GetAllActivities(context));
                    break;
                //case "getalllogbooks":
                //    context.Response.Write(GetLogBooks(context));
                //    break;
                case "getallcommunities":
                    context.Response.Write(GetAllCommunities());
                    break;
                case "getprogramreport":
                    context.Response.Clear();
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Reporte-Programa-{0}.pdf", DateTime.Now.ToShortDateString()));
                    context.Response.ContentType = "application/pdf";
                    context.Response.Buffer = true;
                    context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    byte[] b = GetProgramReport(context);
                    context.Response.OutputStream.Write(b, 0, b.Length);
                   context.Response.OutputStream.Flush();
                   // context.Response.BinaryWrite(GetProgramReport(context));
                    context.Response.End();
                
                
                    break;
                case "getprojectreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Proyecto-programas-{0}.pdf", DateTime.Now.ToShortDateString()));
                    context.Response.BinaryWrite(GetProjectReport(context));
                    break;
                case "getactivityreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=Programa-Actividades.pdf");
                    context.Response.BinaryWrite(GetProgramActivityReport(context));
                    break;
                case "getbeneficiaryprogramreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=Beneficiario-Programa.pdf");
                    context.Response.BinaryWrite(GetProgramBeneficiaryReport(context));
                    break;
                case "getbeneficiarycommunityreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=Beneficiarios-Comunidad.pdf");
                    context.Response.BinaryWrite(GetBeneficiaryCommnityReport(context));
                    break;
                case "getattendancereport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=Asistencia-Acvitidad.pdf");
                    context.Response.BinaryWrite(GetAttendanceActivityReport(context));
                    break;
            }
        }

        

        

        private void InitializeObjects()
        {

            _context = new AWContext();
            _programa = new DataRepository<IAWContext, Programa>(_context);
            _proyecto = new DataRepository<IAWContext, Proyecto>(_context);
            _comunidad = new DataRepository<IAWContext, Comunidad>(_context);
            _actividad = new DataRepository<IAWContext, Actividad>(_context);
            _beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);
        }


        private string GetAllActivities(HttpContext context)
        {
            string options = "";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());
            try
            {
                var programa = _programa.GetFirst(p => p.ID_Programa == ID_Programa);

                if (programa.Actividad.Count > 0)
                {
                    foreach (var actividad in programa.Actividad.OrderByDescending(a => a.Fecha))
                    {
                        options += "<option data-id-activity='" + actividad.ID_Actividad + "'>" + actividad.Fecha.ToShortDateString() + "</option>";
                    }
                }


                response.IsSucess = true;
                response.ResponseData = options;
                response.Message = string.Empty;
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string GetAllPrograms()
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsProgramas = "";
            try
            {
                var programas = _programa.Select();


                foreach (var programa in programas)
                {
                    
                        optionsProgramas += "<option data-id-programs='" + programa.ID_Programa + "'>"+programa.TipoPrograma.TipoPrograma1 + "-" + programa.Comunidad.Comunidad1 + "</option>";
                    

                }
                response.IsSucess = true;
                response.ResponseData = optionsProgramas;
                response.Message = string.Empty;
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        private string GetAllCommunities()
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsCommunities = "";
            try
            {
                var comunidades = _comunidad.Select();


                foreach (var comunidad in comunidades)
                {

                    optionsCommunities += "<option data-id-community='" + comunidad.ID_Comunidad + "'>" + comunidad.Municipio.Municipio1 + "-" + comunidad.Comunidad1 + "</option>";


                }
                response.IsSucess = true;
                response.ResponseData = optionsCommunities;
                response.Message = string.Empty;
                response.CallBack = string.Empty;

            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public byte[] GetProgramReport(HttpContext context)
        {


            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());

            var programa = _programa.GetFirst(p=> p.ID_Programa == ID_Programa);

           

          

            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                writer.CloseStream = false;
                HeaderFooter headerFooter = new HeaderFooter();

                headerFooter.Titulo = "Reporte General de Programa";
                headerFooter.SubTitulo = "Vision Mundial";

                writer.PageEvent = headerFooter;
                

                #region Informacion de Programa

                PdfPTable pdfTab = new PdfPTable(3);
                int[] arrHeader = new int[3];
                arrHeader[0] = 1;
                arrHeader[1] = 1;
                arrHeader[2] = 1;
                pdfTab.SetWidths(arrHeader);

                Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
                Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
                Font tinyFontBoldUnderline = new Font(Font.FontFamily.HELVETICA, 7f, Font.UNDERLINE | Font.BOLD, BaseColor.BLACK);
                BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

                ////We will have to create separate cells to include image logo and 2 separate strings
                ////Row 1
                PdfPCell pdfCell1 = new PdfPCell(new Phrase("Proyecto", tinyFontBold));
                PdfPCell pdfCell2 = new PdfPCell(new Phrase(programa.Proyecto.Codigo, tinyFont));
                PdfPCell pdfCell3 = new PdfPCell(new Phrase("Código", tinyFontBold));
                PdfPCell pdfCell4 = new PdfPCell(new Phrase(programa.Codigo, tinyFont));
                PdfPCell pdfCell5 = new PdfPCell(new Phrase("Tipo de Programa", tinyFontBold));
                PdfPCell pdfCell6 = new PdfPCell(new Phrase(programa.TipoPrograma.TipoProgramaDescripcion, tinyFont));
                PdfPCell pdfCell7 = new PdfPCell(new Phrase("Comunidad", tinyFontBold));
                PdfPCell pdfCell8 = new PdfPCell(new Phrase(programa.Comunidad.Comunidad1, tinyFont));
                PdfPCell pdfCell9 = new PdfPCell(new Phrase("Municipio", tinyFontBold));
                PdfPCell pdfCell10 = new PdfPCell(new Phrase(programa.Comunidad.Municipio.Municipio1, tinyFont));
                PdfPCell pdfCell11 = new PdfPCell(new Phrase("Departamento", tinyFontBold));
                PdfPCell pdfCell12 = new PdfPCell(new Phrase(programa.Comunidad.Municipio.Departamento.Departamento1, tinyFont));
                PdfPCell pdfCell13 = new PdfPCell(new Phrase("Fecha de Inicio", tinyFontBold));
                PdfPCell pdfCell14 = new PdfPCell(new Phrase(programa.FechaInicio.ToShortDateString(), tinyFont));
                PdfPCell pdfCell15 = new PdfPCell(new Phrase("Fecha de Finalización", tinyFontBold));
                PdfPCell pdfCell16 = new PdfPCell(new Phrase(programa.FechaFinal.ToShortDateString(), tinyFont));
                PdfPCell pdfCell17 = new PdfPCell(new Phrase("", tinyFont));
                PdfPCell pdfCell18 = new PdfPCell(new Phrase("", tinyFont));

                //pdfCell1.BackgroundColor = backgroundColor;
                //pdfCell2.BackgroundColor = backgroundColor;

                pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell3.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell4.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell5.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell6.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell7.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell8.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell9.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell10.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell11.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell12.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell13.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell14.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell15.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell16.HorizontalAlignment = Element.ALIGN_LEFT;

                pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell8.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell9.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell10.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell11.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell12.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell13.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell14.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell15.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell16.VerticalAlignment = Element.ALIGN_MIDDLE;


                pdfCell1.Border = 0;
                pdfCell2.Border = 0;
                pdfCell3.Border = 0;
                pdfCell4.Border = 0;
                pdfCell5.Border = 0;
                pdfCell6.Border = 0;
                pdfCell7.Border = 0;
                pdfCell8.Border = 0;
                pdfCell9.Border = 0;
                pdfCell10.Border = 0;
                pdfCell11.Border = 0;
                pdfCell12.Border = 0;
                pdfCell13.Border = 0;
                pdfCell14.Border = 0;
                pdfCell15.Border = 0;
                pdfCell16.Border = 0;
                pdfCell17.Border = 0;
                pdfCell18.Border = 0;


                pdfCell1.FixedHeight = 25f;
                pdfCell2.FixedHeight = 25f;
                pdfCell3.FixedHeight = 25f;
                pdfCell4.FixedHeight = 25f;
                pdfCell5.FixedHeight = 25f;
                pdfCell6.FixedHeight = 25f;
                pdfCell7.FixedHeight = 25f;
                pdfCell8.FixedHeight = 25f;
                pdfCell9.FixedHeight = 25f;
                pdfCell10.FixedHeight = 25f;
                pdfCell11.FixedHeight = 25f;
                pdfCell12.FixedHeight = 25f;
                pdfCell13.FixedHeight = 25f;
                pdfCell14.FixedHeight = 25f;
                pdfCell15.FixedHeight = 25f;
                pdfCell16.FixedHeight = 25f;
                pdfCell17.FixedHeight = 25f;
                pdfCell18.FixedHeight = 25f;

                //add all three cells into PdfTable
                pdfTab.AddCell(pdfCell1);
                pdfTab.AddCell(pdfCell3);
                pdfTab.AddCell(pdfCell5);
                pdfTab.AddCell(pdfCell2);
                pdfTab.AddCell(pdfCell4);
                pdfTab.AddCell(pdfCell6);
                pdfTab.AddCell(pdfCell7);
                pdfTab.AddCell(pdfCell9);
                pdfTab.AddCell(pdfCell11);
                pdfTab.AddCell(pdfCell8);
                pdfTab.AddCell(pdfCell10);
                pdfTab.AddCell(pdfCell12);
                pdfTab.AddCell(pdfCell13);
                pdfTab.AddCell(pdfCell15);
                pdfTab.AddCell(pdfCell17);
                pdfTab.AddCell(pdfCell14);
                pdfTab.AddCell(pdfCell16);
                pdfTab.AddCell(pdfCell18);


                pdfTab.TotalWidth = document.PageSize.Width - 80f;
                pdfTab.WidthPercentage = 90;
                pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
                Paragraph paragraph = new Paragraph();
                paragraph.IndentationLeft = 30;
                paragraph.SpacingAfter = 10;
                paragraph.Add(pdfTab);

                Paragraph pDescripcion = new Paragraph("Información del Proyecto:", tinyFontBoldUnderline);
                pDescripcion.IndentationLeft = 30;
                pDescripcion.SpacingAfter = 20;
                

                #endregion

                #region Asignacion Personal
                Dictionary<string, Object> param = new Dictionary<string, Object>();
                param.Add("projectIdentity", programa.ID_Proyecto);

                string query = "spGetAsignation";
                DataSet myDataset = GetDataSet(query, CommandType.StoredProcedure, param);

                PdfPTable pdfTabContent = new PdfPTable(5);
                pdfTabContent.DefaultCell.FixedHeight = 100f;
                int[] arrContent = new int[5];
                arrContent[0] = 2;
                arrContent[1] = 1;
                arrContent[2] = 1;
                arrContent[3] = 1;
                arrContent[4] = 1;

                pdfTabContent.SetWidths(arrContent);

                PdfPCell pdfCellContent1 = new PdfPCell(new Phrase("Nombre Completo", tinyFontBold));
                PdfPCell pdfCellContent2 = new PdfPCell(new Phrase("DUI", tinyFontBold));
                PdfPCell pdfCellContent3 = new PdfPCell(new Phrase("Correo Electrónico", tinyFontBold));
                PdfPCell pdfCellContent4 = new PdfPCell(new Phrase("Puesto", tinyFontBold));
                PdfPCell pdfCellContent5 = new PdfPCell(new Phrase("Tipo de Puesto", tinyFontBold));


                pdfCellContent1.BackgroundColor = backgroundColor;
                pdfCellContent2.BackgroundColor = backgroundColor;
                pdfCellContent3.BackgroundColor = backgroundColor;
                pdfCellContent4.BackgroundColor = backgroundColor;
                pdfCellContent5.BackgroundColor = backgroundColor;

                pdfCellContent1.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContent2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContent3.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContent4.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContent5.HorizontalAlignment = Element.ALIGN_LEFT;

                pdfCellContent1.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContent2.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContent3.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContent4.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContent5.VerticalAlignment = Element.ALIGN_MIDDLE;

                pdfCellContent1.Border = 0;
                pdfCellContent2.Border = 0;
                pdfCellContent3.Border = 0;
                pdfCellContent4.Border = 0;
                pdfCellContent5.Border = 0;

                //add all three cells into PdfTable
                pdfTabContent.AddCell(pdfCellContent1);
                pdfTabContent.AddCell(pdfCellContent2);
                pdfTabContent.AddCell(pdfCellContent3);
                pdfTabContent.AddCell(pdfCellContent4);
                pdfTabContent.AddCell(pdfCellContent5);




                foreach (DataRow row in myDataset.Tables[0].Rows)
                {
                    PdfPCell contentCellFirst = new PdfPCell(new Phrase(row["NombreCompleto"].ToString(), tinyFont));
                    PdfPCell contentCellSecond = new PdfPCell(new Phrase(row["Dui"].ToString(), tinyFont));
                    PdfPCell contentCellThird = new PdfPCell(new Phrase(row["Email"].ToString(), tinyFont));
                    PdfPCell contentCellFourth = new PdfPCell(new Phrase(row["Puesto"].ToString(), tinyFont));
                    PdfPCell contentCellFifth = new PdfPCell(new Phrase(row["TipoPuesto"].ToString(), tinyFont));

                    contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellThird.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellFourth.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellFifth.HorizontalAlignment = Element.ALIGN_LEFT;

                    contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellThird.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellFourth.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellFifth.VerticalAlignment = Element.ALIGN_MIDDLE;


                    contentCellFirst.Border = 0;
                    contentCellSecond.Border = 0;
                    contentCellThird.Border = 0;
                    contentCellFourth.Border = 0;
                    contentCellFifth.Border = 0;

                    contentCellFirst.FixedHeight = 20f;
                    contentCellSecond.FixedHeight = 20f;
                    contentCellThird.FixedHeight = 20f;
                    contentCellFourth.FixedHeight = 20f;
                    contentCellFifth.FixedHeight = 20f;


                    pdfTabContent.AddCell(contentCellFirst);
                    pdfTabContent.AddCell(contentCellSecond);
                    pdfTabContent.AddCell(contentCellThird);
                    pdfTabContent.AddCell(contentCellFourth);
                    pdfTabContent.AddCell(contentCellFifth);

                }

                pdfTabContent.TotalWidth = document.PageSize.Width - 80f;
                pdfTabContent.WidthPercentage = 95;
                pdfTabContent.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTabContent.SpacingAfter = 20f;
                Paragraph pContent = new Paragraph();
                pContent.IndentationLeft = 30;
                pContent.Add(pdfTabContent);

                Paragraph pAsignacion = new Paragraph("Personal Asignado:", tinyFontBoldUnderline);
                pAsignacion.IndentationLeft = 30;
                pAsignacion.SpacingAfter = 20;
                

                #endregion

                #region Actividades de Programa

                PdfPTable pdfTabContent2 = new PdfPTable(5);
                pdfTabContent.DefaultCell.FixedHeight = 100f;
                int[] arrContent2 = new int[5];
                arrContent[0] = 1;
                arrContent[1] = 1;
                arrContent[2] = 3;
                arrContent[3] = 1;
                arrContent[4] = 3;

                pdfTabContent2.SetWidths(arrContent);

                PdfPCell pdfCellContentActivity1 = new PdfPCell(new Phrase("Fecha", tinyFontBold));
                PdfPCell pdfCellContentActivity2 = new PdfPCell(new Phrase("Codigo de Actividad", tinyFontBold));
                PdfPCell pdfCellContentActivity3 = new PdfPCell(new Phrase("Actividad", tinyFontBold));
                PdfPCell pdfCellContentActivity4 = new PdfPCell(new Phrase("Estado", tinyFontBold));
                PdfPCell pdfCellContentActivity5 = new PdfPCell(new Phrase("Observación", tinyFontBold));


                pdfCellContentActivity1.BackgroundColor = backgroundColor;
                pdfCellContentActivity2.BackgroundColor = backgroundColor;
                pdfCellContentActivity3.BackgroundColor = backgroundColor;
                pdfCellContentActivity4.BackgroundColor = backgroundColor;
                pdfCellContentActivity5.BackgroundColor = backgroundColor;

                pdfCellContentActivity1.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentActivity2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentActivity3.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentActivity4.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentActivity5.HorizontalAlignment = Element.ALIGN_LEFT;

                pdfCellContentActivity1.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentActivity2.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentActivity3.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentActivity4.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentActivity5.VerticalAlignment = Element.ALIGN_MIDDLE;

                pdfCellContentActivity1.Border = 0;
                pdfCellContentActivity2.Border = 0;
                pdfCellContentActivity3.Border = 0;
                pdfCellContentActivity4.Border = 0;
                pdfCellContentActivity5.Border = 0;

                //add all three cells into PdfTable
                pdfTabContent2.AddCell(pdfCellContentActivity1);
                pdfTabContent2.AddCell(pdfCellContentActivity2);
                pdfTabContent2.AddCell(pdfCellContentActivity3);
                pdfTabContent2.AddCell(pdfCellContentActivity4);
                pdfTabContent2.AddCell(pdfCellContentActivity5);


                if (programa.Actividad.Count > 0)
                {

                    foreach (var act in programa.Actividad.OrderByDescending(f => f.Fecha))
                    {
                        PdfPCell contentCellFirst = new PdfPCell(new Phrase(act.Fecha.ToShortDateString(), tinyFont));
                        PdfPCell contentCellSecond = new PdfPCell(new Phrase(act.Codigo, tinyFont));
                        PdfPCell contentCellThird = new PdfPCell(new Phrase(act.ActividadDescripcion, tinyFont));
                        PdfPCell contentCellFourth = new PdfPCell(new Phrase(GetFormattedState( act.Estado), tinyFont));
                        PdfPCell contentCellFifth = new PdfPCell(new Phrase(act.Observacion, tinyFont));

                        contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                        contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                        contentCellThird.HorizontalAlignment = Element.ALIGN_LEFT;
                        contentCellFourth.HorizontalAlignment = Element.ALIGN_LEFT;
                        contentCellFifth.HorizontalAlignment = Element.ALIGN_LEFT;

                        contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                        contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                        contentCellThird.VerticalAlignment = Element.ALIGN_MIDDLE;
                        contentCellFourth.VerticalAlignment = Element.ALIGN_MIDDLE;
                        contentCellFifth.VerticalAlignment = Element.ALIGN_MIDDLE;


                        contentCellFirst.Border = 0;
                        contentCellSecond.Border = 0;
                        contentCellThird.Border = 0;
                        contentCellFourth.Border = 0;
                        contentCellFifth.Border = 0;

                        contentCellFirst.FixedHeight = 20f;
                        contentCellSecond.FixedHeight = 20f;
                        contentCellThird.FixedHeight = 20f;
                        contentCellFourth.FixedHeight = 20f;
                        contentCellFifth.FixedHeight = 20f;


                        pdfTabContent2.AddCell(contentCellFirst);
                        pdfTabContent2.AddCell(contentCellSecond);
                        pdfTabContent2.AddCell(contentCellThird);
                        pdfTabContent2.AddCell(contentCellFourth);
                        pdfTabContent2.AddCell(contentCellFifth);

                    }
                }

                pdfTabContent2.TotalWidth = document.PageSize.Width - 80f;
                pdfTabContent2.WidthPercentage = 95;
                pdfTabContent2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTabContent2.SpacingAfter = 20;
                Paragraph pContent2 = new Paragraph();
                pContent2.IndentationLeft = 30;
                pContent2.Add(pdfTabContent2);

                Paragraph pActividades = new Paragraph("Actividades Registradas:", tinyFontBoldUnderline);
                pActividades.IndentationLeft = 30;
                pActividades.SpacingAfter = 20;
                

                #endregion

                #region Informacion Beneficiarios

                Paragraph pBeneficiarios = new Paragraph("Informacion Adicional:", tinyFontBoldUnderline);
                pBeneficiarios.IndentationLeft = 30;
                pBeneficiarios.SpacingAfter = 20;
                

                PdfPTable pdfTabContentAditional = new PdfPTable(2);
                pdfTabContentAditional.DefaultCell.FixedHeight = 100f;
                int[] arrContentAditional = new int[2];
                arrContentAditional[0] = 2;
                arrContentAditional[1] = 1;
                pdfTabContentAditional.SetWidths(arrContentAditional);

                PdfPCell pdfCellContentAditional1 = new PdfPCell(new Phrase("Número de Beneficiarios en el programa: ", tinyFontBold));
                PdfPCell pdfCellContentAditional2 = new PdfPCell(new Phrase(programa.Beneficiario.Count().ToString(), tinyFontBold));
                PdfPCell pdfCellContentAditional3 = new PdfPCell(new Phrase("Número de Beneficiarios del sexo masculino:", tinyFontBold));
                PdfPCell pdfCellContentAditional4 = new PdfPCell(new Phrase(programa.Beneficiario.Count(ben => ben.Sexo == "M").ToString(), tinyFontBold));
                PdfPCell pdfCellContentAditional5 = new PdfPCell(new Phrase("Numero de Beneficiarios del sexo femenino:", tinyFontBold));
                PdfPCell pdfCellContentAditional6 = new PdfPCell(new Phrase(programa.Beneficiario.Count(ben => ben.Sexo == "F").ToString(), tinyFontBold));
                PdfPCell pdfCellContentAditional7 = new PdfPCell(new Phrase("Numero de Beneficiarios Patrocinados:", tinyFontBold));
                PdfPCell pdfCellContentAditional8 = new PdfPCell(new Phrase(programa.Beneficiario.Count(ben => !string.IsNullOrEmpty(ben.Codigo)).ToString(), tinyFontBold));

                pdfCellContentAditional1.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional3.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional4.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional5.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional6.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional7.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional8.HorizontalAlignment = Element.ALIGN_LEFT;

                pdfCellContentAditional1.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional2.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional3.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional4.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional5.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional6.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional7.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional8.VerticalAlignment = Element.ALIGN_MIDDLE;


                pdfCellContentAditional1.Border = 0;
                pdfCellContentAditional2.Border = 0;
                pdfCellContentAditional3.Border = 0;
                pdfCellContentAditional4.Border = 0;
                pdfCellContentAditional5.Border = 0;
                pdfCellContentAditional6.Border = 0;
                pdfCellContentAditional7.Border = 0;
                pdfCellContentAditional8.Border = 0;

                pdfCellContentAditional1.FixedHeight = 20f;
                pdfCellContentAditional2.FixedHeight = 20f;
                pdfCellContentAditional3.FixedHeight = 20f;
                pdfCellContentAditional4.FixedHeight = 20f;
                pdfCellContentAditional5.FixedHeight = 20f;
                pdfCellContentAditional6.FixedHeight = 20f;
                pdfCellContentAditional7.FixedHeight = 20f;
                pdfCellContentAditional8.FixedHeight = 20f;


                pdfTabContentAditional.AddCell(pdfCellContentAditional1);
                pdfTabContentAditional.AddCell(pdfCellContentAditional2);
                pdfTabContentAditional.AddCell(pdfCellContentAditional3);
                pdfTabContentAditional.AddCell(pdfCellContentAditional4);
                pdfTabContentAditional.AddCell(pdfCellContentAditional5);
                pdfTabContentAditional.AddCell(pdfCellContentAditional6);
                pdfTabContentAditional.AddCell(pdfCellContentAditional7);
                pdfTabContentAditional.AddCell(pdfCellContentAditional8);


                pdfTabContentAditional.TotalWidth = document.PageSize.Width - 80f;
                pdfTabContentAditional.WidthPercentage = 60;
                pdfTabContentAditional.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTabContentAditional.SpacingAfter = 20;
                Paragraph pContentActivity = new Paragraph();
                pContentActivity.IndentationLeft = 30;
                pContentActivity.Add(pdfTabContentAditional);
                
                #endregion

                #region Charts

                ChartingHelper chartSex = new ChartingHelper();

                Dictionary<string, object> Parameters = new Dictionary<string, object>();
                Parameters.Add("width", 300);
                Parameters.Add("height", 400);
                Parameters.Add("chartTitle", "Participación por Género");
                Parameters.Add("xTitle", "Genero");
                Parameters.Add("yTitle", "Cantidad");
                Parameters.Add("chartType","Column");
                Dictionary<string, object> Data = new Dictionary<string, object>();
                Data.Add("Masculino", programa.Beneficiario.Count(ben => ben.Sexo == "M"));
                Data.Add("Femenino", programa.Beneficiario.Count(ben => ben.Sexo == "F"));

                chartSex.ChartParameters = Parameters;
                chartSex.Data = Data;

                ChartingHelper chartAges = new ChartingHelper();
                Dictionary<string, object> ParametersAges = new Dictionary<string, object>();
                ParametersAges.Add("width", 450);
                ParametersAges.Add("height", 400);
                ParametersAges.Add("chartTitle", "Participación por Edades");
                ParametersAges.Add("xTitle", "Rangos");
                ParametersAges.Add("yTitle", "Cantidad");
                ParametersAges.Add("chartType", "Bar");

                Dictionary<string, object> DataAges = new Dictionary<string, object>();
                List<int> Ranges = chartAges.GetAgeRanges(programa.TipoPrograma.TipoPrograma1);
                int menor1=0, menor2=0, menor3=0;


                foreach (var beneficiario in programa.Beneficiario)
                {
                    string[] personAge = beneficiario.Edad.Split('|');
                    int years = Convert.ToInt32(personAge[0]);
                     if (years <= Ranges[0] && years >= Ranges[1])
	                 {
                         menor1++;
	                 }
                     else if (years <= Ranges[1] && years >= Ranges[2])
                     {
                         menor2++;
                     }
                     else if(years <= Ranges[2] )
                    {
                        menor3++;
                    }
                }

                DataAges.Add("Menor a " + Ranges[0], menor1);
                DataAges.Add("Menor a " + Ranges[1], menor2);
                DataAges.Add("Menor a " + Ranges[2], menor3);

                chartAges.ChartParameters = ParametersAges;
                chartAges.Data = DataAges;


                #endregion
                document.Open();
                document.Add(pDescripcion);
                document.Add(paragraph);
                document.Add(pAsignacion);
                document.Add(pContent);
                document.Add(pActividades);
                document.Add(pContent2);
                document.Add(pBeneficiarios);
                document.Add(pContentActivity);
                
                byte[] imageBytes = chartSex.GetChart();
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
                image.ScalePercent(0.8f * 100);
                
                Paragraph pContentImage = new Paragraph(new Chunk(image,0,0,true));
                pContentImage.IndentationLeft = 100;
                pContentImage.IndentationRight = 100;
                Paragraph pImageHeader = new Paragraph("Gráfico de Participación:", tinyFontBoldUnderline);
                pImageHeader.IndentationLeft = 30;
                pImageHeader.SpacingAfter = 20;
                document.Add(pImageHeader);
                document.Add(pContentImage);

                byte[] imageBytes1 = chartAges.GetChart();
                iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(imageBytes1);
                image1.ScalePercent(0.8f * 100);

                Paragraph pContentImage1 = new Paragraph(new Chunk(image1, 0, 0, true));
                pContentImage1.IndentationLeft = 100;
                pContentImage1.IndentationRight = 100;
                Paragraph pImageHeader1 = new Paragraph("Gráfico de Participación por Edades:", tinyFontBoldUnderline);
                pImageHeader1.IndentationLeft = 30;
                pImageHeader1.SpacingAfter = 20;
                document.Add(pImageHeader1);
                document.Add(pContentImage1);


                document.Close();


                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                

                return bytes;
                
            }


        }

        public byte[] GetProjectReport(HttpContext context)
        {
            int index = 0;
            int ID_Proyecto = Int32.Parse(context.Request.Params["ID_Proyecto"].ToString());

            var proyecto = _proyecto.GetFirst(p => p.ID_Proyecto == ID_Proyecto);

            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte General de Proyecto";
            headerFooter.SubTitulo = "Vision Mundial";

            writer.PageEvent = headerFooter;

            document.Open();

            Font tiny = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyBold= new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
            

            PdfPTable pdfTab1 = new PdfPTable(2);
            int[] arr = new int[2];
            arr[0] = 1;
            arr[1] = 1;
            pdfTab1.SetWidths(arr);

            PdfPCell pdfCellHead = new PdfPCell(new Phrase("Nombre del Proyecto:", tinyBold));
            PdfPCell pdfCellHead1 = new PdfPCell(new Phrase(proyecto.Codigo, tinyBold));


            pdfCellHead.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead1.HorizontalAlignment = Element.ALIGN_LEFT;


            pdfCellHead.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCellHead.Border = 0;
            pdfCellHead1.Border = 0;



            //add all three cells into PdfTable
            pdfTab1.AddCell(pdfCellHead);
            pdfTab1.AddCell(pdfCellHead1);

            pdfTab1.TotalWidth = document.PageSize.Width - 80f;
            pdfTab1.WidthPercentage = 50;
            pdfTab1.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p1 = new Paragraph();
            p1.IndentationLeft = 30;
            p1.Add(pdfTab1);
            p1.SpacingAfter = 20;

            
            document.Add(p1);


            foreach (var programa in proyecto.Programa)
            {
                index++;
                #region Informacion de Programa

                PdfPTable pdfTab = new PdfPTable(3);
                int[] arrHeader = new int[3];
                arrHeader[0] = 1;
                arrHeader[1] = 1;
                arrHeader[2] = 1;
                pdfTab.SetWidths(arrHeader);

                Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
                Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
                Font tinyFontBoldUnderline = new Font(Font.FontFamily.HELVETICA, 7f, Font.UNDERLINE | Font.BOLD, BaseColor.BLACK);
                BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

                ////We will have to create separate cells to include image logo and 2 separate strings
                ////Row 1
                PdfPCell pdfCell1 = new PdfPCell(new Phrase("Numero de Programa", tinyFontBold));
                PdfPCell pdfCell2 = new PdfPCell(new Phrase(index.ToString(), tinyFont));
                PdfPCell pdfCell3 = new PdfPCell(new Phrase("Código", tinyFontBold));
                PdfPCell pdfCell4 = new PdfPCell(new Phrase(programa.Codigo, tinyFont));
                PdfPCell pdfCell5 = new PdfPCell(new Phrase("Tipo de Programa", tinyFontBold));
                PdfPCell pdfCell6 = new PdfPCell(new Phrase(programa.TipoPrograma.TipoProgramaDescripcion, tinyFont));
                PdfPCell pdfCell7 = new PdfPCell(new Phrase("Comunidad", tinyFontBold));
                PdfPCell pdfCell8 = new PdfPCell(new Phrase(programa.Comunidad.Comunidad1, tinyFont));
                PdfPCell pdfCell9 = new PdfPCell(new Phrase("Municipio", tinyFontBold));
                PdfPCell pdfCell10 = new PdfPCell(new Phrase(programa.Comunidad.Municipio.Municipio1, tinyFont));
                PdfPCell pdfCell11 = new PdfPCell(new Phrase("Departamento", tinyFontBold));
                PdfPCell pdfCell12 = new PdfPCell(new Phrase(programa.Comunidad.Municipio.Departamento.Departamento1, tinyFont));
                PdfPCell pdfCell13 = new PdfPCell(new Phrase("Fecha de Inicio", tinyFontBold));
                PdfPCell pdfCell14 = new PdfPCell(new Phrase(programa.FechaInicio.ToShortDateString(), tinyFont));
                PdfPCell pdfCell15 = new PdfPCell(new Phrase("Fecha de Finalización", tinyFontBold));
                PdfPCell pdfCell16 = new PdfPCell(new Phrase(programa.FechaFinal.ToShortDateString(), tinyFont));
                PdfPCell pdfCell17 = new PdfPCell(new Phrase("", tinyFont));
                PdfPCell pdfCell18 = new PdfPCell(new Phrase("", tinyFont));

                //pdfCell1.BackgroundColor = backgroundColor;
                //pdfCell2.BackgroundColor = backgroundColor;

                pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell3.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell4.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell5.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell6.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell7.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell8.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell9.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell10.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell11.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell12.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell13.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell14.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell15.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell16.HorizontalAlignment = Element.ALIGN_LEFT;

                pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell8.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell9.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell10.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell11.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell12.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell13.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell14.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell15.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell16.VerticalAlignment = Element.ALIGN_MIDDLE;


                pdfCell1.Border = 0;
                pdfCell2.Border = 0;
                pdfCell3.Border = 0;
                pdfCell4.Border = 0;
                pdfCell5.Border = 0;
                pdfCell6.Border = 0;
                pdfCell7.Border = 0;
                pdfCell8.Border = 0;
                pdfCell9.Border = 0;
                pdfCell10.Border = 0;
                pdfCell11.Border = 0;
                pdfCell12.Border = 0;
                pdfCell13.Border = 0;
                pdfCell14.Border = 0;
                pdfCell15.Border = 0;
                pdfCell16.Border = 0;
                pdfCell17.Border = 0;
                pdfCell18.Border = 0;


                pdfCell1.FixedHeight = 25f;
                pdfCell2.FixedHeight = 25f;
                pdfCell3.FixedHeight = 25f;
                pdfCell4.FixedHeight = 25f;
                pdfCell5.FixedHeight = 25f;
                pdfCell6.FixedHeight = 25f;
                pdfCell7.FixedHeight = 25f;
                pdfCell8.FixedHeight = 25f;
                pdfCell9.FixedHeight = 25f;
                pdfCell10.FixedHeight = 25f;
                pdfCell11.FixedHeight = 25f;
                pdfCell12.FixedHeight = 25f;
                pdfCell13.FixedHeight = 25f;
                pdfCell14.FixedHeight = 25f;
                pdfCell15.FixedHeight = 25f;
                pdfCell16.FixedHeight = 25f;
                pdfCell17.FixedHeight = 25f;
                pdfCell18.FixedHeight = 25f;

                //add all three cells into PdfTable
                pdfTab.AddCell(pdfCell1);
                pdfTab.AddCell(pdfCell3);
                pdfTab.AddCell(pdfCell5);
                pdfTab.AddCell(pdfCell2);
                pdfTab.AddCell(pdfCell4);
                pdfTab.AddCell(pdfCell6);
                pdfTab.AddCell(pdfCell7);
                pdfTab.AddCell(pdfCell9);
                pdfTab.AddCell(pdfCell11);
                pdfTab.AddCell(pdfCell8);
                pdfTab.AddCell(pdfCell10);
                pdfTab.AddCell(pdfCell12);
                pdfTab.AddCell(pdfCell13);
                pdfTab.AddCell(pdfCell15);
                pdfTab.AddCell(pdfCell17);
                pdfTab.AddCell(pdfCell14);
                pdfTab.AddCell(pdfCell16);
                pdfTab.AddCell(pdfCell18);


                pdfTab.TotalWidth = document.PageSize.Width - 80f;
                pdfTab.WidthPercentage = 90;
                pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
                Paragraph paragraph = new Paragraph();
                paragraph.IndentationLeft = 30;
                paragraph.SpacingAfter = 10;
                paragraph.Add(pdfTab);

                Paragraph pDescripcion = new Paragraph("Información del Programa:", tinyFontBoldUnderline);
                pDescripcion.IndentationLeft = 30;
                pDescripcion.SpacingAfter = 20;

                #region Informacion Beneficiarios

                PdfPTable pdfTabContentAditional = new PdfPTable(2);
                pdfTabContentAditional.DefaultCell.FixedHeight = 100f;
                int[] arrContentAditional = new int[2];
                arrContentAditional[0] = 2;
                arrContentAditional[1] = 1;
                pdfTabContentAditional.SetWidths(arrContentAditional);


                PdfPCell pdfCellContentAditional3 = new PdfPCell(new Phrase("Número de Beneficiarios del sexo masculino:", tinyFontBold));
                PdfPCell pdfCellContentAditional4 = new PdfPCell(new Phrase(programa.Beneficiario.Count(ben=> ben.Sexo=="M").ToString(), tinyFontBold));
                PdfPCell pdfCellContentAditional5 = new PdfPCell(new Phrase("Numero de Beneficiarios del sexo femenino:", tinyFontBold));
                PdfPCell pdfCellContentAditional6 = new PdfPCell(new Phrase(programa.Beneficiario.Count(ben => ben.Sexo == "F").ToString(), tinyFontBold));
                PdfPCell pdfCellContentAditional7 = new PdfPCell(new Phrase("Numero de Beneficiarios patrocinados:", tinyFontBold));
                PdfPCell pdfCellContentAditional8 = new PdfPCell(new Phrase(programa.Beneficiario.Count(ben=> !string.IsNullOrEmpty(ben.Codigo)).ToString(), tinyFontBold));



                pdfCellContentAditional3.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional4.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional5.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional6.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional7.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional8.HorizontalAlignment = Element.ALIGN_LEFT;



                pdfCellContentAditional3.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional4.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional5.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional6.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional7.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional8.VerticalAlignment = Element.ALIGN_MIDDLE;



                pdfCellContentAditional3.Border = 0;
                pdfCellContentAditional4.Border = 0;
                pdfCellContentAditional5.Border = 0;
                pdfCellContentAditional6.Border = 0;
                pdfCellContentAditional7.Border = 0;
                pdfCellContentAditional8.Border = 0;



                pdfCellContentAditional3.FixedHeight = 20f;
                pdfCellContentAditional4.FixedHeight = 20f;
                pdfCellContentAditional5.FixedHeight = 20f;
                pdfCellContentAditional6.FixedHeight = 20f;
                pdfCellContentAditional7.FixedHeight = 20f;
                pdfCellContentAditional8.FixedHeight = 20f;




                pdfTabContentAditional.AddCell(pdfCellContentAditional3);
                pdfTabContentAditional.AddCell(pdfCellContentAditional4);
                pdfTabContentAditional.AddCell(pdfCellContentAditional5);
                pdfTabContentAditional.AddCell(pdfCellContentAditional6);
                pdfTabContentAditional.AddCell(pdfCellContentAditional7);
                pdfTabContentAditional.AddCell(pdfCellContentAditional8);



                pdfTabContentAditional.TotalWidth = document.PageSize.Width - 80f;
                pdfTabContentAditional.WidthPercentage = 60;
                pdfTabContentAditional.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTabContentAditional.SpacingAfter = 20;
                Paragraph pContentAditional = new Paragraph();
                pContentAditional.IndentationLeft = 30;
                pContentAditional.Add(pdfTabContentAditional);

                #endregion

                #region Charts
 
                #endregion


                document.Add(pDescripcion);
                document.Add(paragraph);
                document.Add(pContentAditional);

                #endregion
            }


            



            document.Close();

            return output.ToArray();
        }

        public byte[] GetProgramActivityReport(HttpContext context)
        {
            int index = 0;
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());

            var programa = _programa.GetFirst(p => p.ID_Programa == ID_Programa);

            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Actividades de Programa";
            headerFooter.SubTitulo = "Vision Mundial";

            writer.PageEvent = headerFooter;

            document.Open();

            Font tiny = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);


            PdfPTable pdfTab1 = new PdfPTable(2);
            int[] arr = new int[2];
            arr[0] = 1;
            arr[1] = 1;
            pdfTab1.SetWidths(arr);

            PdfPCell pdfCellHead = new PdfPCell(new Phrase("Nombre del Proyecto:", tinyBold));
            PdfPCell pdfCellHead1 = new PdfPCell(new Phrase(programa.Proyecto.Codigo, tinyBold));


            pdfCellHead.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead1.HorizontalAlignment = Element.ALIGN_LEFT;


            pdfCellHead.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCellHead.Border = 0;
            pdfCellHead1.Border = 0;



            //add all three cells into PdfTable
            pdfTab1.AddCell(pdfCellHead);
            pdfTab1.AddCell(pdfCellHead1);

            pdfTab1.TotalWidth = document.PageSize.Width - 80f;
            pdfTab1.WidthPercentage = 50;
            pdfTab1.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p1 = new Paragraph();
            p1.IndentationLeft = 30;
            p1.Add(pdfTab1);
            p1.SpacingAfter = 20;


            document.Add(p1);


           
                index++;
                #region Informacion de Programa

                PdfPTable pdfTab = new PdfPTable(3);
                int[] arrHeader = new int[3];
                arrHeader[0] = 1;
                arrHeader[1] = 1;
                arrHeader[2] = 1;
                pdfTab.SetWidths(arrHeader);

                Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
                Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
                Font tinyFontBoldUnderline = new Font(Font.FontFamily.HELVETICA, 7f, Font.UNDERLINE | Font.BOLD, BaseColor.BLACK);
                BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

                ////We will have to create separate cells to include image logo and 2 separate strings
                ////Row 1
                PdfPCell pdfCell1 = new PdfPCell(new Phrase("Numero de Programa", tinyFontBold));
                PdfPCell pdfCell2 = new PdfPCell(new Phrase(index.ToString(), tinyFont));
                PdfPCell pdfCell3 = new PdfPCell(new Phrase("Código", tinyFontBold));
                PdfPCell pdfCell4 = new PdfPCell(new Phrase(programa.Codigo, tinyFont));
                PdfPCell pdfCell5 = new PdfPCell(new Phrase("Tipo de Programa", tinyFontBold));
                PdfPCell pdfCell6 = new PdfPCell(new Phrase(programa.TipoPrograma.TipoProgramaDescripcion, tinyFont));
                PdfPCell pdfCell7 = new PdfPCell(new Phrase("Comunidad", tinyFontBold));
                PdfPCell pdfCell8 = new PdfPCell(new Phrase(programa.Comunidad.Comunidad1, tinyFont));
                PdfPCell pdfCell9 = new PdfPCell(new Phrase("Municipio", tinyFontBold));
                PdfPCell pdfCell10 = new PdfPCell(new Phrase(programa.Comunidad.Municipio.Municipio1, tinyFont));
                PdfPCell pdfCell11 = new PdfPCell(new Phrase("Departamento", tinyFontBold));
                PdfPCell pdfCell12 = new PdfPCell(new Phrase(programa.Comunidad.Municipio.Departamento.Departamento1, tinyFont));
                PdfPCell pdfCell13 = new PdfPCell(new Phrase("Fecha de Inicio", tinyFontBold));
                PdfPCell pdfCell14 = new PdfPCell(new Phrase(programa.FechaInicio.ToShortDateString(), tinyFont));
                PdfPCell pdfCell15 = new PdfPCell(new Phrase("Fecha de Finalización", tinyFontBold));
                PdfPCell pdfCell16 = new PdfPCell(new Phrase(programa.FechaFinal.ToShortDateString(), tinyFont));
                PdfPCell pdfCell17 = new PdfPCell(new Phrase("", tinyFont));
                PdfPCell pdfCell18 = new PdfPCell(new Phrase("", tinyFont));

                //pdfCell1.BackgroundColor = backgroundColor;
                //pdfCell2.BackgroundColor = backgroundColor;

                pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell3.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell4.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell5.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell6.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell7.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell8.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell9.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell10.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell11.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell12.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell13.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell14.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell15.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell16.HorizontalAlignment = Element.ALIGN_LEFT;

                pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell8.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell9.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell10.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell11.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell12.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell13.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell14.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell15.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell16.VerticalAlignment = Element.ALIGN_MIDDLE;


                pdfCell1.Border = 0;
                pdfCell2.Border = 0;
                pdfCell3.Border = 0;
                pdfCell4.Border = 0;
                pdfCell5.Border = 0;
                pdfCell6.Border = 0;
                pdfCell7.Border = 0;
                pdfCell8.Border = 0;
                pdfCell9.Border = 0;
                pdfCell10.Border = 0;
                pdfCell11.Border = 0;
                pdfCell12.Border = 0;
                pdfCell13.Border = 0;
                pdfCell14.Border = 0;
                pdfCell15.Border = 0;
                pdfCell16.Border = 0;
                pdfCell17.Border = 0;
                pdfCell18.Border = 0;


                pdfCell1.FixedHeight = 25f;
                pdfCell2.FixedHeight = 25f;
                pdfCell3.FixedHeight = 25f;
                pdfCell4.FixedHeight = 25f;
                pdfCell5.FixedHeight = 25f;
                pdfCell6.FixedHeight = 25f;
                pdfCell7.FixedHeight = 25f;
                pdfCell8.FixedHeight = 25f;
                pdfCell9.FixedHeight = 25f;
                pdfCell10.FixedHeight = 25f;
                pdfCell11.FixedHeight = 25f;
                pdfCell12.FixedHeight = 25f;
                pdfCell13.FixedHeight = 25f;
                pdfCell14.FixedHeight = 25f;
                pdfCell15.FixedHeight = 25f;
                pdfCell16.FixedHeight = 25f;
                pdfCell17.FixedHeight = 25f;
                pdfCell18.FixedHeight = 25f;

                //add all three cells into PdfTable
                pdfTab.AddCell(pdfCell1);
                pdfTab.AddCell(pdfCell3);
                pdfTab.AddCell(pdfCell5);
                pdfTab.AddCell(pdfCell2);
                pdfTab.AddCell(pdfCell4);
                pdfTab.AddCell(pdfCell6);
                pdfTab.AddCell(pdfCell7);
                pdfTab.AddCell(pdfCell9);
                pdfTab.AddCell(pdfCell11);
                pdfTab.AddCell(pdfCell8);
                pdfTab.AddCell(pdfCell10);
                pdfTab.AddCell(pdfCell12);
                pdfTab.AddCell(pdfCell13);
                pdfTab.AddCell(pdfCell15);
                pdfTab.AddCell(pdfCell17);
                pdfTab.AddCell(pdfCell14);
                pdfTab.AddCell(pdfCell16);
                pdfTab.AddCell(pdfCell18);


                pdfTab.TotalWidth = document.PageSize.Width - 80f;
                pdfTab.WidthPercentage = 90;
                pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
                Paragraph paragraph = new Paragraph();
                paragraph.IndentationLeft = 30;
                paragraph.SpacingAfter = 10;
                paragraph.Add(pdfTab);

                Paragraph pDescripcion = new Paragraph("Información del Programa:", tinyFontBoldUnderline);
                pDescripcion.IndentationLeft = 30;
                pDescripcion.SpacingAfter = 20;
                document.Add(pDescripcion);
                document.Add(paragraph);

                #endregion

                #region Actividades de Programa

                PdfPTable pdfTabContent2 = new PdfPTable(5);
                pdfTabContent2.DefaultCell.FixedHeight = 100f;
                int[] arrContent2 = new int[5];
                arrContent2[0] = 1;
                arrContent2[1] = 1;
                arrContent2[2] = 3;
                arrContent2[3] = 1;
                arrContent2[4] = 3;

                pdfTabContent2.SetWidths(arrContent2);

                PdfPCell pdfCellContentActivity1 = new PdfPCell(new Phrase("Fecha", tinyFontBold));
                PdfPCell pdfCellContentActivity2 = new PdfPCell(new Phrase("Codigo de Actividad", tinyFontBold));
                PdfPCell pdfCellContentActivity3 = new PdfPCell(new Phrase("Actividad", tinyFontBold));
                PdfPCell pdfCellContentActivity4 = new PdfPCell(new Phrase("Estado", tinyFontBold));
                PdfPCell pdfCellContentActivity5 = new PdfPCell(new Phrase("Observación", tinyFontBold));


                pdfCellContentActivity1.BackgroundColor = backgroundColor;
                pdfCellContentActivity2.BackgroundColor = backgroundColor;
                pdfCellContentActivity3.BackgroundColor = backgroundColor;
                pdfCellContentActivity4.BackgroundColor = backgroundColor;
                pdfCellContentActivity5.BackgroundColor = backgroundColor;

                pdfCellContentActivity1.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentActivity2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentActivity3.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentActivity4.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentActivity5.HorizontalAlignment = Element.ALIGN_LEFT;

                pdfCellContentActivity1.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentActivity2.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentActivity3.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentActivity4.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentActivity5.VerticalAlignment = Element.ALIGN_MIDDLE;

                pdfCellContentActivity1.Border = 0;
                pdfCellContentActivity2.Border = 0;
                pdfCellContentActivity3.Border = 0;
                pdfCellContentActivity4.Border = 0;
                pdfCellContentActivity5.Border = 0;

                //add all three cells into PdfTable
                pdfTabContent2.AddCell(pdfCellContentActivity1);
                pdfTabContent2.AddCell(pdfCellContentActivity2);
                pdfTabContent2.AddCell(pdfCellContentActivity3);
                pdfTabContent2.AddCell(pdfCellContentActivity4);
                pdfTabContent2.AddCell(pdfCellContentActivity5);


                if (programa.Actividad.Count > 0)
                {

                    foreach (var act in programa.Actividad.OrderByDescending(f => f.Fecha))
                    {
                        PdfPCell contentCellFirst = new PdfPCell(new Phrase(act.Fecha.ToShortDateString(), tinyFont));
                        PdfPCell contentCellSecond = new PdfPCell(new Phrase(act.Codigo, tinyFont));
                        PdfPCell contentCellThird = new PdfPCell(new Phrase(act.ActividadDescripcion, tinyFont));
                        PdfPCell contentCellFourth = new PdfPCell(new Phrase(GetFormattedState( act.Estado), tinyFont));
                        PdfPCell contentCellFifth = new PdfPCell(new Phrase(act.Observacion, tinyFont));

                        contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                        contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                        contentCellThird.HorizontalAlignment = Element.ALIGN_LEFT;
                        contentCellFourth.HorizontalAlignment = Element.ALIGN_LEFT;
                        contentCellFifth.HorizontalAlignment = Element.ALIGN_LEFT;

                        contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                        contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                        contentCellThird.VerticalAlignment = Element.ALIGN_MIDDLE;
                        contentCellFourth.VerticalAlignment = Element.ALIGN_MIDDLE;
                        contentCellFifth.VerticalAlignment = Element.ALIGN_MIDDLE;


                        contentCellFirst.Border = 0;
                        contentCellSecond.Border = 0;
                        contentCellThird.Border = 0;
                        contentCellFourth.Border = 0;
                        contentCellFifth.Border = 0;

                        contentCellFirst.FixedHeight = 20f;
                        contentCellSecond.FixedHeight = 20f;
                        contentCellThird.FixedHeight = 20f;
                        contentCellFourth.FixedHeight = 20f;
                        contentCellFifth.FixedHeight = 20f;


                        pdfTabContent2.AddCell(contentCellFirst);
                        pdfTabContent2.AddCell(contentCellSecond);
                        pdfTabContent2.AddCell(contentCellThird);
                        pdfTabContent2.AddCell(contentCellFourth);
                        pdfTabContent2.AddCell(contentCellFifth);

                    }
                }

                pdfTabContent2.TotalWidth = document.PageSize.Width - 80f;
                pdfTabContent2.WidthPercentage = 95;
                pdfTabContent2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTabContent2.SpacingAfter = 20;
                Paragraph pContent2 = new Paragraph();
                pContent2.IndentationLeft = 30;
                pContent2.Add(pdfTabContent2);

                Paragraph pActividades = new Paragraph("Actividades Registradas:", tinyFontBoldUnderline);
                pActividades.IndentationLeft = 30;
                pActividades.SpacingAfter = 20;
                document.Add(pActividades);
                document.Add(pContent2);

                #endregion
            

            document.Close();

            return output.ToArray();
        }

        public byte[] GetProgramBeneficiaryReport(HttpContext context)
        {
            int index = 0;
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());

            var programa = _programa.GetFirst(p => p.ID_Programa == ID_Programa);

            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Beneficiarios por Programa";
            headerFooter.SubTitulo = "Consolidado";

            writer.PageEvent = headerFooter;

            document.Open();

            Font tiny = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);


            PdfPTable pdfTab1 = new PdfPTable(2);
            int[] arr = new int[2];
            arr[0] = 1;
            arr[1] = 1;
            pdfTab1.SetWidths(arr);

            PdfPCell pdfCellHead = new PdfPCell(new Phrase("Nombre del Proyecto:", tinyBold));
            PdfPCell pdfCellHead1 = new PdfPCell(new Phrase(programa.Proyecto.Codigo, tinyBold));


            pdfCellHead.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead1.HorizontalAlignment = Element.ALIGN_LEFT;


            pdfCellHead.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCellHead.Border = 0;
            pdfCellHead1.Border = 0;



            //add all three cells into PdfTable
            pdfTab1.AddCell(pdfCellHead);
            pdfTab1.AddCell(pdfCellHead1);

            pdfTab1.TotalWidth = document.PageSize.Width - 80f;
            pdfTab1.WidthPercentage = 50;
            pdfTab1.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p1 = new Paragraph();
            p1.IndentationLeft = 30;
            p1.Add(pdfTab1);
            p1.SpacingAfter = 20;


            document.Add(p1);



            index++;
            #region Informacion de Programa

            PdfPTable pdfTab = new PdfPTable(3);
            int[] arrHeader = new int[3];
            arrHeader[0] = 1;
            arrHeader[1] = 1;
            arrHeader[2] = 1;
            pdfTab.SetWidths(arrHeader);

            Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
            Font tinyFontBoldUnderline = new Font(Font.FontFamily.HELVETICA, 7f, Font.UNDERLINE | Font.BOLD, BaseColor.BLACK);
            BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

            ////We will have to create separate cells to include image logo and 2 separate strings
            ////Row 1
            PdfPCell pdfCell1 = new PdfPCell(new Phrase("Numero de Programa", tinyFontBold));
            PdfPCell pdfCell2 = new PdfPCell(new Phrase(index.ToString(), tinyFont));
            PdfPCell pdfCell3 = new PdfPCell(new Phrase("Código", tinyFontBold));
            PdfPCell pdfCell4 = new PdfPCell(new Phrase(programa.Codigo, tinyFont));
            PdfPCell pdfCell5 = new PdfPCell(new Phrase("Tipo de Programa", tinyFontBold));
            PdfPCell pdfCell6 = new PdfPCell(new Phrase(programa.TipoPrograma.TipoProgramaDescripcion, tinyFont));
            PdfPCell pdfCell7 = new PdfPCell(new Phrase("Comunidad", tinyFontBold));
            PdfPCell pdfCell8 = new PdfPCell(new Phrase(programa.Comunidad.Comunidad1, tinyFont));
            PdfPCell pdfCell9 = new PdfPCell(new Phrase("Municipio", tinyFontBold));
            PdfPCell pdfCell10 = new PdfPCell(new Phrase(programa.Comunidad.Municipio.Municipio1, tinyFont));
            PdfPCell pdfCell11 = new PdfPCell(new Phrase("Departamento", tinyFontBold));
            PdfPCell pdfCell12 = new PdfPCell(new Phrase(programa.Comunidad.Municipio.Departamento.Departamento1, tinyFont));
            PdfPCell pdfCell13 = new PdfPCell(new Phrase("Fecha de Inicio", tinyFontBold));
            PdfPCell pdfCell14 = new PdfPCell(new Phrase(programa.FechaInicio.ToShortDateString(), tinyFont));
            PdfPCell pdfCell15 = new PdfPCell(new Phrase("Fecha de Finalización", tinyFontBold));
            PdfPCell pdfCell16 = new PdfPCell(new Phrase(programa.FechaFinal.ToShortDateString(), tinyFont));
            PdfPCell pdfCell17 = new PdfPCell(new Phrase("", tinyFont));
            PdfPCell pdfCell18 = new PdfPCell(new Phrase("", tinyFont));

            //pdfCell1.BackgroundColor = backgroundColor;
            //pdfCell2.BackgroundColor = backgroundColor;

            pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell4.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell5.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell6.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell7.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell8.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell9.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell10.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell11.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell12.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell13.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell14.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell15.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell16.HorizontalAlignment = Element.ALIGN_LEFT;

            pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell8.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell9.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell10.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell11.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell12.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell13.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell14.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell15.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell16.VerticalAlignment = Element.ALIGN_MIDDLE;


            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 0;
            pdfCell7.Border = 0;
            pdfCell8.Border = 0;
            pdfCell9.Border = 0;
            pdfCell10.Border = 0;
            pdfCell11.Border = 0;
            pdfCell12.Border = 0;
            pdfCell13.Border = 0;
            pdfCell14.Border = 0;
            pdfCell15.Border = 0;
            pdfCell16.Border = 0;
            pdfCell17.Border = 0;
            pdfCell18.Border = 0;


            pdfCell1.FixedHeight = 25f;
            pdfCell2.FixedHeight = 25f;
            pdfCell3.FixedHeight = 25f;
            pdfCell4.FixedHeight = 25f;
            pdfCell5.FixedHeight = 25f;
            pdfCell6.FixedHeight = 25f;
            pdfCell7.FixedHeight = 25f;
            pdfCell8.FixedHeight = 25f;
            pdfCell9.FixedHeight = 25f;
            pdfCell10.FixedHeight = 25f;
            pdfCell11.FixedHeight = 25f;
            pdfCell12.FixedHeight = 25f;
            pdfCell13.FixedHeight = 25f;
            pdfCell14.FixedHeight = 25f;
            pdfCell15.FixedHeight = 25f;
            pdfCell16.FixedHeight = 25f;
            pdfCell17.FixedHeight = 25f;
            pdfCell18.FixedHeight = 25f;

            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell5);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell4);
            pdfTab.AddCell(pdfCell6);
            pdfTab.AddCell(pdfCell7);
            pdfTab.AddCell(pdfCell9);
            pdfTab.AddCell(pdfCell11);
            pdfTab.AddCell(pdfCell8);
            pdfTab.AddCell(pdfCell10);
            pdfTab.AddCell(pdfCell12);
            pdfTab.AddCell(pdfCell13);
            pdfTab.AddCell(pdfCell15);
            pdfTab.AddCell(pdfCell17);
            pdfTab.AddCell(pdfCell14);
            pdfTab.AddCell(pdfCell16);
            pdfTab.AddCell(pdfCell18);


            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 90;
            pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph paragraph = new Paragraph();
            paragraph.IndentationLeft = 30;
            paragraph.SpacingAfter = 10;
            paragraph.Add(pdfTab);

            Paragraph pDescripcion = new Paragraph("Información del Programa:", tinyFontBoldUnderline);
            pDescripcion.IndentationLeft = 30;
            pDescripcion.SpacingAfter = 20;
            document.Add(pDescripcion);
            document.Add(paragraph);

            #endregion

            #region Beneficiarios de Programa

            PdfPTable pdfTabContent2 = new PdfPTable(5);
            pdfTabContent2.DefaultCell.FixedHeight = 100f;
            int[] arrContent2 = new int[5];
            arrContent2[0] = 1;
            arrContent2[1] = 1;
            arrContent2[2] = 4;
            arrContent2[3] = 1;
            arrContent2[4] = 2;

            pdfTabContent2.SetWidths(arrContent2);

            PdfPCell pdfCellContentActivity1 = new PdfPCell(new Phrase("N°", tinyFontBold));
            PdfPCell pdfCellContentActivity2 = new PdfPCell(new Phrase("RC", tinyFontBold));
            PdfPCell pdfCellContentActivity3 = new PdfPCell(new Phrase("Nombre del Beneficiario", tinyFontBold));
            PdfPCell pdfCellContentActivity4 = new PdfPCell(new Phrase("Sexo", tinyFontBold));
            PdfPCell pdfCellContentActivity5 = new PdfPCell(new Phrase("Edad", tinyFontBold));


            pdfCellContentActivity1.BackgroundColor = backgroundColor;
            pdfCellContentActivity2.BackgroundColor = backgroundColor;
            pdfCellContentActivity3.BackgroundColor = backgroundColor;
            pdfCellContentActivity4.BackgroundColor = backgroundColor;
            pdfCellContentActivity5.BackgroundColor = backgroundColor;

            pdfCellContentActivity1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity4.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity5.HorizontalAlignment = Element.ALIGN_LEFT;

            pdfCellContentActivity1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity5.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCellContentActivity1.Border = 0;
            pdfCellContentActivity2.Border = 0;
            pdfCellContentActivity3.Border = 0;
            pdfCellContentActivity4.Border = 0;
            pdfCellContentActivity5.Border = 0;

            //add all three cells into PdfTable
            pdfTabContent2.AddCell(pdfCellContentActivity1);
            pdfTabContent2.AddCell(pdfCellContentActivity2);
            pdfTabContent2.AddCell(pdfCellContentActivity3);
            pdfTabContent2.AddCell(pdfCellContentActivity4);
            pdfTabContent2.AddCell(pdfCellContentActivity5);


            if (programa.Beneficiario.Count > 0)
            {
                int num = 0;
                foreach (var beneficiario in programa.Beneficiario)
                {
                    num++;
                    PdfPCell contentCellFirst = new PdfPCell(new Phrase(num.ToString(), tinyFont));
                    PdfPCell contentCellSecond = new PdfPCell(new Phrase(string.IsNullOrEmpty(beneficiario.Codigo) ? "---" : beneficiario.Codigo, tinyFont));
                    PdfPCell contentCellThird = new PdfPCell(new Phrase(beneficiario.Nombre+" "+beneficiario.Apellido, tinyFont));
                    PdfPCell contentCellFourth = new PdfPCell(new Phrase(beneficiario.Sexo =="M"? "Masculino": "Femenino", tinyFont));
                    string[] edad = beneficiario.Edad.Split('|');
                    string edadFinal = "";
                    if (edad[1] == "0")
                    {
                        edadFinal = edad[0] + " Años";
                    }
                    else if (edad[0] == "0")
                    {
                        edadFinal = edad[1] + " Meses";
                    }
                    else
                    {
                        edadFinal = edad[0] + " Año(s) " + edad[1] + " meses";
                    }
                    PdfPCell contentCellFifth = new PdfPCell(new Phrase(edadFinal, tinyFont));

                    contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellThird.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellFourth.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellFifth.HorizontalAlignment = Element.ALIGN_LEFT;

                    contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellThird.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellFourth.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellFifth.VerticalAlignment = Element.ALIGN_MIDDLE;


                    contentCellFirst.Border = 0;
                    contentCellSecond.Border = 0;
                    contentCellThird.Border = 0;
                    contentCellFourth.Border = 0;
                    contentCellFifth.Border = 0;

                    contentCellFirst.FixedHeight = 20f;
                    contentCellSecond.FixedHeight = 20f;
                    contentCellThird.FixedHeight = 20f;
                    contentCellFourth.FixedHeight = 20f;
                    contentCellFifth.FixedHeight = 20f;


                    pdfTabContent2.AddCell(contentCellFirst);
                    pdfTabContent2.AddCell(contentCellSecond);
                    pdfTabContent2.AddCell(contentCellThird);
                    pdfTabContent2.AddCell(contentCellFourth);
                    pdfTabContent2.AddCell(contentCellFifth);

                }
            }
            else
            {
                PdfPCell contentCellFirst = new PdfPCell(new Phrase("No existen Beneficiarios en inscritos en este programa", tinyFont));
                pdfTabContent2.AddCell(contentCellFirst);
            }

            pdfTabContent2.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContent2.WidthPercentage = 95;
            pdfTabContent2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTabContent2.SpacingAfter = 20;
            Paragraph pContent2 = new Paragraph();
            pContent2.IndentationLeft = 30;
            pContent2.Add(pdfTabContent2);

            Paragraph pBeneficiarios = new Paragraph("Beneficiarios Registrados:", tinyFontBoldUnderline);
            pBeneficiarios.IndentationLeft = 30;
            pBeneficiarios.SpacingAfter = 20;

            #region Informacion Beneficiarios

           

            PdfPTable pdfTabContentAditional = new PdfPTable(2);
            pdfTabContentAditional.DefaultCell.FixedHeight = 100f;
            int[] arrContentAditional = new int[2];
            arrContentAditional[0] = 2;
            arrContentAditional[1] = 1;
            pdfTabContentAditional.SetWidths(arrContentAditional);

            
            PdfPCell pdfCellContentAditional3 = new PdfPCell(new Phrase("Número de Beneficiarios del sexo masculino:", tinyFontBold));
            PdfPCell pdfCellContentAditional4 = new PdfPCell(new Phrase(programa.Beneficiario.Count(ben=> ben.Sexo == "M").ToString(), tinyFontBold));
            PdfPCell pdfCellContentAditional5 = new PdfPCell(new Phrase("Numero de Beneficiarios del sexo femenino:", tinyFontBold));
            PdfPCell pdfCellContentAditional6 = new PdfPCell(new Phrase(programa.Beneficiario.Count(ben => ben.Sexo == "F").ToString(), tinyFontBold));
            

           
            pdfCellContentAditional3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional4.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional5.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional6.HorizontalAlignment = Element.ALIGN_LEFT;
         

         
            pdfCellContentAditional3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional6.VerticalAlignment = Element.ALIGN_MIDDLE;
     


            pdfCellContentAditional3.Border = 0;
            pdfCellContentAditional4.Border = 0;
            pdfCellContentAditional5.Border = 0;
            pdfCellContentAditional6.Border = 0;
           

           
            pdfCellContentAditional3.FixedHeight = 20f;
            pdfCellContentAditional4.FixedHeight = 20f;
            pdfCellContentAditional5.FixedHeight = 20f;
            pdfCellContentAditional6.FixedHeight = 20f;
          


            
            pdfTabContentAditional.AddCell(pdfCellContentAditional3);
            pdfTabContentAditional.AddCell(pdfCellContentAditional4);
            pdfTabContentAditional.AddCell(pdfCellContentAditional5);
            pdfTabContentAditional.AddCell(pdfCellContentAditional6);
         


            pdfTabContentAditional.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContentAditional.WidthPercentage = 60;
            pdfTabContentAditional.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTabContentAditional.SpacingAfter = 20;
            Paragraph pContentActivity = new Paragraph();
            pContentActivity.IndentationLeft = 30;
            pContentActivity.Add(pdfTabContentAditional);
            
            #endregion


            document.Add(pBeneficiarios);
            document.Add(pContentActivity);
            document.Add(pContent2);

            #endregion


            document.Close();

            return output.ToArray();
        }

        public byte[] GetBeneficiaryCommnityReport(HttpContext context)
        {
            int index = 0;
            int ID_Comunidad = Int32.Parse(context.Request.Params["ID_Comunidad"].ToString());
            int numeroHombres = 0, numeroMujeres = 0, numeroPatrocinados = 0;
            var comunidad = _comunidad.GetFirst(p => p.ID_Comunidad == ID_Comunidad);

            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Beneficiarios por Comunidad";
            headerFooter.SubTitulo = "Consolidado";

            writer.PageEvent = headerFooter;

            document.Open();

            Font tiny = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);


            PdfPTable pdfTab1 = new PdfPTable(2);
            int[] arr = new int[2];
            arr[0] = 1;
            arr[1] = 1;
            pdfTab1.SetWidths(arr);

            PdfPCell pdfCellHead = new PdfPCell(new Phrase("Nombre de Comunidad:", tinyBold));
            PdfPCell pdfCellHead1 = new PdfPCell(new Phrase(comunidad.Comunidad1, tinyBold));
            PdfPCell pdfCellHead2 = new PdfPCell(new Phrase("Municipio:", tinyBold));
            PdfPCell pdfCellHead21 = new PdfPCell(new Phrase(comunidad.Municipio.Municipio1, tinyBold));
            PdfPCell pdfCellHead3 = new PdfPCell(new Phrase("Departamento:", tinyBold));
            PdfPCell pdfCellHead31 = new PdfPCell(new Phrase(comunidad.Municipio.Departamento.Departamento1, tinyBold));


            pdfCellHead.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead21.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead31.HorizontalAlignment = Element.ALIGN_LEFT;


            pdfCellHead.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead21.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead31.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCellHead.Border = 0;
            pdfCellHead1.Border = 0;
            pdfCellHead2.Border = 0;
            pdfCellHead21.Border = 0;
            pdfCellHead3.Border = 0;
            pdfCellHead31.Border = 0;



            //add all three cells into PdfTable
            pdfTab1.AddCell(pdfCellHead);
            pdfTab1.AddCell(pdfCellHead1);
            pdfTab1.AddCell(pdfCellHead2);
            pdfTab1.AddCell(pdfCellHead21);
            pdfTab1.AddCell(pdfCellHead3);
            pdfTab1.AddCell(pdfCellHead31);

            pdfTab1.TotalWidth = document.PageSize.Width - 80f;
            pdfTab1.WidthPercentage = 50;
            pdfTab1.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p1 = new Paragraph();
            p1.IndentationLeft = 30;
            p1.Add(pdfTab1);
            p1.SpacingAfter = 20;


            document.Add(p1);



            index++;

            Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
            Font tinyFontBoldUnderline = new Font(Font.FontFamily.HELVETICA, 7f, Font.UNDERLINE | Font.BOLD, BaseColor.BLACK);
            BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");


            #region Beneficiarios de Programa

            PdfPTable pdfTabContent2 = new PdfPTable(5);
            pdfTabContent2.DefaultCell.FixedHeight = 100f;
            int[] arrContent2 = new int[5];
            arrContent2[0] = 1;
            arrContent2[1] = 1;
            arrContent2[2] = 4;
            arrContent2[3] = 2;
            arrContent2[4] = 1;

            pdfTabContent2.SetWidths(arrContent2);

            PdfPCell pdfCellContentActivity1 = new PdfPCell(new Phrase("N°", tinyFontBold));
            PdfPCell pdfCellContentActivity2 = new PdfPCell(new Phrase("RC", tinyFontBold));
            PdfPCell pdfCellContentActivity3 = new PdfPCell(new Phrase("Nombre del Beneficiario", tinyFontBold));
            PdfPCell pdfCellContentActivity4 = new PdfPCell(new Phrase("Edad", tinyFontBold));
            PdfPCell pdfCellContentActivity5 = new PdfPCell(new Phrase("Programa", tinyFontBold));


            pdfCellContentActivity1.BackgroundColor = backgroundColor;
            pdfCellContentActivity2.BackgroundColor = backgroundColor;
            pdfCellContentActivity3.BackgroundColor = backgroundColor;
            pdfCellContentActivity4.BackgroundColor = backgroundColor;
            pdfCellContentActivity5.BackgroundColor = backgroundColor;

            pdfCellContentActivity1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity4.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity5.HorizontalAlignment = Element.ALIGN_LEFT;

            pdfCellContentActivity1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity5.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCellContentActivity1.Border = 0;
            pdfCellContentActivity2.Border = 0;
            pdfCellContentActivity3.Border = 0;
            pdfCellContentActivity4.Border = 0;
            pdfCellContentActivity5.Border = 0;

            //add all three cells into PdfTable
            pdfTabContent2.AddCell(pdfCellContentActivity1);
            pdfTabContent2.AddCell(pdfCellContentActivity2);
            pdfTabContent2.AddCell(pdfCellContentActivity3);
            pdfTabContent2.AddCell(pdfCellContentActivity4);
            pdfTabContent2.AddCell(pdfCellContentActivity5);



            int num = 0;
            foreach (var programa in comunidad.Programa)
	        {
                numeroHombres += programa.Beneficiario.Count(ben=>ben.Sexo=="M");
                numeroMujeres    += programa.Beneficiario.Count(ben => ben.Sexo == "F");
                numeroPatrocinados += programa.Beneficiario.Count(ben => !string.IsNullOrEmpty(ben.Codigo));
                foreach (var beneficiario in programa.Beneficiario)
                {
                    num++;
                    PdfPCell contentCellFirst = new PdfPCell(new Phrase(num.ToString(), tinyFont));
                    PdfPCell contentCellSecond = new PdfPCell(new Phrase(string.IsNullOrEmpty(beneficiario.Codigo) ? "---" : beneficiario.Codigo, tinyFont));
                    PdfPCell contentCellThird = new PdfPCell(new Phrase(beneficiario.Nombre + " " + beneficiario.Apellido, tinyFont));
                    string[] edad = beneficiario.Edad.Split('|');
                    string edadFinal = "";
                    if (edad[1] == "0")
                    {
                        edadFinal = edad[0] + " Años";
                    }
                    else if (edad[0] == "0")
                    {
                        edadFinal = edad[1] + " Meses";
                    }
                    else
                    {
                        edadFinal = edad[0] + " Año(s) " + edad[1] + " meses";
                    }

                    PdfPCell contentCellFourth = new PdfPCell(new Phrase(edadFinal, tinyFont));
                    PdfPCell contentCellFifth = new PdfPCell(new Phrase(beneficiario.Programa.TipoPrograma.TipoPrograma1, tinyFont));

                    contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellThird.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellFourth.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellFifth.HorizontalAlignment = Element.ALIGN_LEFT;

                    contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellThird.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellFourth.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellFifth.VerticalAlignment = Element.ALIGN_MIDDLE;


                    contentCellFirst.Border = 0;
                    contentCellSecond.Border = 0;
                    contentCellThird.Border = 0;
                    contentCellFourth.Border = 0;
                    contentCellFifth.Border = 0;

                    contentCellFirst.FixedHeight = 20f;
                    contentCellSecond.FixedHeight = 20f;
                    contentCellThird.FixedHeight = 20f;
                    contentCellFourth.FixedHeight = 20f;
                    contentCellFifth.FixedHeight = 20f;


                    pdfTabContent2.AddCell(contentCellFirst);
                    pdfTabContent2.AddCell(contentCellSecond);
                    pdfTabContent2.AddCell(contentCellThird);
                    pdfTabContent2.AddCell(contentCellFourth);
                    pdfTabContent2.AddCell(contentCellFifth);

                }
	        }

            pdfTabContent2.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContent2.WidthPercentage = 95;
            pdfTabContent2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTabContent2.SpacingAfter = 20;
            Paragraph pContent2 = new Paragraph();
            pContent2.IndentationLeft = 30;
            pContent2.Add(pdfTabContent2);

            Paragraph pBeneficiarios = new Paragraph("Beneficiarios Registrados:", tinyFontBoldUnderline);
            pBeneficiarios.IndentationLeft = 30;
            pBeneficiarios.SpacingAfter = 20;

            #region Informacion Beneficiarios

            PdfPTable pdfTabContentAditional = new PdfPTable(2);
            pdfTabContentAditional.DefaultCell.FixedHeight = 100f;
            int[] arrContentAditional = new int[2];
            arrContentAditional[0] = 2;
            arrContentAditional[1] = 1;
            pdfTabContentAditional.SetWidths(arrContentAditional);


            PdfPCell pdfCellContentAditional3 = new PdfPCell(new Phrase("Número de Beneficiarios del sexo masculino:", tinyFontBold));
            PdfPCell pdfCellContentAditional4 = new PdfPCell(new Phrase(numeroHombres.ToString(), tinyFontBold));
            PdfPCell pdfCellContentAditional5 = new PdfPCell(new Phrase("Numero de Beneficiarios del sexo femenino:", tinyFontBold));
            PdfPCell pdfCellContentAditional6 = new PdfPCell(new Phrase(numeroMujeres.ToString(), tinyFontBold));
            PdfPCell pdfCellContentAditional7 = new PdfPCell(new Phrase("Numero de Beneficiarios patrocinados:", tinyFontBold));
            PdfPCell pdfCellContentAditional8 = new PdfPCell(new Phrase(numeroPatrocinados.ToString(), tinyFontBold));



            pdfCellContentAditional3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional4.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional5.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional6.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional7.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional8.HorizontalAlignment = Element.ALIGN_LEFT;



            pdfCellContentAditional3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional6.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional7.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional8.VerticalAlignment = Element.ALIGN_MIDDLE;



            pdfCellContentAditional3.Border = 0;
            pdfCellContentAditional4.Border = 0;
            pdfCellContentAditional5.Border = 0;
            pdfCellContentAditional6.Border = 0;
            pdfCellContentAditional7.Border = 0;
            pdfCellContentAditional8.Border = 0;



            pdfCellContentAditional3.FixedHeight = 20f;
            pdfCellContentAditional4.FixedHeight = 20f;
            pdfCellContentAditional5.FixedHeight = 20f;
            pdfCellContentAditional6.FixedHeight = 20f;
            pdfCellContentAditional7.FixedHeight = 20f;
            pdfCellContentAditional8.FixedHeight = 20f;




            pdfTabContentAditional.AddCell(pdfCellContentAditional3);
            pdfTabContentAditional.AddCell(pdfCellContentAditional4);
            pdfTabContentAditional.AddCell(pdfCellContentAditional5);
            pdfTabContentAditional.AddCell(pdfCellContentAditional6);
            pdfTabContentAditional.AddCell(pdfCellContentAditional7);
            pdfTabContentAditional.AddCell(pdfCellContentAditional8);



            pdfTabContentAditional.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContentAditional.WidthPercentage = 60;
            pdfTabContentAditional.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTabContentAditional.SpacingAfter = 20;
            Paragraph pContentAditional = new Paragraph();
            pContentAditional.IndentationLeft = 30;
            pContentAditional.Add(pdfTabContentAditional);

            #endregion


            document.Add(pBeneficiarios);
            document.Add(pContentAditional);
            document.Add(pContent2);

            #endregion


            document.Close();

            return output.ToArray();
        }

        public byte[] GetAttendanceActivityReport(HttpContext context)
        {
            int index = 0;
            int ID_Programa = Int32.Parse(context.Request.Params["ID_Programa"].ToString());

            int ID_Actividad = Int32.Parse(context.Request.Params["ID_Actividad"].ToString());

            var programa = _programa.GetFirst(p => p.ID_Programa == ID_Programa);

            var actividad = _actividad.GetFirst(a=> a.ID_Actividad == ID_Actividad);

            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Asistencia a Actividad";
            headerFooter.SubTitulo = "Vision Mundial";

            writer.PageEvent = headerFooter;

            document.Open();

            Font tiny = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);


            PdfPTable pdfTab1 = new PdfPTable(2);
            int[] arr = new int[2];
            arr[0] = 1;
            arr[1] = 1;
            pdfTab1.SetWidths(arr);

            PdfPCell pdfCellHead = new PdfPCell(new Phrase("Nombre del Proyecto:", tinyBold));
            PdfPCell pdfCellHead1 = new PdfPCell(new Phrase(programa.Proyecto.Codigo, tinyBold));


            pdfCellHead.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead1.HorizontalAlignment = Element.ALIGN_LEFT;


            pdfCellHead.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCellHead.Border = 0;
            pdfCellHead1.Border = 0;



            //add all three cells into PdfTable
            pdfTab1.AddCell(pdfCellHead);
            pdfTab1.AddCell(pdfCellHead1);

            pdfTab1.TotalWidth = document.PageSize.Width - 80f;
            pdfTab1.WidthPercentage = 50;
            pdfTab1.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p1 = new Paragraph();
            p1.IndentationLeft = 30;
            p1.Add(pdfTab1);
            p1.SpacingAfter = 20;


            document.Add(p1);



            index++;
            #region Informacion de Programa

            PdfPTable pdfTab = new PdfPTable(3);
            int[] arrHeader = new int[3];
            arrHeader[0] = 1;
            arrHeader[1] = 1;
            arrHeader[2] = 1;
            pdfTab.SetWidths(arrHeader);

            Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
            Font tinyFontBoldUnderline = new Font(Font.FontFamily.HELVETICA, 7f, Font.UNDERLINE | Font.BOLD, BaseColor.BLACK);
            BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

            ////We will have to create separate cells to include image logo and 2 separate strings
            ////Row 1
            PdfPCell pdfCell1 = new PdfPCell(new Phrase("Numero de Programa", tinyFontBold));
            PdfPCell pdfCell2 = new PdfPCell(new Phrase(index.ToString(), tinyFont));
            PdfPCell pdfCell3 = new PdfPCell(new Phrase("Código", tinyFontBold));
            PdfPCell pdfCell4 = new PdfPCell(new Phrase(programa.Codigo, tinyFont));
            PdfPCell pdfCell5 = new PdfPCell(new Phrase("Tipo de Programa", tinyFontBold));
            PdfPCell pdfCell6 = new PdfPCell(new Phrase(programa.TipoPrograma.TipoProgramaDescripcion, tinyFont));
            PdfPCell pdfCell7 = new PdfPCell(new Phrase("Comunidad", tinyFontBold));
            PdfPCell pdfCell8 = new PdfPCell(new Phrase(programa.Comunidad.Comunidad1, tinyFont));
            PdfPCell pdfCell9 = new PdfPCell(new Phrase("Municipio", tinyFontBold));
            PdfPCell pdfCell10 = new PdfPCell(new Phrase(programa.Comunidad.Municipio.Municipio1, tinyFont));
            PdfPCell pdfCell11 = new PdfPCell(new Phrase("Departamento", tinyFontBold));
            PdfPCell pdfCell12 = new PdfPCell(new Phrase(programa.Comunidad.Municipio.Departamento.Departamento1, tinyFont));
            PdfPCell pdfCell13 = new PdfPCell(new Phrase("Fecha de Inicio", tinyFontBold));
            PdfPCell pdfCell14 = new PdfPCell(new Phrase(programa.FechaInicio.ToShortDateString(), tinyFont));
            PdfPCell pdfCell15 = new PdfPCell(new Phrase("Fecha de Finalización", tinyFontBold));
            PdfPCell pdfCell16 = new PdfPCell(new Phrase(programa.FechaFinal.ToShortDateString(), tinyFont));
            PdfPCell pdfCell17 = new PdfPCell(new Phrase("", tinyFont));
            PdfPCell pdfCell18 = new PdfPCell(new Phrase("", tinyFont));

            //pdfCell1.BackgroundColor = backgroundColor;
            //pdfCell2.BackgroundColor = backgroundColor;

            pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell4.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell5.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell6.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell7.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell8.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell9.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell10.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell11.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell12.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell13.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell14.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell15.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell16.HorizontalAlignment = Element.ALIGN_LEFT;

            pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell8.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell9.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell10.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell11.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell12.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell13.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell14.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell15.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell16.VerticalAlignment = Element.ALIGN_MIDDLE;


            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 0;
            pdfCell7.Border = 0;
            pdfCell8.Border = 0;
            pdfCell9.Border = 0;
            pdfCell10.Border = 0;
            pdfCell11.Border = 0;
            pdfCell12.Border = 0;
            pdfCell13.Border = 0;
            pdfCell14.Border = 0;
            pdfCell15.Border = 0;
            pdfCell16.Border = 0;
            pdfCell17.Border = 0;
            pdfCell18.Border = 0;


            pdfCell1.FixedHeight = 25f;
            pdfCell2.FixedHeight = 25f;
            pdfCell3.FixedHeight = 25f;
            pdfCell4.FixedHeight = 25f;
            pdfCell5.FixedHeight = 25f;
            pdfCell6.FixedHeight = 25f;
            pdfCell7.FixedHeight = 25f;
            pdfCell8.FixedHeight = 25f;
            pdfCell9.FixedHeight = 25f;
            pdfCell10.FixedHeight = 25f;
            pdfCell11.FixedHeight = 25f;
            pdfCell12.FixedHeight = 25f;
            pdfCell13.FixedHeight = 25f;
            pdfCell14.FixedHeight = 25f;
            pdfCell15.FixedHeight = 25f;
            pdfCell16.FixedHeight = 25f;
            pdfCell17.FixedHeight = 25f;
            pdfCell18.FixedHeight = 25f;

            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell5);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell4);
            pdfTab.AddCell(pdfCell6);
            pdfTab.AddCell(pdfCell7);
            pdfTab.AddCell(pdfCell9);
            pdfTab.AddCell(pdfCell11);
            pdfTab.AddCell(pdfCell8);
            pdfTab.AddCell(pdfCell10);
            pdfTab.AddCell(pdfCell12);
            pdfTab.AddCell(pdfCell13);
            pdfTab.AddCell(pdfCell15);
            pdfTab.AddCell(pdfCell17);
            pdfTab.AddCell(pdfCell14);
            pdfTab.AddCell(pdfCell16);
            pdfTab.AddCell(pdfCell18);


            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 90;
            pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph paragraph = new Paragraph();
            paragraph.IndentationLeft = 30;
            paragraph.SpacingAfter = 10;
            paragraph.Add(pdfTab);

            Paragraph pDescripcion = new Paragraph("Información del Programa:", tinyFontBoldUnderline);
            pDescripcion.IndentationLeft = 30;
            pDescripcion.SpacingAfter = 20;
            document.Add(pDescripcion);
            document.Add(paragraph);

            #endregion

            #region Actividad

            PdfPTable pdfTabContent2 = new PdfPTable(2);
            pdfTabContent2.DefaultCell.FixedHeight = 100f;
            int[] arrContent2 = new int[2];
            arrContent2[0] = 1;
            arrContent2[1] = 2;

            pdfTabContent2.SetWidths(arrContent2);

            PdfPCell pdfCellContentActivity1 = new PdfPCell(new Phrase("Fecha", tinyFontBold));
            PdfPCell pdfCellContentActivity11 = new PdfPCell(new Phrase(actividad.Fecha.ToShortDateString(), tinyFont));
            PdfPCell pdfCellContentActivity2 = new PdfPCell(new Phrase("Codigo de Actividad", tinyFontBold));
            PdfPCell pdfCellContentActivity21 = new PdfPCell(new Phrase(actividad.Codigo, tinyFont));
            PdfPCell pdfCellContentActivity3 = new PdfPCell(new Phrase("Actividad", tinyFontBold));
            PdfPCell pdfCellContentActivity31 = new PdfPCell(new Phrase(actividad.ActividadDescripcion, tinyFont));
            PdfPCell pdfCellContentActivity4 = new PdfPCell(new Phrase("Observación", tinyFontBold));
            PdfPCell pdfCellContentActivity41 = new PdfPCell(new Phrase(actividad.Observacion, tinyFont));
           

            pdfCellContentActivity1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity4.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity11.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity21.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity31.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity41.HorizontalAlignment = Element.ALIGN_LEFT;
            

            pdfCellContentActivity1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity11.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity21.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity31.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity41.VerticalAlignment = Element.ALIGN_MIDDLE;
            

            pdfCellContentActivity1.Border = 0;
            pdfCellContentActivity2.Border = 0;
            pdfCellContentActivity3.Border = 0;
            pdfCellContentActivity4.Border = 0;
            pdfCellContentActivity11.Border = 0;
            pdfCellContentActivity21.Border = 0;
            pdfCellContentActivity31.Border = 0;
            pdfCellContentActivity41.Border = 0;
           

            //add all three cells into PdfTable
            pdfTabContent2.AddCell(pdfCellContentActivity1);
            pdfTabContent2.AddCell(pdfCellContentActivity11);
            pdfTabContent2.AddCell(pdfCellContentActivity2);
            pdfTabContent2.AddCell(pdfCellContentActivity21);
            pdfTabContent2.AddCell(pdfCellContentActivity3);
            pdfTabContent2.AddCell(pdfCellContentActivity31);
            pdfTabContent2.AddCell(pdfCellContentActivity4);
            pdfTabContent2.AddCell(pdfCellContentActivity41);
            

            pdfTabContent2.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContent2.WidthPercentage = 90;
            pdfTabContent2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTabContent2.SpacingAfter = 20;
            Paragraph pContent2 = new Paragraph();
            pContent2.IndentationLeft = 30;
            pContent2.Add(pdfTabContent2);
            Paragraph pActividades = new Paragraph("Información de Actividad:", tinyFontBoldUnderline);
            pActividades.IndentationLeft = 30;
            pActividades.SpacingAfter = 20;
            document.Add(pActividades);
            document.Add(pContent2);

            #endregion

            #region Beneficiarios en Actividad

            PdfPTable pdfTabContent22 = new PdfPTable(3);
            pdfTabContent22.DefaultCell.FixedHeight = 100f;
            int[] arrContent22 = new int[3];
            arrContent22[0] = 1;
            arrContent22[1] = 3;
            arrContent22[2] = 1;
            

            pdfTabContent22.SetWidths(arrContent22);

            PdfPCell pdfCellContentActivity101 = new PdfPCell(new Phrase("N°", tinyFontBold));
            PdfPCell pdfCellContentActivity201 = new PdfPCell(new Phrase("Nombre del Beneficiario", tinyFontBold));
            PdfPCell pdfCellContentActivity301 = new PdfPCell(new Phrase("Estado", tinyFontBold));



            pdfCellContentActivity101.BackgroundColor = backgroundColor;
            pdfCellContentActivity201.BackgroundColor = backgroundColor;
            pdfCellContentActivity301.BackgroundColor = backgroundColor;


            pdfCellContentActivity101.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity201.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity301.HorizontalAlignment = Element.ALIGN_LEFT;


            pdfCellContentActivity101.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity201.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity301.VerticalAlignment = Element.ALIGN_MIDDLE;


            pdfCellContentActivity101.Border = 0;
            pdfCellContentActivity201.Border = 0;
            pdfCellContentActivity301.Border = 0;
           

            //add all three cells into PdfTable
            pdfTabContent22.AddCell(pdfCellContentActivity101);
            pdfTabContent22.AddCell(pdfCellContentActivity201);
            pdfTabContent22.AddCell(pdfCellContentActivity301);
            


            if (actividad.Asistencia.Count > 0)
            {
                int num = 0;
                foreach (var asistencia in actividad.Asistencia)
                {
                    num++;

                    var beneficiario = _beneficiario.GetFirst(ben=>ben.ID_Beneficiario==asistencia.ID_Beneficiario);

                    PdfPCell contentCellFirst = new PdfPCell(new Phrase(num.ToString(), tinyFont));
                    PdfPCell contentCellSecond = new PdfPCell(new Phrase(beneficiario.Nombre +" "+beneficiario.Apellido, tinyFont));
                    PdfPCell contentCellThird = new PdfPCell(new Phrase(asistencia.Estado, tinyFont));
                    

                    contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellThird.HorizontalAlignment = Element.ALIGN_LEFT;
                    

                    contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellThird.VerticalAlignment = Element.ALIGN_MIDDLE;
                    


                    contentCellFirst.Border = 0;
                    contentCellSecond.Border = 0;
                    contentCellThird.Border = 0;
                   

                    contentCellFirst.FixedHeight = 20f;
                    contentCellSecond.FixedHeight = 20f;
                    contentCellThird.FixedHeight = 20f;
                    


                    pdfTabContent22.AddCell(contentCellFirst);
                    pdfTabContent22.AddCell(contentCellSecond);
                    pdfTabContent22.AddCell(contentCellThird);
                    

                }
            }
            else
            {
                PdfPCell contentCellFirst = new PdfPCell(new Phrase("No existen Beneficiarios en inscritos en esta actividad", tinyFont));
                pdfTabContent2.AddCell(contentCellFirst);
            }

            pdfTabContent22.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContent22.WidthPercentage = 95;
            pdfTabContent22.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTabContent22.SpacingAfter = 20;
            Paragraph pContent201 = new Paragraph();
            pContent201.IndentationLeft = 30;
            pContent201.Add(pdfTabContent22);
            document.Add(pContent201);
            #endregion



            document.Close();

            return output.ToArray();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetFormattedState(string field)
        {
            string estado = "";
            switch (field)
            {
                case "A":
                    estado = "Activo";
                    break;
                case "I":
                    estado = "Inactivo";
                    break;
                case "S":
                    estado = "Suspendido";
                    break;
            }
            return estado;
        }
    }
}