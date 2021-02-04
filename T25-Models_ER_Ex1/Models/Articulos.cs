using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T25_Models_ER_Ex1.Models
{
    public class Articulos
    {
        // ATRIBUTOS, GETTERS Y SETTERS
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int Fabricante { get; set; }

        // CONSTRUCTORES
        public Fabricantes Fabricantes { get; set; }
    }
}
