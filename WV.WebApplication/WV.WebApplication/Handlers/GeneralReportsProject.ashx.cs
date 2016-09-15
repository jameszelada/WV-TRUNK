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
    /// Summary description for GeneralReportsProject
    /// </summary>
    public class GeneralReportsProject : DataAccess, IHttpHandler, IRequiresSessionState
    {

       
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        AWContext _context1;
        IDataRepository<Persona> _persona;
        IDataRepository<Proyecto> _proyecto;
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            switch (MethodName.ToLower())
            {
                case "getallpersons":
                    context.Response.Write(GetAllPersons());
                    break;
                case "getallprojects":
                    context.Response.Write(GetAllProjects());
                    break;
                case "getalllogbooks":
                    context.Response.Write(GetLogBooks(context));
                    break;
                case "getallweeklyplans":
                    context.Response.Write(GetWeeklyPlans(context));
                    break;
                case "getjobreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Puestos-{0}.pdf", DateTime.Now.ToShortDateString()));
                    context.Response.BinaryWrite(GetAllJobReport());
                    break;
                case "getassignreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Asignacion-Proyecto-{0}.pdf", DateTime.Now.ToShortDateString()));
                    context.Response.BinaryWrite(GetAsignationReport(context));
                    break;
                case "getlogbookreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=Bitacora-Personal.pdf");
                    context.Response.BinaryWrite(GetLogbookReport(context));
                    break;
                case "getweeklyplanreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=Plan-Semanal.pdf");
                    context.Response.BinaryWrite(GetWeeklyPlanReport(context));
                    break;
                case "getsummaryseport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Consolidado-Proyecto-{0}.pdf",DateTime.Now.ToShortDateString()));
                    context.Response.BinaryWrite(GetConsolidatedProjectReport(context));
                    break;
            }
        }

        private void InitializeObjects()
        {

            _context = new AWContext();
            _context1 = new AWContext();
            _persona = new DataRepository<IAWContext, Persona>(_context);
            _proyecto = new DataRepository<IAWContext, Proyecto>(_context);
        }

        public string GetAllPersons()
        {
            string options = "";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var personas = _persona.GetAll().Select((x, index) => new
                {
                    index,
                    x.Nombre,
                    x.Apellido,
                    x.ID_Persona
                });

                foreach (var persona in personas)
                {
                    int index = persona.index + 1;

                    options += "<option data-id-person='" + persona.ID_Persona + "'>" + persona.Nombre[0] + ". " + persona.Apellido.Split(' ')[0] + "</option>";
                }

                if (personas.ToList().Count > 0)
                {
                    response.IsSucess = true;
                    response.ResponseData = options;
                    response.Message = string.Empty;
                    response.CallBack = string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string GetAllProjects()
        {
            string options = "";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var proyectos = _proyecto.GetAll().Select((x, index) => new
                {
                    index,
                    x.Codigo,
                    x.ID_Proyecto
                 
                });

                foreach (var proyecto in proyectos)
                {
                    int index = proyecto.index + 1;

                    options += "<option data-id-project='" + proyecto.ID_Proyecto + "'>" + proyecto.Codigo + "</option>";
                }

                if (proyectos.ToList().Count > 0)
                {
                    response.IsSucess = true;
                    response.ResponseData = options;
                    response.Message = string.Empty;
                    response.CallBack = string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSucess = false;
            }

            return serializer.Serialize(response);
        }

        public string GetLogBooks(HttpContext context)
        {
            string options = "";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Persona = Int32.Parse(context.Request.Params["ID_Persona"].ToString());
            try
            {
                var persona = _persona.GetFirst(p=> p.ID_Persona == ID_Persona);

                if (persona.Bitacora.Count > 0)
                {
                    foreach (var bitacora in persona.Bitacora)
                    {
                         options += "<option data-id-logbook='" + bitacora.ID_Bitacora + "'>" + bitacora.FechaBitacora.ToShortDateString() + "</option>";
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

        public string GetWeeklyPlans(HttpContext context)
        {
            string options = "";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int ID_Persona = Int32.Parse(context.Request.Params["ID_Persona"].ToString());
            try
            {
                var persona = _persona.GetFirst(p => p.ID_Persona == ID_Persona);

                if (persona.PlanSemanal.Count > 0)
                {
                    foreach (var plan in persona.PlanSemanal)
                    {
                        options += "<option data-id-weeklyplan='" + plan.ID_PlanSemanal + "'>" + plan.FechaInicio.ToShortDateString() +" a "+plan.FechaFinal.ToShortDateString()+ "</option>";
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

        public byte[] GetAllJobReport()
        {

            //Get Report Data
            // Empty Dictionary
            Dictionary<string, Object> param = new Dictionary<string, Object>();

            string query = "spGetJobInformation";
            DataSet myDataset = GetDataSet(query, CommandType.StoredProcedure, param);



            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Puestos";
            headerFooter.SubTitulo = "Listado de Puestos registrados";

            writer.PageEvent = headerFooter;

            document.Open();
            PdfPTable pdfTab = new PdfPTable(2);
            int[] arr = new int[2];
            arr[0] = 1;
            arr[1] = 1;
            pdfTab.SetWidths(arr);

            Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
            BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");
            BaseColor backgroundColorSecond = WebColors.GetRGBColor("#FF7900");

            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
            PdfPCell pdfCell1 = new PdfPCell(new Phrase("Tipo de Puesto", tinyFontBold));
            PdfPCell pdfCell2 = new PdfPCell(new Phrase("Descripción del Tipo de Puesto", tinyFontBold));
            pdfCell1.BackgroundColor = backgroundColor;
            pdfCell2.BackgroundColor = backgroundColor;

            pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;


            pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCell1.Border = 0;
            pdfCell2.Border = 0;



            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);

            foreach (DataRow row in myDataset.Tables[0].Rows)
            {
                PdfPCell contentCellFirst = new PdfPCell(new Phrase(row["TipoPuesto"].ToString(), tinyFont));
                PdfPCell contentCellSecond = new PdfPCell(new Phrase(row["TipoPuestoDescripcion"].ToString(), tinyFont));
                contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;

                contentCellFirst.Border = 0;
                contentCellSecond.Border = 0;

                pdfTab.AddCell(contentCellFirst);
                pdfTab.AddCell(contentCellSecond);
            }



            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 50;
            pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;

            Paragraph pDescripcion = new Paragraph("Cuadro de Tipos de Puesto Registrados en el sistema:",tinyFontBold);
            pDescripcion.IndentationLeft = 30;
            pDescripcion.SpacingAfter = 20;

            Paragraph p = new Paragraph();
            p.IndentationLeft = 30;
            p.Add(pdfTab);
            p.SpacingAfter = 20;

            document.Add(pDescripcion);
            document.Add(p);

            //Second Table

            PdfPTable pdfTabContent = new PdfPTable(3);
            int[] arrContent = new int[3];
            arrContent[0] = 1;
            arrContent[1] = 2;
            arrContent[2] = 1;
            pdfTabContent.SetWidths(arrContent);

            PdfPCell pdfCellContent1 = new PdfPCell(new Phrase("Nombre Puesto", tinyFontBold));
            PdfPCell pdfCellContent2 = new PdfPCell(new Phrase("Descripcion del Puesto", tinyFontBold));
            PdfPCell pdfCellContent3 = new PdfPCell(new Phrase("Tipo de Puesto", tinyFontBold));

            pdfCellContent1.BackgroundColor = backgroundColorSecond;
            pdfCellContent2.BackgroundColor = backgroundColorSecond;
            pdfCellContent3.BackgroundColor = backgroundColorSecond;

            pdfCellContent1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContent2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContent3.HorizontalAlignment = Element.ALIGN_LEFT;




            pdfCellContent1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContent2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContent3.VerticalAlignment = Element.ALIGN_MIDDLE;


            pdfCellContent1.Border = 0;
            pdfCellContent2.Border = 0;
            pdfCellContent3.Border = 0;




            //add all three cells into PdfTable
            pdfTabContent.AddCell(pdfCellContent1);
            pdfTabContent.AddCell(pdfCellContent2);
            pdfTabContent.AddCell(pdfCellContent3);



            foreach (DataRow row in myDataset.Tables[1].Rows)
            {
                PdfPCell contentCellFirst = new PdfPCell(new Phrase(row["Puesto"].ToString(), tinyFont));
                PdfPCell contentCellSecond = new PdfPCell(new Phrase(row["PuestoDescripcion"].ToString(), tinyFont));
                PdfPCell contentCellThird = new PdfPCell(new Phrase(row["TipoPuesto"].ToString(), tinyFont));
                contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellThird.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentCellThird.VerticalAlignment = Element.ALIGN_MIDDLE;

                contentCellFirst.Border = 0;
                contentCellSecond.Border = 0;
                contentCellThird.Border = 0;

                pdfTabContent.AddCell(contentCellFirst);
                pdfTabContent.AddCell(contentCellSecond);
                pdfTabContent.AddCell(contentCellThird);
            }

            pdfTabContent.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContent.WidthPercentage = 80;
            pdfTabContent.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph pContent = new Paragraph();
            pContent.IndentationLeft = 30;
            pContent.Add(pdfTabContent);

            Paragraph pDescripcionPuesto = new Paragraph("Cuadro de Puestos Registrados en el sistema:", tinyFontBold);
            pDescripcionPuesto.IndentationLeft = 30;
            pDescripcionPuesto.SpacingAfter = 20;

            document.Add(pDescripcionPuesto);
            document.Add(pContent);

            document.Close();

            return output.ToArray();
        }

        public byte[] GetAsignationReport(HttpContext context)
        {

            int ID_Proyecto = Int32.Parse(context.Request.Params["ID_Proyecto"].ToString());
            //Get Report Data
            Dictionary<string, Object> param = new Dictionary<string, Object>();
            param.Add("projectIdentity", ID_Proyecto);

            string query = "spGetAsignation";
            DataSet myDataset = GetDataSet(query, CommandType.StoredProcedure, param);
            DataRow headerData = myDataset.Tables[0].Rows[0];



            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Asignación de Personal";
            headerFooter.SubTitulo = "Detalle de Asignación";

            writer.PageEvent = headerFooter;

            document.Open();
            PdfPTable pdfTab = new PdfPTable(1);
            int[] arrHeader = new int[1];
            arrHeader[0] = 1;
            pdfTab.SetWidths(arrHeader);

            Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
            BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

            ////We will have to create separate cells to include image logo and 2 separate strings
            ////Row 1
            PdfPCell pdfCell1 = new PdfPCell(new Phrase("Nombre de Proyecto", tinyFontBold));
            PdfPCell pdfCell2 = new PdfPCell(new Phrase(headerData["Codigo"].ToString(), tinyFont));
            PdfPCell pdfCell3 = new PdfPCell(new Phrase("Descripción de Proyecto", tinyFontBold));
            PdfPCell pdfCell4 = new PdfPCell(new Phrase(headerData["ProyectoDescripcion"].ToString(), tinyFont));
            PdfPCell pdfCell5 = new PdfPCell(new Phrase("Estado", tinyFontBold));
            PdfPCell pdfCell6 = new PdfPCell(new Phrase(headerData["Estado"].ToString(), tinyFont));

            //pdfCell1.BackgroundColor = backgroundColor;
            //pdfCell2.BackgroundColor = backgroundColor;

            pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell4.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell5.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell6.HorizontalAlignment = Element.ALIGN_LEFT;




            pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;


            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 0;




            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell4);
            pdfTab.AddCell(pdfCell5);
            pdfTab.AddCell(pdfCell6);



            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 70;
            pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p = new Paragraph();
            p.IndentationLeft = 30;
            p.SpacingAfter = 10;
            p.Add(pdfTab);

            document.Add(p);

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
                PdfPCell contentCellThird  = new PdfPCell(new Phrase(row["Email"].ToString(), tinyFont));
                PdfPCell contentCellFourth = new PdfPCell(new Phrase(row["Puesto"].ToString(), tinyFont));
                PdfPCell contentCellFifth  = new PdfPCell(new Phrase(row["TipoPuesto"].ToString(), tinyFont));
                
                contentCellFirst .HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellThird.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellFourth.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellFifth.HorizontalAlignment = Element.ALIGN_LEFT;
                
                contentCellFirst .VerticalAlignment = Element.ALIGN_MIDDLE;
                contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentCellThird.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentCellFourth.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentCellFifth.VerticalAlignment = Element.ALIGN_MIDDLE;
                

                contentCellFirst .Border = 0;
                contentCellSecond.Border = 0;
                contentCellThird .Border = 0;
                contentCellFourth.Border = 0;
                contentCellFifth .Border = 0;
                

                pdfTabContent.AddCell(contentCellFirst);
                pdfTabContent.AddCell(contentCellSecond);
                pdfTabContent.AddCell(contentCellThird);
                pdfTabContent.AddCell(contentCellFourth);
                pdfTabContent.AddCell(contentCellFifth);
               
            }

            pdfTabContent.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContent.WidthPercentage = 95;
            pdfTabContent.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph pContent = new Paragraph();
            pContent.IndentationLeft = 30;
            pContent.Add(pdfTabContent);

            document.Add(pContent);

            document.Close();

            return output.ToArray();
        }

        public byte[] GetLogbookReport(HttpContext context)
        {

            int ID_Persona = Int32.Parse(context.Request.Params["ID_Persona"].ToString());
            int ID_Bitacora = Int32.Parse(context.Request.Params["ID_Bitacora"].ToString());
            //Get Report Data
            Dictionary<string, Object> param = new Dictionary<string, Object>();
            param.Add("personIdentity", ID_Persona);
            param.Add("logbookIdentity", ID_Bitacora);

            string query = "spGetLogBook";
            DataSet myDataset = GetDataSet(query, CommandType.StoredProcedure, param);
            DataRow headerData = myDataset.Tables[0].Rows[0];



            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Bitacora Diaria";
            headerFooter.SubTitulo = "Detalle de actividades";

            writer.PageEvent = headerFooter;

            document.Open();
            PdfPTable pdfTab = new PdfPTable(2);
            int[] arrHeader = new int[2];
            arrHeader[0] = 1;
            arrHeader[1] = 1;
            pdfTab.SetWidths(arrHeader);

            Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
            Font tinyFontBoldUnderline = new Font(Font.FontFamily.HELVETICA, 7f, Font.UNDERLINE|Font.BOLD, BaseColor.BLACK);
            BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

            ////We will have to create separate cells to include image logo and 2 separate strings
            ////Row 1
            PdfPCell pdfCell1 = new PdfPCell(new Phrase("Nombre Completo", tinyFontBold));
            PdfPCell pdfCell2 = new PdfPCell(new Phrase(headerData["NombreCompleto"].ToString(), tinyFont));
            PdfPCell pdfCell3 = new PdfPCell(new Phrase("DUI", tinyFontBold));
            PdfPCell pdfCell4 = new PdfPCell(new Phrase(headerData["Dui"].ToString(), tinyFont));
            PdfPCell pdfCell5 = new PdfPCell(new Phrase("Correo Electrónico", tinyFontBold));
            PdfPCell pdfCell6 = new PdfPCell(new Phrase(headerData["Email"].ToString(), tinyFont));
            PdfPCell pdfCell7 = new PdfPCell(new Phrase("Telefono", tinyFontBold));
            PdfPCell pdfCell8 = new PdfPCell(new Phrase(headerData["Telefono"].ToString(), tinyFont));

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




            pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell8.VerticalAlignment = Element.ALIGN_MIDDLE;


            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 0;
            pdfCell7.Border = 0;
            pdfCell8.Border = 0;




            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell4);
            pdfTab.AddCell(pdfCell5);
            pdfTab.AddCell(pdfCell7);
            pdfTab.AddCell(pdfCell6);
            pdfTab.AddCell(pdfCell8);



            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 70;
            pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p = new Paragraph();
            p.IndentationLeft = 30;
            p.SpacingAfter = 10;
            p.Add(pdfTab);

            Paragraph pDescripcion = new Paragraph("Datos Personales:", tinyFontBoldUnderline);
            pDescripcion.IndentationLeft = 30;
            pDescripcion.SpacingAfter = 20;
            document.Add(pDescripcion);
            document.Add(p);

            DateTime LogBookDate = (DateTime)headerData["FechaBitacora"];

            Paragraph pContenido = new Paragraph("Bitacora de Actividades para la fecha:    " + LogBookDate.ToShortDateString(), tinyFontBoldUnderline);
            pContenido.IndentationLeft = 30;
            pContenido.SpacingAfter = 20;
            document.Add(pContenido);

            PdfPTable pdfTabContent = new PdfPTable(2);
            pdfTabContent.DefaultCell.FixedHeight = 100f;
            int[] arrContent = new int[2];
            arrContent[0] = 1;
            arrContent[1] = 1;
            

            pdfTabContent.SetWidths(arrContent);

            PdfPCell pdfCellContent1 = new PdfPCell(new Phrase("Actividad", tinyFontBold));
            PdfPCell pdfCellContent2 = new PdfPCell(new Phrase("Observacion", tinyFontBold));
            


            pdfCellContent1.BackgroundColor = backgroundColor;
            pdfCellContent2.BackgroundColor = backgroundColor;
           


            pdfCellContent1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContent2.HorizontalAlignment = Element.ALIGN_LEFT;
 
            pdfCellContent1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContent2.VerticalAlignment = Element.ALIGN_MIDDLE;
            

            pdfCellContent1.Border = 0;
            pdfCellContent2.Border = 0;
            


            //add all three cells into PdfTable
            pdfTabContent.AddCell(pdfCellContent1);
            pdfTabContent.AddCell(pdfCellContent2);


            foreach (DataRow row in myDataset.Tables[0].Rows)
            {
                PdfPCell contentCellFirst = new PdfPCell(new Phrase(row["Actividad"].ToString(), tinyFont));
                PdfPCell contentCellSecond = new PdfPCell(new Phrase(row["Observaciones"].ToString(), tinyFont));
                

                contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                

                contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                


                contentCellFirst.Border = 0;
                contentCellSecond.Border = 0;
                


                pdfTabContent.AddCell(contentCellFirst);
                pdfTabContent.AddCell(contentCellSecond);
                

            }

            pdfTabContent.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContent.WidthPercentage = 80;
            pdfTabContent.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph pContent = new Paragraph();
            pContent.IndentationLeft = 30;
            pContent.Add(pdfTabContent);

            document.Add(pContent);

            document.Close();

            return output.ToArray();
        }

        public byte[] GetWeeklyPlanReport(HttpContext context)
        {

            int ID_Persona = Int32.Parse(context.Request.Params["ID_Persona"].ToString());
            int ID_PlanSemanal = Int32.Parse(context.Request.Params["ID_PlanSemanal"].ToString());
            //Get Report Data
            Dictionary<string, Object> param = new Dictionary<string, Object>();
            param.Add("personIdentity", ID_Persona);
            param.Add("weeklyplanIdentity", ID_PlanSemanal);

            string query = "spGetWeeklyPlan";
            DataSet myDataset = GetDataSet(query, CommandType.StoredProcedure, param);
            DataRow headerData = myDataset.Tables[0].Rows[0];



            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Plan Semanal";
            headerFooter.SubTitulo = "Detalle de actividades";

            writer.PageEvent = headerFooter;

            document.Open();
            PdfPTable pdfTab = new PdfPTable(2);
            int[] arrHeader = new int[2];
            arrHeader[0] = 1;
            arrHeader[1] = 1;
            pdfTab.SetWidths(arrHeader);

            Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
            Font tinyFontBoldUnderline = new Font(Font.FontFamily.HELVETICA, 7f, Font.UNDERLINE | Font.BOLD, BaseColor.BLACK);
            BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

            ////We will have to create separate cells to include image logo and 2 separate strings
            ////Row 1
            PdfPCell pdfCell1 = new PdfPCell(new Phrase("Nombre Completo", tinyFontBold));
            PdfPCell pdfCell2 = new PdfPCell(new Phrase(headerData["NombreCompleto"].ToString(), tinyFont));
            PdfPCell pdfCell3 = new PdfPCell(new Phrase("DUI", tinyFontBold));
            PdfPCell pdfCell4 = new PdfPCell(new Phrase(headerData["Dui"].ToString(), tinyFont));
            PdfPCell pdfCell5 = new PdfPCell(new Phrase("Correo Electrónico", tinyFontBold));
            PdfPCell pdfCell6 = new PdfPCell(new Phrase(headerData["Email"].ToString(), tinyFont));
            PdfPCell pdfCell7 = new PdfPCell(new Phrase("Telefono", tinyFontBold));
            PdfPCell pdfCell8 = new PdfPCell(new Phrase(headerData["Telefono"].ToString(), tinyFont));

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




            pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell8.VerticalAlignment = Element.ALIGN_MIDDLE;


            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 0;
            pdfCell7.Border = 0;
            pdfCell8.Border = 0;




            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell4);
            pdfTab.AddCell(pdfCell5);
            pdfTab.AddCell(pdfCell7);
            pdfTab.AddCell(pdfCell6);
            pdfTab.AddCell(pdfCell8);



            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 70;
            pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p = new Paragraph();
            p.IndentationLeft = 30;
            p.SpacingAfter = 10;
            p.Add(pdfTab);

            Paragraph pDescripcion = new Paragraph("Datos Personales:", tinyFontBoldUnderline);
            pDescripcion.IndentationLeft = 30;
            pDescripcion.SpacingAfter = 20;
            document.Add(pDescripcion);
            document.Add(p);

            DateTime FechaInicio = (DateTime)headerData["FechaInicio"];
            DateTime FechaFinal = (DateTime)headerData["FechaFinal"];

            Paragraph pContenido = new Paragraph("Plan Semanal desde  " + FechaInicio.ToShortDateString()+ " hasta "+FechaFinal.ToShortDateString(), tinyFontBoldUnderline);
            pContenido.IndentationLeft = 30;
            pContenido.SpacingAfter = 20;
            document.Add(pContenido);

            PdfPTable pdfTabContent = new PdfPTable(3);
            pdfTabContent.DefaultCell.FixedHeight = 100f;
            int[] arrContent = new int[3];
            arrContent[0] = 1;
            arrContent[1] = 1;
            arrContent[2] = 1;


            pdfTabContent.SetWidths(arrContent);

            PdfPCell pdfCellContent1 = new PdfPCell(new Phrase("Actividad", tinyFontBold));
            PdfPCell pdfCellContent2 = new PdfPCell(new Phrase("Observacion", tinyFontBold));
            PdfPCell pdfCellContent3 = new PdfPCell(new Phrase("Recurso", tinyFontBold));



            pdfCellContent1.BackgroundColor = backgroundColor;
            pdfCellContent2.BackgroundColor = backgroundColor;
            pdfCellContent3.BackgroundColor = backgroundColor;



            pdfCellContent1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContent2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCellContent3.HorizontalAlignment = Element.ALIGN_LEFT;

            pdfCellContent1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContent2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCellContent3.VerticalAlignment = Element.ALIGN_MIDDLE;


            pdfCellContent1.Border = 0;
            pdfCellContent2.Border = 0;
            pdfCellContent3.Border = 0;



            
            pdfTabContent.AddCell(pdfCellContent1);
            pdfTabContent.AddCell(pdfCellContent2);
            pdfTabContent.AddCell(pdfCellContent3);


            foreach (DataRow row in myDataset.Tables[0].Rows)
            {
                PdfPCell contentCellFirst = new PdfPCell(new Phrase(row["Actividad"].ToString(), tinyFont));
                PdfPCell contentCellSecond = new PdfPCell(new Phrase(row["Observaciones"].ToString(), tinyFont));
                PdfPCell contentCellThird = new PdfPCell(new Phrase(row["Observaciones"].ToString(), tinyFont));


                contentCellFirst.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellSecond.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCellThird.HorizontalAlignment = Element.ALIGN_LEFT;


                contentCellFirst.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentCellSecond.VerticalAlignment = Element.ALIGN_MIDDLE;
                contentCellThird.VerticalAlignment = Element.ALIGN_MIDDLE;



                contentCellFirst.Border = 0;
                contentCellSecond.Border = 0;
                contentCellThird.Border = 0;



                pdfTabContent.AddCell(contentCellFirst);
                pdfTabContent.AddCell(contentCellSecond);
                pdfTabContent.AddCell(contentCellThird);


            }

            pdfTabContent.TotalWidth = document.PageSize.Width - 80f;
            pdfTabContent.WidthPercentage = 90;
            pdfTabContent.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph pContent = new Paragraph();
            pContent.IndentationLeft = 30;
            pContent.Add(pdfTabContent);

            document.Add(pContent);

            document.Close();

            return output.ToArray();
        }

        public byte[] GetConsolidatedProjectReport(HttpContext context) 
        {
            int ID_Proyecto = Int32.Parse(context.Request.Params["ID_Proyecto"].ToString());

            var proyecto = _proyecto.GetFirst(p => p.ID_Proyecto == ID_Proyecto);

            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                writer.CloseStream = false;
                HeaderFooter headerFooter = new HeaderFooter();

                headerFooter.Titulo = "Consolidado Global de Programa";
                headerFooter.SubTitulo = "Vision Mundial";

                writer.PageEvent = headerFooter;


                #region Informacion de Proyecto

                PdfPTable pdfTab = new PdfPTable(1);
                int[] arrHeader = new int[1];
                arrHeader[0] = 1;
                pdfTab.SetWidths(arrHeader);

                Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
                Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
                Font tinyFontBoldUnderline = new Font(Font.FontFamily.HELVETICA, 7f, Font.UNDERLINE | Font.BOLD, BaseColor.BLACK);
                BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

                ////We will have to create separate cells to include image logo and 2 separate strings
                ////Row 1
                PdfPCell pdfCell1 = new PdfPCell(new Phrase("Nombre de Proyecto", tinyFontBold));
                PdfPCell pdfCell2 = new PdfPCell(new Phrase(proyecto.Codigo, tinyFont));
                PdfPCell pdfCell3 = new PdfPCell(new Phrase("Descripción de Proyecto", tinyFontBold));
                PdfPCell pdfCell4 = new PdfPCell(new Phrase(proyecto.ProyectoDescripcion, tinyFont));
                PdfPCell pdfCell5 = new PdfPCell(new Phrase("Estado", tinyFontBold));
                PdfPCell pdfCell6 = new PdfPCell(new Phrase(proyecto.Estado, tinyFont));

                //pdfCell1.BackgroundColor = backgroundColor;
                //pdfCell2.BackgroundColor = backgroundColor;

                pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell3.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell4.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell5.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCell6.HorizontalAlignment = Element.ALIGN_LEFT;




                pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;


                pdfCell1.Border = 0;
                pdfCell2.Border = 0;
                pdfCell3.Border = 0;
                pdfCell4.Border = 0;
                pdfCell5.Border = 0;
                pdfCell6.Border = 0;




                //add all three cells into PdfTable
                pdfTab.AddCell(pdfCell1);
                pdfTab.AddCell(pdfCell2);
                pdfTab.AddCell(pdfCell3);
                pdfTab.AddCell(pdfCell4);
                pdfTab.AddCell(pdfCell5);
                pdfTab.AddCell(pdfCell6);



                pdfTab.TotalWidth = document.PageSize.Width - 80f;
                pdfTab.WidthPercentage = 70;
                pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
                Paragraph p = new Paragraph();
                p.IndentationLeft = 30;
                p.SpacingAfter = 10;
                p.Add(pdfTab);

                

                Paragraph pDescripcion = new Paragraph("Información del Proyecto:", tinyFontBoldUnderline);
                pDescripcion.IndentationLeft = 30;
                pDescripcion.SpacingAfter = 20;


                #endregion

                #region Primer Grafico
                PdfPTable pdfTabContentAditional = new PdfPTable(2);
                pdfTabContentAditional.DefaultCell.FixedHeight = 100f;
                int[] arrContentAditional = new int[2];
                arrContentAditional[0] = 2;
                arrContentAditional[1] = 1;
                pdfTabContentAditional.SetWidths(arrContentAditional);

                PdfPCell pdfCellContentAditional1 = new PdfPCell(new Phrase("Número de Programas en el Proyecto: ", tinyFontBold));
                PdfPCell pdfCellContentAditional2 = new PdfPCell(new Phrase(proyecto.Programa.Count().ToString(), tinyFontBold));

                pdfCellContentAditional1.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional2.HorizontalAlignment = Element.ALIGN_LEFT;
               
                pdfCellContentAditional1.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional2.VerticalAlignment = Element.ALIGN_MIDDLE;
                
                pdfCellContentAditional1.Border = 0;
                pdfCellContentAditional2.Border = 0;
                
                pdfCellContentAditional1.FixedHeight = 20f;
                pdfCellContentAditional2.FixedHeight = 20f;
               
                pdfTabContentAditional.AddCell(pdfCellContentAditional1);
                pdfTabContentAditional.AddCell(pdfCellContentAditional2);
                
                pdfTabContentAditional.TotalWidth = document.PageSize.Width - 80f;
                pdfTabContentAditional.WidthPercentage = 60;
                pdfTabContentAditional.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTabContentAditional.SpacingAfter = 20;
                Paragraph pContentProgram = new Paragraph();
                pContentProgram.IndentationLeft = 30;
                pContentProgram.Add(pdfTabContentAditional);

                //#region Charts

                ChartingHelper ChartProgramsByType = new ChartingHelper();

                Dictionary<string, object> Parameters = new Dictionary<string, object>();
                Parameters.Add("width", 300);
                Parameters.Add("height", 400);
                Parameters.Add("chartTitle", "Programas por Mecanismo");
                Parameters.Add("xTitle", "Tipo de Programa");
                Parameters.Add("yTitle", "Cantidad");
                Parameters.Add("chartType", "Column");
                Dictionary<string, object> Data = new Dictionary<string, object>();

                List<string> programTypesId = _context1.Database.SqlQuery<string>("SELECT CONVERT(varchar(10), ID_TipoPrograma) +'-'+ TipoPrograma as result FROM TipoPrograma").ToList();

                foreach (var tipoPrograma in programTypesId)
                {
                    string[] result = tipoPrograma.Split('-');
                    int ID_TipoPrograma = Convert.ToInt32(result[0]);
                    string tipoPrograma1 = result[1];

                    Data.Add(tipoPrograma1, proyecto.Programa.Count(programa => programa.ID_TipoPrograma == ID_TipoPrograma));
                    
                }

                ChartProgramsByType.ChartParameters = Parameters;
                ChartProgramsByType.Data = Data;
                byte[] imageBytes = ChartProgramsByType.GetChart();
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
                image.ScalePercent(0.8f * 100);

                Paragraph pContentImage1 = new Paragraph(new Chunk(image, 0, 0, true));
                pContentImage1.IndentationLeft = 100;
                pContentImage1.IndentationRight = 100;
                Paragraph pImageHeader1 = new Paragraph("Gráfico de Programas por Mecanismo:", tinyFontBoldUnderline);
                pImageHeader1.IndentationLeft = 30;
                pImageHeader1.SpacingAfter = 20;

                #endregion

                #region Segundo Gráfico

                PdfPTable pdfTabContentAditional2 = new PdfPTable(2);
                pdfTabContentAditional2.DefaultCell.FixedHeight = 100f;
                int[] arrContentAditional2 = new int[2];
                arrContentAditional2[0] = 2;
                arrContentAditional2[1] = 1;
                pdfTabContentAditional2.SetWidths(arrContentAditional);

                int total = 0;

                proyecto.Programa.ToList().ForEach(prog=> total += prog.Beneficiario.Count());

                PdfPCell pdfCellContentAditional11 = new PdfPCell(new Phrase("Total de beneficiarios en proyecto: ", tinyFontBold));
                PdfPCell pdfCellContentAditional21 = new PdfPCell(new Phrase( total.ToString(), tinyFontBold));

                pdfCellContentAditional11.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfCellContentAditional21.HorizontalAlignment = Element.ALIGN_LEFT;

                pdfCellContentAditional11.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCellContentAditional21.VerticalAlignment = Element.ALIGN_MIDDLE;

                pdfCellContentAditional11.Border = 0;
                pdfCellContentAditional21.Border = 0;

                pdfCellContentAditional11.FixedHeight = 20f;
                pdfCellContentAditional21.FixedHeight = 20f;

                pdfTabContentAditional2.AddCell(pdfCellContentAditional11);
                pdfTabContentAditional2.AddCell(pdfCellContentAditional21);

                pdfTabContentAditional2.TotalWidth = document.PageSize.Width - 80f;
                pdfTabContentAditional2.WidthPercentage = 60;
                pdfTabContentAditional2.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTabContentAditional2.SpacingAfter = 20;
                Paragraph pContentCommunity = new Paragraph();
                pContentCommunity.IndentationLeft = 30;
                pContentCommunity.Add(pdfTabContentAditional2);

                Paragraph pHeaderCommunity = new Paragraph("Información de proyecto:", tinyFontBoldUnderline);
                pHeaderCommunity.IndentationLeft = 30;
                pHeaderCommunity.SpacingAfter = 20;

                //Chart
                ChartingHelper ChartGenderCommunity = new ChartingHelper();

                Dictionary<string, object> ParametersChartGenderCommunity = new Dictionary<string, object>();
                ParametersChartGenderCommunity.Add("width", 300);
                ParametersChartGenderCommunity.Add("height", 400);
                ParametersChartGenderCommunity.Add("chartTitle", "Participación por Género");
                ParametersChartGenderCommunity.Add("xTitle", "Género");
                ParametersChartGenderCommunity.Add("yTitle", "Cantidad");
                ParametersChartGenderCommunity.Add("chartType", "Column");
                Dictionary<string, object> DataChartGenderCommunity = new Dictionary<string, object>();

                //loading data

                int male = 0;
                int female = 0;

                proyecto.Programa.ToList().ForEach((pro)=>
                {
                    male += pro.Beneficiario.Count(ben=> ben.Sexo=="M");
                    female += pro.Beneficiario.Count(ben => ben.Sexo == "F");
                } );
                DataChartGenderCommunity.Add("Masculino",male);
                DataChartGenderCommunity.Add("Femenino", female);

                ChartGenderCommunity.ChartParameters = ParametersChartGenderCommunity;
                ChartGenderCommunity.Data = DataChartGenderCommunity;
                byte[] imageBytesGender = ChartGenderCommunity.GetChart();
                iTextSharp.text.Image imageGender = iTextSharp.text.Image.GetInstance(imageBytesGender);
                imageGender.ScalePercent(0.8f * 100);

                Paragraph pContentImageGender = new Paragraph(new Chunk(imageGender, 0, 0, true));
                pContentImageGender.IndentationLeft = 100;
                pContentImageGender.IndentationRight = 100;
                Paragraph pImageHeaderCommunity = new Paragraph("Gráfico de Participacion por Género en Proyecto:", tinyFontBoldUnderline);
                pImageHeaderCommunity.IndentationLeft = 30;
                pImageHeaderCommunity.SpacingAfter = 20;


               
                #endregion 



                #region Tercer Gráfico

                ChartingHelper ChartNCR = new ChartingHelper();

                Dictionary<string, object> ParametersChartNCR = new Dictionary<string, object>();
                ParametersChartNCR.Add("width", 300);
                ParametersChartNCR.Add("height", 400);
                ParametersChartNCR.Add("chartTitle", "Participantes Patrocinados");
                ParametersChartNCR.Add("xTitle", "Patrocinio");
                ParametersChartNCR.Add("yTitle", "Cantidad");
                ParametersChartNCR.Add("chartType", "Column");
                Dictionary<string, object> DataChartNCR = new Dictionary<string, object>();

                //loading data

                int NCR = 0;
                int RC = 0;

                proyecto.Programa.ToList().ForEach((pro) =>
                {
                    NCR += pro.Beneficiario.Count(ben => string.IsNullOrEmpty(ben.Codigo));
                    RC += pro.Beneficiario.Count(ben => !string.IsNullOrEmpty(ben.Codigo));
                });
                DataChartNCR.Add("No Patrocinados", NCR);
                DataChartNCR.Add("Patrocinados", RC);

                ChartNCR.ChartParameters = ParametersChartNCR;
                ChartNCR.Data = DataChartNCR;
                byte[] imageBytesNCR = ChartNCR.GetChart();
                iTextSharp.text.Image imageNCR = iTextSharp.text.Image.GetInstance(imageBytesNCR);
                imageNCR.ScalePercent(0.8f * 100);

                Paragraph pContentImageNCR = new Paragraph(new Chunk(imageNCR, 0, 0, true));
                pContentImageNCR.IndentationLeft = 100;
                pContentImageNCR.IndentationRight = 100;
                Paragraph pImageHeaderNCR = new Paragraph("Gráfico de Beneficiarios Patrocinados:", tinyFontBoldUnderline);
                pImageHeaderNCR.IndentationLeft = 30;
                pImageHeaderNCR.SpacingAfter = 20;

                #endregion

                #region Cuarto Gráfico

                ChartingHelper chartAges = new ChartingHelper();
                Dictionary<string, object> ParametersAges = new Dictionary<string, object>();
                ParametersAges.Add("width", 450);
                ParametersAges.Add("height", 400);
                ParametersAges.Add("chartTitle", "Participación por Edades");
                ParametersAges.Add("xTitle", "Rangos");
                ParametersAges.Add("yTitle", "Cantidad");
                ParametersAges.Add("chartType", "Bar");

                Dictionary<string, object> DataAges = new Dictionary<string, object>();
                List<int> Ranges = chartAges.GetAgeRanges("other");
                int menor1 = 0, menor2 = 0, menor3 = 0;

                proyecto.Programa.ToList().ForEach(
                    (pro) => 
                    {
                        foreach (var ben in pro.Beneficiario)
                        {
                            string[] personAge = ben.Edad.Split('|');
                            int years = Convert.ToInt32(personAge[0]);
                            if (years <= Ranges[0] && years >= Ranges[1])
                            {
                                menor1++;
                            }
                            else if (years <= Ranges[1] && years >= Ranges[2])
                            {
                                menor2++;
                            }
                            else if (years <= Ranges[2])
                            {
                                menor3++;
                            }
                        }

                    }

                    );

                DataAges.Add("Menor a " + Ranges[0], menor1);
                DataAges.Add("Menor a " + Ranges[1], menor2);
                DataAges.Add("Menor a " + Ranges[2], menor3);


                chartAges.ChartParameters = ParametersAges;
                chartAges.Data = DataAges;

                byte[] imageBytesAges = chartAges.GetChart();
                iTextSharp.text.Image imageAges = iTextSharp.text.Image.GetInstance(imageBytesAges);
                imageAges.ScalePercent(0.8f * 100);

                Paragraph pContentImageAge = new Paragraph(new Chunk(imageAges, 0, 0, true));
                pContentImageAge.IndentationLeft = 100;
                pContentImageAge.IndentationRight = 100;
                Paragraph pImageHeaderAges = new Paragraph("Gráfico de Beneficiarios Por Edades", tinyFontBoldUnderline);
                pImageHeaderAges.IndentationLeft = 30;
                pImageHeaderAges.SpacingAfter = 20;
                #endregion


                Paragraph pDescripcionProgramas= new Paragraph("Información del Programas Implementados:", tinyFontBoldUnderline);
                pDescripcionProgramas.IndentationLeft = 30;
                pDescripcionProgramas.SpacingAfter = 20;

              
                document.Open();
                document.Add(pDescripcion);
                document.Add(p);
                document.Add(pDescripcionProgramas);
                document.Add(pContentProgram);
                document.Add(pImageHeader1);
                document.Add(pContentImage1);
                document.Add(pHeaderCommunity);
                document.Add(pContentCommunity);
                document.Add(pImageHeaderCommunity);
                document.Add(pContentImageGender);
                document.Add(pImageHeaderNCR);
                document.Add(pContentImageNCR);
                document.Add(pImageHeaderAges);
                document.Add(pContentImageAge);



                document.Close();


                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();



                return bytes;

            }
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