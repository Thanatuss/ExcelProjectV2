
namespace SSO.Share.Domain.Sql.Admin.Area
{
    public class Area
    {

        public Guid id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Code { get; set; }
        public string HierarchicalCode { get; set; }

        public Guid? ParentId { get; set; }
        public Area Parent { get; set; }
        public List<Area> Children { get; set; }

        public string OrganizationId { get; set; } = "6DC91922-D68C-4174-A2F3-5B366ED8E4BC";
        //public Organization.Organization Organization { get; private set; }

        public Guid? AreaTypeId { get; set; }
        public AreaType AreaType{ get; set; }

        //public List<AreaChartUser> AreaChartUsers { get; private set; }
        //public List<EstekhdamUserDetail> BirthEstekhdamUserDetails { get; private set; }
        //public List<EstekhdamUserDetail> AddressEstekhdamUserDetails { get; private set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public float Score { get; set; }
        public float Ratio { get; set; }
        public int Population { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? Creator { get; set; }
        //public Guid? Modifire { get; set; }
    }
}
