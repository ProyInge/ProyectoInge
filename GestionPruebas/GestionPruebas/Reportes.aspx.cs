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
            ViewState["estaGuardado"] = false;
            int[] idsP = (int[])ViewState["idsproys"];
            if (proyecto1.SelectedIndex != 0)
            {
                ViewState["idproy1"] = idsP[proyecto1.SelectedIndex];
                llenaDisenos1(idsP[proyecto1.SelectedIndex]);
                if (tipo.Value == "progreso")
                {
                    ViewState["ejec1"] = new EntidadEjecucion(); ;
                    ViewState["conf1"] = new EntidadNoConformidad[1];
                }
            }
            else
            {
                ViewState["idsejec1"] = null;
                ViewState["idsdise1"] = null;
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
            ViewState["estaGuardado"] = false;
            int[] idsP = (int[])ViewState["idsproys"];
            if (proyecto2.SelectedIndex != 0)
            {
                ViewState["idproy2"] = idsP[proyecto2.SelectedIndex];
                llenaDisenos2(idsP[proyecto2.SelectedIndex]);
                if (tipo.Value == "progreso")
                {
                    ViewState["ejec2"] = new EntidadEjecucion(); ;
                    ViewState["conf2"] = new EntidadNoConformidad[1];
                }
            }
            else
            {
                ViewState["idsejec2"] = null;
                ViewState["idsdise2"] = null;
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
            ViewState["estaGuardado"] = false;
            int[] idsD = (int[])ViewState["idsdise1"];
            if (diseno1.SelectedIndex != 0)
            {
                ViewState["iddise1"] = idsD[diseno1.SelectedIndex];
                llenaEjecuciones1(idsD[diseno1.SelectedIndex]);
            }
            else
            {
                ViewState["idsejec1"] = null;
                ViewState["iddise1"] = null;
                ViewState["idejec1"] = null;
                ejecucion1.Items.Clear();
                ViewState["ejec1"] = null;
                ViewState["conf1"] = null;
            }
        }

        protected void cambiaDiseno2Box(object sender, EventArgs e)
        {
            ViewState["estaGuardado"] = false;
            int[] idsD = (int[])ViewState["idsdise2"];
            if (diseno2.SelectedIndex != 0)
            {
                ViewState["iddise2"] = idsD[diseno2.SelectedIndex];
                llenaEjecuciones2(idsD[diseno2.SelectedIndex]);
            }
            else
            {
                ViewState["idsejec2"] = null;
                ViewState["iddise2"] = null;
                ViewState["idejec2"] = null;
                ejecucion2.Items.Clear();
                ViewState["ejec2"] = null;
                ViewState["conf2"] = null;
            }
        }

        protected void cambiaEjecucion1Box(object sender, EventArgs e)
        {
            ViewState["estaGuardado"] = false;
            int[] idsE = (int[])ViewState["idsejec1"];
            if (ejecucion1.SelectedIndex != 0)
            {
                ViewState["idejec1"] = idsE[ejecucion1.SelectedIndex];
                ViewState["ejec1"] = controlRepo.consultarEjecucion(idsE[ejecucion1.SelectedIndex]);
                DataTable dt = controlRepo.consultarNoConformidades(idsE[ejecucion1.SelectedIndex]);
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
            ViewState["estaGuardado"] = false;
            int[] idsE = (int[])ViewState["idsejec2"];
            if (ejecucion2.SelectedIndex != 0)
            {
                ViewState["idejec2"] = idsE[ejecucion2.SelectedIndex];
                ViewState["ejec2"] = controlRepo.consultarEjecucion(idsE[ejecucion2.SelectedIndex]);
                DataTable dt = controlRepo.consultarNoConformidades(idsE[ejecucion2.SelectedIndex]);
                EntidadNoConformidad[] listNC = new EntidadNoConformidad[dt.Rows.Count];
                int i = 0;
                foreach (DataRow r in dt.Rows)
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
                    proyecto1.Items.Add(new ListItem(row[nombre].ToString(), row[nombre].ToString()));
                    proyecto2.Items.Add(new ListItem(row[nombre].ToString(), row[nombre].ToString()));
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
                    proyecto1.Items.Add(new ListItem(row[nombre].ToString(), row[nombre].ToString()));
                    proyecto2.Items.Add(new ListItem(row[nombre].ToString(), row[nombre].ToString()));
                    i++;
                }
                ViewState["idsproys"] = ids;
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
                diseno1.Items.Add(new ListItem(row[id].ToString() + " - " + row[prop].ToString(), row[prop].ToString()));
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
                diseno2.Items.Add(new ListItem(row[id].ToString() + " - " + row[prop].ToString(), row[prop].ToString()));
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
                    diseno1.Attributes.Remove("required");
                    ejecucion1.Attributes.Remove("required");
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
                    diseno1.Attributes.Add("required", "true");
                    ejecucion1.Attributes.Add("required", "true");
                }
            }
            else
            {
                creaReporte();
            }
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
                if (tipo.Value == "progreso")
                {
                    ViewState["ejec1"] = null;
                    ViewState["ejec2"] = null;
                    ViewState["dise1"] = null;
                    ViewState["dise2"] = null;
                }

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
            if (ViewState["ejec1"] != null && ViewState["conf1"] != null)
            {
                EntidadEjecucion ejec1 = (EntidadEjecucion)ViewState["ejec1"];
                EntidadNoConformidad[] conf1 = (EntidadNoConformidad[])ViewState["conf1"];
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
                            string c2 = "Fecha de generación de reporte: " + DateTime.Now.ToString() + "\n"; app.Selection.TypeText(c2);
                            string c1 = "Nombre de proyecto: " + proyecto1.Value + "\n"; app.Selection.TypeText(c1);
                            string c3 = "Fecha de ejecución: " + ejec1.Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                            string c4 = "Responsable de ejecución: " + ejec1.NombreResponsable + "\n"; app.Selection.TypeText(c4);
                            string c5 = "Propósito de diseño: " + diseno1.Value + "\n\n"; app.Selection.TypeText(c5);

                            Word.Paragraph pr = doc.Paragraphs.Add();
                            pr.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            Word.Table t = doc.Tables.Add(pr.Range, 5, 4);
                            //Llena dinámicamente la tabla
                            llenaTablaCalidad(t, conf1);
                            pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            pr.Range.InsertParagraphAfter();
                            pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                            if (ViewState["ejec2"] != null && ViewState["conf2"] != null)
                            {
                                EntidadEjecucion ejec2 = (EntidadEjecucion)ViewState["ejec2"];
                                EntidadNoConformidad[] conf2 = (EntidadNoConformidad[])ViewState["conf2"];
                                doc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                                Word.Paragraph pr2 = doc.Paragraphs.Add();
                                pr2.Range.Select();
                                c1 = "Nombre de proyecto: " + proyecto2.Value + "\n"; app.Selection.TypeText(c1);
                                c3 = "Fecha de ejecución: " + ejec2.Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                                c4 = "Responsable de ejecución: " + ejec2.NombreResponsable + "\n"; app.Selection.TypeText(c4);
                                c5 = "Propósito de diseño: " + diseno2.Value + "\n\n"; app.Selection.TypeText(c5);
                                pr2 = doc.Paragraphs.Add();
                                pr2.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                Word.Table t2 = doc.Tables.Add(pr2.Range, 5, 4);
                                //Llena dinámicamente la tabla
                                llenaTablaCalidad(t2, conf2);
                                pr2.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                                pr2.Range.InsertParagraphAfter();
                                pr2.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            }

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
                            string c2 = "Fecha de generación de reporte: " + DateTime.Now.ToString() + "\n"; app.Selection.TypeText(c2);
                            string c1 = "Nombre de proyecto: " + proyecto1.Value + "\n"; app.Selection.TypeText(c1);
                            string c3 = "Fecha de ejecución: " + ejec1.Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                            string c4 = "Responsable de ejecución: " + ejec1.NombreResponsable + "\n"; app.Selection.TypeText(c4);
                            string c5 = "Propósito de diseño: " + diseno1.Value + "\n\n"; app.Selection.TypeText(c5);

                            Word.Paragraph pr = doc.Paragraphs.Add();
                            pr.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            Word.Table t = doc.Tables.Add(pr.Range, 8, 4);
                            //Llena dinámicamente la tabla
                            llenaTablaNoConf(t, conf1);
                            pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            pr.Range.InsertParagraphAfter();
                            pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                            if (ViewState["ejec2"] != null && ViewState["conf2"] != null)
                            {
                                EntidadEjecucion ejec2 = (EntidadEjecucion)ViewState["ejec2"];
                                EntidadNoConformidad[] conf2 = (EntidadNoConformidad[])ViewState["conf2"];
                                doc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                                Word.Paragraph pr2 = doc.Paragraphs.Add();
                                pr2.Range.Select();
                                c1 = "Nombre de proyecto: " + proyecto2.Value + "\n"; app.Selection.TypeText(c1);
                                c3 = "Fecha de ejecución: " + ejec2.Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                                c4 = "Responsable de ejecución: " + ejec2.NombreResponsable + "\n"; app.Selection.TypeText(c4);
                                c5 = "Propósito de diseño: " + diseno2.Value + "\n\n"; app.Selection.TypeText(c5);
                                pr2 = doc.Paragraphs.Add();
                                pr2.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                Word.Table t2 = doc.Tables.Add(pr2.Range, 8, 4);
                                //Llena dinámicamente la tabla
                                llenaTablaNoConf(t2, conf2);
                                pr2.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                                pr2.Range.InsertParagraphAfter();
                                pr2.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            }

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
                            string c2 = "Fecha de generación de reporte: " + DateTime.Now.ToString() + "\n"; app.Selection.TypeText(c2);
                            string c1 = "Nombre de proyecto: " + proyecto1.Value + "\n"; app.Selection.TypeText(c1);
                            string c3 = "Fecha de ejecución: " + ejec1.Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                            string c4 = "Responsable de ejecución: " + ejec1.NombreResponsable + "\n"; app.Selection.TypeText(c4);
                            string c5 = "Propósito de diseño: " + diseno1.Value + "\n\n"; app.Selection.TypeText(c5);

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
                            t.Cell(3, 1).Range.Text = "Imagen"; t.Cell(3, 1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                            t.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                            t.Cell(3, 1).Range.Font.Bold = 1;
                            t.Cell(3, 5).Merge(t.Cell(3, 4));
                            t.Cell(3, 4).Merge(t.Cell(3, 3));
                            t.Cell(3, 3).Merge(t.Cell(3, 2));
                            //Falta for para llenar dinámicamente la tabla
                            pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            pr.Range.InsertParagraphAfter();
                            pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                            if (ViewState["ejec2"] != null && ViewState["conf2"] != null)
                            {
                                EntidadEjecucion ejec2 = (EntidadEjecucion)ViewState["ejec2"];
                                EntidadNoConformidad[] conf2 = (EntidadNoConformidad[])ViewState["conf2"];
                                doc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                                Word.Paragraph pr2 = doc.Paragraphs.Add();
                                pr2.Range.Select();
                                c1 = "Nombre de proyecto: " + proyecto2.Value + "\n"; app.Selection.TypeText(c1);
                                c3 = "Fecha de ejecución: " + ejec2.Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                                c4 = "Responsable de ejecución: " + ejec2.NombreResponsable + "\n"; app.Selection.TypeText(c4);
                                c5 = "Propósito de diseño: " + diseno2.Value + "\n\n"; app.Selection.TypeText(c5);
                                pr2 = doc.Paragraphs.Add();
                                pr2.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                Word.Table t2 = doc.Tables.Add(pr2.Range, 3, 5);
                                t2.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                                t2.Borders.InsideColor = Word.WdColor.wdColorBlack;
                                t2.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                                t2.Borders.OutsideColor = Word.WdColor.wdColorBlack;
                                t2.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                                t2.Rows[1].Range.Font.Bold = 1;
                                t2.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                t2.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                t2.Cell(1, 1).Range.Text = "Caso";
                                t2.Cell(1, 2).Range.Text = "Tipo";
                                t2.Cell(1, 3).Range.Text = "Descripción";
                                t2.Cell(1, 4).Range.Text = "Justificación";
                                t2.Cell(1, 5).Range.Text = "Estado";
                                t2.Cell(3, 1).Range.Text = "Imagen"; t2.Cell(3, 1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                                t2.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                                t2.Cell(3, 1).Range.Font.Bold = 1;
                                t2.Cell(3, 5).Merge(t2.Cell(3, 4));
                                t2.Cell(3, 4).Merge(t2.Cell(3, 3));
                                t2.Cell(3, 3).Merge(t2.Cell(3, 2));
                                //Falta for para llenar dinámicamente la tabla
                                pr2.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                                pr2.Range.InsertParagraphAfter();
                                pr2.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            }

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
                        DataTable dt1 = controlRepo.consultaHistoria(proyecto1.Value);
                        EntidadEjecucion[] ejecs1 = obtieneEjecuciones(dt1);
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
                        string c2 = "Fecha de generación de reporte: " + DateTime.Now.ToString() + "\n"; app.Selection.TypeText(c2);
                        string c1 = "Nombre de proyecto: " + proyecto1.Value + "\n"; app.Selection.TypeText(c1);
                        string c3 = "Fecha de ejecución: " + ejec1.Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                        string c4 = "Responsable de ejecución: " + ejec1.NombreResponsable + "\n"; app.Selection.TypeText(c4);
                        string c5 = "Propósito de diseño: " + diseno1.Value + "\n"; app.Selection.TypeText(c5);
                        string c6 = "Fecha de ejecución inicial: " + ejecs1[0].Fecha.ToString() + "\n"; app.Selection.TypeText(c6);
                        string c7 = "Fecha de última ejecución: " + ejecs1[1].Fecha.ToString() + "\n"; app.Selection.TypeText(c7);
                        string c8 = "Responsable de última ejecución: " + ejecs1[1].NombreResponsable + "\n\n"; app.Selection.TypeText(c8);

                        //Tabla calidad
                        Word.Paragraph pr = doc.Paragraphs.Add();
                        pr.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        Word.Table t = doc.Tables.Add(pr.Range, 5, 4);
                        //Llena dinámicamente la tabla
                        llenaTablaCalidad(t, conf1);
                        pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                        pr.Range.InsertParagraphAfter();
                        pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                        //Tabla no conformidades
                        Word.Paragraph pr2 = doc.Paragraphs.Add();
                        pr2.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        Word.Table t2 = doc.Tables.Add(pr2.Range, 8, 4);
                        //Llena dinámicamente la tabla
                        llenaTablaNoConf(t2, conf1);
                        pr2.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                        pr2.Range.InsertParagraphAfter();
                        pr2.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

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
                        t3.Cell(3, 1).Range.Font.Bold = 1;
                        t3.Cell(3, 5).Merge(t3.Cell(3, 4));
                        t3.Cell(3, 4).Merge(t3.Cell(3, 3));
                        t3.Cell(3, 3).Merge(t3.Cell(3, 2));
                        //Falta for para llenar dinámicamente la tabla
                        pr3.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                        pr3.Range.InsertParagraphAfter();
                        pr3.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                        if (ViewState["ejec2"] != null && ViewState["conf2"] != null)
                        {
                            DataTable dt2 = controlRepo.consultaHistoria(proyecto2.Value);
                            EntidadEjecucion[] ejecs2 = obtieneEjecuciones(dt2);
                            EntidadEjecucion ejec2 = (EntidadEjecucion)ViewState["ejec2"];
                            EntidadNoConformidad[] conf2 = (EntidadNoConformidad[])ViewState["conf2"];
                            doc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                            Word.Paragraph pr4 = doc.Paragraphs.Add();
                            pr2.Range.Select();
                            c1 = "Nombre de proyecto: " + proyecto2.Value + "\n"; app.Selection.TypeText(c1);
                            c3 = "Fecha de ejecución: " + ejec2.Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                            c4 = "Responsable de ejecución: " + ejec2.NombreResponsable + "\n"; app.Selection.TypeText(c4);
                            c5 = "Propósito de diseño: " + diseno2.Value + "\n"; app.Selection.TypeText(c5);
                            c6 = "Fecha de ejecución inicial: " + ejecs2[0].Fecha.ToString() + "\n"; app.Selection.TypeText(c6);
                            c7 = "Fecha de última ejecución: " + ejecs2[1].Fecha.ToString() + "\n"; app.Selection.TypeText(c7);
                            c8 = "Responsable de última ejecución: " + ejecs2[1].NombreResponsable + "\n\n"; app.Selection.TypeText(c8);
                            pr4 = doc.Paragraphs.Add();
                            //Tabla calidad
                            pr4.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            Word.Table t4 = doc.Tables.Add(pr4.Range, 5, 4);
                            //Llena dinámicamente la tabla
                            llenaTablaCalidad(t4, conf2);
                            pr4.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            pr4.Range.InsertParagraphAfter();
                            pr4.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                            //Tabla no conformidades
                            Word.Paragraph pr5 = doc.Paragraphs.Add();
                            pr5.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            Word.Table t5 = doc.Tables.Add(pr5.Range, 8, 4);
                            //Llena dinámicamente la tabla
                            llenaTablaNoConf(t5, conf2);
                            pr5.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            pr5.Range.InsertParagraphAfter();
                            pr5.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                            //Tabla estado
                            Word.Paragraph pr6 = doc.Paragraphs.Add();
                            pr6.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            Word.Table t6 = doc.Tables.Add(pr6.Range, 3, 5);
                            t6.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            t6.Borders.InsideColor = Word.WdColor.wdColorBlack;
                            t6.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            t6.Borders.OutsideColor = Word.WdColor.wdColorBlack;
                            t6.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                            t6.Rows[1].Range.Font.Bold = 1;
                            t6.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            t6.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            t6.Cell(1, 1).Range.Text = "Caso";
                            t6.Cell(1, 2).Range.Text = "Tipo";
                            t6.Cell(1, 3).Range.Text = "Descripción";
                            t6.Cell(1, 4).Range.Text = "Justificación";
                            t6.Cell(1, 5).Range.Text = "Estado";
                            t6.Cell(3, 1).Range.Text = "Imagen"; t6.Cell(3, 1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
                            t6.Cell(3, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                            t6.Cell(3, 1).Range.Font.Bold = 1;
                            t6.Cell(3, 5).Merge(t6.Cell(3, 4));
                            t6.Cell(3, 4).Merge(t6.Cell(3, 3));
                            t6.Cell(3, 3).Merge(t6.Cell(3, 2));
                            //Falta for para llenar dinámicamente la tabla
                            pr6.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            pr6.Range.InsertParagraphAfter();
                            pr6.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                            doc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);

                            //Llena dinámicamente la gráfica
                            List<DataPoint> a1 = obtieneAceptados(dt1);
                            List<DataPoint> a2 = obtieneAceptados(dt2);
                            var img = generaGrafica(a1, a2, proyecto1.Value, proyecto2.Value);
                            img.Save(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            Word.Paragraph pr7 = doc.Paragraphs.Add();
                            pr7.Range.Font.Bold = 1;
                            pr7.Range.Text = "Gráfica de porcentaje de casos aceptados en proyectos";
                            pr7.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            pr7.Range.Select();
                            app.Selection.MoveDown();
                            doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            pr7.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            pr7.Range.InsertParagraphAfter();
                            pr7.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                            //Llena dinámicamente la gráfica
                            List<DataPoint> na1 = obtieneNoAceptados(dt1);
                            List<DataPoint> na2 = obtieneNoAceptados(dt2);
                            var img2 = generaGrafica(na1, na2, proyecto1.Value, proyecto2.Value);
                            img2.Save(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            Word.Paragraph pr8 = doc.Paragraphs.Add();
                            pr8.Range.Text = "Gráfica de porcentaje de casos no aceptados en proyectos";
                            pr8.Range.Font.Bold = 1;
                            pr8.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            pr8.Range.Select();
                            app.Selection.MoveDown();
                            doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                        }
                        else
                        {
                            doc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                            //Falta for para llenar dinámicamente la gráfica
                            List<DataPoint> a1 = obtieneAceptados(dt1);
                            var img = generaGrafica(a1, proyecto1.Value);
                            img.Save(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            Word.Paragraph pr4 = doc.Paragraphs.Add();
                            pr4.Range.Font.Bold = 1;
                            pr4.Range.Text = "Gráfica de porcentaje de casos aceptados en proyecto";
                            pr4.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            pr4.Range.Select();
                            app.Selection.MoveDown();
                            doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            pr4.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                            pr4.Range.InsertParagraphAfter();
                            pr4.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                            //Llena dinámicamente la gráfica
                            List<DataPoint> na1 = obtieneAceptados(dt1);
                            var img2 = generaGrafica(na1, proyecto1.Value);
                            img2.Save(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            Word.Paragraph pr5 = doc.Paragraphs.Add();
                            pr5.Range.Text = "Gráfica de porcentaje de casos no aceptados en proyecto";
                            pr5.Range.Font.Bold = 1;
                            pr5.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            pr5.Range.Select();
                            app.Selection.MoveDown();
                            doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                        }

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
                            DataTable dt1 = controlRepo.consultaHistoria(proyecto1.Value);
                            EntidadEjecucion[] ejecs1 = obtieneEjecuciones(dt1);
                            //Creamos una instancia de una Aplicación Word. 
                            app = new Word.Application();
                            //Entonces a la aplicación Word, le añadimos un documento.
                            doc = app.Documents.Add();

                            //Activamos el documento recien creado, de forma que podamos escribir en el 
                            doc.Activate();
                            //Comenzamos a escribir dentro del documento.
                            app.Selection.Font.Size = 18;
                            app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            string tit = "Reporte de Progreso de Proyecto\n"; app.Selection.TypeText(tit);
                            app.Selection.Font.Size = 12;
                            app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                            string c2 = "Fecha de generación de reporte: " + DateTime.Now.ToString() + "\n"; app.Selection.TypeText(c2);

                            if (ViewState["ejec2"] != null && ViewState["conf2"] != null)
                            {
                                DataTable dt2 = controlRepo.consultaHistoria(proyecto2.Value);
                                EntidadEjecucion[] ejecs2 = obtieneEjecuciones(dt2);
                                doc.Words.Last.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
                                Word.Section s = doc.Sections.Add();
                                Word.TextColumn tc = s.PageSetup.TextColumns.Add();
                                s.Range.Select();
                                app.Selection.Font.Size = 12;
                                app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                                string c1 = "Nombre de proyecto: " + proyecto1.Value + "\n"; app.Selection.TypeText(c1);
                                string c3 = "Fecha de ejecución inicial: " + ejecs1[0].Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                                string c5 = "Fecha de última ejecución: " + ejecs1[1].Fecha.ToString() + "\n"; app.Selection.TypeText(c5);
                                string c4 = "Responsable de última ejecución: " + ejecs1[1].NombreResponsable + "\n"; app.Selection.TypeText(c4);
                                c1 = "Nombre de proyecto: " + proyecto2.Value + "\n"; app.Selection.TypeText(c1);
                                c3 = "Fecha de ejecución inicial: " + ejecs2[0].Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                                c4 = "Fecha de última ejecución: " + ejecs2[1].Fecha.ToString() + "\n"; app.Selection.TypeText(c4);
                                c5 = "Responsable de última ejecución: " + ejecs2[1].NombreResponsable + "\n"; app.Selection.TypeText(c5);
                                doc.Words.Last.InsertBreak(Word.WdBreakType.wdSectionBreakContinuous);
                                Word.Section s1 = doc.Sections.Add();
                                s1.Range.Select();
                                s1.Range.PageSetup.TextColumns.SetCount(1);
                                app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                                //Llena dinámicamente la gráfica
                                List<DataPoint> a1 = obtieneAceptados(dt1);
                                List<DataPoint> a2 = obtieneAceptados(dt2);
                                var img = generaGrafica(a1, a2, proyecto1.Value, proyecto2.Value);
                                img.Save(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                                Word.Paragraph pr = doc.Paragraphs.Add();
                                pr.Range.Font.Bold = 1;
                                pr.Range.Text = "Gráfica de porcentaje de casos aceptados en proyectos";
                                pr.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                pr.Range.Select();
                                app.Selection.MoveDown();
                                doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                                pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                                pr.Range.InsertParagraphAfter();
                                pr.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                                //Llena dinámicamente la gráfica
                                List<DataPoint> na1 = obtieneNoAceptados(dt1);
                                List<DataPoint> na2 = obtieneNoAceptados(dt2);
                                var img2 = generaGrafica(na1, na2, proyecto1.Value, proyecto2.Value);
                                img2.Save(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                                Word.Paragraph pr2 = doc.Paragraphs.Add();
                                pr2.Range.Text = "Gráfica de porcentaje de casos no aceptados en proyectos";
                                pr2.Range.Font.Bold = 1;
                                pr2.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                pr2.Range.Select();
                                app.Selection.MoveDown();
                                doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            }
                            else
                            {
                                string c1 = "Nombre de proyecto: " + proyecto1.Value + "\n"; app.Selection.TypeText(c1);
                                string c3 = "Fecha de ejecución inicial: " + ejecs1[0].Fecha.ToString() + "\n"; app.Selection.TypeText(c3);
                                string c5 = "Fecha de última ejecución: " + ejecs1[1].Fecha.ToString() + "\n"; app.Selection.TypeText(c5);
                                string c4 = "Responsable de última ejecución: " + ejecs1[1].NombreResponsable + "\n"; app.Selection.TypeText(c4);
                                app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                //Llena dinámicamente la gráfica
                                DataTable dt = controlRepo.consultaHistoria(proyecto1.Value);
                                List<DataPoint> a = obtieneAceptados(dt);
                                var img = generaGrafica(a, proyecto1.Value);
                                img.Save(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                                Word.Paragraph pr4 = doc.Paragraphs.Add();
                                pr4.Range.Text = "Gráfica de porcentaje de casos aceptados en proyecto";
                                pr4.Range.Font.Bold = 1;
                                pr4.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                pr4.Range.Select();
                                app.Selection.MoveDown();
                                doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                                pr4.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                                pr4.Range.InsertParagraphAfter();
                                pr4.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd);

                                //Falta for para llenar dinámicamente la gráfi}                                
                                List<DataPoint> na = obtieneNoAceptados(dt);
                                var img2 = generaGrafica(na, proyecto1.Value);
                                img2.Save(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                                Word.Paragraph pr5 = doc.Paragraphs.Add();
                                pr5.Range.Text = "Gráfica de porcentaje de casos no aceptados en proyecto";
                                pr5.Range.Font.Bold = 1;
                                pr5.Format.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                pr5.Range.Select();
                                app.Selection.MoveDown();
                                doc.InlineShapes.AddPicture(HttpRuntime.AppDomainAppPath + "ReportesTMP\\chtemp.jpg");
                            }

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
        }

        public void llenaTablaCalidad(Word.Table tabla, EntidadNoConformidad[] conf1)
        {

            tabla.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tabla.Borders.InsideColor = Word.WdColor.wdColorBlack;
            tabla.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tabla.Borders.OutsideColor = Word.WdColor.wdColorBlack;
            tabla.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
            tabla.Rows[1].Range.Font.Bold = 1;
            tabla.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            tabla.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            tabla.Cell(1, 1).Range.Text = "Estado";
            tabla.Cell(1, 2).Range.Text = "Casos";
            tabla.Cell(1, 3).Range.Text = "Cantidad";
            tabla.Cell(1, 4).Range.Text = "Porcentaje";
            tabla.Cell(2, 1).Range.Text = "Satisfactoria";
            tabla.Cell(3, 1).Range.Text = "Fallida";
            tabla.Cell(4, 1).Range.Text = "Cancelada";
            tabla.Cell(5, 1).Range.Text = "Pendiente";
            string[] ncs = new string[4]; ncs[0] = ""; ncs[1] = ""; ncs[2] = ""; ncs[3] = "";
            float[] acums = new float[4]; acums[0] = 0; acums[1] = 0; acums[2] = 0; acums[3] = 0;
            foreach (EntidadNoConformidad nc in conf1)
            {
                switch (nc.Estado)
                {
                    case "Satisfactoria":
                        acums[0]++;
                        ncs[0] += nc.IdCaso + " - Tipo: " + nc.Tipo + "\n";
                        break;
                    case "Fallida":
                        acums[1]++;
                        ncs[1] += nc.IdCaso + " - Tipo: " + nc.Tipo + "\n";
                        break;
                    case "Cancelada":
                        acums[2]++;
                        ncs[2] += nc.IdCaso + " - Tipo: " + nc.Tipo + "\n";
                        break;
                    case "Pendiente":
                        acums[3]++;
                        ncs[3] += nc.IdCaso + " - Tipo: " + nc.Tipo + "\n";
                        break;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (acums[i] == 0)
                {
                    ncs[i] = " ";
                }
                ncs[i] = ncs[i].Remove(ncs[i].Length - 1, 1);
                tabla.Cell(i + 2, 2).Range.Text = ncs[i];
                tabla.Cell(i + 2, 3).Range.Text = "" + acums[i];
                tabla.Cell(i + 2, 4).Range.Text = (acums[i] / conf1.Length) * 100 + "%";
            }
        }

        public void llenaTablaNoConf(Word.Table tabla, EntidadNoConformidad[] conf1)
        {

            tabla.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tabla.Borders.InsideColor = Word.WdColor.wdColorBlack;
            tabla.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tabla.Borders.OutsideColor = Word.WdColor.wdColorBlack;
            tabla.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorOrange;
            tabla.Rows[1].Range.Font.Bold = 1;
            tabla.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            tabla.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            tabla.Cell(1, 1).Range.Text = "No Conformidad";
            tabla.Cell(1, 2).Range.Text = "Casos";
            tabla.Cell(1, 3).Range.Text = "Cantidad";
            tabla.Cell(1, 4).Range.Text = "Porcentaje";
            tabla.Cell(2, 1).Range.Text = "Funcionalidad";
            tabla.Cell(3, 1).Range.Text = "Validación";
            tabla.Cell(4, 1).Range.Text = "Opciones que no funcionaban";
            tabla.Cell(5, 1).Range.Text = "Error de usabilidad";
            tabla.Cell(6, 1).Range.Text = "Excepciones";
            tabla.Cell(7, 1).Range.Text = "No correspondencia";
            tabla.Cell(8, 1).Range.Text = "Ortografía";
            string[] ncs = new string[7]; ncs[0] = ""; ncs[1] = ""; ncs[2] = ""; ncs[3] = ""; ncs[4] = ""; ncs[5] = ""; ncs[6] = "";
            float[] acums = new float[7]; acums[0] = 0; acums[1] = 0; acums[2] = 0; acums[3] = 0; acums[4] = 0; acums[5] = 0; acums[6] = 0;
            foreach (EntidadNoConformidad nc in conf1)
            {
                switch (nc.Tipo)
                {
                    case "Funcionalidad":
                        acums[0]++;
                        ncs[0] += nc.IdCaso + " - Estado: " + nc.Estado + "\n";
                        break;
                    case "Validación":
                        acums[1]++;
                        ncs[1] += nc.IdCaso + " - Estado: " + nc.Estado + "\n";
                        break;
                    case "Opciones que no funcionaban":
                        acums[2]++;
                        ncs[2] += nc.IdCaso + " - Estado: " + nc.Estado + "\n";
                        break;
                    case "Error de usabilidad":
                        acums[3]++;
                        ncs[3] += nc.IdCaso + " - Estado: " + nc.Estado + "\n";
                        break;
                    case "Excepciones":
                        acums[4]++;
                        ncs[4] += nc.IdCaso + " - Estado: " + nc.Estado + "\n";
                        break;
                    case "No correspondencia":
                        acums[5]++;
                        ncs[5] += nc.IdCaso + " - Estado: " + nc.Estado + "\n";
                        break;
                    case "Ortografía":
                        acums[6]++;
                        ncs[6] += nc.IdCaso + " - Estado: " + nc.Estado + "\n";
                        break;
                }
            }
            for (int i = 0; i < acums.Length; i++)
            {
                if (acums[i] == 0)
                {
                    ncs[i] = " ";
                }
                ncs[i] = ncs[i].Remove(ncs[i].Length - 1, 1);
                tabla.Cell(i + 2, 2).Range.Text = ncs[i];
                tabla.Cell(i + 2, 3).Range.Text = "" + acums[i];
                tabla.Cell(i + 2, 4).Range.Text = (acums[i] / conf1.Length) * 100 + "%";
            }
        }

        public List<DataPoint> obtieneAceptados(DataTable dt)
        {
            int idEjecActual = -1;
            List<DataPoint> res = new List<DataPoint>();
            float tot = 0;
            float acum = 0;
            DateTime fecha = DateTime.Today;
            foreach (DataRow r in dt.Rows)
            {
                if (idEjecActual != (int)r[2])
                {
                    if (idEjecActual != -1)
                    {
                        DataPoint pt = new DataPoint(fecha.ToOADate(), (acum / tot) * 100);
                        res.Add(pt);
                    }
                    idEjecActual = (int)r[2];
                    acum = 0;
                    tot = 0;
                    fecha = (DateTime)r[3];
                }
                if ((string)r[1] == "Satisfactoria")
                {
                    acum += (int)r[0];
                }
                if ((string)r[1] != "Cancelada")
                {
                    tot += (int)r[0];
                }

            }
            DataPoint pt2 = new DataPoint(fecha.ToOADate(), (acum / tot) * 100);
            res.Add(pt2);

            return res;
        }

        public List<DataPoint> obtieneNoAceptados(DataTable dt)
        {
            int idEjecActual = -1;
            List<DataPoint> res = new List<DataPoint>();
            float tot = 0;
            float acum = 0;
            DateTime fecha = DateTime.Today;
            foreach (DataRow r in dt.Rows)
            {
                if (idEjecActual != (int)r[2])
                {
                    if (idEjecActual != -1)
                    {

                        DataPoint pt = new DataPoint(fecha.ToOADate(), (acum / tot) * 100);
                        res.Add(pt);
                    }
                    idEjecActual = (int)r[2];
                    acum = 0;
                    tot = 0;
                    fecha = (DateTime)r[3];
                }
                if ((string)r[1] == "Fallida" || (string)r[1] == "Pendiente")
                {
                    acum += (int)r[0];
                }
                if ((string)r[1] != "Cancelada")
                {
                    tot += (int)r[0];
                }
            }
            DataPoint pt2 = new DataPoint(fecha.ToOADate(), (acum / tot) * 100);
            res.Add(pt2);

            return res;
        }

        public EntidadEjecucion[] obtieneEjecuciones(DataTable dt)
        {
            int idEjecActual = -1;
            EntidadEjecucion[] res = new EntidadEjecucion[2];
            foreach (DataRow r in dt.Rows)
            {
                if (idEjecActual != (int)r[2])
                {
                    if (idEjecActual == -1)
                    {
                        res[0] = controlRepo.consultarEjecucion((int)r[2]);
                    }
                    idEjecActual = (int)r[2];
                }
            }
            res[1] = controlRepo.consultarEjecucion(idEjecActual);

            return res;
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

        public System.Drawing.Image generaGrafica(IList<DataPoint> series, string Pr1)
        {
            using (MemoryStream graph = new MemoryStream())
            using (var ch = new Chart())
            {
                ch.ChartAreas.Add(new ChartArea());

                var s = new Series();
                foreach (var pnt in series) s.Points.Add(pnt);
                s.XValueType = ChartValueType.DateTime;
                ch.Series.Add(s);
                s.ChartType = SeriesChartType.FastLine;
                s.Color = Color.Red;
                s.Name = Pr1;

                ch.Size = new Size(600, 400);
                ch.SaveImage(graph, ChartImageFormat.Jpeg);
                ch.Legends.Add("leyenda");

                MemoryStream ms = new MemoryStream(graph.GetBuffer());
                var image = System.Drawing.Image.FromStream(ms);
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
                s.XValueType = ChartValueType.DateTime;
                ch.Series.Add(s);
                s.ChartType = SeriesChartType.FastLine;
                s.Color = Color.Red;
                s.Name = Pr1;

                var s1 = new Series();
                foreach (var pnt in series2) s1.Points.Add(pnt);
                s1.XValueType = ChartValueType.DateTime;
                ch.Series.Add(s1);
                s1.ChartType = SeriesChartType.FastLine;
                s1.Color = Color.Blue;
                s1.Name = Pr2;

                ch.Size = new Size(600, 400);
                ch.Legends.Add("leyenda");
                ch.SaveImage(graph, ChartImageFormat.Jpeg);
                MemoryStream ms = new MemoryStream(graph.GetBuffer());
                var image = System.Drawing.Image.FromStream(ms);

                return image;
            }
        }


    }
}