using System;
using System.Collections.Generic;

namespace Externo.Models
{
    public partial class TipoVehiculo
    {
        public TipoVehiculo()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdTipoVehiculo { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
