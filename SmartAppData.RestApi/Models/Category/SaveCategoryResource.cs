using System.ComponentModel.DataAnnotations;

namespace SmartAppData.RestApi.Models.Category
{
    public class SaveCategoryResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
