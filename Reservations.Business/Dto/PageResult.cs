
using System.ComponentModel.DataAnnotations;

namespace Reservations.Business.Dto
{

    public class PageResult
    {  
        public PageResult(int skipCount, int maxCount)
        {
            this.SkipCount = skipCount;
            this.MaxCount = maxCount;
        }

        [Required]
        public int SkipCount { get; set; }

       
        [Required]
        public int MaxCount { get; set; }
    }
}