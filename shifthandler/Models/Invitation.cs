using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace shifthandler.Models
{
    public class Invitation
    {
        public int Id { get; set; }

        [ForeignKey("Shifts")]
        public int ShiftId { get; set; }

        [ForeignKey("Worker")]
        public int WorkerId { get; set; }

        public DateTime? InvitationDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public Guid? ConfirmationGuid { get; set; }

        public virtual Shifts? Shift { get; set; }
        public virtual Worker? Worker { get; set; }
    }
}
