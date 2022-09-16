using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protectora.Persistencia;

namespace Protectora.Dominio
{
    public class Padrino : Persona
    {

        private string datosBancarios;
        private int importeMensual;
        private string formaPago;
        private DateTime fechaEntrada;
        private PadrinoDAO padDAO;

        public string DatosBancarios { get => datosBancarios; set => datosBancarios = value; }
        public int ImporteMensual { get => importeMensual; set => importeMensual = value; }
        public string FormaPago { get => formaPago; set => formaPago = value; }
        public DateTime FechaEntrada { get => fechaEntrada; set => fechaEntrada = value; }
        internal PadrinoDAO PadDAO { get => padDAO; set => padDAO = value; }

        public Padrino() : base()
        {
            this.PadDAO = new PadrinoDAO();
        }


        public Padrino(Nullable<int> id, string nombreCompleto, string correo, string dni, int telefono,
            string datosBancarios, int importeMensual, string formaPago, DateTime fechaEntrada) : base(id, nombreCompleto, correo, dni, telefono)
        {
            this.PadDAO = new PadrinoDAO();
            this.DatosBancarios = datosBancarios;
            this.ImporteMensual = importeMensual;
            this.FormaPago = formaPago;
            this.FechaEntrada = fechaEntrada;

        }
        public int ObtenerId()
        {
            return this.PadDAO.obtenerId((Padrino)this.MemberwiseClone());
        }
        public void InsertarPadrino()
        {
            this.PadDAO.insertar((Padrino)this.MemberwiseClone());
            Console.Write(" ");
        }

        public void EliminarPadrino()
        {
            this.PadDAO.eliminar((Padrino)this.MemberwiseClone());
            Console.Write(" ");
        }

        public void ModificarPadrino()
        {
            this.PadDAO.actualizar((Padrino)this.MemberwiseClone());
            Console.Write(" ");
        }

        public Padrino LeerunPadrino()
        {
            Console.Write(" ");
            return this.PadDAO.leer((Padrino)this.MemberwiseClone());
        }

        public List<Padrino> LeerTodosPadrino()
        {
            List<Padrino> arrayPadrino = new List<Padrino>();
            PadrinoDAO padDAO = new PadrinoDAO();
            arrayPadrino = padDAO.leerTodas();
            Console.Write(" ");

            return arrayPadrino;
        }

    }
}