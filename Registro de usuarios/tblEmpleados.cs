//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Registro_de_usuarios
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblEmpleados
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Nullable<int> Edad { get; set; }
        public string Email { get; set; }
        public Nullable<int> IdPuesto { get; set; }
        public Nullable<int> IdPais { get; set; }
        public Nullable<int> IdEstado { get; set; }
    
        public virtual tblCatPuestos tblCatPuestos { get; set; }
        public virtual tblCatEstados tblCatEstados { get; set; }
        public virtual tblCatPaises tblCatPaises { get; set; }
    }
}