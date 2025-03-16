namespace SSO.Share.Domain.Sql.Admin.Area
{
    public class AreaType
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public Guid Creator { get; private set; }
        public Guid? Modifier { get; private set; }
        public List<Area> Areas { get; private set; } = new();

        public AreaType(string name, string displayName, Guid creator)
        {
            Id = Guid.NewGuid();
            Name = name;
            DisplayName = displayName;
            CreatedOn = DateTime.UtcNow;
            Creator = creator;
            ModifiedOn = null;
        }

        public void Update(string name, string displayName, Guid modifier)
        {
            Name = name;
            DisplayName = displayName;
            Modifier = modifier;
            ModifiedOn = DateTime.UtcNow;
        }
    }
}