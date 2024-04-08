using ClosedXML.Excel;
using DiyorMarket.Services;
using Inflow.Api.Helper;
using Inflow.Domain.DTOs.SaleItem;
using Inflow.Domain.DTOsSaleItem;
using Inflow.Domain.Interfaces.Services;
using Inflow.Domain.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Inflow.Controllers
{
    [Route("api/saleItems")]
    [ApiController]
    //[Authorize]
    public class SaleItemsController : Controller
    {
        private readonly ISaleItemService _saleItemService;
        public SaleItemsController(ISaleItemService saleItemService)
        {
            _saleItemService = saleItemService;
        }


        [HttpGet(Name = "GetSaleItems")]
        public IActionResult GetSaleItemsAsync(
                 [FromQuery] SaleItemResourceParameters saleItemResourceParameters)
        {
            var saleItems = _saleItemService.GetSaleItems(saleItemResourceParameters);
            var links = GetLinks(saleItemResourceParameters, saleItems.HasNextPage, saleItems.HasPreviousPage);
            var metadata = new
            {
                saleItems.PageNumber,
                saleItems.PageSize,
                saleItems.HasNextPage,
                saleItems.HasPreviousPage,
                saleItems.TotalPages,
                saleItems.TotalCount
            };

            var result = new
            {
                data = saleItems.Data,
                links,
                metadata
            };

            return Ok(result);
        }


        [HttpGet("SalesSaleItems/{salesId}")]
        public ActionResult<IEnumerable<SaleItemDto>> GetSalesSaleItems(int salesId)
        {
            var salesSaleItems = _saleItemService.GetSalesSaleItems(salesId);
            return Ok(salesSaleItems);
        }
        [HttpGet("export/{saleId}")]
        public ActionResult ExportSaleItems(int saleId)
        {
            var saleItems = _saleItemService.GetSalesSaleItems(saleId);

            using XLWorkbook wb = new XLWorkbook();
            var sheet1 = wb.AddWorksheet(GetSaleItemssDataTable(saleItems), "SaleItems");

            sheet1.Column(1).Style.Font.FontColor = XLColor.Red;

            sheet1.Columns(2, 4).Style.Font.FontColor = XLColor.Blue;

            sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;
            //sheet1.Row(1).Cells(1,3).Style.Fill.BackgroundColor = XLColor.Yellow;
            sheet1.Row(1).Style.Font.FontColor = XLColor.White;

            sheet1.Row(1).Style.Font.Bold = true;
            sheet1.Row(1).Style.Font.Shadow = true;
            sheet1.Row(1).Style.Font.Underline = XLFontUnderlineValues.Single;
            sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
            sheet1.Row(1).Style.Font.Italic = true;

            sheet1.Rows(2, 3).Style.Font.FontColor = XLColor.AshGrey;

            using MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SaleItems.xlsx");
        }

        [HttpGet("{id}", Name = "GetSaleItemById")]
        public ActionResult<SaleItemDto> Get(int id)
        {
            var saleItem = _saleItemService.GetSaleItemById(id);

            if (saleItem is null)
            {
                return NotFound($"SaleItem with id: {id} does not exist.");
            }

            return Ok(saleItem);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SaleItemForCreateDto saleItem)
        {
            var createSaleItem = _saleItemService.CreateSaleItem(saleItem);

            return CreatedAtAction(nameof(Get), new { id = createSaleItem.Id }, createSaleItem);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SaleItemForUpdateDto saleItem)
        {
            if (id != saleItem.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {saleItem.Id}.");
            }

            _saleItemService.UpdateSaleItem(saleItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _saleItemService.DeleteSaleItem(id);

            return NoContent();
        }
        private DataTable GetSaleItemssDataTable(IEnumerable<SaleItemDto> saleItemDtos)
        {
            DataTable table = new DataTable();
            table.TableName = "Sales Data";
            table.Columns.Add("ProductName", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("UnitPrice", typeof(decimal));
            table.Columns.Add("TotalDue", typeof(decimal));

            foreach (var saleitem in saleItemDtos)
            {
                table.Rows.Add(
                    saleitem.ProductName,
                    saleitem.Quantity,
                    saleitem.UnitPrice,
                    saleitem.TotalDue);
            }

            return table;
        }
        private List<ResourceLink> GetLinks(
             SaleItemResourceParameters resourceParameters,
             bool hasNext,
             bool hasPrevious)
        {
            List<ResourceLink> links = new();

            links.Add(new ResourceLink(
                "self",
                CreateSaleItemResourceLink(resourceParameters, ResourceType.CurrentPage),
                "GET"));

            if (hasNext)
            {
                links.Add(new ResourceLink(
                "next",
                CreateSaleItemResourceLink(resourceParameters, ResourceType.NextPage),
                "GET"));
            }

            if (hasPrevious)
            {
                links.Add(new ResourceLink(
                "previous",
                CreateSaleItemResourceLink(resourceParameters, ResourceType.PreviousPage),
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

        private string? CreateSaleItemResourceLink(SaleItemResourceParameters resourceParameters, ResourceType type)
        {
            if (type == ResourceType.PreviousPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber - 1,
                };
                return Url.Link("GetSaleItems", parameters);
            }

            if (type == ResourceType.NextPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber + 1,
                };
                return Url.Link("GetSaleItems", parameters);
            }

            return Url.Link("GetSaleItems", resourceParameters);
        }
    }
}
