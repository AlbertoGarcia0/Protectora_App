using Protectora.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora.Persistencia
{
    class UsuarioDAO : IDAO<Usuario>
    {
        public readonly List<Usuario> usuarios;

        public UsuarioDAO()
        {
            this.usuarios = new List<Usuario>();
        }


        // ESTA PARTE ES LA QUE VALE
        public List<Usuario> leerTodas()
        {
            List<List<String>> arrayCarUsuarios = AgenteDB.obtenerAgente().leer("SELECT * FROM usuarios");

            foreach (List<String> user in arrayCarUsuarios)
            {
                Usuario p = new Usuario(Int32.Parse(user[0]), user[1], user[2], DateTime.Parse(user[3]));
                usuarios.Add(p);
            }
            return usuarios;
        }

        public Usuario leer(Usuario obj)
        {
            List<List<String>> arrayCarUsuarios = AgenteDB.obtenerAgente().leer("SELECT * FROM usuarios WHERE user='" + obj.Nombre + "' and password='" + obj.Password + "';");

            foreach (List<String> user in arrayCarUsuarios)
            {
                Usuario p = new Usuario(Int32.Parse(user[0]), user[1], user[2], DateTime.Parse(user[3]));
                usuarios.Add(p);
            }
            if (usuarios.Count==0)
            {
                return obj;
            }
            return usuarios[0];
        }

        public string contrasenaUsuario(Usuario obj)
        {
            List<List<String>> arrayCarUsuarios = AgenteDB.obtenerAgente().leer("SELECT usuarios.password FROM usuarios WHERE user='" + obj.Nombre + "';");
            string contrasenia = "";

            foreach (List<String> user in arrayCarUsuarios)
            {
                contrasenia = user[0];
            }

            return contrasenia;
        }

        public int insertar(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public int actualizar(Usuario obj)
        {
            return AgenteDB.obtenerAgente().modificar("UPDATE usuarios SET ultimaConexion= '" + DateTime.Now.ToString() + "' WHERE Id = " + obj.Id.ToString() + "; ");
        }

        public int eliminar(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
