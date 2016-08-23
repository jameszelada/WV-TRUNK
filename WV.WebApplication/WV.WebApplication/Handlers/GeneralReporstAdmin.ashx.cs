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
using System.Web.Script.Serialization;
using System.Web.SessionState;
using WV.WebApplication.Reports;
using WV.WebApplication.Utils;

namespace WV.WebApplication.Handlers
{
    /// <summary>
    /// Summary description for GeneralReporstAdmin
    /// </summary>
    public class GeneralReporstAdmin : DataAccess, IHttpHandler, IRequiresSessionState
    {
        string connection = ConfigurationManager.ConnectionStrings["VISIONMUNDIALEntities"].ConnectionString;
        string MethodName = string.Empty;
        string CallBackMethodName = string.Empty;
        object Parameter = string.Empty;
        IAWContext _context;
        IDataRepository<Usuario> _usuario;
        IDataRepository<Rol> _rol;
        public void ProcessRequest(HttpContext context)
        {
            InitializeObjects();
            MethodName = context.Request.Params["method"];
            switch (MethodName.ToLower())
            {
                case "getallroles":
                    context.Response.Write(GetAllRoles());
                    break;
                case "getallusers":
                    context.Response.Write(GetAllUsers());
                    break;
                case "getrolesreport":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Admin-Roles-{0}.pdf", DateTime.Now.ToShortDateString()));
                    context.Response.BinaryWrite(GetAllRolesReport());
                    break;
                case "getuserinfo":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Info-Usuario-{0}.pdf", DateTime.Now.ToShortDateString()));
                    context.Response.BinaryWrite(GetUserInfoReport(context));
                    break;
                case "getroleinfo":
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Info-Rol-{0}.pdf", DateTime.Now.ToShortDateString()));
                    context.Response.BinaryWrite(GetRoleInfoReport(context));
                    break;
            }
        }

        private void InitializeObjects()
        {

            _context = new AWContext(connection);
            _usuario = new DataRepository<IAWContext, Usuario>(_context);
            _rol = new DataRepository<IAWContext, Rol>(_context);
        }

        public string GetAllRoles()
        {
            string options = "";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var roles = _rol.GetAll().Select((x, index) => new
                {
                    index,
                    x.ID_Rol,
                    x.Rol1
                });

                foreach (var rol in roles)
                {
                    int index = rol.index + 1;
                    options += "<option data-id-role='" + rol.ID_Rol + "'>" + rol.Rol1 + "</option>";
                }



                if (roles.ToList().Count > 0)
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

        public string GetAllUsers()
        {
            string options = "";

            JsonResponse response = new JsonResponse();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var usuarios = _usuario.GetAll().Select((x, index) => new
                {
                    index,
                    x.ID_Usuario,
                    x.Nombre,
                    x.NombreUsuario
                });

                foreach (var usuario in usuarios)
                {
                    int index = usuario.index + 1;
                    options += "<option data-id-user='" + usuario.ID_Usuario + "'>" + usuario.NombreUsuario + "</option>";
                }



                if (usuarios.ToList().Count > 0)
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

        public byte[] GetAllRolesReport()
        {

            //Get Report Data
            // Empty Dictionary
            Dictionary<string, Object> param = new Dictionary<string, Object>();

            string query = "spGetSystemRoles";
            DataSet myDataset = GetDataSet(query, CommandType.StoredProcedure, param);


            
            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Roles de Sistema";
            headerFooter.SubTitulo = "Listado de Roles registrados";

            writer.PageEvent = headerFooter;

            document.Open();
            PdfPTable pdfTab = new PdfPTable(2);
            int[] arr = new int[2];
            arr[0]= 1;
            arr[1]=1;
            pdfTab.SetWidths(arr);
            
            Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
            BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
            PdfPCell pdfCell1 = new PdfPCell(new Phrase("Nombre del Rol", tinyFontBold));
            PdfPCell pdfCell2 = new PdfPCell(new Phrase("Descripción", tinyFontBold));
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
                PdfPCell contentCellFirst = new PdfPCell(new Phrase(row["Rol"].ToString(), tinyFont));
                PdfPCell contentCellSecond = new PdfPCell(new Phrase(row["Descripcion"].ToString(), tinyFont));
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
            pdfTab.WidthPercentage = 95;
            pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p = new Paragraph();
            p.IndentationLeft = 30;
            p.Add(pdfTab);


            document.Add(p);
            
            document.Close();

            return output.ToArray();
        }

        public byte[] GetUserInfoReport(HttpContext context)
        {

            int ID_Usuario = Int32.Parse(context.Request.Params["ID_Usuario"].ToString());
            //Get Report Data
            Dictionary<string, Object> param = new Dictionary<string, Object>();
            param.Add("userIdentity",ID_Usuario);

            string query = "spGetUserInformation";
            DataSet myDataset = GetDataSet(query, CommandType.StoredProcedure, param);
            DataRow headerData = myDataset.Tables[0].Rows[0];



            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de usuario de Sistema";
            headerFooter.SubTitulo = "Detalle de opciones";

            writer.PageEvent = headerFooter;

            document.Open();
            PdfPTable pdfTab = new PdfPTable(3);
            int[] arrHeader = new int[3];
            arrHeader[0] = 1;
            arrHeader[1] = 1;
            arrHeader[2] = 1;
            pdfTab.SetWidths(arrHeader);

            Font tinyFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, BaseColor.BLACK);
            Font tinyFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, BaseColor.BLACK);
            BaseColor backgroundColor = WebColors.GetRGBColor("#DCDDDE");

            ////We will have to create separate cells to include image logo and 2 separate strings
            ////Row 1
            PdfPCell pdfCell1 = new PdfPCell(new Phrase("Nombre de Usuario", tinyFontBold));
            PdfPCell pdfCell2 = new PdfPCell(new Phrase("Nombre", tinyFontBold));
            PdfPCell pdfCell3 = new PdfPCell(new Phrase("Apellido", tinyFontBold));
            PdfPCell pdfCell4 = new PdfPCell(new Phrase(headerData["NombreUsuario"].ToString(), tinyFont));
            PdfPCell pdfCell5 = new PdfPCell(new Phrase(headerData["Nombre"].ToString(), tinyFont));
            PdfPCell pdfCell6 = new PdfPCell(new Phrase(headerData["Apellido"].ToString(), tinyFont));
            PdfPCell pdfCell7 = new PdfPCell(new Phrase("Correo Electronico", tinyFontBold));
            PdfPCell pdfCell8 = new PdfPCell(new Phrase("Rol Asignado", tinyFontBold));
            PdfPCell pdfCell9 = new PdfPCell(new Phrase(string.Empty, tinyFontBold));
            PdfPCell pdfCell10 = new PdfPCell(new Phrase(headerData["Email"].ToString(), tinyFont));
            PdfPCell pdfCell11 = new PdfPCell(new Phrase(headerData["Rol"].ToString(), tinyFont));
            PdfPCell pdfCell12 = new PdfPCell(new Phrase(string.Empty, tinyFont));
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



            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell4);
            pdfTab.AddCell(pdfCell5);
            pdfTab.AddCell(pdfCell6);
            pdfTab.AddCell(pdfCell7);
            pdfTab.AddCell(pdfCell8);
            pdfTab.AddCell(pdfCell9);
            pdfTab.AddCell(pdfCell10);
            pdfTab.AddCell(pdfCell11);
            pdfTab.AddCell(pdfCell12);


            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 95;
            pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p = new Paragraph();
            p.IndentationLeft = 30;
            p.SpacingAfter = 20;
            p.Add(pdfTab);

