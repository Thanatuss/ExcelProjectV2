namespace SSO.Share.Domain.Sql.Admin.Area
{
    public class AreaType
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid Creator { get; set; }
        public Guid? Modifire { get; set; }
        public List<Area> Areas { get; set; } = new();

        public AreaType(string name, string displayName, Guid creator)
        {
            Id = Guid.NewGuid();
            Name = name;
            DisplayName = displayName;
            CreatedOn = DateTime.UtcNow;
            Creator = creator;
        }
    }

}