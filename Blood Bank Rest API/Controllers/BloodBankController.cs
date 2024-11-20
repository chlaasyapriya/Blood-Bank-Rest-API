using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blood_Bank_Rest_API.Models;

namespace Blood_Bank_Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodBankController : ControllerBase
    {
        static List<BloodBankEntry> bloodBankEntries = new List<BloodBankEntry>()
        {
            new BloodBankEntry { Id = 1, DonorName="James", Age=21, BloodType="A+", ContactInfo="james@gmail.com", Quantity=500, CollectionDate=new DateOnly(2024,10,11), ExpirationDate=new DateOnly(2024,11,16), Status="Available"},
            new BloodBankEntry { Id = 2, DonorName="Albus", Age=20, BloodType="AB+", ContactInfo="albus@gmail.com", Quantity=450, CollectionDate=new DateOnly(2024,11,03), ExpirationDate=new DateOnly(2024,12,8), Status="Requested"},
            new BloodBankEntry { Id = 3, DonorName="Jessica", Age=21, BloodType="A-", ContactInfo="jessica@gmail.com", Quantity=400, CollectionDate=new DateOnly(2024,09,10), ExpirationDate=new DateOnly(2024,10,15), Status="Expired"},
            new BloodBankEntry { Id = 4, DonorName="Scorpious", Age=21, BloodType="O+", ContactInfo="scorpious@gmail.com", Quantity=550, CollectionDate=new DateOnly(2024,10,31), ExpirationDate=new DateOnly(2024,12,05), Status="Available"}
        };

        [HttpGet]
        public ActionResult<IEnumerable<BloodBankEntry>> GetAllBloodBankEntries()
        {
            if (bloodBankEntries.Any())
                return bloodBankEntries;
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<BloodBankEntry> GetBloodBankEntryById(int id)
        {
            var bloodBankEntry=bloodBankEntries.Find(entry=> entry.Id == id);
            if(bloodBankEntry == null)
                return NotFound();
            return bloodBankEntry;
        }

        [HttpPost]
        public ActionResult<BloodBankEntry> AddBloodBankEntry(BloodBankEntry bloodBankEntry)
        {
            if (bloodBankEntry.Age < 18)
                return BadRequest("Minimum age for donating blood is 18 years.");
            if (bloodBankEntry.Quantity <= 0)
                return BadRequest("Enter quantity of blood donated in ml");
            bloodBankEntry.Id = bloodBankEntries.Any()? bloodBankEntries.Max(i => i.Id)+1 :1;
            bloodBankEntry.CollectionDate = DateOnly.FromDateTime(DateTime.Now);
            bloodBankEntry.ExpirationDate = bloodBankEntry.CollectionDate.AddDays(35);
            bloodBankEntry.Status = "Available";
            bloodBankEntries.Add(bloodBankEntry);
            return CreatedAtAction(nameof(GetBloodBankEntryById), new { Id = bloodBankEntry.Id }, bloodBankEntry);
        }

        [HttpPut ("{id}")]
        public ActionResult<BloodBankEntry> UpdateBloodBankEntry(int id,BloodBankEntry bloodBankEntry)
        {
            var entry=bloodBankEntries.Find(ent=>ent.Id == id);
            if(entry == null)
                return NotFound();
            if (bloodBankEntry.Age < 18)
                return BadRequest("Minimum age for donating blood is 18 years.");
            if (bloodBankEntry.Quantity <= 0)
                return BadRequest("Enter quantity of blood donated in ml");
            entry.DonorName = bloodBankEntry.DonorName;
            entry.Age = bloodBankEntry.Age;
            entry.BloodType = bloodBankEntry.BloodType;
            entry.ContactInfo= bloodBankEntry.ContactInfo;
            entry.Quantity= bloodBankEntry.Quantity;
            entry.Status= bloodBankEntry.Status;
            return CreatedAtAction(nameof(GetBloodBankEntryById), new { Id = id }, entry);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBloodBankEntry(int id)
        {
            var bloodBankEntry=bloodBankEntries.Find(entry=>entry.Id == id);
            if(bloodBankEntry == null)
                return NotFound();
            bloodBankEntries.Remove(bloodBankEntry);
            return NoContent();
        }

        [HttpGet("page={pageNumber}&size={pageSize}")]
        public ActionResult<IEnumerable<BloodBankEntry>> GetPaginatedEntries(int pageNumber, int pageSize)
        {
            var resultEntries=bloodBankEntries.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            return resultEntries;
        }

        [HttpGet("searchbloodType={bloodType}")]
        public ActionResult<IEnumerable<BloodBankEntry>> GetEntriesByBloodType(string bloodType)
        {
            var resultEntries = bloodBankEntries.FindAll(entry => entry.BloodType == bloodType);
            return resultEntries;
        }

        [HttpGet("searchstatus={status}")]
        public ActionResult<IEnumerable<BloodBankEntry>> GetEntriesByStatus(string status)
        {
            var resultEntries=bloodBankEntries.FindAll(entry=>entry.Status.Equals(status,StringComparison.OrdinalIgnoreCase));
            return resultEntries;
        }

        [HttpGet("searchdonorName={donorName}")]
        public ActionResult<IEnumerable<BloodBankEntry>> GetEntriesByName(string donorName)
        {
            var resultEntries=bloodBankEntries.FindAll(entry=>entry.DonorName.Contains(donorName, StringComparison.OrdinalIgnoreCase));
            return resultEntries;
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<BloodBankEntry>> SearchBloodBankEntries(string parameter, string key)
        {
            var resultEntries=bloodBankEntries;
            if (parameter.Equals("bloodtype", StringComparison.OrdinalIgnoreCase))
                resultEntries = bloodBankEntries.FindAll(entry => entry.BloodType == key);
            else if (parameter.Equals("status", StringComparison.OrdinalIgnoreCase))
                resultEntries = bloodBankEntries.FindAll(entry => entry.Status == key);
            else if (parameter.Equals("donorname", StringComparison.OrdinalIgnoreCase))
                resultEntries = bloodBankEntries.FindAll(entry => entry.DonorName == key);
            else
                return BadRequest("Enter a proper parameter for searching");
            return resultEntries;
        }

        [HttpGet ("filter")]
        public ActionResult<IEnumerable<BloodBankEntry>> FilterBloodBankEntries(string bloodType=null, string status=null, string donorName = null)
        {
            var resultEntries=bloodBankEntries;
            if (!string.IsNullOrEmpty(bloodType))
                resultEntries = resultEntries.FindAll(entry => entry.BloodType == bloodType);
            if (!string.IsNullOrEmpty(status))
                resultEntries = resultEntries.FindAll(entry => entry.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(donorName))
                resultEntries = resultEntries.FindAll(entry => entry.DonorName.Contains(donorName, StringComparison.OrdinalIgnoreCase));
            return resultEntries;
        }

        [HttpGet("sort")]
        public ActionResult<IEnumerable<BloodBankEntry>> GetSortedBloodBankEntries(string parameter,string order)
        {
            var resultEntries=bloodBankEntries;
            if (parameter.Equals("bloodType", StringComparison.OrdinalIgnoreCase))
                if (order.Equals("desc", StringComparison.OrdinalIgnoreCase))
                    resultEntries = resultEntries.OrderByDescending(entry => entry.BloodType).ToList();
                else if (order.Equals("asc", StringComparison.OrdinalIgnoreCase))
                    resultEntries = resultEntries.OrderBy(entry => entry.BloodType).ToList();
                else
                    return BadRequest("Enter proper order of sorting");
            else if (parameter.Equals("collectionDate", StringComparison.OrdinalIgnoreCase))
                if (order.Equals("desc", StringComparison.OrdinalIgnoreCase))
                    resultEntries = resultEntries.OrderByDescending(entry => entry.CollectionDate).ToList();
                else if (order.Equals("asc", StringComparison.OrdinalIgnoreCase))
                    resultEntries = resultEntries.OrderBy(entry => entry.CollectionDate).ToList();
                else
                    return BadRequest("Enter proper order of sorting");
            else
                return BadRequest("Enter proper parameter for sorting");
            return resultEntries;
        }
    }
}
