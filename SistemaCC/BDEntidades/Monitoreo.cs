namespace SistemaCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Monitoreo")]
    public partial class Monitoreo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_At { get; set; }

        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(15)]
        public string Tipo { get; set; }

        public int? fk_U { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
