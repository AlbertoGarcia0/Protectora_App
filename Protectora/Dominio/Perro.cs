using System;
using Protectora.Persistencia;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protectora.Dominio;

namespace Protectora.Dominio
{
    public class Perro : Animal
    {
        private string raza;
        private AnimalDAO aniDAO;

        public string Raza { get => raza; set => raza = value; }
        internal AnimalDAO AniDAO { get => aniDAO; set => aniDAO = value; }

        public Perro() : base()
        {
            this.AniDAO = new AnimalDAO();
        }

        public Perro(Nullable<int> id, string nombre, string sexo, int tamanio, int peso, int edad, DateTime fechaEntrada
            , string foto, string enlace, string descripcion, string estado, int apadrinado, string raza) :
            base(id, nombre, sexo, tamanio, peso, edad, fechaEntrada, foto, enlace, descripcion, estado, apadrinado)
        {
            this.AniDAO = new AnimalDAO();
            this.Raza = raza;
        }

        // PARTE DTO
        public void InsertarPerro()
        {
            this.AniDAO.insertar((Perro)this.MemberwiseClone());
            //Console.Write(" ");
        }

        public void EliminarPerro()
        {
            this.AniDAO.eliminar((Perro)this.MemberwiseClone());
            //Console.Write(" ");
        }

        public void ModificarPerro()
        {
            this.AniDAO.actualizar((Perro)this.MemberwiseClone());
            //Console.Write(" ");
        }

        public Perro Leer(String nombre)
        {
            Nombre = nombre;
            return AniDAO.leer((Perro)this.MemberwiseClone());
        }

        public List<Perro> LeerTodosAnimales()
        {
            return AniDAO.leerTodas();
        }

    }
}