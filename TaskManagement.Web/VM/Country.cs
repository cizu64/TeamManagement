namespace TaskManagement.Web.VM
{
    public class Country
    {
        public Countries[] Countries { get; set; }
    }

    public class Countries
    {
        public string name { get; set; }
        public bool isActive { get; set; }
        public int id { get; set; }
    }
}
