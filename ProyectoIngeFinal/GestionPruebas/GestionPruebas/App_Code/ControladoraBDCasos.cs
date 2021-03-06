﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace GestionPruebas.App_Code
{
    public class ControladoraBDCasos
    {
        //Clase que controla el acceso a la base de datos

        private AccesoBaseDatos baseDatos;

        public AccesoBaseDatos BaseDatos
        {
            get { return baseDatos; }
            set { baseDatos = value; }
        }

        /*
         * Descripción: Constructor por defecto de la controladora.
         * Requiere: Nada.
         * Retorna: La controladora nueva.
         */
        public ControladoraBDCasos()
        {
            BaseDatos = new AccesoBaseDatos();
        }

        /** 
         * Descripción: Obtiene el campo String de la tabla de forma segura (revisando si es null o no antes de leerlo)
         * Recibe un SqlDataReader con el que se obtiene el campo y el índice de la columna a consultar
         * Devuelve un valor String dependiendo del resultado de la consulta. String.empty si el campo está nulo
         */
        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        /** 
          * Descripción: Obtiene el campo entero de la tabla de forma segura (revisando si es null o no antes de leerlo)
          * Recibe un SqlDataReader con el que se obtiene el campo y el índice de la columna a consultar
          * Devuelve un valor entero dependiendo del resultado de la consulta. -1 si el campo está nulo
          */
        public static int SafeGetInt32(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            else
                return -1;
        }

        /*
         * Descripción: Inserta un caso en la BD.
         * Recibe: el objeto Entidad Caso.
         * Retorna: n/a.
         */
        public int insertarCaso(EntidadCaso caso)
        {
            int resultado = 1;
            string consulta = "";

            consulta = "INSERT INTO CasoPrueba (id,proposito,entrada,resultadoEsperado,flujoCentral,idDise) VALUES (@0, @1, @2, @3, @4, @5);";
            Object[] args = new Object[6];
            args[0] = caso.Id;
            args[1] = caso.Proposito;
            args[2] = caso.Entrada;
            args[3] = caso.ResultadoEsperado;
            args[4] = caso.FlujoCentral;
            args[5] = caso.IdDise;

            try
            {

                SqlDataReader res = baseDatos.ejecutarConsulta(consulta, args);
                res.Close();
            }
            catch (SqlException ex)
            {
                //resultado = "Error al insertar. Error 1: " + ex.Message;
                throw ex;
            }
            
            return resultado;
        }

        /*
         * Descripción: Inserta un caso en la BD.
         * Recibe:
         * Retorna: n/a.
         */
        public int modificaCaso(EntidadCaso caso, string idV, int idDiseV)
        {
            //Si no se modificó el usuario correctamente se devuelve -1
            int resultado = -1;
            string consulta = "";
            
            try
            {
                consulta = " UPDATE CasoPrueba Set id= '" + caso.Id + "', proposito= '" + caso.Proposito + "', entrada='" + caso.Entrada + "', resultadoEsperado = '" + caso.ResultadoEsperado + "', flujoCentral='" + caso.FlujoCentral + "' WHERE id = '" + idV + "' AND idDise = " + idDiseV + "; ";
                                   
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                
                if (reader.RecordsAffected > 0)
                {
                    resultado = 0; 
                }
                reader.Close();
            }
            //En caso de una excepcion SQL se tira para tratarla en la capa superior
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        
        }

        public DataTable consultarCasos(string idDise)
        {
            DataTable data = new DataTable();

            try
            {
                string consulta = "SELECT id as 'ID Caso', proposito as 'Propósito', entrada as 'Entrada', resultadoEsperado as 'Resultado esperado', flujoCentral as 'Flujo central' FROM CasoPrueba WHERE idDise = "+idDise+";";
                data = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return data;
        }

        public EntidadCaso consultaCaso(string id, string idDis)
        {
            //Hace la consulta de todos los campos
            string consulta = "SELECT c.id, c.proposito, c.entrada, c.resultadoEsperado, c.flujoCentral, d.id, d.idProy FROM Diseno d, CasoPrueba c WHERE c.id = '" + id + "' AND c.idDise = '" + idDis + "' AND c.idDise = d.id";

            //Inicialice variables locales
            EntidadCaso caso = null;

            String idCaso = "";
            String proposito = "";
            String entrada = "";
            String resultadoEsperado = "";
            String flujoCentral = "";
            int idDise = 0;
            int idProy = 0;

            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                try
                {
                    if (reader.Read())
                    {
                        //Si pudo leer, obtenga los datos de forma segura
                        idCaso = SafeGetString(reader, 0);
                        proposito = SafeGetString(reader, 1);
                        entrada = SafeGetString(reader, 2);
                        resultadoEsperado = SafeGetString(reader, 3);
                        flujoCentral = SafeGetString(reader, 4);
                        idDise = SafeGetInt32(reader, 5);
                        idProy = SafeGetInt32(reader, 6);
                       
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

                //Encapsulo los datos
                caso = new EntidadCaso(idCaso, proposito, entrada, resultadoEsperado, flujoCentral, idDise, idProy);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return caso;
        }

        /**
         * Requiere: string usuario
         * Retorna: string
         * Consulta la tabla RRHH y devuelve el tipo de perfil del usuario.
         */
        public string getPerfil(string usuario)
        {
            string resultado = "";
            try
            {
                string consulta = "Select perfil from Usuario where nomUsuario = '" + usuario + "'";
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.Read())
                {
                    resultado = reader.GetString(0);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }

        public string consultarReq(string idDis)
        {
            string resultado = "";
            try
            {
                string consulta = "SELECT cr.idReq FROM DisenoRequerimiento cr WHERE  cr.idDise = '" + idDis +"'";
                
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                while (reader.Read())
                {
                    string s = reader.GetString(0) + "\n";
                    resultado += s;
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }

        public Object[] hacerResumen(string idDiseno)
        {
            Object[] nuevo = new Object[3];

            try
            {
                string consulta = "Select p.nombre,d.nivel,d.proposito from Diseno d Join Proyecto p On p.id = d.idProy where d.id = '" + idDiseno +"'";
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if(reader.Read())
                {
                    nuevo[0] = reader.GetString(0);
                    nuevo[1] = reader.GetString(1);
                    nuevo[2] = reader.GetString(2);
                }
                reader.Close();
            }
            catch(SqlException ex)
            {
                throw ex;
            }

            return nuevo;
        }

        internal int eliminarCaso(string idCaso, string idDise)
        {
            int resultado = 0;
            try
            {
                string consulta = "DELETE FROM CasoPrueba WHERE id ='" + idCaso + "' AND idDise =" + idDise + ";";

                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);

                resultado = reader.RecordsAffected;
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }
    }
}