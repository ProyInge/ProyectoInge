using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace GestionPruebas.App_Code
{
    public class ControladoraBDEjecucion
    {
        private AccesoBaseDatos baseDatos;

        public ControladoraBDEjecucion()
        {
            baseDatos = new AccesoBaseDatos();
        }

        public void modificarEjecucion(EntidadEjecucion entidad)
        {

        }

        public int insertarEjecucion(EntidadEjecucion ent, List<EntidadNoConformidad> listaConf)
        {
            int resultado = 2;
            int idEjec = 0;

            try
            {
                string consulta = "insert into Ejecuciones (fecha, incidencias, cedResp,idDise, idProy) values( @0,  @1, @2,  @3, @4);";

                Object[] dis = new Object[5];
                dis[0] = ent.Fecha;
                dis[1] = ent.Incidencias;
                dis[2] = ent.Responsable;
                dis[3] = ent.IdDise;
                dis[4] = ent.IdProy;
 
                SqlDataReader dr = baseDatos.ejecutarConsulta(consulta,dis);
                if (dr.RecordsAffected > 0)
                {
                    //Todo bien, todo sano
                    resultado = 0;
                }
                dr.Close();

                consulta = "Select Max(id) from Ejecuciones";
                SqlDataReader read = baseDatos.ejecutarConsulta(consulta);
                while (read.Read())
                {
                    idEjec = read.GetInt32(0);
                }
                read.Close();

                if (listaConf != null)
                {
                    for (int i = 0; i < listaConf.Count; i++)
                    {
                        consulta = "insert into NoConformidad (idEjecucion, idDise, idCaso, tipo, descripcion, justificacion, estado,imagen) values (@0,@1,@2,@3,@4,@5,@6,@7)";

                        Object[] dist = new Object[8];
                        dist[0] = idEjec;
                        dist[1] = listaConf.ElementAt(i).IdDise;
                        dist[2] = listaConf.ElementAt(i).IdCaso;
                        dist[3] = listaConf.ElementAt(i).Tipo;
                        dist[4] = listaConf.ElementAt(i).Descripcion;
                        dist[5] = listaConf.ElementAt(i).Justificacion;
                        dist[6] = listaConf.ElementAt(i).Estado;
                        dist[7] = listaConf.ElementAt(i).Imagen;

                        SqlDataReader ddr = baseDatos.ejecutarConsulta(consulta, dist);
                        if (dr.RecordsAffected > 0)
                        {
                            //Todo bien, todo sano
                            resultado = 0;
                        }
                        ddr.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                resultado = e.Number;
            }

            return resultado;
        }

        public Object[] hacerResumen(int idEje)
        {
            Object[] nuevo = new Object[3];
            try
            {
                string consulta = "SELECT p.nombre,d.nivel,d.proposito FROM Diseno d, Proyecto p, Ejecuciones e WHERE p.id = d.idProy AND d.id = e.idDise AND e.id = '" + idEje + "'";
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.Read())
                {
                    nuevo[0] = reader.GetString(0);
                    nuevo[1] = reader.GetString(1);
                    nuevo[2] = reader.GetString(2);
                }
                reader.Close();
                return nuevo;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public void eliminarNC(int idNC)
        {
            SqlDataReader reader;
            try
            {
                reader = baseDatos.ejecutarConsulta("DELETE FROM NoConformidad WHERE idTupla = " + idNC + ";");
                if (reader.RecordsAffected > 0)
                {
                    reader.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public string consultarReq(int idEje)
        {
            string resultado = "";
            try
            {
                string consulta = "SELECT cr.idReq FROM DisenoRequerimiento cr, Ejecuciones e WHERE cr.idDise=e.idDise AND e.id = " + idEje + ";";

                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                while (reader.Read())
                {
                    string s = reader.GetString(0) + "\n";
                    resultado += s;
                }
                reader.Close();
                return resultado;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable consultarEjecuciones(string idProy, string idDise)
        {
            string consulta = "SELECT e.id, e.fecha, e.incidencias, e.cedResp, CONCAT(u.pNombre, ' ', u.pApellido) AS 'n' FROM Ejecuciones e, Usuario u WHERE e.cedResp = u.cedula AND e.idProy = '" + idProy + "' AND e.idDise = " + idDise + ";";
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

        public DataTable consultarNoConformidades(string idEjecucion)
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

        public DataTable consultarEjecucionesDt(string idProy, string idDise)
        {
            string consulta = "SELECT e.id AS 'ID', e.fecha AS 'Fecha última ejecución' FROM Ejecuciones e, Usuario u WHERE e.cedResp = u.cedula AND e.idProy = '" + idProy + "' AND e.idDise = " + idDise + ";";
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

        public void eliminarEjecucion(string id)
        {
            string consulta = "DELETE FROM Ejecuciones WHERE id = '"+id+"';";
            SqlDataReader reader;
            try
            {
                 reader = baseDatos.ejecutarConsulta(consulta);

                 if (reader.RecordsAffected > 0)
                 {
                     reader.Close();
                 }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int modifica_Ejec(EntidadEjecucion enEjec,  List<EntidadNoConformidad> listaConf)
        {
            int resultado = 0;

            DateTime fecha = enEjec.Fecha;
            string fec = fecha.ToString("yyy-MM-dd", CultureInfo.InvariantCulture);
            try
            {
                string consulta = "Update Ejecuciones Set fecha ='"+ fec +"', incidencias ='" + enEjec.Incidencias + "',cedResp ='" + enEjec.Responsable + "'"+
                    "where id =  '"+enEjec.Id +"';";

                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.RecordsAffected > 0)
                {
                    //Todo bien, todo sano
                    resultado = 1;
                }
                reader.Close();
            }
            catch (SqlException e)
            {
                throw e;
                // return -1;
            }
            for (int i = 0; i < listaConf.Count; i++)
            {
                if (listaConf.ElementAt(i).Id == -1)
                {
                    try
                    {
                        string consulta = "insert into NoConformidad (idEjecucion, idDise, idCaso, tipo, descripcion, justificacion, estado,imagen) values (@0,@1,@2,@3,@4,@5,@6,@7)";
                        Object[] dist = new Object[8];
                        dist[0] = listaConf.ElementAt(i).IdEjecu;
                        dist[1] = listaConf.ElementAt(i).IdDise;
                        dist[2] = listaConf.ElementAt(i).IdCaso;
                        dist[3] = listaConf.ElementAt(i).Tipo;
                        dist[4] = listaConf.ElementAt(i).Descripcion;
                        dist[5] = listaConf.ElementAt(i).Justificacion;
                        dist[6] = listaConf.ElementAt(i).Estado;
                        dist[7] = listaConf.ElementAt(i).Imagen;

                        SqlDataReader dr = baseDatos.ejecutarConsulta(consulta, dist);
                        if (dr.RecordsAffected > 0)
                        {
                            //Todo bien, todo sano
                            //resultado+=1;
                        }
                        dr.Close();
                    }
                    catch (SqlException e)
                    {
                        throw e;
                        //return -1;                    
                    }
                }
                else
                {
     
                        try
                        {
                            string consulta = "UPDATE NoConformidad set tipo ='" + listaConf.ElementAt(i).Tipo + "', idCaso='" + listaConf.ElementAt(i).IdCaso + "' , descripcion='" + listaConf.ElementAt(i).Descripcion + "', justificacion= '" + listaConf.ElementAt(i).Justificacion + "' , estado='" + listaConf.ElementAt(i).Estado + "'" +
                            " where idTupla = " + listaConf.ElementAt(i).Id + " and idEjecucion= '" + listaConf.ElementAt(i).IdEjecu + "';";
                            SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                            if (reader.RecordsAffected > 0)
                            {
                                //Todo bien, todo sano
                                resultado=1;
                            }
                            reader.Close();
                        }
                        catch (SqlException s)
                        {
                        throw s;   
                        //return -1;
                           
                        }
                }
            }           
            return resultado;
        }

        public List<string> traerResp(string idProy)
        {
            string consulta = "Select id from Proyecto where nombre = '" + idProy +"'";
            int idP = 0;
            List<string> lista = new List<string>();

            try
            {
                SqlDataReader read = baseDatos.ejecutarConsulta(consulta);
                while (read.Read())
                {
                    idP = read.GetInt32(0);
                }
                read.Close();

                consulta = "Select CONCAT(pNombre, ' ', pApellido, ' ', sApellido, '(',cedula,')') from Usuario where idProy = '" + idP + "'"; 
           

                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        lista.Add(reader.GetString(0));
                        i++;
                    }
                }
                reader.Close();
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            
            return lista;
        }

        public List<string> traerCasos(string idDise)
        {
            string consulta = "Select id from CasoPrueba where idDise = '" + idDise + "'"; 
            List<string> lista = new List<string>();
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lista.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return lista;
        }

        public string nombrarProy(string idDise)
        {
            string consulta = "Select P.nombre from Proyecto P Join Diseno D on D.idProy = P.id where D.id = '" + idDise + "'";
            string resultado = "";
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                while (reader.Read())
                {
                    resultado = reader.GetString(0);
                }
                reader.Close();
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }

    }
}