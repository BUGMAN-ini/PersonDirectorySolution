namespace PersonDirectory.Domain.Entity
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public ICollection<Person> Persons { get; set; } = new List<Person>();

    }
}
