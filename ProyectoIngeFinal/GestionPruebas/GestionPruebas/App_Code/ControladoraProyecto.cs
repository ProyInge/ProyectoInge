﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace GestionPruebas.App_Code
{
    public class ControladoraProyecto
    {
        private EntidadProyecto nuevoProyecto; //instanciade la entidad proyecto  
        private ControladoraBDProyecto controladoraBDProyecto; // instancia de controladora de BD de proyecto

        /* Descripcion: Constructor de ControladoraProyecto
        * 
        * REQ: N/A
        * 
        * RET: N/A
        */

        public ControladoraProyecto()
        {
            controladoraBDProyecto = new ControladoraBDProyecto();
        }

        /* Descripcion: Casos que revisan que accion ejecutar dentro del IMEC
        * 
        * REQ: int,Object,Object
        * 
        * RET: string
        */
       
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

        /* Descripcion: Trae los usuarios que son Lideres
        * 
        * REQ: N/A
        * 
        * RET: List<string>
        */

        public List<string> seleccionarLideres()
        {
            List<string> lideres = controladoraBDProyecto.traerLideres();

            return lideres;
        }

        /* Descripcion: Metodo que verifica la informacion repetida
        * 
        * REQ: string,string
        * 
        * RET: int
        */

        public int revisarExistentes(string nomP, string nomOf)
        {
            int resultado;
            resultado = controladoraBDProyecto.revisarExistentes(nomP, nomOf);
            return resultado;
        }

        /* Descripcion: Caso aparte en caso de tener que insertar el Telefono2,
        * 
        * REQ: string numero de Telefono , string Oficina a la que se va a asociar
        * 
        * RET: N/A
        */

        public void insertarTel2(string tel2, string of)
        {
            controladoraBDProyecto.insertarTel2(tel2, of);
        }

        /* Descripcion: Consulta la informacion completa de los proyectos con filtro
        * 
        * REQ: string
        * 
        * RET: DataTable
        */

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

        /* Descripcion: Consulta la informacion completa de todos los proyectos
        * 
        * REQ: N/A
        * 
        * RET: DataTable
        */

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

        /* Descripcion: Encapsula la informacion consultada
        * 
        * REQ: string
        * 
        * RET: EntidadProyecto
        */

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

        /* Descripcion: Trae el perfil del usuario
        * 
        * REQ: string
        * 
        * RET: string
        */

        public string getPerfil(string usuario)
        {
            string resultado = controladoraBDProyecto.getPerfil(usuario);
            return resultado;
        }

        /* Descripcion: 
        * 
        * REQ: SqlDataReader, int
        * 
        * RET: static string
        */

        /* Descripcion: Trae los recursos disponibles para asignar
        * 
        * REQ: N/A
        * 
        * RET: List<EntidadRecursoH>
        */
        
        public List<EntidadRecursoH> getRecursosDisponibles()
        {
            return controladoraBDProyecto.getRecursosDisponibles();
        }

        /* Descripcion: Trae los recursos disponibles para asignar
        * 
        * REQ: N/A
        * 
        * RET: List<EntidadRecursoH>
        */

        public List<EntidadRecursoH> getRecursosAsignados(string nomP)
        {
            return controladoraBDProyecto.getRecursosAsignados(nomP);
        }

        /* Descripcion: Asigna el proyecto al recurso asignado
        * 
        * REQ: string, EntidadRecursoH
        * 
        * RET: N/A
        */

        public void asignarProyectoAEmpleado(string nombreProyecto, EntidadRecursoH e)
        {
            controladoraBDProyecto.asignarProyectoAEmpleado(e.Cedula.ToString(), nombreProyecto);
        }

        /* Descripcion: Cambia el Estado del Proyecto cuando un Miembro elimina
        * 
        * REQ: string
        * 
        * RET: N/A
        */

        public void cambiarEstado(string nombreP)
        {
            controladoraBDProyecto.cambiarEstado(nombreP);
        }

        /* Descripcion: Caso Especial de consulta que diferencia Administrador de Miembro de Equipo
       * 
       * REQ: string
       * 
       * RET: EntidadProyecto
       */

        public EntidadProyecto consultarProyectoMiembro(string nombreUsuario) {
            //consulta el proyecto en el que el miembro loggeado participa
            try
            {
                return controladoraBDProyecto.consultarProyectoM(nombreUsuario);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            
        }
        public EntidadProyecto actProy(Object[] datos, Object[] originales)
        {
            //consulta el proyecto en el que el miembro loggeado participa
            try
            {
                return controladoraBDProyecto.actualizaProyecto(datos, originales);
            }
            catch (SqlException ex)
            {
                throw ex;
            }



        }

        internal void desasignarRecurso(EntidadRecursoH rec)
        {
            try
            {
                controladoraBDProyecto.desasignarRecurso(rec);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        internal void asignarRecurso(EntidadRecursoH rec, object p)
        {
            try
            {
                controladoraBDProyecto.asignarRecurso(rec, (string)p);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}