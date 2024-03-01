using Inflow.Domain.DTOs.Category;
using Inflow.Domain.Pagniation;
using Inflow.Domain.ResourceParameters;

namespace Inflow.Domain.Interfaces.Services;

public interface ICategoryService
{
    Task<PaginatedList<CategoryDto>> GetCategoriesAsync(CategoryResourceParameters categoryResourceParameters);
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryDto> CreateCategoryAsync(CategoryForCreateDto categoryToCreate);
    Task<CategoryDto> UpdateCategory(CategoryForUpdateDto categoryToUpdate);
    Task DeleteCategory(int id);
}
