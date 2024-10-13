using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NArticulo
    {

        // Este es el Metodo instertar que llama al metodo Insertar de la clase DArticulo de la CapaDatos

        public static string Insertar(string codigo, string nombre, string descripcion, byte[] imagen, int idcategoria, int idpresentacion)
        {

            DArticulo Obj = new DArticulo();
            Obj.Codigo = codigo;
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;
            Obj.Imagen = imagen;
            Obj.Idcategoria = idcategoria;
            Obj.Idpresentacion = idpresentacion;
            return Obj.Insertar(Obj);
        }

        // Este es el Metodo Editar que llama al metodo Editar de la clase DArticulo de la CapaDatos

        public static string Editar(int idarticulo, string codigo, string nombre, string descripcion, byte[] imagen, int idcategoria, int idpresentacion)
        {

            DArticulo Obj = new DArticulo();
            Obj.Idarticulo = idarticulo;
            Obj.Codigo = codigo;
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;
            Obj.Imagen = imagen;
            Obj.Idcategoria = idcategoria;
            Obj.Idpresentacion = idpresentacion;
            return Obj.Insertar(Obj);
        }

        // Este es el Metodo Eliminar que llama al metodo Eliminar de la clase DArticulo de la CapaDatos

        public static string Eliminar(int idarticulo)
        {

            DArticulo Obj = new DArticulo();
            Obj.Idarticulo = idarticulo;
            return Obj.Eliminar(Obj);
        }

        // Este es el Metodo Mostrar que llama al metodo Mostrar de la clase DArticulo de la CapaDatos

        public static DataTable Mostrar()
        {
            return new DArticulo().Mostrar();
        }

        // Este es el Metodo Mostrar que llama al metodo Mostrar de la clase DArticulo de la CapaDatos

        public static DataTable BuscarNombre(string textobuscar)
        {
            DArticulo Obj = new DArticulo();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNombre(Obj);
        }


        // fin
    }
}
