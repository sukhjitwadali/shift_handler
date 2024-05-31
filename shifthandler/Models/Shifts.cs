namespace shifthandler.Models
{
    public class Shifts
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string? Location { get; set; }
        public string? Task { get; set; }
        public decimal? Rate { get; set; }
/*        public virtual ICollection<Invitation> Invitations { get; set; }
*/    }
   
}
