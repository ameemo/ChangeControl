namespace SistemaCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Documentos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Do { get; set; }

        [Required]
        [StringLength(150)]
        public string DocPath { get; set; }

        public int? fk_CC { get; set; }

        public virtual ControlCambio ControlCambio { get; set; }
    }
}
