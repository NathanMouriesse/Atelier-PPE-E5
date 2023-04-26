using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{[Table("Medecin")]
    public partial class Medecin
    {
        [Key]
        public int id_medic { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Adresse { get; set; }

        public bool specialite { get; set; }

        [Required]
        [StringLength(50)]
        public string Telephone { get; set; }

        public int num_dep { get; set; }

        public virtual Departement Departement { get; set; }
    }
}
