using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TissueSample2.Shared.Models
{
    [Table("samples")]
    public class Sample
    {
        [Key]
        public int id { get; set; }
        
        [Required]
        public int c_id { get; set; }
        
        [Required(ErrorMessage = "Donor Count is essential")]
        [Range(1, 2000000, ErrorMessage = "Donor Count must be greater than 0.")]
        public int donor_count { get; set; } = 0;
        
        [Required(ErrorMessage = "Material Type is essential")]
        [MaxLength(500)]
        [StringLength(500, ErrorMessage = "Material Type is too long.")]
        public string mat_type { get; set; } = null!;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime date { get; set; }

        public Sample(int id, int c_id, int donor_count, string mat_type)
        {
            this.id = id;
            this.c_id = c_id;
            this.donor_count = donor_count;
            this.mat_type = mat_type;
            this.date = DateTime.Now;
        }

        // Virtual method
        public virtual void Edit() { }
        public virtual void Delete() { }

    }
}