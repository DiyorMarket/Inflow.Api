using ClosedXML.Excel;
using Inflow.Api.Helper;
using Inflow.Domain.DTOs.Supply;
using Inflow.Domain.Interfaces.Services;
using Inflow.Domain.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Inflow.Controllers
{
    [Route("api/supplies")]
    [ApiController]
    //[Authorize]
    public class SuppliesController : Controller
    {
        private readonly ISupplyService _supplyService;
        public SuppliesController(ISupplyService supplyService)
        {
            _supplyService = supplyService;
        }

        [HttpGet(Name = "GetSupplies")]
        public IActionResult GetSuppliesAsync(
          [FromQuery] SupplyResourceParameters supplyResourceParameters)
        {
            var supplies = _supplyService.GetSupplies(supplyResourceParameters);
            var links = GetLinks(supplyResourceParameters, supplies.HasNextPage, supplies.HasPreviousPage);
            var metadata = new
            {
                supplies.PageNumber,
                supplies.PageSize,
                supplies.HasNextPage,
                supplies.HasPreviousPage,
                supplies.TotalPages,
                supplies.TotalCount
            };

            var result = new
            {
                data = supplies.Data,
                links,
                metadata
            };

            return Ok(result);
        }

        [HttpGet("export")]
        public ActionResult ExportSupplies()
        {
            var category = _supplyService.GetAllSupplies();

            using XLWorkbook wb = new XLWorkbook();
            var sheet1 = wb.AddWorksheet(GetSuppliesDataTable(category), "Supplies");

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
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Supplies.xlsx");
        }

        [HttpGet("{id}", Name = "GetSupplyById")]
        public ActionResult<SupplyDto> Get(int id)
        {
            var supply = _supplyService.GetSupplyById(id);

            if (supply is null)
            {
                return NotFound($"Supply with id: {id} does not exist.");
            }

            return Ok(supply);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplyForCreateDto supply)
        {
            var createSupply = _supplyService.CreateSupply(supply);

            return CreatedAtAction(nameof(Get), new { createSupply.Id }, createSupply);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplyForUpdateDto supply)
        {
            if (id != supply.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supply.Id}.");
            }

            _supplyService.UpdateSupply(supply);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _supplyService.DeleteSupply(id);

            return NoContent();
        }

        private DataTable GetSuppliesDataTable(IEnumerable<SupplyDto> supplyDtos)
        {
            DataTable table = new DataTable();
            table.TableName = "Supplies Data";
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("TotalDue", typeof(decimal));
            table.Columns.Add("SupplyDate", typeof(DateTime));
            table.Columns.Add("SupplierId", typeof(int));

            foreach (var supply in supplyDtos)
            {
                table.Rows.Add(supply.Id,
                    supply.TotalDue,
                    supply.SupplyDate,
                    supply.SupplierId);
            }

            return table;
        }

        private List<ResourceLink> GetLinks(
        SupplyResourceParameters resourceParameters,
        bool hasNext,
        bool hasPrevious)
        {
            List<ResourceLink> links = new();

            links.Add(new ResourceLink(
                "self",
                CreateSuppliesResourceLink(resourceParameters, ResourceType.CurrentPage),
                "GET"));

            if (hasNext)
            {
                links.Add(new ResourceLink(
                "next",
                CreateSuppliesResourceLink(resourceParameters, ResourceType.NextPage),
                "GET"));
            }

            if (hasPrevious)
            {
                links.Add(new ResourceLink(
                "previous",
                CreateSuppliesResourceLink(resourceParameters, ResourceType.PreviousPage),
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

        private string? CreateSuppliesResourceLink(SupplyResourceParameters resourceParameters, ResourceType type)
        {
            if (type == ResourceType.PreviousPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber - 1,
                };
                return Url.Link("GetSupplies", parameters);
            }

            if (type == ResourceType.NextPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber + 1,
                };
                return Url.Link("GetSupplies", parameters);
            }

            return Url.Link("GetSupplies", resourceParameters);
        }
    }
}
