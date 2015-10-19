using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1.App_Code
{
    public class ControladoraProyecto
    {
        private EntidadProyecto nuevoProyecto; //instanciade la entidad proyecto  
        private ControladoraBDProyecto controladoraBDProyecto; // instancia de controladora de BD de proyecto

        public ControladoraProyecto()
        {
            controladoraBDProyecto = new ControladoraBDProyecto();
        }
       
        public string ejecutarProyecto(int accion, Object[] datos, Object[] originales)
        {
            string resultado = "Exito";

            switch (accion)
            {
                case 1:
                    {
                        nuevoProyecto = new EntidadProyecto(datos);
                        resultado = controladoraBDProyecto.insertarProyecto(nuevoProyecto);
                    }
                    break;

                case 4:
                    {
                        controladoraBDProyecto.eliminarProyecto(datos[0].ToString()); 
                    }
                    break;
            }


            return resultado;
        }

        public List<string> seleccionarLideres()
        {
            List<string> lideres = controladoraBDProyecto.traerLideres();

            return lideres;
        }

        public int revisarExistentes(string nomP, string nomOf)
        {
            int resultado;
            resultado = controladoraBDProyecto.revisarExistentes(nomP, nomOf);
            return resultado;
        }

        public void insertarTel2(string tel2, string of)
        {
            controladoraBDProyecto.insertarTel2(tel2, of);
        }

        public DataTable consultar_Total_ProyectoFiltro(string nombreFiltro)
        {
            try
            {
                return controladoraBDProyecto.consultar_Total_ProyectoFiltro(nombreFiltro);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        //obtener una lista con todos los proyectos
        public DataTable consultar_Total_Proyecto()
        {
            try
            {
                return controladoraBDProyecto.consultar_Total_Proyecto();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        //obtener los datos de un proyecto
        public EntidadProyecto consultarProyecto(string nombre)
        {
            try
            {
                return controladoraBDProyecto.consultar_Proyecto(nombre);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public string getPerfil(string usuario)
        {
            string resultado = controladoraBDProyecto.getPerfil(usuario);
            return resultado;
        }

        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        public static int SafeGetInt32(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            else
                return -1;
        }
        
        public List<EntidadRecursoH> getRecursosDisponibles()
        {
            List<EntidadRecursoH> recursos = new List<EntidadRecursoH>();
            
            try
            {
                SqlDataReader reader = controladoraBDProyecto.getRecursosDisponibles();
                while (reader.Read())
                {
                    int cedula = SafeGetInt32(reader, 0);
                    String nombre = SafeGetString(reader, 1);
                    String pApellido = SafeGetString(reader, 2);
                    String sApellido = SafeGetString(reader, 3);
                    String rol = SafeGetString(reader, 4);

                    EntidadRecursoH rh = new EntidadRecursoH(cedula, nombre, pApellido, sApellido, rol);
                    recursos.Add(rh);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return recursos;
        }

        public void asignarProyectoAEmpleado(string nombreProyecto, EntidadRecursoH e)
        {
            controladoraBDProyecto.asignarProyectoAEmpleado(e.Cedula.ToString(), nombreProyecto);
        }

        public void cambiarEstado(string nombreP)
        {
            controladoraBDProyecto.cambiarEstado(nombreP);
        }
    }
}