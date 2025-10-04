namespace CRM
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<string> Likely { get; set; } = new List<string>();
        public string Access { get; set; }
        public List<int> Offers { get; set; } = new List<int>();
    }
}
