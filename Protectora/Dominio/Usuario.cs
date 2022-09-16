using System;
using Protectora.Persistencia;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora.Dominio
{
    public class Usuario
    {
        private int? id; // ? hace que la variable acepte los valores propios del tipo de dato y tambien el valor null
        private string nombre;
        private string password;
        private DateTime? fechaUltimaConex;
        private UsuarioDAO usuDAO;

        public int? Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Password { get => password; set => password = value; }
        public DateTime? FechaUltimaConex { get => fechaUltimaConex; set => fechaUltimaConex = value; }
        internal UsuarioDAO UsuDAO { get => usuDAO; set => usuDAO = value; }


        public Usuario()
        {
            this.UsuDAO = new UsuarioDAO();
        }


        public Usuario(Nullable<int> id, string nombre, string password, DateTime? fechaUltimaConex)
        {
            this.UsuDAO = new UsuarioDAO();
            this.id = id;
            this.nombre = nombre;
            this.password = password;
            this.fechaUltimaConex = fechaUltimaConex;
        }



        public List<Usuario> LeerTodosUsuarios()
        {
            return UsuDAO.leerTodas();
        }

        public Usuario LeerUsuario()
        {

            return UsuDAO.leer(this);
        }


        public string LeerContraseniaUsuario()
        {

            return UsuDAO.contrasenaUsuario(this);
        }

        public int ModificarUsuarioFecha()
        {
            return UsuDAO.actualizar(this);
        }
    }
}