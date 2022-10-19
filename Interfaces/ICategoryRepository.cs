using la_mia_pizzeria_crud_webapi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_crud_webapi.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    IEnumerable<SelectListItem> GetSelectListItem();
}