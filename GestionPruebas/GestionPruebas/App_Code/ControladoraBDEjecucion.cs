﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

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

        public int insertarEjecucion(EntidadEjecucion ent)
        {
            int resultado = 2;

            try
            {
                string consulta = "insert into Ejecuciones (fecha, incidencias, cedResp,idDise, idProy) values( @0,  @1, @2,  @3);";

                Object[] dis = new Object[5];
                dis[0] = ent.Fecha;
                dis[1] = ent.Incidencias;
                dis[2] = ent.NombreResponsable;
                dis[3] = ent.IdDise;
                dis[4] = ent.IdProy;
 
                SqlDataReader dr = baseDatos.ejecutarConsulta(consulta);
                if (dr.RecordsAffected > 0)
                {
                    //Todo bien, todo sano
                    resultado = 0;
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
            try
            {
                 baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public string modifica_NC(EntidadNoConformidad noConf_ant, EntidadNoConformidad noConf_nuev )
        {
            string resultado = "";
            try
            {
                string consulta = "UPDATE from NoConformidad set  descripcion='" +noConf_nuev.Descripcion + "', justificacion= '" +noConf_nuev.Justificacion + "' , estado='" +noConf_nuev.Estado+ "'" +
                "where idTupla = ";//'" + noConf_ant.id + "' and idEjecucion= '" + noConf_ant.idEjecucion + "';";                

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
    }
}