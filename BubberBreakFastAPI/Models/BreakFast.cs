using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BubberBreakFastAPI.Models
{
    public class BreakFast
    {                     
        [Key]
        public Guid id { get;  private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string TasteOfOrigin { get; private set; }
        public DateTime LastModifiedDate { get; private set; }

        public BreakFast() { }

        public BreakFast(Guid id,
                         string name,
                         string description,
                         DateTime startDate,
                         DateTime endDate,
                         string tasteOfOrigin,
                         DateTime lastModifiedDate)
        {
            this.id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            TasteOfOrigin = tasteOfOrigin;
            LastModifiedDate = lastModifiedDate;
        }
       
        public void Update(string name , string description,DateTime startDate,DateTime endDate,string tasteOfOrigin)
        {
            Name = name;
            Description = description;
            StartDate= startDate;
            EndDate = endDate;
            TasteOfOrigin= tasteOfOrigin;
            LastModifiedDate = DateTime.Now;
        }


    }
}

