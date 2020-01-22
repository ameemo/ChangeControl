namespace SistemaCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActividadesControl")]
    public partial class ActividadesControl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_CA { get; set; }

        public int? fk_Ac { get; set; }

        public int? fk_CC { get; set; }

        public virtual Actividades Actividades { get; set; }

        public virtual ControlCambio ControlCambio { get; set; }
    }
}
