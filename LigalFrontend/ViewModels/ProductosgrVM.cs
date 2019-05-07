using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class ProductosgrVM
    {
        public gen_productosgr productogr { get; set; }
        public gen_productos producto { get; set; }

        public List<gen_productos> listaProductos { get; set; }

        public ProductosgrVM()
        {
            productogr = new gen_productosgr();
            producto = new gen_productos();
        }
    }
}