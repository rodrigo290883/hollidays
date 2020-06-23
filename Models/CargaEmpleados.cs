using Microsoft.AspNetCore.Http;
using System;

namespace desconectate.Models
{
	public class CargaEmpleados {

		public string nombre { get; set;}

		public string edad { get; set; }
		
		public IFormFile archivo { get; set; }

		public CargaEmpleados(string nombre, string edad){
			this.nombre = nombre;
			this.edad = edad;
		}
	}
}