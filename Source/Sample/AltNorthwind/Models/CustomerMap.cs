namespace AltNorthwind
{
    using FluentNHibernate.Mapping;

    public class CustomerMap : ClassMap<Customers>
    {
        public CustomerMap()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Id(c => c.CustomerID).Length(5).Not.Nullable();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            Map(c => c.CompanyName).Length(40).Not.Nullable();
            Map(c => c.ContactName).Length(30);
            Map(c => c.ContactTitle).Length(30);
            Map(c => c.Address).Length(60);
            Map(c => c.City).Length(15);
            Map(c => c.Region).Length(15);
            Map(c => c.PostalCode).Length(10);
            Map(c => c.Country).Length(15);
            Map(c => c.Phone).Length(24);
            Map(c => c.Fax).Length(24);
        }
    }
}