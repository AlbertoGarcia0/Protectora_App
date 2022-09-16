using Protectora.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora.Persistencia
{
    class SocioDAO : IDAO<Socio>
    {
        public readonly List<Socio> socios;
        public SocioDAO()
        {
            this.socios = new List<Socio>();
        }

        public List<Socio> leerTodas()
        {
            AgenteDB agente = AgenteDB.obtenerAgente();

            List<List<String>> arrayCarSocios = new List<List<String>>();
            arrayCarSocios = agente.leer("SELECT * FROM personas p, socios s WHERE p.id=s.idPersona");

            foreach (List<String> user in arrayCarSocios)
            {
                Socio s = new Socio(Int32.Parse(user[0]), user[1], user[2], user[3], Int32.Parse(user[4]), user[6], Int32.Parse(user[7]), user[8]);
                socios.Add(s);
            }
            Console.Write(" ");
            return socios;
        }

        public Socio leer(Socio obj)
        {
            List<Socio> arraySocios = new List<Socio>();
            List<List<String>> arrayCarSocios = AgenteDB.obtenerAgente().leer("SELECT * FROM personas p, socios s WHERE p.id=s.idPersona  AND p.id = " + obj.Id.ToString() + "; ");

            foreach (List<String> user in arrayCarSocios)
            {
                Socio s = new Socio(Int32.Parse(user[0]), user[1], user[2], user[3], Int32.Parse(user[4]), user[6], Int32.Parse(user[7]), user[8]);
                arraySocios.Add(s);
            }
            return arraySocios[0];
        }

        public int insertar(Socio obj)
        {
            AgenteDB agente = AgenteDB.obtenerAgente();

            agente.modificar("INSERT INTO personas (nombreCompleto,correo,dni,telefono) VALUES ('" + obj.NombreCompleto.ToString() + "', '" + obj.Correo.ToString() + "'," +
                " '" + obj.Dni.ToString() + "', " + obj.Telefono.ToString() + ");");
            obj.Id = (Int32.Parse(agente.leer("SELECT MAX(Id) FROM personas")[0][0]));

            return agente.modificar("INSERT INTO socios ( datosBancarios, cuantiaAyuda, formaPago, IdPersona ) VALUES ('" + obj.DatosBancarios.ToString() + "'," +
                " " + obj.CuantiaAyuda.ToString() + ", '" + obj.FormaPago.ToString() + "', " + obj.Id.ToString() + ");");

        }

        public int actualizar(Socio obj)
        {
            AgenteDB agente = AgenteDB.obtenerAgente();

            agente.modificar("UPDATE socios SET datosBancarios='" + obj.DatosBancarios.ToString() + "' ,cuantiaAyuda=" + obj.CuantiaAyuda.ToString() + ",formaPago= '" + obj.FormaPago.ToString() + "' WHERE IdPersona = " + obj.Id.ToString() + "; ");

            return agente.modificar("UPDATE personas SET nombreCompleto= '" + obj.NombreCompleto.ToString() + "',correo='" + obj.Correo.ToString() + "'," +
                "dni='" + obj.Dni.ToString() + "',telefono=" + obj.Telefono.ToString() + " WHERE Id = " + obj.Id.ToString() + "; ");
        }

        public int eliminar(Socio obj)
        {
            AgenteDB agente = AgenteDB.obtenerAgente();
            return agente.modificar("DELETE FROM personas WHERE Id=" + obj.Id.ToString() + ";");
        }
    }
}
