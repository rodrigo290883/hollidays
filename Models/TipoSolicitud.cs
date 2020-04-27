using System;
namespace desconectate.Models
{
    public class TipoSolicitud
    {
       
        public int id_tipo_solicitud { get; set; }
        public string solicitud { get; set; }
        public Nullable<int> maximo_dias { get; set; }
        public char consume_vacaciones { get; set; }
    
    }
}
