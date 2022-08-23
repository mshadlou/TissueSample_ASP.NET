using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TissueSample2.Shared.Models
{
    [Table("collections")]
    public class Collection
    {
        [Key]
        public int c_id { get; set; }
        
        [Required(ErrorMessage = "Disease Term is essential")]
        [MaxLength(500)]
        [StringLength(500, ErrorMessage = "Disease Term is too long.")]
        public string disease_term { get; set; } = null!;
        
        [Required(ErrorMessage = "Title is essential")]
        [MaxLength(500)]
        [StringLength(500, ErrorMessage = "Title is too long.")]
        public string title { get; set; } = null!;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public DateTime date { get; set; } = DateTime.Now;


        // Virtual method
        public virtual void Edit() { }
        public virtual void Delete() { }
    }
}
