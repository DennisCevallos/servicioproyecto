using System;
using System.Collections.Generic;

namespace Externo.Models
{
    public partial class Color
    {
        public Color()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdColor { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
