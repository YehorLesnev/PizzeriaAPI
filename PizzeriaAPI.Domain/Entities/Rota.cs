using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaAPI.Domain.Entities
{
    [Table("rota")]
    public class Rota
    {
        [Key, Column("row_id")]
        public Guid RowId { get; set; }

        [Required, Column("rota_id")]
        public required Guid RotaId { get; set; }

        [Required, Column("date", TypeName = "DateTime")]
        public DateTime Date { get; set; }

        [Required, Column("shift_id")]
        public Guid ShiftId { get; set; }

        [Required, Column("staff_id")]
        public Guid StaffId { get; set; }

        [ForeignKey("StaffId")]
        public Staff Staff { get; set; } = null!;
    }
}
