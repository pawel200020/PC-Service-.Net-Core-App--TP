namespace Authn.Models
{
    public class RepairModel
    {
        public int Id {get; set; }
        public string Brand { get; set; }
        public string SerialNumber { get; set; }
        public string Condition { get; set; }
        public string Description { get; set; }
        public int IdOwner { get; set; }
    }
}
