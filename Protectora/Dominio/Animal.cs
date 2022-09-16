using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora.Dominio
{
    public class Animal
    {
        protected int? id;
        protected string nombre;
        protected string sexo;
        protected int tamanio;
        protected int peso;
        protected int edad;
        protected DateTime fechaEntrada;
        protected string foto;
        protected string enlace = "";
        protected string descripcion;
        protected string estado;
        protected int apadrinado;

        public int? Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public int Tamanio { get => tamanio; set => tamanio = value; }
        public int Peso { get => peso; set => peso = value; }
        public int Edad { get => edad; set => edad = value; }
        public DateTime FechaEntrada { get => fechaEntrada; set => fechaEntrada = value; }
        public string Foto { get => foto; set => foto = value; }
        public string Enlace { get => enlace; set => enlace = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Estado { get => estado; set => estado = value; }
        public int Apadrinado { get => apadrinado; set => apadrinado = value; }

        public Animal()
        {
        }


        public Animal(Nullable<int> id, string nombre, string sexo, int tamanio, int peso, int edad, DateTime fechaEntrada, string foto, string enlace, string descripcion, string estado, int apadrinado)
        {
            this.id = id;
            this.nombre = nombre;
            this.sexo = sexo;
            this.tamanio = tamanio;
            this.peso = peso;
            this.edad = edad;
            this.fechaEntrada = fechaEntrada;
            this.foto = foto;
            this.enlace = enlace;
            this.descripcion = descripcion;
            this.estado = estado;
            this.apadrinado = apadrinado;
        }

    }
}