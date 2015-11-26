using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Diagnostics;
//using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using Word = Microsoft.Office.Interop.Word;

namespace GestionPruebas
{
    public partial class Reportes : System.Web.UI.Page
    {
        private ControladoraEjecucion controlEjecu;
        private EntidadEjecucion ejecucion;
        private EntidadNoConformidad[] conformidades;

        protected void Page_Load(object sender, EventArgs e)
        {
            string idEje = "";
            if (Request.QueryString["idEje"] != null)
            {
                idEje = Request.QueryString["idEje"];
            }

            //Si ya está logueado:
            if (Request.IsAuthenticated)
            {

                //Inicializamos controladora
                controlEjecu = new ControladoraEjecucion();
                if (idEje != "")
                {
                    hacerResumen(idEje);
                }

                //Si es la primera vez que se carga la página:
                if (!this.IsPostBack)
                {

                }
            }
            else
            {//En caso de que no esté logueado, redirija a login
                Response.Redirect("Login.aspx");
            }
        }

        /*
         * Desc: Se encarga de realizar las operaciones de creacion de reporte
         * Requiere: n/a
         * Retorna. n/a
         */
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            creaReporte();
        }

        protected void volverEj(object sender, EventArgs e)
        {
            Response.Redirect("Ejecucion.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }


        protected int parseInt(string valor)
        {
            int parsedInt;
            string trimmed = valor;
            bool parsed = int.TryParse(trimmed.Replace("-", ""), out parsedInt);
            if (!parsed)
            {
                parsedInt = -1;
            }
            return parsedInt;
        }

        /**
         * Descripcion: se revisa el perfil de la persona que inició sesión en el sistema.
         * Si es miembro no se le muestra el grid, ni los botones de eliminar e insertar,
         * se llenan los campos con su información personal.
         * Si es Administrador se muestra todo, es decir, no se cambia nada.
         * Recibe: un string @usuario que es el nombre de usuario de la persona en el sistema.
         *         un booleano @esInicio que determina si se llama a la función desde la primera carga o desde alguna otra parte de la interfaz
         * Devuelve verdadero si es administrador o falso si es un miembro de equipo.
         */
        protected bool revisarPerfil(string usuario, bool esInicio)
        {
            /*
            //Obtiene el perfil de la controladora
            string perfilS = controlEjecu.getPerfil(usuario);
            //Si es miembro de equipo:
            if (perfilS.Equals("M"))
            {
                //Si el método se llama desde la primera carga de la página, controle lo necesario
                if (esInicio)
                {

                }
                //No es administrador
                return false;
            }
            else
            {
                //Es administrador
                return true;
            }
            */
            return true;
        }

        protected void hacerResumen(string idEje)
        {
            int id = parseInt(idEje);
            Object[] resumen = controlEjecu.hacerResumen(id);
            TextProyecto.Value = resumen[0].ToString();
            nivelPrueba.Value = resumen[1].ToString();
            propositoDiseno.Value = resumen[2].ToString();
            TextReq.Value = controlEjecu.consultarReq(id);
            ejecucion = controlEjecu.consultarEjecucion(id);
            conformidades = controlEjecu.consultarNoConformidades(id);
        }

        protected void creaReporte()
        {
            if (true || ejecucion != null && conformidades != null)
            {
                if (formato.Value == "pdf")
                {
                    //Usa itextsharp para crear reporte
                    if (tipo.Value == "calidad")
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            var doc = new Document();
                            PdfWriter.GetInstance(doc, stream);
                            doc.Open();
                            Paragraph tit = new Paragraph("Reporte de Calidad de Proyecto"); tit.Alignment = Element.ALIGN_CENTER; tit.Font.Size = 18;
                            doc.Add(tit);
                            doc.Add(new Paragraph(" "));
                            doc.Add(new Paragraph("Nombre de proyecto: " + "[AQUI NOMBRE]"));
                            doc.Add(new Paragraph("Fecha de generación de reporte: " + "[AQUI FECHA]"));
                            doc.Add(new Paragraph("Fecha de ejecución: " + "[AQUI FECHA]"));
                            doc.Add(new Paragraph("Responsable de diseño: " + "[AQUI RESPONSABLE]"));
                            PdfPTable table = new PdfPTable(4);
                            table.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.TotalWidth = 340f;
                            table.LockedWidth = true;
                            float[] widths = new float[] { 80f, 100f, 80f, 80f };
                            table.SetWidths(widths);
                            PdfPCell cell = new PdfPCell(new Phrase("Porcentajes por cada estado de ejecución"));
                            cell.Colspan = 4; cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = 2;
                            cell.Border = 0;
                            cell.PaddingTop = 30f;
                            table.AddCell(cell);
                            PdfPCell cellH1 = new PdfPCell(new Phrase("Estado")); cellH1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH1.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH1);
                            PdfPCell cellH4 = new PdfPCell(new Phrase("Casos")); cellH4.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH4.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH4);
                            PdfPCell cellH2 = new PdfPCell(new Phrase("Cantidad")); cellH2.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH2.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH2);
                            PdfPCell cellH3 = new PdfPCell(new Phrase("Porcentaje")); cellH3.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH3.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH3);
                            //Estos se llenan en un for y se van insertando
                            PdfPCell celle1 = new PdfPCell(new Phrase("Aceptado")); celle1.HorizontalAlignment = Element.ALIGN_CENTER; celle1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            table.AddCell(celle1);
                            PdfPCell cellc1 = new PdfPCell(new Phrase("Caso 1\nCaso 2\nCaso 3\nCaso n")); cellc1.HorizontalAlignment = Element.ALIGN_CENTER; cellc1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            table.AddCell(cellc1);
                            PdfPCell celln1 = new PdfPCell(new Phrase("n")); celln1.HorizontalAlignment = Element.ALIGN_CENTER; celln1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            table.AddCell(celln1);
                            PdfPCell cellp1 = new PdfPCell(new Phrase("p%")); cellp1.HorizontalAlignment = Element.ALIGN_CENTER; cellp1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            table.AddCell(cellp1);

                            doc.Add(table);
                            doc.Close();
                            descargaReporte(stream, "reporteCalidad.pdf");
                        }
                    }
                    else if (tipo.Value == "estado")
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            var doc = new Document();
                            PdfWriter.GetInstance(doc, stream);
                            doc.Open();
                            Paragraph tit = new Paragraph("Reporte de Estado de Proyecto"); tit.Alignment = Element.ALIGN_CENTER; tit.Font.Size = 18;
                            doc.Add(tit);
                            doc.Add(new Paragraph(" "));
                            doc.Add(new Paragraph("Nombre de proyecto: " + "[AQUI NOMBRE]"));
                            doc.Add(new Paragraph("Fecha de generación de reporte: " + "[AQUI FECHA]"));
                            doc.Add(new Paragraph("Fecha de ejecución: " + "[AQUI FECHA]"));
                            doc.Add(new Paragraph("Responsable de diseño: " + "[AQUI RESPONSABLE]"));
                            PdfPTable table = new PdfPTable(5);
                            table.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.TotalWidth = 520f;  
                            table.LockedWidth = true;
                            float[] widths = new float[] { 50f, 50f, 140f, 140f, 140f };
                            table.SetWidths(widths);
                            PdfPCell cell = new PdfPCell(new Phrase("Resumen de ejecución de casos"));
                            cell.Colspan = 6; cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = 2;
                            cell.Border = 0;
                            cell.PaddingTop = 30f;
                            table.AddCell(cell);
                            PdfPCell cellH1 = new PdfPCell(new Phrase("Caso")); cellH1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH1.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH1);
                            PdfPCell cellH2 = new PdfPCell(new Phrase("Tipo")); cellH2.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH2.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH2);
                            PdfPCell cellH3 = new PdfPCell(new Phrase("Descripción")); cellH3.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH3.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH3);
                            PdfPCell cellH4 = new PdfPCell(new Phrase("Justificación")); cellH4.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH4.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH4);
                            PdfPCell cellH5 = new PdfPCell(new Phrase("Estado")); cellH5.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH5.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH5);
                            //Estos se llenan en un for y se van insertando
                            table.AddCell("Caso N");
                            table.AddCell("Tipo 1");
                            table.AddCell("Esta es la descripción del mae");
                            table.AddCell("Esta es la justificación del mae");
                            table.AddCell("Este es el estado del mae");
                            PdfPCell cellH6 = new PdfPCell(new Phrase("Imagen")); cellH6.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH6.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH6);
                            PdfPCell cellIm = new PdfPCell(new Phrase("Esta es la imagen del mae")); cellIm.HorizontalAlignment = Element.ALIGN_CENTER; cellIm.Colspan = 4;
                            table.AddCell(cellIm);

                            doc.Add(table);
                            doc.Close();
                            descargaReporte(stream, "reporteEstado.pdf");
                        }
                    }
                    else if (tipo.Value == "resumen")
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            var doc = new Document();
                            PdfWriter.GetInstance(doc, stream);
                            doc.Open();
                            Paragraph tit = new Paragraph("Reporte de Resumen de Proyecto"); tit.Alignment = Element.ALIGN_CENTER; tit.Font.Size = 18;
                            doc.Add(tit);
                            doc.Add(new Paragraph(" "));
                            doc.Add(new Paragraph("Nombre de proyecto: " + "[AQUI NOMBRE]"));
                            doc.Add(new Paragraph("Fecha de generación de reporte: " + "[AQUI FECHA]"));
                            doc.Add(new Paragraph("Fecha de ejecución: " + "[AQUI FECHA]"));
                            doc.Add(new Paragraph("Responsable de diseño: " + "[AQUI RESPONSABLE]"));
                            //Tabla porcentajes
                            PdfPTable table = new PdfPTable(4);
                            table.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.TotalWidth = 340f;
                            table.LockedWidth = true;
                            float[] widths = new float[] { 80f, 100f, 80f, 80f };
                            table.SetWidths(widths);
                            PdfPCell cell = new PdfPCell(new Phrase("Porcentajes por cada estado de ejecución"));
                            cell.Colspan = 4; cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = 2;
                            cell.Border = 0;
                            cell.PaddingTop = 30f;
                            table.AddCell(cell);
                            PdfPCell cellH1 = new PdfPCell(new Phrase("Estado")); cellH1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH1.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH1);
                            PdfPCell cellH4 = new PdfPCell(new Phrase("Casos")); cellH4.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH4.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH4);
                            PdfPCell cellH2 = new PdfPCell(new Phrase("Cantidad")); cellH2.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH2.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH2);
                            PdfPCell cellH3 = new PdfPCell(new Phrase("Porcentaje")); cellH3.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellH3.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table.AddCell(cellH3);
                            //Estos se llenan en un for y se van insertando
                            PdfPCell celle1 = new PdfPCell(new Phrase("Aceptado")); celle1.HorizontalAlignment = Element.ALIGN_CENTER; celle1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            table.AddCell(celle1);
                            PdfPCell cellc1 = new PdfPCell(new Phrase("Caso 1\nCaso 2\nCaso 3\nCaso n")); cellc1.HorizontalAlignment = Element.ALIGN_CENTER; cellc1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            table.AddCell(cellc1);
                            PdfPCell celln1 = new PdfPCell(new Phrase("n")); celln1.HorizontalAlignment = Element.ALIGN_CENTER; celln1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            table.AddCell(celln1);
                            PdfPCell cellp1 = new PdfPCell(new Phrase("p%")); cellp1.HorizontalAlignment = Element.ALIGN_CENTER; cellp1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            table.AddCell(cellp1);
                            doc.Add(table);
                            // Tabla casos
                            PdfPTable table2 = new PdfPTable(5);
                            table2.HorizontalAlignment = Element.ALIGN_CENTER;
                            table2.TotalWidth = 520f;
                            table2.LockedWidth = true;
                            float[] widths2 = new float[] { 50f, 50f, 140f, 140f, 140f };
                            table2.SetWidths(widths2);
                            PdfPCell cell2 = new PdfPCell(new Phrase("Resumen de ejecución de casos"));
                            cell2.Colspan = 6; cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell2.VerticalAlignment = 2;
                            cell2.Border = 0;
                            cell2.PaddingTop = 30f;
                            table2.AddCell(cell2);
                            PdfPCell cell2H1 = new PdfPCell(new Phrase("Caso")); cell2H1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell2H1.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table2.AddCell(cellH1);
                            PdfPCell cell2H2 = new PdfPCell(new Phrase("Tipo")); cell2H2.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell2H2.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table2.AddCell(cell2H2);
                            PdfPCell cell2H3 = new PdfPCell(new Phrase("Descripción")); cell2H3.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell2H3.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table2.AddCell(cell2H3);
                            PdfPCell cell2H4 = new PdfPCell(new Phrase("Justificación")); cell2H4.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell2H4.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table2.AddCell(cell2H4);
                            PdfPCell cell2H5 = new PdfPCell(new Phrase("Estado")); cell2H5.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell2H5.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table2.AddCell(cell2H5);
                            //Estos se llenan en un for y se van insertando
                            table2.AddCell("Caso N");
                            table2.AddCell("Tipo 1");
                            table2.AddCell("Esta es la descripción del mae");
                            table2.AddCell("Esta es la justificación del mae");
                            table2.AddCell("Este es el estado del mae");
                            PdfPCell cell2H6 = new PdfPCell(new Phrase("Imagen")); cell2H6.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell2H6.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            table2.AddCell(cell2H6);
                            PdfPCell cellIm = new PdfPCell(new Phrase("Esta es la imagen del mae")); cellIm.HorizontalAlignment = Element.ALIGN_CENTER; cellIm.Colspan = 4;
                            table2.AddCell(cellIm);

                            doc.Add(table2);
                            doc.Close();
                            descargaReporte(stream, "reporteResumen.pdf");
                        }
                    }
                    else if(tipo.Value == "progreso")
                    {
                        using (MemoryStream stream = new MemoryStream()) { 
                            var doc = new Document();
                            PdfWriter.GetInstance(doc, stream);
                            doc.Open();
                            Paragraph tit = new Paragraph("Reporte de Progreso de Proyecto"); tit.Alignment = Element.ALIGN_CENTER; tit.Font.Size = 18;
                            doc.Add(tit);
                            doc.Add(new Paragraph(" "));
                            doc.Add(new Paragraph("Nombre de proyecto: " + "[AQUI NOMBRE]"));
                            doc.Add(new Paragraph("Fecha de generación de reporte: " + "[AQUI FECHA]"));
                            doc.Add(new Paragraph("Fecha de primera ejecución: " + "[AQUI FECHA]"));
                            doc.Add(new Paragraph("Fecha de última ejecución: " + "[AQUI FECHA]"));
                            doc.Add(new Paragraph("Responsable de diseño: " + "[AQUI RESPONSABLE]"));
                            


                            doc.Close();
                            descargaReporte(stream, "reporteProgreso.pdf");
                        }
                    }
                }
                else
                {
                    //Usa microsoft.Word
                    if (tipo.Value == "calidad")
                    {

                        //Creamos un Objeto del Tipo Word Application 
                        Word.Application app;
                        //Creamos otro Objeto del Tipo Word Document  
                        Word.Document doc;
                        try
                        {
                            // Creamos otro Objeto para interactuar con el Interop 
                            Object oMissing = System.Reflection.Missing.Value;
                            //Creamos una instancia de una Aplicación Word. 
                            app = new Word.Application();
                            //Entonces a la aplicación Word, le añadimos un documento.
                            doc = app.Documents.Add(ref oMissing);

                            //Activamos el documento recien creado, de forma que podamos escribir en el 
                            doc.Activate();
                            //Comenzamos a escribir dentro del documento.
                            app.Selection.Font.Size = 18;
                            app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            string tit = "Reporte de Calidad de Proyecto\n"; app.Selection.TypeText(tit);
                            app.Selection.Font.Size = 12;
                            app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                            //Estos se llenan en un for y se van insertando
                            string c1 = "Nombre de proyecto: " + "[AQUI NOMBRE]\n"; app.Selection.TypeText(c1);
                            string c2 = "Fecha de generación de reporte: " + "[AQUI FECHA]\n";  app.Selection.TypeText(c2);
                            string c3 = "Fecha de ejecución: " + "[AQUI FECHA]\n";  app.Selection.TypeText(c3);
                            string c4 = "Responsable de diseño: " + "[AQUI RESPONSABLE]\n\n"; app.Selection.TypeText(c4);

                            object start = tit.Length + c1.Length + c2.Length + c3.Length + c4.Length;
                            object end = start;
                            Word.Range tableLocation = doc.Range(ref start, ref end);
                            app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            doc.Tables.Add(tableLocation, 2, 3);
                            doc.Tables[1].Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            doc.Tables[1].Borders.InsideColor = Word.WdColor.wdColorBlack;
                            doc.Tables[1].Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            doc.Tables[1].Borders.OutsideColor = Word.WdColor.wdColorBlack;
                            //PdfPTable table = new PdfPTable(3);
                            //PdfPCell cell = new PdfPCell(new Phrase("Porcentajes por cada clase de conformidad"));
                            //cell.Colspan = 3; cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cell.VerticalAlignment = 2;
                            //cell.Border = 0;
                            //cell.PaddingTop = 30f;
                            //table.AddCell(cell);
                            //PdfPCell cellH1 = new PdfPCell(new Phrase("Estado")); cellH1.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cellH1.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            //table.AddCell(cellH1);
                            //PdfPCell cellH2 = new PdfPCell(new Phrase("Cantidad")); cellH2.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cellH2.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            //table.AddCell(cellH2);
                            //PdfPCell cellH3 = new PdfPCell(new Phrase("Porcentaje")); cellH3.HorizontalAlignment = Element.ALIGN_CENTER;
                            //cellH3.BackgroundColor = new iTextSharp.text.BaseColor(209, 132, 31);
                            //table.AddCell(cellH3);
                            //table.AddCell("Aceptado");
                            //table.AddCell("n");
                            //table.AddCell("p%");
                            //doc.Add(table);
                            //doc.Close();


                            string fileName = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.docx";
                            app.ActiveDocument.SaveAs(fileName);
                            doc.Close();
                            descargaReporte(null, "reporteCalidad.docx");
                            app.Quit(false);

                        }
                        catch (Exception e)
                        {
                            //doc.Close();
                            app = null;
                            GC.Collect();
                            throw e;
                        }



                    }
                    else if (tipo.Value == "estado")
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {

                            //descargaReporte(stream, "reporteEstado.pdf");
                        }
                    }
                    else if (tipo.Value == "resumen")
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {

                            //descargaReporte(stream, "reporteResumen.pdf");
                        }
                    }
                    else if (tipo.Value == "progreso")
                    {

                    }
                }
            }
            else
            {
                string faltante = "Debe seleccionar una ejecucion desde la vista de ejecuciones primero.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltante + "')", true);
            }
        }

        private void descargaReporte(MemoryStream ms, string nombre)
        {
            if (ms != null)
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + nombre);
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.End();
                ms.Close();
            } else
            {
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/docx";
                response.AddHeader("Content-Disposition", "attachment; filename=" + nombre + ";");
                string fileName = "./ReportesTMP/tmp.docx";
                response.TransmitFile(Server.MapPath(fileName));
                response.Flush();
                response.End();
            }
        }


    }
}