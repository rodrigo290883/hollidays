﻿using System;
namespace desconectate.Models
{
    public class Solicitud
    {
        
        public int folio { get; set; }
        public string nombre { get; set; }
        public string solicitudName { get; set; }
        public string estatusDescripcion { get; set; }
        public Nullable<int> id_sap { get; set; }
        public string tipo_solicitud { get; set; }
        public Nullable<System.DateTime> fecha_solicitud { get; set; }
        public Nullable<System.DateTime> fecha_inicio { get; set; }
        public Nullable<System.DateTime> fecha_fin { get; set; }
        public string observacion_solicitante { get; set; }
        public Nullable<int> estatus { get; set; }
        public Nullable<int> id_sap_aprobador { get; set; }
        public string aprobador { get; set; }
        public Nullable<System.DateTime> fecha_aprobacion { get; set; }
        public string observacion_aprobador { get; set; }
        public int antiguedad { get; set; }
        public int maximo_dias { get; set; }
        public int dias_disponibles { get; set; }
        public Nullable<System.DateTime> aniversario { get; set; }
        
    }
}
