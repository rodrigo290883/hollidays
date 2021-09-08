using System;
namespace desconectate.Models
{
    public class Poliza
        {

            public int idsap { get; set; }
            public string nombre { get; set; }
            public string area { get; set; }
            public string banda { get; set; }
            public Nullable<System.DateTime> fecha_ingreso_grupo { get; set; }
            public string archivo_poliza { get; set; }

        }
}
