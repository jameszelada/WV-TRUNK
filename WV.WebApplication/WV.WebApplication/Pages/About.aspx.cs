using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using WV.WebApplication.Reports;
using Repository;

namespace WV.WebApplication.Pages
{
    public partial class About : PageBase
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //Dictionary<string,Object> param= new Dictionary<string,Object>();
            //param.Add("userName", "Jaime");
            //param.Add("roleName", "ADMIN");
            
            //string query = "dbo.spGetUserInfo;";
            //DataSet myDataset = GetDataSet(query,CommandType.StoredProcedure,param);
            
            //var X=0;

           

            //if (ValidateSession())
            //{
            //    AddUserTag();
            //    ValidateOptions();
            //    if (!hasPermissions(pagename.InnerHtml))
            //    {
            //        Context.Response.Redirect("Unauthorized");
            //    }
            //}
           
        }

        


        protected void Button1_Click(object sender, EventArgs e)
        {
            // Create a Document object
            var document = new Document(PageSize.A4, 10f, 10f, 110f, 50f);
            //var dc = new Document()

            // Create a new PdfWrite object, writing the output to a MemoryStream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            writer.PageEvent = new HeaderFooter();

            document.Open();
            PdfPTable pdfTab = new PdfPTable(3);
            Font tyniFont = new Font(Font.FontFamily.HELVETICA, 7f, Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            Font tyniFontBold = new Font(Font.FontFamily.HELVETICA, 7f, Font.BOLD, iTextSharp.text.BaseColor.BLACK); 

            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
            PdfPCell pdfCell1 = new PdfPCell(new Phrase("Nombre:",tyniFontBold));
            PdfPCell pdfCell2 = new PdfPCell(new Phrase("Jaime Alejandro Zelada Ramirez", tyniFont));
            PdfPCell pdfCell3 = new PdfPCell();


            PdfPCell pdfCell4 = new PdfPCell();
            //Row 3


            PdfPCell pdfCell5 = new PdfPCell(new Phrase("Ocupacion:", tyniFontBold));
            PdfPCell pdfCell6 = new PdfPCell(new Phrase("Ingeniero de Software", tyniFont));
            PdfPCell pdfCell7 = new PdfPCell();


            //set the alignment of all three cells and set border to 0
            pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell4.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell5.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell6.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell7.HorizontalAlignment = Element.ALIGN_LEFT;

            pdfCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;

            pdfCell4.Colspan = 3;

            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 0;
            pdfCell7.Border = 0;


            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell4);
            pdfTab.AddCell(pdfCell5);
            pdfTab.AddCell(pdfCell6);
            pdfTab.AddCell(pdfCell7);

            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 30;
            pdfTab.HorizontalAlignment = Element.ALIGN_LEFT;
            Paragraph p = new Paragraph();
            p.IndentationLeft = 30;
            p.Add(pdfTab);


            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            //pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
            document.Add(p);
            //document.Close();
            //// Open the Document for writing
            //document.Open();

            PdfPTable table = new PdfPTable(5);
            table.HeaderRows = 1;
            table.SplitRows = false;
            table.Complete = false;

            for (int i = 0; i < 5; i++) { table.AddCell("Header " + i); }

            for (int i = 0; i < 500; i++)
            {
                if (i % 5 == 0)
                {
                    document.Add(table);
                }
                table.AddCell("Test " + i);
            }

            table.Complete = true;
            document.Add(table);
            document.Close();
            

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", "Test Text"));
            Response.BinaryWrite(output.ToArray());
        }
    }
}