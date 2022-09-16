using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora.Dominio
{
    public class Persona
    {
        private int? id;
        private string nombreCompleto;
        private string correo;
        private string dni;
        private int telefono;

        public int? Id { get => id; set => id = value; }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Dni { get => dni; set => dni = value; }
        public int Telefono { get => telefono; set => telefono = value; }

        public Persona()
        {
        }

        public Persona(Nullable<int> id, string nombreCompleto, string correo, string dni, int telefono)
        {
            this.id = id;
            this.NombreCompleto = nombreCompleto;
            this.Correo = correo;
            this.Dni = dni;
            this.Telefono = telefono;
        }


    }
}
