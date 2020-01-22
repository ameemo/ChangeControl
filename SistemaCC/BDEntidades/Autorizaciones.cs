namespace SistemaCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Autorizaciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Au { get; set; }

        public int? fk_U { get; set; }

        public int? fk_CC { get; set; }

        [Required]
        [StringLength(15)]
        public string Tipo { get; set; }

        public DateTime? Fecha { get; set; }

        public bool Autorizado { get; set; }

        public virtual ControlCambio ControlCambio { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
