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
    /// Summary description for GeneralReportsGrades
    /// </summary>
    public class GeneralReportsGrades : DataAccess, IHttpHandler, IRequiresSessionState
    {

        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Examen> _examen;
        IDataRepository<Programa> _programa;
        IDataRepository<Materia> _materia;
        //IDataRepository<Comunidad> _comunidad;
        //IDataRepository<Actividad> _actividad;
        IDataRepository<Beneficiario> _beneficiario;
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            switch (MethodName.ToLower())
            {
                case "getallsubjects":
                    context.Response.Write(GetAllSubjects());
                    break;
                case "getallexams":
                    context.Response.Write(GetAllExams(context));
                    break;
                case "getinscriptionreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=Inscripcion-materia.pdf");
                    context.Response.BinaryWrite(GetInscriptionReport(context));
                    break;
                case "getresultsreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=Resultados-Examen.pdf");
                    context.Response.BinaryWrite(GetResultsReport(context));
                    break;
                

            }
        }





        private void InitializeObjects()
        {

            _context = new AWContext();
            _materia = new DataRepository<IAWContext, Materia>(_context);
            _programa = new DataRepository<IAWContext, Programa>(_context);
            _examen = new DataRepository<IAWContext, Examen>(_context);
            //_actividad = new DataRepository<IAWContext, Actividad>(_context);
            _beneficiario = new DataRepository<IAWContext, Beneficiario>(_context);
        }


        private string GetAllExams(HttpContext context)
        {
            string options = "";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Materia = Int32.Parse(context.Request.Params["ID_Materia"].ToString());
            try
            {
                var materia = _materia.GetFirst(p => p.ID_Materia == ID_Materia);

                if (materia.Examen.Count > 0)
                {
                    foreach (var examen in materia.Examen.OrderByDescending(a => a.NumeroExamen))
                    {
                        options += "<option data-id-exam='" + examen.ID_Examen + "'>" + examen.NumeroExamen + "</option>";
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

        public string GetAllSubjects()
        {
            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsMaterias = "";
            try
            {
                var materias = _materia.Select();


                foreach (var materia in materias)
                {

                    optionsMaterias += "<option data-id-subject='" + materia.ID_Materia + "'>" + materia.Nombre + "-" + materia.Grado +"</option>";


                }
                response.IsSucess = true;
                response.ResponseData = optionsMaterias;
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

        public byte[] GetInscriptionReport(HttpContext context)
        {
            int index = 0;
            int ID_Materia = Int32.Parse(context.Request.Params["ID_Materia"].ToString());

            var materia = _materia.GetFirst(p => p.ID_Materia == ID_Materia);
            var programa = materia.AsignacionMateria.First().Beneficiario.Programa;

            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Beneficiarios Inscritos en materia";
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

            PdfPCell pdfCellHead = new PdfPCell(new Phrase("Nombre Materia:", tinyBold));
            PdfPCell pdfCellHead1 = new PdfPCell(new Phrase(materia.Nombre +" " + materia.Grado +" "+ materia.Anio, tinyBold));


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


            if (materia.AsignacionMateria.Count > 0)
            {
                int num = 0;
                foreach (var asignacion in materia.AsignacionMateria)
                {
                   
                    num++;
                    PdfPCell contentCellFirst = new PdfPCell(new Phrase(num.ToString(), tinyFont));
                    PdfPCell contentCellSecond = new PdfPCell(new Phrase(string.IsNullOrEmpty(asignacion.Beneficiario.Codigo) ? "---" : asignacion.Beneficiario.Codigo, tinyFont));
                    PdfPCell contentCellThird = new PdfPCell(new Phrase(asignacion.Beneficiario.Nombre + " " + asignacion.Beneficiario.Apellido, tinyFont));
                    PdfPCell contentCellFourth = new PdfPCell(new Phrase(asignacion.Beneficiario.Sexo == "M" ? "Masculino" : "Femenino", tinyFont));
                    string[] edad = asignacion.Beneficiario.Edad.Split('|');
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

            #endregion


            document.Add(pBeneficiarios);
            document.Add(pContent2);

            #endregion


            document.Close();

            return output.ToArray();
        }

        public byte[] GetResultsReport(HttpContext context)
        {
            int index = 0;
            int ID_Materia = Int32.Parse(context.Request.Params["ID_Materia"].ToString());
            int ID_Examen = Int32.Parse(context.Request.Params["ID_Examen"].ToString());

            var materia = _materia.GetFirst(p => p.ID_Materia == ID_Materia);
            var programa = materia.AsignacionMateria.First().Beneficiario.Programa;

            var examen = _examen.GetFirst(e=> e.ID_Examen == ID_Examen);

            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Notas por Examen";
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

            PdfPCell pdfCellHead = new PdfPCell(new Phrase("Nombre Materia:", tinyBold));
            PdfPCell pdfCellHead1 = new PdfPCell(new Phrase(materia.Nombre + " " + materia.Grado + " " + materia.Anio, tinyBold));
            PdfPCell pdfCellHead2 = new PdfPCell(new Phrase("ID Examen:", tinyBold));
            PdfPCell pdfCellHead21 = new PdfPCell(new Phrase(examen.NumeroExamen, tinyBold));


            pdfCellHead.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellHead21.HorizontalAlignment = Element.ALIGN_LEFT;


            pdfCellHead.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellHead21.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCellHead.Border = 0;
            pdfCellHead1.Border = 0;
            pdfCellHead2.Border = 0;
            pdfCellHead21.Border = 0;



            //add all three cells into PdfTable
            pdfTab1.AddCell(pdfCellHead);
            pdfTab1.AddCell(pdfCellHead1);
            pdfTab1.AddCell(pdfCellHead2);
            pdfTab1.AddCell(pdfCellHead21);

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

            PdfPTable pdfTabContent2 = new PdfPTable(4);
            pdfTabContent2.DefaultCell.FixedHeight = 100f;
            int[] arrContent2 = new int[4];
            arrContent2[0] = 1;
            arrContent2[1] = 1;
            arrContent2[2] = 4;
            arrContent2[3] = 1;
            

            pdfTabContent2.SetWidths(arrContent2);

            PdfPCell pdfCellContentActivity1 = new PdfPCell(new Phrase("N°", tinyFontBold));
            PdfPCell pdfCellContentActivity2 = new PdfPCell(new Phrase("RC", tinyFontBold));
            PdfPCell pdfCellContentActivity3 = new PdfPCell(new Phrase("Nombre del Beneficiario", tinyFontBold));
            PdfPCell pdfCellContentActivity4 = new PdfPCell(new Phrase("Nota", tinyFontBold));
            


            pdfCellContentActivity1.BackgroundColor = backgroundColor;
            pdfCellContentActivity2.BackgroundColor = backgroundColor;
            pdfCellContentActivity3.BackgroundColor = backgroundColor;
            pdfCellContentActivity4.BackgroundColor = backgroundColor;
           

            pdfCellContentActivity1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentActivity4.HorizontalAlignment = Element.ALIGN_LEFT;
            

            pdfCellContentActivity1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentActivity4.VerticalAlignment = Element.ALIGN_MIDDLE;
            

            pdfCellContentActivity1.Border = 0;
            pdfCellContentActivity2.Border = 0;
            pdfCellContentActivity3.Border = 0;
            pdfCellContentActivity4.Border = 0;
            

            //add all three cells into PdfTable
            pdfTabContent2.AddCell(pdfCellContentActivity1);
            pdfTabContent2.AddCell(pdfCellContentActivity2);
            pdfTabContent2.AddCell(pdfCellContentActivity3);
            pdfTabContent2.AddCell(pdfCellContentActivity4);
            


            if (examen.ExamenResultado.Count > 0)
            {
                int num = 0;
                foreach (var exam in examen.ExamenResultado)
                {
                    var beneficiario = _beneficiario.GetFirst(b=>b.ID_Beneficiario == exam.ID_Beneficiario);
                    num++;
                    PdfPCell contentCellFirst = new PdfPCell(new Phrase(num.ToString(), tinyFont));
                    PdfPCell contentCellSecond = new PdfPCell(new Phrase(string.IsNullOrEmpty(beneficiario.Codigo) ? "---" : beneficiario.Codigo, tinyFont));
                    PdfPCell contentCellThird = new PdfPCell(new Phrase(beneficiario.Nombre + " " + beneficiario.Apellido, tinyFont));
                    PdfPCell contentCellFourth = new PdfPCell(new Phrase(exam.Nota.ToString(), tinyFont));

                    contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellThird.HorizontalAlignment = Element.ALIGN_LEFT;
                    contentCellFourth.HorizontalAlignment = Element.ALIGN_LEFT;
                    

                    contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellThird.VerticalAlignment = Element.ALIGN_MIDDLE;
                    contentCellFourth.VerticalAlignment = Element.ALIGN_MIDDLE;
                    


                    contentCellFirst.Border = 0;
                    contentCellSecond.Border = 0;
                    contentCellThird.Border = 0;
                    contentCellFourth.Border = 0;
                   

                    contentCellFirst.FixedHeight = 20f;
                    contentCellSecond.FixedHeight = 20f;
                    contentCellThird.FixedHeight = 20f;
                    contentCellFourth.FixedHeight = 20f;
                    


                    pdfTabContent2.AddCell(contentCellFirst);
                    pdfTabContent2.AddCell(contentCellSecond);
                    pdfTabContent2.AddCell(contentCellThird);
                    pdfTabContent2.AddCell(contentCellFourth);
                    

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

            Paragraph pInfAdicional = new Paragraph("Informacion Adicional:", tinyFontBoldUnderline);
                pInfAdicional.IndentationLeft = 30;
                pInfAdicional.SpacingAfter = 20;

            PdfPTable pdfTabContentAditional = new PdfPTable(2);
            pdfTabContentAditional.DefaultCell.FixedHeight = 100f;
            int[] arrContentAditional = new int[2];
            arrContentAditional[0] = 2;
            arrContentAditional[1] = 1;
            pdfTabContentAditional.SetWidths(arrContentAditional);

            PdfPCell pdfCellContentAditional1 = new PdfPCell(new Phrase("Nota Mayor: ", tinyFontBold));
            PdfPCell pdfCellContentAditional2 = new PdfPCell(new Phrase(examen.ExamenResultado.Max(n=> Double.Parse( n.Nota)).ToString(), tinyFontBold));
            PdfPCell pdfCellContentAditional3 = new PdfPCell(new Phrase("Nota Menor:", tinyFontBold));
            PdfPCell pdfCellContentAditional4 = new PdfPCell(new Phrase(examen.ExamenResultado.Min(n => Double.Parse(n.Nota)).ToString(), tinyFontBold));
            PdfPCell pdfCellContentAditional5 = new PdfPCell(new Phrase("Promedio:", tinyFontBold));
            PdfPCell pdfCellContentAditional6 = new PdfPCell(new Phrase(examen.ExamenResultado.Average(n => Double.Parse(n.Nota)).ToString(), tinyFontBold));
           

            pdfCellContentAditional1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional4.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional5.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContentAditional6.HorizontalAlignment = Element.ALIGN_LEFT;
            

            pdfCellContentAditional1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContentAditional6.VerticalAlignment = Element.ALIGN_MIDDLE;
          


            pdfCellContentAditional1.Border = 0;
            pdfCellContentAditional2.Border = 0;
            pdfCellContentAditional3.Border = 0;
            pdfCellContentAditional4.Border = 0;
            pdfCellContentAditional5.Border = 0;
            pdfCellContentAditional6.Border = 0;
            

            pdfCellContentAditional1.FixedHeight = 20f;
            pdfCellContentAditional2.FixedHeight = 20f;
            pdfCellContentAditional3.FixedHeight = 20f;
            pdfCellContentAditional4.FixedHeight = 20f;
            pdfCellContentAditional5.FixedHeight = 20f;
            pdfCellContentAditional6.FixedHeight = 20f;
            


            pdfTabContentAditional.AddCell(pdfCellContentAditional1);
            pdfTabContentAditional.AddCell(pdfCellContentAditional2);
            pdfTabContentAditional.AddCell(pdfCellContentAditional3);
            pdfTabContentAditional.AddCell(pdfCellContentAditional4);
            pdfTabContentAditional.AddCell(pdfCellContentAditional5);
            pdfTabContentAditional.AddCell(pdfCellContentAditional6);
            


            pdfTabContentAditional.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContentAditional.WidthPercentage = 30;
            pdfTabContentAditional.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTabContentAditional.SpacingAfter = 20;
            Paragraph pContentAdditional = new Paragraph();
            pContentAdditional.IndentationLeft = 30;
            pContentAdditional.Add(pdfTabContentAditional);

            #endregion


            document.Add(pBeneficiarios);
            document.Add(pContent2);
            document.Add(pInfAdicional);
            document.Add(pContentAdditional);

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
    }
}