using Protectora.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora.Persistencia
{
    class AnimalDAO : IDAO<Perro>
    {
        public readonly List<Perro> animales;

        public AnimalDAO()
        {
            this.animales = new List<Perro>();
        }

        public List<Perro> leerTodas()
        {
            AgenteDB agente = AgenteDB.obtenerAgente();
            List<List<String>> arrayCarAnimales = new List<List<String>>();
            arrayCarAnimales = agente.leer("SELECT * FROM animales a, perros p WHERE a.id=p.idAnimal");

            foreach (List<String> user in arrayCarAnimales)
            {
                Console.Write(" ");
                Perro a = new Perro(Int32.Parse(user[0]), user[1], user[2],
                    Int32.Parse(user[3]), Int32.Parse(user[4]), Int32.Parse(user[5]),
                    DateTime.Parse(user[6]), user[7], user[8], user[9], user[10]
                    , Int32.Parse(user[11]), user[13]);
                animales.Add(a);
            }
            Console.Write(" ");
            return animales;
        }

        public Perro leer(Perro obj)
        {
            List<Perro> arrayAnimales = new List<Perro>();
            AgenteDB agente = AgenteDB.obtenerAgente();
            List<List<String>> arrayCarAnimales = new List<List<String>>();
            arrayCarAnimales = agente.leer("SELECT * FROM animales a, perros p WHERE a.id=p.idAnimal AND a.nombre = '" + obj.Nombre.ToString() + "'; ");

            foreach (List<String> user in arrayCarAnimales)
            {
                Console.Write(" ");
                Perro a = new Perro(Int32.Parse(user[0]), user[1], user[2],
                    Int32.Parse(user[3]), Int32.Parse(user[4]), Int32.Parse(user[5]),
                    DateTime.Parse(user[6]), user[7], user[8], user[9], user[10]
                    , Int32.Parse(user[11]), user[13]);
                arrayAnimales.Add(a);
            }

            Console.Write(" ");
            return arrayAnimales[0];
        }

        public int insertar(Perro obj)
        {
            AgenteDB agente = AgenteDB.obtenerAgente();

            agente.modificar("INSERT INTO animales (nombre,sexo,tamanio,peso,edad,entradaProtectora,foto,enlace,descripcion,estado,padrinos) VALUES ('" + obj.Nombre.ToString() + "', '" + obj.Sexo.ToString() + "', " + obj.Tamanio.ToString() + "" +
                ", " + obj.Peso.ToString() + ", " + obj.Edad.ToString() + ", '" + obj.FechaEntrada.ToString() + "', '" + obj.Foto.ToString() + "', '" + obj.Enlace.ToString() + "'" +
                ", '" + obj.Descripcion.ToString() + "', '" + obj.Estado.ToString() + "', " + obj.Apadrinado.ToString() + ");");

            obj.Id = (Int32.Parse(agente.leer("SELECT MAX(id) FROM animales")[0][0]));

            return agente.modificar("INSERT INTO perros ( raza, IdAnimal ) VALUES ('" + obj.Raza.ToString() + "', " + obj.Id.ToString() + ");");
        }

        public int actualizar(Perro obj)
        {
            AgenteDB agente = AgenteDB.obtenerAgente();

            agente.modificar("UPDATE perros SET raza='" + obj.Raza.ToString() + "' WHERE IdAnimal = " + obj.Id.ToString() + "; ");

            return agente.modificar("UPDATE animales SET nombre= '" + obj.Nombre.ToString() + "',sexo='" + obj.Sexo.ToString() + "',tamanio= " + obj.Tamanio.ToString() + ",peso=" + obj.Peso.ToString() + "," +
                "edad=" + obj.Edad.ToString() + ",entradaProtectora= '" + obj.FechaEntrada.ToString() + "',foto='" + obj.Foto.ToString() + "',enlace='" + obj.Enlace.ToString() + "',descripcion='" + obj.Descripcion.ToString() + "'," +
                "estado='" + obj.Estado.ToString() + "',padrinos=" + obj.Apadrinado.ToString() + " WHERE Id = " + obj.Id.ToString() + "; ");

        }

        public int eliminar(Perro obj)
        {
            AgenteDB agente = AgenteDB.obtenerAgente();
            return agente.modificar("DELETE FROM animales WHERE Id=" + obj.Id.ToString() + ";");
        }
    }
}
