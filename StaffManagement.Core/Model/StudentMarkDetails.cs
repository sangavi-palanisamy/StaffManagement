using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Core.Model
{
    public class StudentMarkDetails
    {
        [Key]
        public int MarkId { get; set; }
        [Required]
        public string Roll_No { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Tamil { get; set; }
        [Required]
        public int English { get; set; }
        [Required]
        public int Maths { get; set; }
        [Required]
        public int Science { get; set; }
        [Required]
        public int Social { get; set; }
        [Required]
        public int Total { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double Average { get; set; }

    }
}
