namespace SistemaCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Revisiones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Re { get; set; }

        [Required]
        [StringLength(150)]
        public string InfGeneral { get; set; }

        [Required]
        [StringLength(150)]
        public string Actividades { get; set; }

        [Required]
        [StringLength(150)]
        public string Riesgos { get; set; }

        [Required]
        [StringLength(150)]
        public string Servicios { get; set; }

        public int? fk_CC { get; set; }

        public virtual ControlCambio ControlCambio { get; set; }
    }
}
