namespace OurSolarSystemAPI.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Defaults to current time
        public int RequestedNoradId { get; set; } // NORAD ID of the satellite
    }
}
