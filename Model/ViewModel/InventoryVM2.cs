using Microsoft.AspNetCore.Mvc.Rendering;
using ProductListAPI.Model;

namespace InventoryWebsite.Models.ViewModel
{

    public class InventoryVM2
    {
        public InventoryVM2 Product { get; set; }
        public List<SelectListItem> CategorySelectList { get; set; }
        public List<SelectListItem> SizeSelectList { get; set; }
    }
}
