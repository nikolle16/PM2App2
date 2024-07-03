using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2App2.Models
{
    public class Empleado
    {
            public int id { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string direccion { get; set; }
            public object foto { get; set; }
    }
}