            document.Add(p);

            PdfPTable pdfTabContent = new PdfPTable(2);
            int[] arrContent = new int[2];
            arrContent[0] = 1;
            arrContent[1] = 1;
            pdfTabContent.SetWidths(arrContent);

            PdfPCell pdfCellContent1 = new PdfPCell(new Phrase("Nombre de Opcion", tinyFontBold));
            PdfPCell pdfCellContent2 = new PdfPCell(new Phrase("Descripcion", tinyFontBold));

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
                PdfPCell contentCellFirst = new PdfPCell(new Phrase(row["Recurso"].ToString(), tinyFont));
                PdfPCell contentCellSecond = new PdfPCell(new Phrase(row["Pagina"].ToString(), tinyFont));
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
            pdfTabContent.WidthPercentage = 95;
            pdfTabContent.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph pContent = new Paragraph();
            pContent.IndentationLeft = 30;
            pContent.Add(pdfTabContent);

            document.Add(pContent);

            document.Close();

            return output.ToArray();
        }

        public byte[] GetRoleInfoReport(HttpContext context)
        {

            int ID_Rol = Int32.Parse(context.Request.Params["ID_Rol"].ToString());
            //Get Report Data
            Dictionary<string, Object> param = new Dictionary<string, Object>();
            param.Add("roleIdentity", ID_Rol);

            string query = "spGetRoleInformation";
            DataSet myDataset = GetDataSet(query, CommandType.StoredProcedure, param);
            DataRow headerData = myDataset.Tables[0].Rows[0];



            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            HeaderFooter headerFooter = new HeaderFooter();

            headerFooter.Titulo = "Reporte de Rol de Sistema";
            headerFooter.SubTitulo = "Opciones por Rol";

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
            PdfPCell pdfCell1 = new PdfPCell(new Phrase("Nombre de Rol", tinyFontBold));
            PdfPCell pdfCell2 = new PdfPCell(new Phrase(headerData["Rol"].ToString(), tinyFont));
            PdfPCell pdfCell3 = new PdfPCell(new Phrase("Descripción", tinyFontBold));
            PdfPCell pdfCell4 = new PdfPCell(new Phrase(headerData["Descripcion"].ToString(), tinyFont));
            
            //pdfCell1.BackgroundColor = backgroundColor;
            //pdfCell2.BackgroundColor = backgroundColor;

            pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell4.HorizontalAlignment = Element.ALIGN_LEFT;
            



            pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            

            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            



            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell4);
            


            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 95;
            pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p = new Paragraph();
            p.IndentationLeft = 30;
            p.SpacingAfter = 20;
            p.Add(pdfTab);

            document.Add(p);

            PdfPTable pdfTabContent = new PdfPTable(2);
            int[] arrContent = new int[2];
            arrContent[0] = 1;
            arrContent[1] = 1;
            pdfTabContent.SetWidths(arrContent);

            PdfPCell pdfCellContent1 = new PdfPCell(new Phrase("Nombre de Opcion", tinyFontBold));
            PdfPCell pdfCellContent2 = new PdfPCell(new Phrase("Descripcion", tinyFontBold));

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
                PdfPCell contentCellFirst = new PdfPCell(new Phrase(row["Recurso"].ToString(), tinyFont));
                PdfPCell contentCellSecond = new PdfPCell(new Phrase(row["Pagina"].ToString(), tinyFont));
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
            pdfTabContent.WidthPercentage = 95;
            pdfTabContent.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph pContent = new Paragraph();
            pContent.IndentationLeft = 30;
            pContent.Add(pdfTabContent);

            document.Add(pContent);

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