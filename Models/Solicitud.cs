using System;
namespace desconectate.Models
{
    public class Solicitud
    {
        
        public int folio { get; set; }
        public string nombre { get; set; }
        public string solicitudName { get; set; }
        public string estatusDescripcion { get; set; }
        public Nullable<int> idsap { get; set; }
        public int tipo_solicitud { get; set; }
        public int tipo_solicitud_goce { get; set; }
        public Nullable<System.DateTime> fecha_solicitud { get; set; }
        public Nullable<System.DateTime> fecha_inicio { get; set; }
        public Nullable<System.DateTime> fecha_fin { get; set; }
        public int dias { get; set; }
        public string dias_detalle { get; set; }
        public string observacion_solicitante { get; set; }
        public Nullable<int> estatus { get; set; }
        public Nullable<int> idsap_aprobador { get; set; }
        public string nombre_aprobador { get; set; }
        public string email_aprobador { get; set; }
        public Nullable<System.DateTime> fecha_aprobacion { get; set; }
        public string observacion_aprobador { get; set; }
        public int antiguedad { get; set; }
        public int maximo_dias { get; set; }
        public int dias_disponibles { get; set; }
        public Nullable<System.DateTime> aniversario { get; set; }
        public string con_goce { get; set; }
        public string area { get; set; }
        public Nullable<System.DateTime> caducidad { get; set; }
        public string periodo { get; set; }
        public int dias_tomados { get; set; }
        public string caducidades { get; set; }
    }
}
