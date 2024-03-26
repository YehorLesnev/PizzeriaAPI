using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaAPI.Domain.Entities
{
    [Table("staff")]
    public class Staff
    {
        [Key, Column("staff_id")]
        public Guid Id { get; set; }

        [Column("first_name", TypeName = "varchar")]
        [MaxLength(55)]
        public string FirstName { get; set; } = null!;
        
        [Column("last_name", TypeName = "varchar")]
        [MaxLength(55)]
        public string LastName { get; set; } = null!;

        [Required, Column("position", TypeName = "varchar")]
        [MaxLength(100)]
        public required string Position { get; set; }

        [Column("hourly_rate", TypeName = "Decimal")]
        public decimal HourlyRate { get; set; }


    }
}
