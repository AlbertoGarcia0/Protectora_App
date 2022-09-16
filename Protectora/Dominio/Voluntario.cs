using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protectora.Persistencia;

namespace Protectora.Dominio
{
    public class Voluntario : Persona
    {
        private string foto;
        private string horario;
        private string zonaDisponibilidad;
        private VoluntarioDAO volDAO;

        public string Foto { get => foto; set => foto = value; }
        public string Horario { get => horario; set => horario = value; }
        public string ZonaDisponibilidad { get => zonaDisponibilidad; set => zonaDisponibilidad = value; }
        internal VoluntarioDAO VolDAO { get => volDAO; set => volDAO = value; }

        public Voluntario() : base()
        {
            this.VolDAO = new VoluntarioDAO();
        }


        public Voluntario(Nullable<int> id, string nombreCompleto, string correo, string dni, int telefono,
            string foto, string horario, string zonaDisponibilidad) : base(id, nombreCompleto, correo, dni, telefono)
        {
            this.VolDAO = new VoluntarioDAO();
            this.Foto = foto;
            this.Horario = horario;
            this.ZonaDisponibilidad = zonaDisponibilidad;
        }

        public void InsertarVoluntario()
        {
            this.VolDAO.insertar((Voluntario)this.MemberwiseClone());
            Console.Write(" ");
        }

        public void EliminarVoluntario()
        {
            this.VolDAO.eliminar((Voluntario)this.MemberwiseClone());
            Console.Write(" ");
        }

        public void ModificarVoluntario()
        {
            this.VolDAO.actualizar((Voluntario)this.MemberwiseClone());
            Console.Write(" ");
        }

        public List<Voluntario> LeerTodosVoluntarios()
        {
            List<Voluntario> arrayVoluntarios = new List<Voluntario>();

            VoluntarioDAO volDao = new VoluntarioDAO();
            arrayVoluntarios = volDao.leerTodas();

            Console.Write(" ");

            return arrayVoluntarios;
        }

    }
}