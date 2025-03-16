namespace SSO.Share.Domain.Sql.Admin.Area
{
    public class Area
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string HierarchicalCode { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }
        public Area? Parent { get; set; }
        public List<Area> Children { get; set; } = new();

        public string OrganizationId { get; set; } = "6DC91922-D68C-4174-A2F3-5B366ED8E4BC";

        public Guid? AreaTypeId { get; set; }
        public AreaType? AreaType { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public float Score { get; set; }
        public float Ratio { get; set; }
        public int Population { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? Creator { get; set; }
        public Guid? Modifier { get; set; } 
    }
}