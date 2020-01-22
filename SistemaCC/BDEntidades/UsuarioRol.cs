namespace SistemaCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UsuarioRol")]
    public partial class UsuarioRol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_UR { get; set; }

        public int? fk_Us { get; set; }

        public int? fk_Rol { get; set; }

        public virtual Roles Roles { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
