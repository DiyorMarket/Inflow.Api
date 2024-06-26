﻿using ClosedXML.Excel;
using Inflow.Api.Helper;
using Inflow.Domain.DTOs.Category;
using Inflow.Domain.DTOs.Product;
using Inflow.Domain.Interfaces.Services;
using Inflow.Domain.ResourceParameters;
using Inflow.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace Inflow.Controllers
{

    [Route("api/categories")]
    [ApiController]
    //[Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet(Name = "GetCategories")]
        public IActionResult GetCategoriesAsync(
                 [FromQuery] CategoryResourceParameters categoryResourceParameters)
        {
            var categories = _categoryService.GetCategories(categoryResourceParameters);
            var links = GetLinks(categoryResourceParameters, categories.HasNextPage, categories.HasPreviousPage);
            var metadata = new
            {
                categories.PageNumber,
                categories.PageSize,
                categories.HasNextPage,
                categories.HasPreviousPage,
                categories.TotalPages,
                categories.TotalCount
            };

            var result = new
            {
                data = categories.Data,
                links,
                metadata
            };

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<CategoryDto> Get(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            return Ok(category);
        }

        [HttpGet("export/xls")]
        public ActionResult ExportCustomers()
        {
            var categories = _categoryService.GetAllCategories();
            byte[] data = GenerateExcle(categories);

            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Categories.xlsx");
        }

        [HttpGet("export/pdf")]
        public IActionResult CreatePDFDocument()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();

            PdfGrid pdfGrid = new PdfGrid();

            var categories = _categoryService.GetAllCategories();
            List<object> data = ConvertCategoriesToData(categories);

            pdfGrid.DataSource = data;

            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            pdfGrid.Draw(page, new PointF(10, 10));

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            string contentType = "application/pdf";
            string fileName = "categories.pdf";

            return File(stream, contentType, fileName);
        }

        [HttpGet("{id}/products")]
        public ActionResult<ProductDto> GetProductsByCategoryId(
            int id,
            ProductResourceParameters productResourceParameters)
        {
            var products = _productService.GetProducts(productResourceParameters);

            return Ok(products);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoryForCreateDto category)
        {
            var createdCategory = _categoryService.CreateCategory(category);

            return CreatedAtAction(nameof(Get), new { createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CategoryForUpdateDto category)
        {
            if (id != category.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {category.Id}.");
            }

            var updatedCategory = _categoryService.UpdateCategory(category);

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            return NoContent();
        }
        private static byte[] GenerateExcle(IEnumerable<CategoryDto> categoryDtos)
        {
            using XLWorkbook wb = new();
            var sheet1 = wb.AddWorksheet(GetCategoriesDataTable(categoryDtos), "Categories");

            sheet1.Columns(1, 3).Style.Font.FontColor = XLColor.Black;
            sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;
            sheet1.Row(1).Style.Font.FontColor = XLColor.White;

            sheet1.Column(1).Width = 5;
            sheet1.Columns(2, 3).Width = 12;

            sheet1.Row(1).Style.Font.FontSize = 15;
            sheet1.Row(1).Style.Font.Bold = true;
            sheet1.Row(1).Style.Font.Shadow = true;
            sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
            sheet1.Row(1).Style.Font.Italic = false;

            using MemoryStream ms = new();
            wb.SaveAs(ms);

            return ms.ToArray();
        }
        private List<object> ConvertCategoriesToData(IEnumerable<CategoryDto> categories)
        {
            List<object> data = new List<object>();

            foreach (var category in categories)
            {
                data.Add(new { ID = category.Id, category.Name, category.NumberOfProducts });
            }

            return data;
        }
        private static DataTable GetCategoriesDataTable(IEnumerable<CategoryDto> categories)
        {
            DataTable table = new DataTable();
            table.TableName = "Categories Data";
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Number of Products", typeof(int));

            foreach (var category in categories)
            {
                table.Rows.Add(category.Id, category.Name, category.NumberOfProducts);
            }

            return table;
        }

        private List<ResourceLink> GetLinks(
          CategoryResourceParameters resourceParameters,
          bool hasNext,
          bool hasPrevious)
        {
            List<ResourceLink> links = new();

            links.Add(new ResourceLink(
                "self",
                CreateCategoryResourceLink(resourceParameters, ResourceType.CurrentPage),
                "GET"));

            if (hasNext)
            {
                links.Add(new ResourceLink(
                "next",
                CreateCategoryResourceLink(resourceParameters, ResourceType.NextPage),
                "GET"));
            }

            if (hasPrevious)
            {
                links.Add(new ResourceLink(
                "previous",
                CreateCategoryResourceLink(resourceParameters, ResourceType.PreviousPage),
                "GET"));
            }

            foreach (var link in links)
            {
                var lastIndex = link.Href.IndexOf("/api");
                if (lastIndex >= 0)
                {
                    link.Href = "https://0wn6qg77-7258.asse.devtunnels.ms" + link.Href.Substring(lastIndex);
                }
            }

            return links;
        }

        private string? CreateCategoryResourceLink(CategoryResourceParameters resourceParameters, ResourceType type)
        {
            if (type == ResourceType.PreviousPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber - 1,
                };
                return Url.Link("GetCategories", parameters);
            }

            if (type == ResourceType.NextPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber + 1,
                };
                return Url.Link("GetCategories", parameters);
            }

            return Url.Link("GetCategories", resourceParameters);
        }
    }
}

