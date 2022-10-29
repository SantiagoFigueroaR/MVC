using System.Collections.Generic;

namespace Registro_de_usuarios.Models
{
    public class EmpleadoModel
    {
        public IEnumerable<tblEmpleados> Empleados
        {
            get;
            set;
        }

        public IEnumerable<tblCatPuestos> Puestos
        {
            get;
            set;
        }

        public IEnumerable<tblCatPaises> Paises
        {
            get;
            set;
        }

        public IEnumerable<tblCatEstados>Estados
        {
            get;
            set;
        }

        public tblEmpleados CurrentEmpleado
        {
            get;
            set;
        }

        public tblCatPuestos CurrentPuesto
        {
            get;
            set;
        }

        public tblCatPaises CurrentPaisVAR
        {
            get;
            set;
        }

        public tblCatEstados CurrentEstado
        {
            get;
            set;
        }
    }
}