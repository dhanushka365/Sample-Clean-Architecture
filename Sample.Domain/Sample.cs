using System.ComponentModel.DataAnnotations;

namespace Sample.Domain
{
    public class Sample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
    }
}
