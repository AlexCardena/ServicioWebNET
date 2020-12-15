using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Descripción breve de WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hola a todos";
    }

    [WebMethod]
    public String[] alumno_consulta(String ci_alumno)
    {
        //El Servicio web retornara un vector con las materias del estudiante
        SqlConnection conexion = new SqlConnection();
        conexion.ConnectionString = "server=DESKTOP-F8JUKD1\\SQLEXPRESS;user=LoginParcial;pwd=123456;database=SistemaEducativo";
        SqlCommand comando = new SqlCommand();
        comando.Connection = conexion;
        comando.CommandText = "select alumno.nombre, alumno.ap_paterno, alumno.ap_materno, materia.nombre, cursa.nota  from materia, alumno, cursa where '" + ci_alumno + "' = cursa.ci and '" + ci_alumno + "' = alumno.ci and materia.id_materia = cursa.id_materia";
        conexion.Open();
        comando.ExecuteNonQuery();
        String [] respuesta = new String[1000];
        SqlDataReader registros = comando.ExecuteReader();
        int i = 1,sw=0;
        while (registros.Read())
        {
            if (sw==0) 
            {
                respuesta[0] = registros[0].ToString();
                respuesta[1] = registros[1].ToString();
                respuesta[2] = registros[2].ToString();
                i = 3;
                sw = 1;
            }
            respuesta[i] = registros[3].ToString();
            respuesta[i+1] = registros[4].ToString();
            i =i+2;
        }
        return respuesta;
    }
}
