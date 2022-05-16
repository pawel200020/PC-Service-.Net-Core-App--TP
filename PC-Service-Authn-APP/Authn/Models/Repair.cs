
using Authn.DataDAO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authn.Models
{
    public class Repair
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Device Brand")]
        [Required(ErrorMessage = "Please select a brand of a device", AllowEmptyStrings = false)]
        public string Brand { get; set; }
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Required(ErrorMessage = "Please select a condition of a device", AllowEmptyStrings = false)]
        public string Condition { get; set; }
        [Required(ErrorMessage = "Please describe a problem with your device", AllowEmptyStrings = false)]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "This filed must contains at least 20 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Select a delivery method", AllowEmptyStrings = false)]
        [Display(Name = "Delivery Method")]
        public string Delivery { get; set; }
        [Display(Name = "Delivery date")]
        //[DateValidation(ErrorMessage = "Sorry, the date can't be later than today's date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        /// <summary>
        /// not public to form
        /// </summary>
        public string ImageName { get; set; }
        [NotMapped]
        [Display(Name = "Upload image with a problem")]
        public IFormFile ImageFile { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Report { get; set; }
        [Display(Name = "Parts used to repair device")]
        public string PartsUsed { get; set; }
        public Dictionary<string, int> PartsUsedList
        {
            get
            {
                if (PartsUsed == null)
                {
                    PartsUsed = "";
                    return new Dictionary<string, int>();
                }
                else if (PartsUsed == "")
                {
                    return new Dictionary<string, int>();
                }
                else
                {
                    var parts = PartsUsed.Split(new string[] { "\r\n", "\r", "\n" },StringSplitOptions.None
);
                    var result = new Dictionary<string, int>();
                    foreach (var part in parts)
                    {
                        var tmp = part.Split(';');
                        if (tmp.Length > 1)
                        {
                            result.Add(tmp[0], Int32.Parse(tmp[1]));
                        }
                        else
                        {
                            result.Add(tmp[0], 1);
                        }

                    }
                    return result;

                }

            }
        }
        [Display(Name = "Price for staff work")]
        public decimal Labour { get; set; }
        public decimal TotalCost
        {
            get
            {
                PartDAO partDAO = new PartDAO("DataSource=Data\\app.db");
                decimal totalCost = 0;
                if (PartsUsed != "")
                {
                    foreach (KeyValuePair<string, int> item in PartsUsedList)
                    {
                        var partc = partDAO.GetPart(item.Key);
                        if (partc != null)
                        {
                            totalCost += partc.Price * item.Value;
                        }
                    }
                }
                DelivertiesDAO delivertiesDAO = new DelivertiesDAO("DataSource=Data\\app.db");

                return totalCost + Labour + delivertiesDAO.GetDeliveryPrice(this.Delivery);
            }
        }

    }
}
