using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1._4.Models
{
    public class Empleado
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        public string nombres { get; set; }

        public string descripcion { get; set; }

        public byte[] imagen { get; set; }
    }
}
