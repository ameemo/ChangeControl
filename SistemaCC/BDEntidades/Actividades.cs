namespace SistemaCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Actividades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Actividades()
        {
            ActividadesControl = new HashSet<ActividadesControl>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Ac { get; set; }

        [Required]
        [StringLength(15)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(150)]
        public string Descripcion { get; set; }

        public DateTime FechaRealizacion { get; set; }

        [Required]
        [StringLength(150)]
        public string Observaciones { get; set; }

        public int? Responsable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActividadesControl> ActividadesControl { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
