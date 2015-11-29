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
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Word = Microsoft.Office.Interop.Word;

namespace GestionPruebas
{
    public partial class Reportes : System.Web.UI.Page
    {
        private ControladoraReporte controlRepo;

        protected void Page_Load(object sender, EventArgs e)
        {

            //Si ya está logueado:
            if (Request.IsAuthenticated)
            {

                //Inicializamos controladora
                controlRepo = new ControladoraReporte();

                //Si es la primera vez que se carga la página:
                if (!this.IsPostBack)
                {
                    llenaProys();
                    btnCancelar.Visible = false;
                    ViewState["estaGuardado"] = false;
                }
            }
            else
            {//En caso de que no esté logueado, redirija a login
                Response.Redirect("Login.aspx");
            }
        }

        protected void cambiaProyecto1Box(object sender, EventArgs e)
        {
            int[] idsP = (int[])ViewState["idsproys"];
            if (proyecto1.SelectedIndex != 0)
            {
                ViewState["idproy1"] = idsP[proyecto1.SelectedIndex];
                llenaDisenos1(idsP[proyecto1.SelectedIndex]);
            }
            else
            {
                ViewState["idproy1"] = null;
                ViewState["iddise1"] = null;
                ViewState["idejec1"] = null;
                diseno1.Items.Clear();
                ejecucion1.Items.Clear();
                ViewState["ejec1"] = null;
                ViewState["conf1"] = null;
            }
        }

        protected void cambiaProyecto2Box(object sender, EventArgs e)
        {
            int[] idsP = (int[])ViewState["idsproys"];
            if (proyecto2.SelectedIndex != 0)
            {
                ViewState["idproy2"] = idsP[proyecto2.SelectedIndex];
                llenaDisenos2(idsP[proyecto2.SelectedIndex]);
                
            }
            else
            {
                ViewState["idproy2"] = null;
                ViewState["iddise2"] = null;
                ViewState["idejec2"] = null;
                diseno2.Items.Clear();
                ejecucion2.Items.Clear();
                ViewState["ejec2"] = null;
                ViewState["conf2"] = null;
            }
        }

        protected void cambiaDiseno1Box(object sender, EventArgs e)
        {
            int[] idsD = (int[])ViewState["idsdise1"];
            if (diseno1.SelectedIndex != 0)
            {
                ViewState["iddise1"] = idsD[diseno1.SelectedIndex];
                llenaEjecuciones1(idsD[diseno1.SelectedIndex]);
            }
            else
            {
                ViewState["iddise1"] = null;
                ViewState["idejec1"] = null;
                ejecucion1.Items.Clear();
                ViewState["ejec1"] = null;
                ViewState["conf1"] = null;
            }
        }

        protected void cambiaDiseno2Box(object sender, EventArgs e)
        {
            int[] idsD = (int[])ViewState["idsdise2"];
            if (diseno2.SelectedIndex != 0)
            {
                ViewState["iddise2"] = idsD[diseno2.SelectedIndex];
                llenaEjecuciones2(idsD[diseno2.SelectedIndex]);
            }
            else
            {
                ViewState["iddise2"] = null;
                ViewState["idejec2"] = null;
                ejecucion2.Items.Clear();
                ViewState["ejec2"] = null;
                ViewState["conf2"] = null;
            }
        }

        protected void cambiaEjecucion1Box(object sender, EventArgs e)
        {
            int[] idsE = (int[])ViewState["idsejec1"];
            if (ejecucion1.SelectedIndex != 0)
            {
                ViewState["idejec1"] = idsE[ejecucion1.SelectedIndex];
                ViewState["ejec1"] = controlRepo.consultarEjecucion(idsE[ejecucion1.SelectedIndex]);
                DataTable dt = controlRepo.consultarNoConformidades(idsE[ejecucion2.SelectedIndex]);
                EntidadNoConformidad[] listNC = new EntidadNoConformidad[dt.Rows.Count];
                int i = 0;
                foreach (DataRow r in dt.Rows)
                {
                    EntidadNoConformidad nc = new EntidadNoConformidad((int)r[0], (int)r[1], (int)r[2], (string)r[3], (string)r[4], (string)r[5], (string)r[6], (string)r[7], (byte[])r[8]);
                    listNC[i] = nc;
                    i++;
                }
                ViewState["conf1"] = listNC;
            }
            else
            {
                ViewState["idejec1"] = null;
                ViewState["ejec1"] = null;
                ViewState["conf1"] = null;
            }
        }

