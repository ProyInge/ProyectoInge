using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    }
}