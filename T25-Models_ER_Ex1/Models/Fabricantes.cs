using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T25_Models_ER_Ex1.Models
{
    public class Fabricantes
    {
        // ATRIBUTOS, GETTERS Y SETTERS
        public int Codigo { get; set; }
        public string Nombre { get; set; }

        // CONSTRUCTORES
        public ICollection<Articulos> Articulos { get; set; }

    }
}
