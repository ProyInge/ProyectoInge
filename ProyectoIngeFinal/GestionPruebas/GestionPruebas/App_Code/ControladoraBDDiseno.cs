using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace GestionPruebas.App_Code
{
    public class ControladoraBDDiseno
    {
        //Clase que controla el acceso a la base de datos
        private AccesoBaseDatos baseDatos;

        public AccesoBaseDatos BaseDatos
        {
            get{ return baseDatos; }
            set{ baseDatos = value; }
        }

        /**
         * Descripción: Constructor por defecto
         * Requiere: Nada
         * Retorna: La controladora construida
         */
        public ControladoraBDDiseno()
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

        /** 
         * Descripción: Obtiene el campo fecha de la tabla de forma segura (revisando si es null o no antes de leerlo)
         * Recibe un SqlDataReader con el que se obtiene el campo y el índice de la columna a consultar
         * Devuelve un valor DateTime dependiendo del resultado de la consulta. La fecha actual si el campo está nulo
         */
        public static DateTime SafeGetDateTime(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            else
                return DateTime.Now;
        }

        /** 
         * Descripción: Obtiene el campo fecha de la tabla de forma segura (revisando si es null o no antes de leerlo)
         * Recibe un SqlDataReader con el que se obtiene el campo y el índice de la columna a consultar
         * Devuelve un valor DateTime dependiendo del resultado de la consulta. La fecha actual si el campo está nulo
         */
        public static DateTime SafeGetDate(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            else
                return DateTime.Today;
        }

        /** 
         * Descripción: Inserta un Requerimiento en la BD
         * Recibe dos strings que son los atributos
         * RET: N/A
         */

        public void insertarReq(string id, string nomReq)
        {
            string resultado = "";
            string consulta = "";

            try
            {
                consulta = "INSERT INTO Requerimiento VALUES (@0, @1);";
                Object[] args = new Object[2];
                args[0] = id;
                args[1] = nomReq;
                SqlDataReader res = baseDatos.ejecutarConsulta(consulta, args);
                res.Close();
            }
            catch (SqlException ex)
            {
                resultado = "Error al insertar, Error 1: " + ex.Message;
            }
        }

        public void modificarReq(string idViejo, string nomViejo, string idNuevo, string nomNuevo)
        {
            string consulta = "UPDATE Requerimiento Set id = '" + idNuevo + "', nombre = '" + nomNuevo + "' WHERE id = '" + idViejo + "';";
            SqlDataReader res = baseDatos.ejecutarConsulta(consulta);
            res.Close();
        }

        /**
         * Requiere: int id
         * Retorna EntidadDiseno.
         * Consulta en la BD en la tabla diseno la fila con el id de diseno dado y la devuelve encapsulada.
         */
        public EntidadDiseno consultaDiseno(int id)
        {//Hace la consulta de todos los campos
            string consultaU = "SELECT criterios, nivel, tecnica, ambiente, procedimiento, fecha, proposito, responsable, idProy"
                + " FROM Diseno d WHERE d.id=" + id + "; ";
            //Inicialice variables locales
            EntidadDiseno dise = null;
            string criterios = "";
            string nivel = "";
            string tecnica = "";
            string ambiente = "";
            string procedimiento = "";
            DateTime fecha = DateTime.Today;
            string proposito = "";
            int responsable = -1;
            int idProy = -1;

            try
            {
                SqlDataReader reader = BaseDatos.ejecutarConsulta(consultaU);
                try
                {
                    if (reader.Read())
                    {//Si pudo leer, obtenga los datos de forma segura
                        criterios = SafeGetString(reader, 0);
                        nivel = SafeGetString(reader, 1);
                        tecnica = SafeGetString(reader, 2);
                        ambiente = SafeGetString(reader, 3);
                        procedimiento = SafeGetString(reader, 4);
                        fecha = SafeGetDate(reader, 5);
                        proposito = SafeGetString(reader, 6);
                        responsable = SafeGetInt32(reader, 7);
                        idProy = SafeGetInt32(reader, 8);
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                //Encapsulo los datos
                dise = new EntidadDiseno(id, criterios, nivel, tecnica, ambiente,
                        procedimiento, fecha, proposito, responsable, idProy);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return dise;
        }

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla
         * Consulta la tabla diseno y la devuelve en un DataTable.
         */
        public DataTable consultaDisenos(int idProy)
        {
            //La consulta debe quedar con las columnas en formato adecuado para que se muestren en el grid
            string consulta = "SELECT id AS 'ID', proposito AS 'Propósito', nivel AS 'Nivel'"
                + " FROM Diseno WHERE idProy = " + idProy + "; ";
            DataTable data = new DataTable();
            try
            {
                //Obtengo la tabla
                data = BaseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;
        }

        /**
         * Requiere: int id
         * Retorna string[].
         * Consulta en la BD en la tabla requerimiento la fila con el id de requerimiento dado y la devuelve en un vector string.
         */
        public DataTable consultaReqDisponibles(int idDise)
        {//Hace la consulta de todos los campos
            string consulta = "SELECT r.id, r.nombre, d.idDise FROM Requerimiento r Left Outer JOIN DisenoRequerimiento d ON d.idReq=r.id where d.idDise IS NULL OR (d.idDise != '" + idDise +"' and r.id not IN (select r1.id from Requerimiento r1 Join DisenoRequerimiento d1 On d1.idReq = r1.id where d1.idDise = '" + idDise +"') )";
            DataTable res = new DataTable();
            try
            {
                res = BaseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return res;
        }

        /**
         * Requiere: int id
         * Retorna string[].
         * Consulta en la BD en la tabla requerimiento la fila con el id de requerimiento dado y la devuelve en un vector string.
         */
        public DataTable consultaReqAsignados(int idDise)
        {//Hace la consulta de todos los campos
            string consulta = "SELECT r.id, r.nombre"
                            + " FROM Requerimiento r, DisenoRequerimiento d"
                            + " WHERE d.idDise=" + idDise + " AND d.idReq=r.id; ";
            //Inicialice variables locales
            DataTable res = new DataTable();
            try
            {
                res = baseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return res;
        }

        /**
         * Requiere: no aplica
         * Retorna: DataTable con la tabla
         * Consulta la tabla requerimiento y la devuelve en un DataTable.
         */
        public DataTable consultaRequerimientos()
        {
            //La consulta debe quedar con las columnas en formato adecuado para que se muestren en el grid
            string consulta = "SELECT id AS 'ID', nombre AS 'Nombre'"
                + " FROM Requerimiento; ";
            DataTable data = new DataTable();
            try
            {
                //Obtengo la tabla
                data = BaseDatos.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return data;
        }

        /**
         * Descripción: Realiza la consulta SQL de eliminación de undiseño de prueba de la base de datos, elimina de tabla Diseño
         * Recibe: Un valor entero que es el identificador del diseño: @id
         * Devuelve un valor entero dependiendo del resultado de la consulta:
         * 0:  Eliminación correcta de tuplas en ambas tablas
         * -1: Error eliminando de tabla Usuario
         */
        public int eliminaDiseno(int id)
        {
            string consulta = " DELETE FROM Diseno WHERE id = " + id + "; ";
            int resultado = -1;
            try
            {
                SqlDataReader reader = BaseDatos.ejecutarConsulta(consulta);
                //Si se eliminó correctamente el diseño de prueba se devuelve un cero
                if (reader.RecordsAffected > 0)
                {
                    reader.Close();                    
                    resultado = 0;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }

        /**
         * Descripción: Realiza la consulta SQL de eliminación de un requerimiento de la base de datos, elimina de la tabla Requerimiento
         * Recibe: Un valor entero que es el identificador del requerimiento a eliminar: @idReq
         * Devuelve un valor entero dependiendo del resultado de la consulta:
         * 0:  Eliminación correcta de tuplas en ambas tablas
         * -1: Error eliminando de tabla Usuario
         */
        public int eliminaRequerimiento(string idReq) {
            string consulta = " DELETE FROM Requerimiento WHERE id = '" + idReq + "'; ";
            int resultado = -1;
            try
            {
                SqlDataReader reader = BaseDatos.ejecutarConsulta(consulta);
                //Si se eliminó correctamente el requerimiento de un diseño de prueba se devuelve un cero
                if (reader.RecordsAffected > 0)
                {
                    reader.Close();
                    resultado = 0;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;
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
                +" WHERE u.nomUsuario='"+usuario+"' AND p.id=u.idProy;";
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

        /*Recibe un objeto con los datos de un diseño, para insertar en la base de datos
         */
        public int insertarDiseno(EntidadDiseno ent_dis)
        {

            string consulta = "insert into Diseno (criterios, nivel, tecnica, ambiente, procedimiento, fecha, proposito, responsable,idProy)"
                                         + "values( @0,         @1,    @2,       @3,        @4,           @5 ,    @6,        @7,     @8);";


            Object[] dis = new Object[9];
            dis[0] = ent_dis.Criterios;
            dis[1] = ent_dis.Nivel;
            dis[2] = ent_dis.Tecnica;
            dis[3] = ent_dis.Ambiente;
            dis[4] = ent_dis.Procedimiento;
            dis[5] = ent_dis.Fecha;
            dis[6] = ent_dis.Proposito;
            dis[7] = ent_dis.Responsable; 
            dis[8] = ent_dis.IdProy;   //idProyecto
            //Inicialice variables localesFmodifi
            int resultado = 0;
            try
            {
                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta, dis);
                try
                {
                    if (reader.RecordsAffected > 0)
                    {
                        reader.Close();
                        resultado = 1;
                    }
                    else
                    {
                        resultado = -1;
                    }

                }
                catch (SqlException ex)
                {
                    throw ex;
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return resultado;

        }
       public EntidadDiseno modificarDiseno(EntidadDiseno ent_dis_Actual, EntidadDiseno ent_dis_Nuevo) {

            EntidadDiseno en = null;
            Boolean cambioRespo= true;
            if (ent_dis_Nuevo.Responsable == 0)
            {
                cambioRespo = false;
        }

            try
            {
                string consulta = "";
                if (!cambioRespo)
                {
                    consulta = "Update Diseno Set criterios='" + ent_dis_Nuevo.Criterios + "', nivel ='" + ent_dis_Nuevo.Nivel + "', tecnica ='" + ent_dis_Nuevo.Tecnica + "',  ambiente= '" + ent_dis_Nuevo.Ambiente + "',   procedimiento ='" + ent_dis_Nuevo.Procedimiento + "',"
                   + "fecha= '" + (ent_dis_Nuevo.Fecha).ToString("yyy-MM-dd", CultureInfo.InvariantCulture) + "', proposito ='" + ent_dis_Nuevo.Proposito + "'"
                   + " where id  = '" + ent_dis_Actual.Id + "';";

                }
                else
                {
                    consulta = "Update Diseno Set criterios='" + ent_dis_Nuevo.Criterios + "', nivel ='" + ent_dis_Nuevo.Nivel + "', tecnica ='" + ent_dis_Nuevo.Tecnica + "',  ambiente= '" + ent_dis_Nuevo.Ambiente + "',   procedimiento ='" + ent_dis_Nuevo.Procedimiento + "',"
                + "fecha= '" + (ent_dis_Nuevo.Fecha).ToString("yyy-MM-dd", CultureInfo.InvariantCulture) + "', proposito ='" + ent_dis_Nuevo.Proposito + "', responsable= '" + ent_dis_Nuevo.Responsable + "'"
                + " where id  = '" + ent_dis_Actual.Id + "';";
                }

                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

           try
            {
                string consulta2 = "select criterios, nivel, tecnica, ambiente, procedimiento, fecha, proposito, responsable "+
                                    "from Diseno"+
                                    " where id  = " + ent_dis_Actual.Id + ";";
                Object[] dato = new Object[10];

                SqlDataReader reader2 = baseDatos.ejecutarConsulta(consulta2);
                if (reader2.Read())
                {
                    //cuando hay solo un telefono
                    dato[0] = 0;
                    dato[1] = reader2.GetString(0);  //criterios           
                    dato[2] = reader2.GetString(1);  //nivel
                    dato[3] = reader2.GetString(2);  //tecnica
                    dato[4] = reader2.GetString(3);  //ambiente
                    dato[5] = reader2.GetString(4);  //procedimiento
                    dato[6] = reader2.GetDateTime(5);//fecha
                    dato[7] = reader2.GetString(6);  //proposito
                    dato[8]=  reader2.GetInt32(7);//cedula del responsable   
                    dato[9] = 1;  //proyecto
                    
                    en = new EntidadDiseno(dato);
                }
                reader2.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return en;

        }

       /**
        * Requiere: List<string>, List<string>, int
        * Retorna: N/A
        * Asigna y Libera los requerimientos segun su localizacion en los paneles
        */

       public void asignarReqs(List<string> listaA, List<string> listaD, int idDiseno)
       {
           int id = -1;
           string consulta = "Select MAX(id) from Diseno";

            try
            {
                if (idDiseno == -1)
                {
                    SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                    if (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        reader.Close();
                    }
                }
                else
                {
                    id = idDiseno;
                }

                if (id != -1)
                {
                    for (int i = 0; i < listaA.Count; i++)
                    {
                        string existe = "Select * from DisenoRequerimiento where idDise = '" + id +"' And idReq = '" + listaA.ElementAt(i) + "';";
                        SqlDataReader si = baseDatos.ejecutarConsulta(existe);

                        if (!(si.Read()))
                        {
                            consulta = "Insert into DisenoRequerimiento values ( @0, @1);";
                            Object[] dis = new Object[2];
                            dis[0] = id;
                            dis[1] = listaA.ElementAt(i);
                            si.Close();
                            SqlDataReader read = baseDatos.ejecutarConsulta(consulta, dis);
                            read.Close();
                        }
                        else
                        {
                            si.Close();
                        }
                    }

                    for (int i = 0; i < listaD.Count; i++)
                    {
                        consulta = "Delete from DisenoRequerimiento where idDise = '" + id +"' and idReq = '" + listaD.ElementAt(i) + "';";
                        SqlDataReader r = baseDatos.ejecutarConsulta(consulta);
                        r.Close();

                    }

                }
            }
            catch(SqlException ex)
            {
                throw ex;
            }
       }

        /** Descripcion: Consulta total de recursos en la tabla
         * REQ: string 
         * RET: DataTable
         */
        public DataTable consultaRRHH()
        {
            string consulta = "";

            DataTable data = new DataTable();
            consulta = "SELECT  CONCAT(pNombre, ' ', pApellido, ' ', sApellido), cedula FROM Usuario;";
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

        /** Descripcion: Consulta total de recursos en la tabla asociados al proyecto determinado
         * REQ: string 
         * RET: DataTable
         */
        public DataTable consultaRRHH(int idProy)
        {
            string consulta = "";

            DataTable data = new DataTable();
            consulta = "SELECT  CONCAT(pNombre, ' ', pApellido, ' ', sApellido), cedula"
                    + " FROM Usuario WHERE idProy="+idProy+";";
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

        /**
         * Requiere: string
         * Retorna: bool
         * Retorna true si hay duplicidad de id de Requerimientos
         */

        public bool revisarReqExistente(string id)
        {
            bool resultado = false;
            try
            {
                string consulta = "Select id from Requerimiento where id = '" + id + "';";
                SqlDataReader leer = baseDatos.ejecutarConsulta(consulta);
                if(leer.HasRows)
                {
                    resultado = true;
                }
                leer.Close();
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            return resultado;
        }

        public string obtenerRH(int ced) {
            string nombre = "";
            try {            
                string consulta = "select Concat(pNombre, ' ', pApellido, ' ', sApellido) AS ced " +
                "from Usuario " +
                " where cedula  = " + ced + ";";

                SqlDataReader reader = baseDatos.ejecutarConsulta(consulta);
                if (reader.Read())
                {
                    nombre = reader.GetString(0);   //nombre responsable
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nombre;
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
    }
}