using System;

namespace desconectate.Models
{
    public class Empleados
    {
        public int IdSAP { get; set; }
        public string Nombre { get; set; }
        public char sexo { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }
        public Nullable<int> estatus { get; set; }
        public string area { get; set; }
        public string banda { get; set; }
        public Nullable<System.DateTime> fecha_ingreso { get; set; }
        public Nullable<int> dias_disponibles { get; set; }
        public Nullable<int> antiguedad { get; set; }
        public Nullable<System.DateTime> ultimo_desconecte { get; set; }
        public Nullable<System.DateTime> caducidad { get; set; }
        public string url_poliza { get; set; }
        public Nullable<int> Idsap_padre { get; set; }
        public Nullable<int> meses_ultimo_desconecte { get; set; }
    }
}