        protected void cambiaEjecucion2Box(object sender, EventArgs e)
        {
            int[] idsE = (int[])ViewState["idsejec2"];
            if (ejecucion2.SelectedIndex != 0)
            {
                ViewState["idejec2"] = idsE[ejecucion2.SelectedIndex];
                ViewState["ejec2"] = controlRepo.consultarEjecucion(idsE[ejecucion2.SelectedIndex]);
                DataTable dt = controlRepo.consultarNoConformidades(idsE[ejecucion2.SelectedIndex]);
                EntidadNoConformidad[] listNC = new EntidadNoConformidad[dt.Rows.Count];
                int i = 0;
                foreach(DataRow r in dt.Rows)
                {
                    EntidadNoConformidad nc = new EntidadNoConformidad((int)r[0], (int)r[1], (int)r[2], (string)r[3], (string)r[4], (string)r[5], (string)r[6], (string)r[7], (byte[])r[8]);
                    listNC[i] = nc;
                    i++;
                }
                ViewState["conf2"] = listNC;
            }
            else
            {
                ViewState["idejec2"] = null;
                ViewState["ejec2"] = null;
                ViewState["conf2"] = null;
            }
        }

        protected void llenaProys()
        {
            string usuario = ((SiteMaster)this.Master).nombreUsuario; 
            if (esAdmin(usuario))
            {
                proyecto1.Items.Clear();
                proyecto2.Items.Clear();
                DataTable dt = controlRepo.consultaProyectos();
                int[] ids = new int[dt.Rows.Count + 1];
                ids[0] = -1;
                proyecto1.Items.Add(new ListItem("Seleccione un Proyecto", ""));
                proyecto2.Items.Add(new ListItem("Ninguno", ""));
                int i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    DataColumn id = dt.Columns[1];
                    ids[i] = parseInt(row[id].ToString());
                    DataColumn nombre = dt.Columns[0];
                    proyecto1.Items.Add(new ListItem(row[nombre].ToString(), "" + i));
                    proyecto2.Items.Add(new ListItem(row[nombre].ToString(), "" + i));
                    i++;
                }
                ViewState["idsproys"] = ids;
            }
            else
            {
                //Es miembro
                proyecto1.Items.Clear();
                proyecto2.Items.Clear();
                DataTable dt = controlRepo.consultaProyecto(usuario);
                int[] ids = new int[dt.Rows.Count + 1];
                ids[0] = -1;
                proyecto1.Items.Add(new ListItem("Seleccione un Proyecto", ""));
                proyecto2.Items.Add(new ListItem("Ninguno", ""));
                int i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    DataColumn id = dt.Columns[1];
                    ids[i] = parseInt(row[id].ToString());
                    DataColumn nombre = dt.Columns[0];
                    proyecto1.Items.Add(new ListItem(row[nombre].ToString(), "" + i));
                    proyecto2.Items.Add(new ListItem(row[nombre].ToString(), "" + i));
                    i++;
                }
                ViewState["idsproys"] = ids;
                if (i > 1)
                {
                    proyecto1.SelectedIndex = 1;
                    proyecto2.SelectedIndex = 1;
                    cambiaProyecto1Box(null, null);
                    cambiaProyecto2Box(null, null);
                }
            }
        }

        protected void llenaDisenos1(int idProy)
        {
            diseno1.Items.Clear();
            DataTable dt = controlRepo.consultaDisenos(idProy);
            int[] ids = new int[dt.Rows.Count + 1];
            ids[0] = -1;
            diseno1.Items.Add(new ListItem("Seleccione un Diseno", ""));
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                DataColumn id = dt.Columns[1];
                ids[i] = parseInt(row[id].ToString());
                DataColumn prop = dt.Columns[0];
                diseno1.Items.Add(new ListItem(row[id].ToString()+" - "+row[prop].ToString(), "" + i));
                i++;
            }
            ViewState["idsdise1"] = ids;
        }

        protected void llenaDisenos2(int idProy)
        {
            diseno2.Items.Clear();
            DataTable dt = controlRepo.consultaDisenos(idProy);
            int[] ids = new int[dt.Rows.Count + 1];
            ids[0] = -1;
            diseno2.Items.Add(new ListItem("Ninguno", ""));
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                DataColumn id = dt.Columns[1];
                ids[i] = parseInt(row[id].ToString());
                DataColumn prop = dt.Columns[0];
                diseno2.Items.Add(row[id].ToString() + " - " + new ListItem(row[prop].ToString(), "" + i));
                i++;
            }
            ViewState["idsdise2"] = ids;
        }

        protected void llenaEjecuciones1(int idDise)
        {
            ejecucion1.Items.Clear();
            DataTable dt = controlRepo.consultaEjecuciones(idDise);
            int[] ids = new int[dt.Rows.Count + 1];
            ids[0] = -1;
            ejecucion1.Items.Add(new ListItem("Seleccione una Ejecución", ""));
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                DataColumn id = dt.Columns[1];
                ids[i] = parseInt(row[id].ToString());
                DataColumn fec = dt.Columns[0];
                ejecucion1.Items.Add(new ListItem(row[id].ToString() + " - " + row[fec].ToString(), "" + i));
                i++;
            }
            ViewState["idsejec1"] = ids;
        }

        protected void llenaEjecuciones2(int idDise)
        {
            ejecucion2.Items.Clear();
            DataTable dt = controlRepo.consultaEjecuciones(idDise);
            int[] ids = new int[dt.Rows.Count + 1];
            ids[0] = -1;
            ejecucion2.Items.Add(new ListItem("Ninguna", ""));
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                DataColumn id = dt.Columns[1];
                ids[i] = parseInt(row[id].ToString());
                DataColumn fec = dt.Columns[0];
                ejecucion2.Items.Add(row[id].ToString() + " - " + new ListItem(row[fec].ToString(), "" + i));
                i++;
            }
            ViewState["idsejec2"] = ids;
        }

        /*
         * Desc: Se encarga de realizar las operaciones de creacion de reporte
         * Requiere: n/a
         * Retorna. n/a
         */
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (btnAceptar.Text == "Aceptar")
            {
                btnAceptar.Text = "Descargar";
                paneltipo.Visible = false;
                panelconf.Visible = true;
                panelresu.Visible = true;
                btnCancelar.Visible = true;
                if (tipo.Value == "progreso")
                {
                    labelDise1.Visible = false;
                    diseno1.Visible = false;
                    labelEjec1.Visible = false;
                    ejecucion1.Visible = false;
                    labelDise2.Visible = false;
                    diseno2.Visible = false;
                    labelEjec2.Visible = false;
                    ejecucion2.Visible = false;
                }
                else
                {
                    labelDise1.Visible = true;
                    diseno1.Visible = true;
                    labelEjec1.Visible = true;
                    ejecucion1.Visible = true;
                    labelDise2.Visible = true;
                    diseno2.Visible = true;
                    labelEjec2.Visible = true;
                    ejecucion2.Visible = true;
                }
            }
            else
            {
                creaReporte();
            }
        }


        protected void volverEj(object sender, EventArgs e)
        {
            Response.Redirect("Ejecucion.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (btnAceptar.Text == "Descargar")
            {
                btnAceptar.Text = "Aceptar";
                paneltipo.Visible = true;
                panelconf.Visible = false;
                panelresu.Visible = false;
                btnCancelar.Visible = false;
                ViewState["estaGuardado"] = false;
            }
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
        protected bool esAdmin(string usuario)
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

        protected void creaReporte()
        {
            if (true || ViewState["ejec1"] != null && ViewState["conf1"] != null)
            {
                //Usa microsoft.Word
                if (tipo.Value == "calidad")
                {
                    if (!(bool)ViewState["estaGuardado"])
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
                            string c2 = "Fecha de generación de reporte: " + "[AQUI FECHA]\n"; app.Selection.TypeText(c2);
                            string c3 = "Fecha de ejecución: " + "[AQUI FECHA]\n"; app.Selection.TypeText(c3);
                            string c4 = "Responsable de diseño: " + "[AQUI RESPONSABLE]\n\n"; app.Selection.TypeText(c4);

                            Word.Paragraph pr = app.Selection.Paragraphs.Add();
                            object start = pr.Range.Start;
                            object end = pr.Range.End;
                            Word.Range tableLocation = doc.Range(ref start, ref end);
                            app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            Word.Table t = doc.Tables.Add(tableLocation, 2, 4);
                            t.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            t.Borders.InsideColor = Word.WdColor.wdColorBlack;
                            t.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            t.Borders.OutsideColor = Word.WdColor.wdColorBlack;
                            t.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                            t.Rows[1].Range.Font.Bold = 1;
                            t.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            t.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            t.Cell(1, 1).Range.Text = "Estado";
                            t.Cell(1, 2).Range.Text = "Casos";
                            t.Cell(1, 3).Range.Text = "Cantidad";
                            t.Cell(1, 4).Range.Text = "Porcentaje";
                            //Falta for para llenar dinámicamente la tabla

                            string fileNameD = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.docx";
                            string fileNameP = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.pdf";
                            ViewState["estaGuardado"] = true;
                            app.ActiveDocument.SaveAs(fileNameD);
                            app.ActiveDocument.ExportAsFixedFormat(fileNameP, Word.WdExportFormat.wdExportFormatPDF);
                            doc.Close();
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
                    descargaReporte(formato.Value, "ReporteCalidad");

                }
                else if (tipo.Value == "noconformidad")
                {
                    if (!(bool)ViewState["estaGuardado"])
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
                            string tit = "Reporte de No Conformidades de Proyecto\n"; app.Selection.TypeText(tit);
                            app.Selection.Font.Size = 12;
                            app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                            //Estos se llenan en un for y se van insertando
                            string c1 = "Nombre de proyecto: " + "[AQUI NOMBRE]\n"; app.Selection.TypeText(c1);
                            string c2 = "Fecha de generación de reporte: " + "[AQUI FECHA]\n"; app.Selection.TypeText(c2);
                            string c3 = "Fecha de ejecución: " + "[AQUI FECHA]\n"; app.Selection.TypeText(c3);
                            string c4 = "Responsable de diseño: " + "[AQUI RESPONSABLE]\n\n"; app.Selection.TypeText(c4);

                            Word.Paragraph pr = doc.Paragraphs.Add();
                            pr.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            Word.Table t = doc.Tables.Add(pr.Range, 2, 4);
                            t.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            t.Borders.InsideColor = Word.WdColor.wdColorBlack;
                            t.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            t.Borders.OutsideColor = Word.WdColor.wdColorBlack;
                            t.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                            t.Rows[1].Range.Font.Bold = 1;
                            t.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            t.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            t.Cell(1, 1).Range.Text = "No Conformidad";
                            t.Cell(1, 2).Range.Text = "Casos";
                            t.Cell(1, 3).Range.Text = "Cantidad";
                            t.Cell(1, 4).Range.Text = "Porcentaje";
                            //Falta for para llenar dinámicamente la tabla

                            string fileNameD = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.docx";
                            string fileNameP = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.pdf";
                            ViewState["estaGuardado"] = true;
                            app.ActiveDocument.SaveAs(fileNameD);
                            app.ActiveDocument.ExportAsFixedFormat(fileNameP, Word.WdExportFormat.wdExportFormatPDF);
                            doc.Close();
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
                    descargaReporte(formato.Value, "ReporteNoConformidades");

                }
                else if (tipo.Value == "estado")
                {
                    if (!(bool)ViewState["estaGuardado"])
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
                            string tit = "Reporte de Estado de Proyecto\n"; app.Selection.TypeText(tit);
                            app.Selection.Font.Size = 12;
                            app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                            //Estos se llenan en un for y se van insertando
                            string c1 = "Nombre de proyecto: " + "[AQUI NOMBRE]\n"; app.Selection.TypeText(c1);
                            string c2 = "Fecha de generación de reporte: " + "[AQUI FECHA]\n"; app.Selection.TypeText(c2);
                            string c3 = "Fecha de ejecución: " + "[AQUI FECHA]\n"; app.Selection.TypeText(c3);
                            string c4 = "Responsable de diseño: " + "[AQUI RESPONSABLE]\n\n"; app.Selection.TypeText(c4);

                            Word.Paragraph pr = doc.Paragraphs.Add();
                            pr.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            Word.Table t = doc.Tables.Add(pr.Range, 3, 5);
                            t.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            t.Borders.InsideColor = Word.WdColor.wdColorBlack;
                            t.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            t.Borders.OutsideColor = Word.WdColor.wdColorBlack;
                            t.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                            t.Rows[1].Range.Font.Bold = 1;
                            t.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            t.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            t.Cell(1, 1).Range.Text = "Caso";
                            t.Cell(1, 2).Range.Text = "Tipo";
                            t.Cell(1, 3).Range.Text = "Descripción";
                            t.Cell(1, 4).Range.Text = "Justificación";
                            t.Cell(1, 5).Range.Text = "Estado";
                            t.Cell(3, 1).Range.Text = "Imagen"; doc.Tables[1].Cell(3, 1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                            t.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                            t.Cell(3, 1).Range.Font.Bold = 1;
                            t.Cell(3, 5).Merge(t.Cell(3, 4));
                            t.Cell(3, 4).Merge(t.Cell(3, 3));
                            t.Cell(3, 3).Merge(t.Cell(3, 2));
                            //Falta for para llenar dinámicamente la tabla

                            string fileNameD = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.docx";
                            string fileNameP = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.pdf";
                            ViewState["estaGuardado"] = true;
                            app.ActiveDocument.SaveAs(fileNameD);
                            app.ActiveDocument.ExportAsFixedFormat(fileNameP, Word.WdExportFormat.wdExportFormatPDF);
                            doc.Close();
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
                    descargaReporte(formato.Value, "ReporteEstado");
                }
                else if (tipo.Value == "completo")
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
                        string tit = "Reporte de Completo de Proyecto\n"; app.Selection.TypeText(tit);
                        app.Selection.Font.Size = 12;
                        app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                        //Estos se llenan en un for y se van insertando
                        string c1 = "Nombre de proyecto: " + "[AQUI NOMBRE]\n"; app.Selection.TypeText(c1);
                        string c2 = "Fecha de generación de reporte: " + "[AQUI FECHA]\n"; app.Selection.TypeText(c2);
                        string c3 = "Fecha de ejecución: " + "[AQUI FECHA]\n"; app.Selection.TypeText(c3);
                        string c4 = "Responsable de diseño: " + "[AQUI RESPONSABLE]\n\n"; app.Selection.TypeText(c4);

                        //Tabla calidad
                        Word.Paragraph pr = doc.Paragraphs.Add();
                        pr.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        Word.Table t = doc.Tables.Add(pr.Range, 2, 4);
                        t.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        t.Borders.InsideColor = Word.WdColor.wdColorBlack;
                        t.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        t.Borders.OutsideColor = Word.WdColor.wdColorBlack;
                        t.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                        t.Rows[1].Range.Font.Bold = 1;
                        t.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        t.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        t.Cell(1, 1).Range.Text = "Estado";
                        t.Cell(1, 2).Range.Text = "Casos";
                        t.Cell(1, 3).Range.Text = "Cantidad";
                        t.Cell(1, 4).Range.Text = "Porcentaje";
                        //Falta for para llenar dinámicamente la tabla
                        pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                        pr.Range.InsertParagraphAfter();
                        pr.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);


                        //Tabla no conformidades
                        Word.Paragraph pr2 = doc.Paragraphs.Add();
                        pr2.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        Word.Table t2 = doc.Tables.Add(pr2.Range, 2, 4);
                        t2.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        t2.Borders.InsideColor = Word.WdColor.wdColorBlack;
                        t2.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        t2.Borders.OutsideColor = Word.WdColor.wdColorBlack;
                        t2.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                        t2.Rows[1].Range.Font.Bold = 1;
                        t2.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        t2.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        t2.Cell(1, 1).Range.Text = "No Conformidad";
                        t2.Cell(1, 2).Range.Text = "Casos";
                        t2.Cell(1, 3).Range.Text = "Cantidad";
                        t2.Cell(1, 4).Range.Text = "Porcentaje";
                        //Falta for para llenar dinámicamente la tabla
                        pr2.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                        pr2.Range.InsertParagraphAfter();
                        pr2.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

                        //Tabla estado
                        Word.Paragraph pr3 = doc.Paragraphs.Add();
                        pr3.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        Word.Table t3 = doc.Tables.Add(pr3.Range, 3, 5);
                        t3.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        t3.Borders.InsideColor = Word.WdColor.wdColorBlack;
                        t3.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        t3.Borders.OutsideColor = Word.WdColor.wdColorBlack;
                        t3.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                        t3.Rows[1].Range.Font.Bold = 1;
                        t3.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        t3.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        t3.Cell(1, 1).Range.Text = "Caso";
                        t3.Cell(1, 2).Range.Text = "Tipo";
                        t3.Cell(1, 3).Range.Text = "Descripción";
                        t3.Cell(1, 4).Range.Text = "Justificación";
                        t3.Cell(1, 5).Range.Text = "Estado";
                        t3.Cell(3, 1).Range.Text = "Imagen"; t3.Cell(3, 1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                        t3.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        t.Cell(3, 1).Range.Font.Bold = 1;
                        t3.Cell(3, 5).Merge(t3.Cell(3, 4));
                        t3.Cell(3, 4).Merge(t3.Cell(3, 3));
                        t3.Cell(3, 3).Merge(t3.Cell(3, 2));
                        //Falta for para llenar dinámicamente la tabla
                        pr3.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                        pr3.Range.InsertParagraphAfter();
                        pr3.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

                        //Falta for para llenar dinámicamente la gráfica
                        Random rdn = new Random();
                        List<DataPoint> datos = new List<DataPoint>();
                        var dato1 = new DataPoint(1, rdn.Next(0, 100)); datos.Add(dato1);
                        var dato2 = new DataPoint(2, rdn.Next(0, 100)); datos.Add(dato2);
                        var dato3 = new DataPoint(3, rdn.Next(0, 100)); datos.Add(dato3);
                        var dato4 = new DataPoint(4, rdn.Next(0, 100)); datos.Add(dato4);
                        var dato5 = new DataPoint(5, rdn.Next(0, 100)); datos.Add(dato5);
                        var dato6 = new DataPoint(6, rdn.Next(0, 100)); datos.Add(dato6);
                        List<DataPoint> datos2 = new List<DataPoint>();
                        dato1 = new DataPoint(1, rdn.Next(0, 100)); datos2.Add(dato1);
                        dato2 = new DataPoint(2, rdn.Next(0, 100)); datos2.Add(dato2);
                        dato3 = new DataPoint(3, rdn.Next(0, 100)); datos2.Add(dato3);
                        dato4 = new DataPoint(4, rdn.Next(0, 100)); datos2.Add(dato4);
                        dato5 = new DataPoint(5, rdn.Next(0, 100)); datos2.Add(dato5);
                        dato6 = new DataPoint(6, rdn.Next(0, 100)); datos2.Add(dato6);
                        var img = generaGrafica(datos, datos2, "Proyecto 1", " Proyecto 2");
                        img.Save(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                        Word.Paragraph pr4 = doc.Paragraphs.Add();
                        pr4.Range.Text = "Gráfica de porcentaje de progreso en proyecto";
                        pr4.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        pr4.Range.Select();
                        app.Selection.MoveDown();
                        doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                        pr4.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                        pr4.Range.InsertParagraphAfter();
                        pr4.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

                        string fileNameD = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.docx";
                        string fileNameP = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.pdf";
                        ViewState["estaGuardado"] = true;
                        app.ActiveDocument.SaveAs(fileNameD);
                        app.ActiveDocument.ExportAsFixedFormat(fileNameP, Word.WdExportFormat.wdExportFormatPDF);
                        doc.Close();
                        app.Quit(false);

                    }
                    catch (Exception e)
                    {
                        //doc.Close();
                        app = null;
                        GC.Collect();
                        throw e;
                    }
                    descargaReporte(formato.Value, "ReporteCompleto");
                }
                else if (tipo.Value == "progreso")
                {
                    if (!(bool)ViewState["estaGuardado"])
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
                            string tit = "Reporte de Progreso de Proyecto\n"; app.Selection.TypeText(tit);
                            app.Selection.Font.Size = 12;
                            app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                            //Estos se llenan en un for y se van insertando
                            string c1 = "Nombre de proyecto: " + "[AQUI NOMBRE]\n"; app.Selection.TypeText(c1);
                            string c2 = "Fecha de generación de reporte: " + "[AQUI FECHA]\n"; app.Selection.TypeText(c2);
                            string c3 = "Fecha de ejecución: " + "[AQUI FECHA]\n"; app.Selection.TypeText(c3);
                            string c4 = "Responsable de diseño: " + "[AQUI RESPONSABLE]\n\n"; app.Selection.TypeText(c4);
                            app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                            //Falta for para llenar dinámicamente la gráfica
                            Random rdn = new Random();
                            List<DataPoint> datos = new List<DataPoint>();
                            var dato1 = new DataPoint(1, rdn.Next(0, 100)); datos.Add(dato1);
                            var dato2 = new DataPoint(2, rdn.Next(0, 100)); datos.Add(dato2);
                            var dato3 = new DataPoint(3, rdn.Next(0, 100)); datos.Add(dato3);
                            var dato4 = new DataPoint(4, rdn.Next(0, 100)); datos.Add(dato4);
                            var dato5 = new DataPoint(5, rdn.Next(0, 100)); datos.Add(dato5);
                            var dato6 = new DataPoint(6, rdn.Next(0, 100)); datos.Add(dato6);
                            List<DataPoint> datos2 = new List<DataPoint>();
                            dato1 = new DataPoint(1, rdn.Next(0, 100)); datos2.Add(dato1);
                            dato2 = new DataPoint(2, rdn.Next(0, 100)); datos2.Add(dato2);
                            dato3 = new DataPoint(3, rdn.Next(0, 100)); datos2.Add(dato3);
                            dato4 = new DataPoint(4, rdn.Next(0, 100)); datos2.Add(dato4);
                            dato5 = new DataPoint(5, rdn.Next(0, 100)); datos2.Add(dato5);
                            dato6 = new DataPoint(6, rdn.Next(0, 100)); datos2.Add(dato6);
                            var img = generaGrafica(datos, datos2, "Proyecto 1", " Proyecto 2");
                            img.Save(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            Word.Paragraph pr = doc.Paragraphs.Add();
                            pr.Range.Text = "Gráfica de porcentaje de casos aceptados en proyecto";
                            pr.Range.Font.Bold = 1;
                            pr.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            pr.Range.Select();
                            app.Selection.MoveDown();
                            doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            pr.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                            pr.Range.InsertParagraphAfter();
                            pr.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

                            //Falta for para llenar dinámicamente la gráfica
                            Word.Paragraph pr2 = doc.Paragraphs.Add();
                            pr2.Range.Text = "Gráfica de porcentaje de casos no aceptados en proyecto";
                            pr2.Range.Font.Bold = 1;
                            pr2.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            pr2.Range.Select();
                            app.Selection.MoveDown();
                            doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            pr2.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
                            pr2.Range.InsertParagraphAfter();
                            pr2.Range.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);

                            string fileNameD = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.docx";
                            string fileNameP = HttpRuntime.AppDomainAppPath + "ReportesTMP\\tmp.pdf";
                            ViewState["estaGuardado"] = true;
                            app.ActiveDocument.SaveAs(fileNameD);
                            app.ActiveDocument.ExportAsFixedFormat(fileNameP, Word.WdExportFormat.wdExportFormatPDF);
                            doc.Close();
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

                    descargaReporte(formato.Value, "ReporteProgreso");

                }
            }
            else
            {
                string faltante = "Debe seleccionar una ejecucion desde la vista de ejecuciones primero.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltante + "')", true);
            }
        }

        private void descargaReporte(string formato, string nombre)
        {
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.AddHeader("Content-Disposition", "attachment; filename=" + nombre + "." + formato + ";");
            string fileName = "";
            if (formato == "pdf")
            {
                response.ContentType = "application/pdf";
                fileName = "./ReportesTMP/tmp.pdf";
            }
            else if (formato == "docx")
            {
                response.ContentType = "application/docx";
                fileName = "./ReportesTMP/tmp.docx";
            }
            response.TransmitFile(Server.MapPath(fileName));
            response.Flush();
            response.End();
        }

        public iTextSharp.text.Image generaGrafica(IList<DataPoint> series, string Pr1)
        {
            using (MemoryStream graph = new MemoryStream())
            using (var ch = new Chart())
            {
                ch.ChartAreas.Add(new ChartArea());

                var s = new Series();
                foreach (var pnt in series) s.Points.Add(pnt);
                ch.Series.Add(s);
                s.ChartType = SeriesChartType.FastLine;
                s.Color = Color.Red;
                s.Name = Pr1;

                ch.Size = new Size(600, 550);
                ch.SaveImage(graph, ChartImageFormat.Jpeg);
                ch.Legends.Add("leyenda");

                var image = iTextSharp.text.Image.GetInstance(graph.GetBuffer());
                return image;
            }
        }

        public System.Drawing.Image generaGrafica(IList<DataPoint> series, IList<DataPoint> series2, string Pr1, string Pr2)
        {
            using (MemoryStream graph = new MemoryStream())
            using (var ch = new Chart())
            {
                ch.ChartAreas.Add(new ChartArea());

                var s = new Series();
                foreach (var pnt in series) s.Points.Add(pnt);
                ch.Series.Add(s);
                s.ChartType = SeriesChartType.FastLine;
                s.Color = Color.Red;
                s.Name = Pr1;

                var s1 = new Series();
                foreach (var pnt in series2) s1.Points.Add(pnt);
                ch.Series.Add(s1);
                s1.ChartType = SeriesChartType.FastLine;
                s1.Color = Color.Blue;
                s1.Name = Pr2;

                ch.Size = new Size(600, 550);
                ch.Legends.Add("leyenda");
                ch.SaveImage(graph, ChartImageFormat.Jpeg);
                MemoryStream ms = new MemoryStream(graph.GetBuffer());
                var image = System.Drawing.Image.FromStream(ms);

                return image;
            }
        }


    }
}