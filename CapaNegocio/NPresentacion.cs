using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NPresentacion
    {

        // Este es el Metodo instertar que llama al metodo Insertar de la clase DPresentacion de la CapaDatos

        public static string Insertar(string nombre, string descripcion)
        {

            DPresentacion Obj = new DPresentacion();
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;
            return Obj.Insertar(Obj);
        }

        // Este es el Metodo Editar que llama al metodo Editar de la clase DPresentacion de la CapaDatos

        public static string Editar(int idpresentacion, string nombre, string descripcion)
        {

            DPresentacion Obj = new DPresentacion();
            Obj.Idpresentacion = idpresentacion;
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;
            return Obj.Editar(Obj);
        }

        // Este es el Metodo Eliminar que llama al metodo Eliminar de la clase DPresentacion de la CapaDatos

        public static string Eliminar(int idpresentacion)
        {

            DPresentacion Obj = new DPresentacion();
            Obj.Idpresentacion = idpresentacion;
            return Obj.Eliminar(Obj);
        }

        // Este es el Metodo Mostrar que llama al metodo Mostrar de la clase DPresentacion de la CapaDatos

        public static DataTable Mostrar()
        {
            return new DPresentacion().Mostrar();
        }

        // Este es el Metodo Mostrar que llama al metodo Mostrar de la clase DPresentacion de la CapaDatos

        public static DataTable BuscarNombre(string textobuscar)
        {
            DPresentacion Obj = new DPresentacion();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNombre(Obj);
        }


        //fin 

    }
}
