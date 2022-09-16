using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protectora.Persistencia;

namespace Protectora.Dominio
{
    public class Socio : Persona
    {

        private string datosBancarios;
        private int cuantiaAyuda;
        private string formaPago;
        private SocioDAO socDAO;

        public string DatosBancarios { get => datosBancarios; set => datosBancarios = value; }
        public int CuantiaAyuda { get => cuantiaAyuda; set => cuantiaAyuda = value; }
        public string FormaPago { get => formaPago; set => formaPago = value; }
        internal SocioDAO SocDAO { get => socDAO; set => socDAO = value; }

        public Socio() : base()
        {
            this.SocDAO = new SocioDAO();
        }


        public Socio(Nullable<int> id, string nombreCompleto, string correo, string dni, int telefono,
            string datosBancarios, int cuantiaAyuda, string formaPago) : base(id, nombreCompleto, correo, dni, telefono)
        {
            this.SocDAO = new SocioDAO();
            this.DatosBancarios = datosBancarios;
            this.CuantiaAyuda = cuantiaAyuda;
            this.FormaPago = formaPago;

        }



        public void InsertarSocio()
        {
            this.SocDAO.insertar((Socio)this.MemberwiseClone());
            Console.Write(" ");
        }

        public void EliminarSocio()
        {
            this.SocDAO.eliminar((Socio)this.MemberwiseClone());
            Console.Write(" ");
        }

        public void ModificarSocio()
        {
            this.SocDAO.actualizar((Socio)this.MemberwiseClone());
            Console.Write(" ");
        }

        public List<Socio> LeerTodosSocios()
        {
            List<Socio> arraySocios = new List<Socio>();
            SocioDAO socDAO = new SocioDAO();
            arraySocios = socDAO.leerTodas();
            Console.Write(" ");

            return arraySocios;
        }


    }
}