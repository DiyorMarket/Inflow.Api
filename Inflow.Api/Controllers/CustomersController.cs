﻿using ClosedXML.Excel;
using Inflow.Api.Helper;
using Inflow.Domain.DTOs.Customer;
using Inflow.Domain.Interfaces.Services;
using Inflow.Domain.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace Inflow.Controllers
{
    [Route("api/customers")]
    [ApiController]
    //[Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet(Name = "GetCustomers")]
        public IActionResult GetCategoriesAsync(
                 [FromQuery] CustomerResourceParameters customerResourceParameters)
        {
            var customers = _customerService.GetCustomers(customerResourceParameters);
            var links = GetLinks(customerResourceParameters, customers.HasNextPage, customers.HasPreviousPage);
            var metadata = new
            {
                customers.PageNumber,
                customers.PageSize,
                customers.HasNextPage,
                customers.HasPreviousPage,
                customers.TotalPages,
                customers.TotalCount
            };

            var result = new
            {
                data = customers.Data,
                links,
                metadata
            };

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<CustomerDto> Get(int id)
        {
            var customer = _customerService.GetCustomerById(id);

            if (customer is null)
            {
                return NotFound($"Customer with id: {id} does not exist.");
            }

            return Ok(customer);
        }

        [HttpGet("export/xls")]
        public ActionResult ExportCustomers()
        {
            var customers = _customerService.GetCustomers();
            byte[] data = GenerateExcle(customers);

            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
        }
        [HttpGet("export/pdf")]
        public IActionResult CreatePDFDocument()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();

            PdfGrid pdfGrid = new PdfGrid();

            var customers = _customerService.GetCustomers();

            List<object> data = ConvertCustomerToData(customers);

            pdfGrid.DataSource = data;

            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

            pdfGrid.Draw(page, new PointF(10, 10));

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            string contentType = "application/pdf";
            string fileName = "customers.pdf";

            return File(stream, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CustomerForCreateDto customer)
        {
            var createCustomer = _customerService.CreateCustomer(customer);

            return CreatedAtAction(nameof(Get), new { createCustomer.Id }, createCustomer);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CustomerForUpdateDto customer)
        {
            if (id != customer.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {customer.Id}.");
            }

            var updateCastomer = _customerService.UpdateCustomer(customer);

            return Ok(updateCastomer);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);

            return NoContent();
        }

        private List<object> ConvertCustomerToData(IEnumerable<CustomerDto> customerDtos)
        {
            List<object> data = new List<object>();

            foreach (var customer in customerDtos)
            {
                data.Add(new { ID = customer.Id, customer.FullName, customer.PhoneNumber });
            }

            return data;
        }
        private static byte[] GenerateExcle(IEnumerable<CustomerDto> customerDtos)
        {
            using XLWorkbook wb = new();
            var sheet1 = wb.AddWorksheet(GetCustomersDataTable(customerDtos), "Categories");

            sheet1.Columns(1, 3).Style.Font.FontColor = XLColor.Black;
            sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;
            sheet1.Row(1).Style.Font.FontColor = XLColor.White;

            sheet1.Column(1).Width = 5;
            sheet1.Columns(2, 3).Width = 18;

            sheet1.Row(1).Style.Font.FontSize = 15;
            sheet1.Row(1).Style.Font.Bold = true;
            sheet1.Row(1).Style.Font.Shadow = true;
            sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
            sheet1.Row(1).Style.Font.Italic = false;

            using MemoryStream ms = new();
            wb.SaveAs(ms);

            return ms.ToArray();
        }
        private static DataTable GetCustomersDataTable(IEnumerable<CustomerDto> customers)
        {
            DataTable table = new DataTable();
            table.TableName = "Customers Data";
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Phone Number", typeof(string));

            foreach (var customer in customers)
            {
                table.Rows.Add(customer.Id, customer.FullName, customer.PhoneNumber);
            }

            return table;
        }
        private List<ResourceLink> GetLinks(
          CustomerResourceParameters resourceParameters,
          bool hasNext,
          bool hasPrevious)
        {
            List<ResourceLink> links = new();

            links.Add(new ResourceLink(
                "self",
                CreateCustomerResourceLink(resourceParameters, ResourceType.CurrentPage),
                "GET"));

            if (hasNext)
            {
                links.Add(new ResourceLink(
                "next",
                CreateCustomerResourceLink(resourceParameters, ResourceType.NextPage),
                "GET"));
            }

            if (hasPrevious)
            {
                links.Add(new ResourceLink(
                "previous",
                CreateCustomerResourceLink(resourceParameters, ResourceType.PreviousPage),
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

        private string? CreateCustomerResourceLink(CustomerResourceParameters resourceParameters, ResourceType type)
        {
            if (type == ResourceType.PreviousPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber - 1,
                };
                return Url.Link("GetCustomers", parameters);
            }

            if (type == ResourceType.NextPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber + 1,
                };
                return Url.Link("GetCustomers", parameters);
            }

            return Url.Link("GetCustomers", resourceParameters);
        }
    }
}
