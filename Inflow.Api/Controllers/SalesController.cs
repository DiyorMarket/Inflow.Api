﻿using ClosedXML.Excel;
using Inflow.Api.Helper;
using Inflow.Domain.DTOs.Sale;
using Inflow.Domain.Interfaces.Services;
using Inflow.Domain.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Inflow.Controllers
{
    [Route("api/sales")]
    [ApiController]
    //[Authorize]
    public class SalesController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly ISaleItemService _saleItemService;
        public SalesController(ISaleService saleService, ISaleItemService saleItemService)
        {
            _saleService = saleService;
            _saleItemService = saleItemService;
        }


        [HttpGet(Name = "GetSales")]
        public IActionResult GetSalesAsync(
                 [FromQuery] SaleResourceParameters saleResourceParameters)
        {
            var sales = _saleService.GetSales(saleResourceParameters);
            var links = GetLinks(saleResourceParameters, sales.HasNextPage, sales.HasPreviousPage);
            var metadata = new
            {
                sales.PageNumber,
                sales.PageSize,
                sales.HasNextPage,
                sales.HasPreviousPage,
                sales.TotalPages,
                sales.TotalCount
            };

            var result = new
            {
                data = sales.Data,
                links,
                metadata
            };

            return Ok(result);
        }


        [HttpGet("export")]
        public ActionResult ExportSales()
        {
            var category = _saleService.GetAllSales();

            using XLWorkbook wb = new XLWorkbook();
            var sheet1 = wb.AddWorksheet(GetSalesDataTable(category), "Sales");

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
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sales.xlsx");
        }

        [HttpGet("CustomersSale/{customersId}")]
        public ActionResult<IEnumerable<SaleDto>> GetCustomersSale(int customersId)
        {
            var customersSales = _saleService.GetCustomersSale(customersId);
            return Ok(customersSales);
        }

        [HttpGet("{id}", Name = "GetSaleById")]
        public ActionResult<SaleDto> Get(int id)
        {
            var sale = _saleService.GetSaleById(id);

            if (sale is null)
            {
                return NotFound($"Sale with id: {id} does not exist.");
            }

            return Ok(sale);
        }


        [HttpPost]
        public ActionResult Post([FromBody] SaleForCreateDto sale)
        {
            var createSale = _saleService.CreateSale(sale);

            return CreatedAtAction(nameof(Get), new { createSale.Id }, createSale);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SaleForUpdateDto sale)
        {
            if (id != sale.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {sale.Id}.");
            }

            _saleService.UpdateSale(sale);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _saleService.DeleteSale(id);

            return NoContent();
        }

        private DataTable GetSalesDataTable(IEnumerable<SaleDto> saleDtos)
        {
            DataTable table = new DataTable();
            table.TableName = "Sales Data";
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("SaleDate", typeof(DateTime));
            table.Columns.Add("TotalDue", typeof(decimal));
            table.Columns.Add("CustomerId", typeof(int));

            foreach (var sale in saleDtos)
            {
                table.Rows.Add(sale.Id,
                    sale.SaleDate,
                    sale.TotalDue,
                    sale.CustomerId);
            }

            return table;
        }

        private List<ResourceLink> GetLinks(
           SaleResourceParameters resourceParameters,
           bool hasNext,
           bool hasPrevious)
        {
            List<ResourceLink> links = new();

            links.Add(new ResourceLink(
                "self",
                CreateSaleResourceLink(resourceParameters, ResourceType.CurrentPage),
                "GET"));

            if (hasNext)
            {
                links.Add(new ResourceLink(
                "next",
                CreateSaleResourceLink(resourceParameters, ResourceType.NextPage),
                "GET"));
            }

            if (hasPrevious)
            {
                links.Add(new ResourceLink(
                "previous",
                CreateSaleResourceLink(resourceParameters, ResourceType.PreviousPage),
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

        private string? CreateSaleResourceLink(SaleResourceParameters resourceParameters, ResourceType type)
        {
            if (type == ResourceType.PreviousPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber - 1,
                };
                return Url.Link("GetSales", parameters);
            }

            if (type == ResourceType.NextPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber + 1,
                };
                return Url.Link("GetSales", parameters);
            }

            return Url.Link("GetSales", resourceParameters);
        }
    }
}
