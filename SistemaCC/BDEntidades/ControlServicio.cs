namespace SistemaCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ControlServicio")]
    public partial class ControlServicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_CS { get; set; }

        public int? fk_CC { get; set; }

        public int? fk_SA { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinal { get; set; }

        public virtual ControlCambio ControlCambio { get; set; }

        public virtual ServiciosAplicaciones ServiciosAplicaciones { get; set; }
    }
}
