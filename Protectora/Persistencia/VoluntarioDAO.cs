using Protectora.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora.Persistencia
{
    class VoluntarioDAO : IDAO<Voluntario>
    {
        public readonly List<Voluntario> voluntarios;
        public VoluntarioDAO()
        {
            this.voluntarios = new List<Voluntario>();
        }
        public List<Voluntario> leerTodas()
        {
            AgenteDB agente = AgenteDB.obtenerAgente();

            List<List<String>> arrayCarVoluntarios = new List<List<String>>();
            arrayCarVoluntarios = agente.leer("SELECT * FROM personas p, voluntarios v WHERE p.id=v.idPersona");

            foreach (List<String> user in arrayCarVoluntarios)
            {
                Voluntario v = new Voluntario(Int32.Parse(user[0]), user[1], user[2], user[3],
                     Int32.Parse(user[4]), user[6], user[7], user[8]);
                voluntarios.Add(v);
            }
            Console.Write(" ");
            return voluntarios;
        }

        public Voluntario leer(Voluntario obj)
        {
            List<Voluntario> arrayVoluntarios = new List<Voluntario>();
            List<List<String>> arrayCarVoluntarios = AgenteDB.obtenerAgente().leer("SELECT * FROM personas p, voluntarios v WHERE p.id=v.idPersona  WHERE Id = " + obj.Id.ToString() + "; ");

            foreach (List<String> user in arrayCarVoluntarios)
            {
                Voluntario v = new Voluntario(Int32.Parse(user[0]), user[1], user[2], user[3],
                     Int32.Parse(user[4]), user[6], user[7], user[8]);
                arrayVoluntarios.Add(v);
            }
            return arrayVoluntarios[0];
        }

        public int insertar(Voluntario obj)
        {
            AgenteDB agente = AgenteDB.obtenerAgente();

            agente.modificar("INSERT INTO personas (nombreCompleto,correo,dni,telefono) VALUES ('" + obj.NombreCompleto.ToString() + "', '" + obj.Correo.ToString() + "'," +
                " '" + obj.Dni.ToString() + "', " + obj.Telefono.ToString() + ");");
            obj.Id = (Int32.Parse(agente.leer("SELECT MAX(Id) FROM personas")[0][0]));

            return agente.modificar("INSERT INTO voluntarios ( fotografia, horario, zonaDisponibilidad, IdPersona ) VALUES ('" + obj.Foto.ToString() + "'," +
                " '" + obj.Horario.ToString() + "', '" + obj.ZonaDisponibilidad.ToString() + "', " + obj.Id.ToString() + ");");
        }

        public int actualizar(Voluntario obj)
        {
            AgenteDB agente = AgenteDB.obtenerAgente();

            agente.modificar("UPDATE voluntarios SET fotografia='" + obj.Foto.ToString() + "' ,horario='" + obj.Horario.ToString() + "',zonaDisponibilidad= '" + obj.ZonaDisponibilidad.ToString() + "' WHERE IdPersona = " + obj.Id.ToString() + "; ");

            return agente.modificar("UPDATE personas SET nombreCompleto= '" + obj.NombreCompleto.ToString() + "',correo='" + obj.Correo.ToString() + "'," +
                "dni='" + obj.Dni.ToString() + "',telefono=" + obj.Telefono.ToString() + " WHERE Id = " + obj.Id.ToString() + "; ");
        }

        public int eliminar(Voluntario obj)
        {
            AgenteDB agente = AgenteDB.obtenerAgente();
            return agente.modificar("DELETE FROM personas WHERE Id=" + obj.Id.ToString() + ";");
        }
    }
}
