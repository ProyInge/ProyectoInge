using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace GestionPruebas.App_Code
{
    public class ControladoraBDReporte
    {
        private AccesoBaseDatos baseDatos;

        public ControladoraBDReporte()
        {
            baseDatos = new AccesoBaseDatos();
        }

        /** Descripcion: Consulta total de un proyecto por filtro 
         * REQ: string 
         * RET: DataTable
         */
        public DataTable consultaProyectos()
        {
            string consulta = "";

            DataTable data = new DataTable();
            consulta = "SELECT  nombre, id FROM Proyecto;";
            try
            {
                data = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return data;
        }

        /** Descripcion: Consulta total de un proyecto por filtro 
         * REQ: string 
         * RET: DataTable
         */
        public DataTable consultaProyecto(string usuario)
        {
            string consulta = "";

            DataTable data = new DataTable();
            consulta = "SELECT  nombre, id FROM proyecto p, usuario u"
                + " WHERE u.nomUsuario='" + usuario + "' AND p.id=u.idProy;";
            try
            {
                data = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return data;
        }

        public DataTable consultaDisenos(int idProy)
        {
            string consulta = "";

            DataTable data = new DataTable();
            consulta = "SELECT  d.proposito, d.id FROM diseno d"
                + " WHERE d.idproy=" + idProy + " ;";
            try
            {
                data = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return data;
        }

        public DataTable consultaEjecuciones(int idDise)
        {
            string consulta = "";

            DataTable data = new DataTable();
            consulta = "SELECT  e.fecha, e.id FROM ejecuciones e"
                + " WHERE e.idDise=" + idDise + " ;";
            try
            {
                data = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return data;
        }

        public EntidadEjecucion consultarEjecucion(int idEjec)
        {
            string consulta = "SELECT e.id, e.cedResp, CONCAT(u.pNombre, ' ', u.pApellido, ' ', u.sApellido), e.fecha, e.incidencias, e.idDise, e.idProy FROM Ejecuciones e, Usuario u "
                            + "WHERE e.id="+idEjec+" AND u.cedula=e.cedResp;";
            int cedResp = -1;
            string nombre = "";
            DateTime fecha = DateTime.Now;
            string incidencias = "";
            int idDise = -1;
            string idProy = "";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.Read())
                {
                    cedResp = reader.GetInt32(1);
                    nombre = reader.GetString(2);
                    fecha = reader.GetDateTime(3);
                    incidencias = reader.GetString(4);
                    idDise = reader.GetInt32(5);
                    idProy = "" +reader.GetInt32(6);
                }
                reader.Close();

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            EntidadEjecucion ejec = new EntidadEjecucion(idEjec, cedResp, nombre, fecha, incidencias, idDise, idProy);
            return ejec;
        }

        public DataTable consultarNoConformidades(int idEjecucion)
        {
            string consulta = "SELECT * FROM NoConformidad WHERE idEjecucion = " + idEjecucion + ";";
            DataTable data = null;
            try
            {
                //Obtengo la tabla
                data = baseDatos.ejecutarConsultaTabla(consulta);

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;
        }

    }
}