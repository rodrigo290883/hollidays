using System;
namespace mvc_dotnet.Models
{
    public class solicitudes
    {
        
        public int id_solicitud { get; set; }
        public Nullable<int> is_sap { get; set; }
        public string tipo_solicitud { get; set; }
        public Nullable<System.DateTime> fecha_solicitud { get; set; }
        public Nullable<System.DateTime> fecha_inicio { get; set; }
        public Nullable<System.DateTime> fecha_fin { get; set; }
        public string observaciones_solicitante { get; set; }
        public Nullable<int> estatus { get; set; }
        public Nullable<int> id_sap_aprobador { get; set; }
        public Nullable<System.DateTime> fecha_aprobacion { get; set; }
        public string observacion_aprobador { get; set; }
        
    }
}
