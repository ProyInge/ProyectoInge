using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionPruebas.App_Code;
using System.Data;
using System.Drawing;
using System.Diagnostics;
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
            if (ejecucion != null && conformidades != null)
            {

                if (formato.Value == "pdf")
                {
                    string plantillaCalidad = "./Plantillas/calidad.pdf";
                    string plantillaEstado = "./Plantillas/estado.docx";
                    string salida = "";
                    //Usa itextsharp para crear reporte
                    if (tipo.Value == "calidad")
                    {
                        byte[] bytes;
                        
                        using (var existingFileStream = new FileStream(Server.MapPath(plantillaCalidad), FileMode.Open))
                        using (MemoryStream stream = new MemoryStream())
                        {
                            // Open existing PDF
                            var pdfReader = new PdfReader(existingFileStream);
                            var stamper = new PdfStamper(pdfReader, stream);
                            var form = stamper.AcroFields;
                            var fieldKeys = form.Fields.Keys;

                            foreach (string fieldKey in fieldKeys)
                            {
                                form.RenameField(fieldKey, fieldKey + "AQUI VA LO QUE METE EN EL FIELDKEY");
                            }
                            // "Flatten" the form so it wont be editable/usable anymore
                            stamper.FormFlattening = true;
                            stamper.Close();
                            pdfReader.Close();
                            bytes = stream.ToArray();
                        }
                        
                        PdfCopyFields _copy = new PdfCopyFields(new FileStream(Server.MapPath(salida), FileMode.Create));
                        _copy.AddDocument(new PdfReader(bytes));
                        //_copy.AddDocument(new PdfReader(new FileStream(Server.MapPath(plantillaCalidad), FileMode.Open)));
                        _copy.Close();
                        //ConcatenateForms
                    }
                    else if (tipo.Value == "estado")
                    {

                    }
                }
                else
                {
                    //Usa microsoft.Word

                }
            }
            else
            {
                string faltante = "Debe seleccionar una ejecucion desde la vista de ejecuciones primero.";
                //textoAlerta.InnerHtml = "Seleccione un Proyecto a Modificar";
                //alerta.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alerta", "alerta('" + faltante + "')", true);
            }
        }

        private void descargaReporte(MemoryStream ms)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment;filename=abc.pdf");
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
            ms.Close();
        }


    }
}