using System;
using System.Diagnostics;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace GestionCobros
{
    /// <summary>
    /// Framework para realizar consultas de una manera mas simplificada en C#.
    /// </summary>
    class Datos
    {
        public static MySqlCommand comando;
        public static MySqlConnection conexion;
        public static DataTable tabla;

        private static string cadena_conexion = string.Format("server={0};Database={1};Uid={2};Pwd={3};Allow Zero Datetime=True;Convert Zero Datetime=True;", VGlobales.HOST, VGlobales.DATABASE, VGlobales.USUARIO_DB, VGlobales.PASSWORD_DB);

        /// <summary>
        /// Metodo usado para crear un objeto de tipo MysqlCommand, el cual ya posee la conexión creada y esta listo para que le asignen la Query y sus parametros.
        /// </summary>
        /// <returns> Objeto de tipo MysqlCommand </returns>
        public static MySqlCommand crearComando()
        {
            if (conexion == null)
            {
                conexion = new MySqlConnection(cadena_conexion);
            }

            if (comando == null)
            {
                comando = conexion.CreateCommand();
            }
            return comando;
        }

        /// <summary>
        /// Metodo encargado de ejecutar un comando de tipo select
        /// </summary>
        /// <param name="comando"> Comando el cual posee la consulta que se va a efectuar. Junto con sus parametros.</param>
        /// <returns>DataTable con el contenido de la Query.; En caso de error arroja excepción.</returns>
        public static DataTable ejecutarComandoSelect(MySqlCommand cmd)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            try
            {
                da.Fill(ds,"Table");    
            }
            catch (Exception ex)
            {
                //LOGS.LOGGER.crearLog(ex.StackTrace + "\n" + ex.Message);
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// Metodo usado para ejecutar un comando de tipo INSERT, UPDATE, DELETE. Querys que no retornen resultados.
        /// </summary>
        /// <param name="comando"> Comando el cual posee la consulta que se va a efectuar. Junto con sus parametros.</param>
        /// <returns> True si fue satisfactorio ; False si ocurrió un error.</returns>
        public static bool ejecutarComando(MySqlCommand cmd)
        {
            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // LOGS.LOGGER.crearLog(ex.StackTrace + "\n" + ex.Message);
                return false;

                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        /// <summary>
        /// Metodo usado para ejecutar un comando de tipo INSERT, UPDATE, DELETE. Querys que no retornen resultados.
        /// </summary>
        /// <param name="comando"> Comando el cual posee la consulta que se va a efectuar. Junto con sus parametros.</param>
        /// <returns> True si fue satisfactorio ; False si ocurrió un error.</returns>
        public static object ejecutarComandoEscalar(MySqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();

                return cmd.ExecuteScalar();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // LOGS.LOGGER.crearLog(ex.StackTrace + "\n" + ex.Message);


                throw ex;

            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        /// <summary>
        /// Se inserta un LOG del proceso realizado por el usuario.
        /// </summary>
        /// <param name="formulario"> Formulario del cual se llamo el LOG</param>
        /// <param name="descripcion"> Que operación se efectuo.</param>
        public static void crearLOG(string formulario, string descripcion)
        {
            MySqlCommand cmd = Datos.crearComando();
            cmd.Parameters.Clear();

            cmd.CommandText = "INSERT INTO logs (fecha,usuario,pc_usuario,formulario,descripcion) VALUES (now(),@usuario,@pc_usuario,@formulario,@descripcion)";
            cmd.Parameters.AddWithValue("@usuario", VGlobales.nombre);
            cmd.Parameters.AddWithValue("@pc_usuario", System.Environment.MachineName);
            cmd.Parameters.AddWithValue("@formulario", formulario);
            cmd.Parameters.AddWithValue("@descripcion", descripcion);

            Datos.ejecutarComando(cmd);
        }

        /// <summary>
        /// Metodo especial para inserciones a la base de datos, en los cuales se requiere obtener el id de la inserción.
        /// </summary>
        /// <param name="comando">Comando con la consulta tipo INSERT</param>
        /// <returns>El id de la inserción</returns>
        public static int ejecutarComandoInsertId(MySqlCommand comando)
        {
            int id = -1;
            try
            {
                comando.Connection.Open();
                comando.ExecuteNonQuery();
                id = unchecked((int)comando.LastInsertedId);
                return id;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // LOGS.LOGGER.crearLog(ex.StackTrace + "\n" + ex.Message);
                // throw ex;

                return id;

            }
            finally
            {
                comando.Connection.Close();
            }
        }

        /// <summary>
        /// Exporta a excel un datagridview
        /// </summary>
        /// <param name="datagrid"> DataGrid que se desea exportar</param>
        public static void ConvertirExcel(DataGridView datagrid)
        {

            datagrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            datagrid.MultiSelect = true;
            datagrid.SelectAll();
            DataObject dataObj = datagrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);

            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            xlWorkSheet.Columns.AutoFit();
        }
    }
}
