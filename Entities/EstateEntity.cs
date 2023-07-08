namespace EstateManager.Entities
{
    public class Estate
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string? LatLng { get; set; } = string.Empty;
        public IEnumerable<EstateBuilding> Buildings { get; } = new List<EstateBuilding>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
