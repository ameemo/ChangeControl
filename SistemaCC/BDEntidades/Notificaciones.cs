namespace SistemaCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notificaciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_No { get; set; }

        [Required]
        [StringLength(150)]
        public string Contenido { get; set; }

        public DateTime FechaEnvio { get; set; }

        public int? fk_U { get; set; }

        public int? fk_CC { get; set; }

        public virtual ControlCambio ControlCambio { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
