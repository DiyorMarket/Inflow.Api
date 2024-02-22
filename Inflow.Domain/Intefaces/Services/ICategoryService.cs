using Inflow.Domain.DTOs.Category;
using Inflow.Domain.ResourceParameters;
using Inflow.Domain.Responses;

namespace Inflow.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();
        GetBaseResponse<CategoryDto> GetCategories(CategoryResourceParameters categoryResourceParameters);
        CategoryDto? GetCategoryById(int id);
        CategoryDto CreateCategory(CategoryForCreateDto categoryToCreate);
        CategoryDto UpdateCategory(CategoryForUpdateDto categoryToUpdate);
        void DeleteCategory(int id);
    }
}
