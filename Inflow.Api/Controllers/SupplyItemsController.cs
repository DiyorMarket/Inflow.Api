using Inflow.Api.Helper;
using Inflow.Domain.DTOs.SupplyItem;
using Inflow.Domain.Interfaces.Services;
using Inflow.Domain.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

namespace Inflow.Controllers
{
    [Route("api/supplyItems")]
    [ApiController]
    //[Authorize]
    public class SupplyItemsController : Controller
    {
        private readonly ISupplyItemService _supplyItemService;
        public SupplyItemsController(ISupplyItemService supplyItemService)
        {
            _supplyItemService = supplyItemService;
        }
        [HttpGet(Name = "GetSupplyItems")]
        public IActionResult GetSupplyItemsAsync(
                [FromQuery] SupplyItemResourceParameters supplyItemResourceParameters)
        {
            var supplyItems = _supplyItemService.GetSupplyItems(supplyItemResourceParameters);
            var links = GetLinks(supplyItemResourceParameters, supplyItems.HasNextPage, supplyItems.HasPreviousPage);
            var metadata = new
            {
                supplyItems.PageNumber,
                supplyItems.PageSize,
                supplyItems.HasNextPage,
                supplyItems.HasPreviousPage,
                supplyItems.TotalPages,
                supplyItems.TotalCount
            };

            var result = new
            {
                data = supplyItems.Data,
                links,
                metadata
            };

            return Ok(result);
        }
        [HttpGet("{id}", Name = "GetSupplyItemById")]
        public ActionResult<SupplyItemDto> Get(int id)
        {
            var supplyItem = _supplyItemService.GetSupplyItemById(id);

            if (supplyItem is null)
            {
                return NotFound($"SupplyItem with id: {id} does not exist.");
            }

            return Ok(supplyItem);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplyItemForCreateDto supplyItem)
        {
            var createSupplyItem = _supplyItemService.CreateSupplyItem(supplyItem);

            return CreatedAtAction(nameof(Get), new { createSupplyItem.Id }, createSupplyItem);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplyItemForUpdateDto supplyItem)
        {
            if (id != supplyItem.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supplyItem.Id}.");
            }

            _supplyItemService.UpdateSupplyItem(supplyItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _supplyItemService.DeleteSupplyItem(id);

            return NoContent();
        }
        private List<ResourceLink> GetLinks(
       SupplyItemResourceParameters resourceParameters,
       bool hasNext,
       bool hasPrevious)
        {
            List<ResourceLink> links = new();

            links.Add(new ResourceLink(
                "self",
                CreateSupplItemsResourceLink(resourceParameters, ResourceType.CurrentPage),
                "GET"));

            if (hasNext)
            {
                links.Add(new ResourceLink(
                "next",
                CreateSupplItemsResourceLink(resourceParameters, ResourceType.NextPage),
                "GET"));
            }

            if (hasPrevious)
            {
                links.Add(new ResourceLink(
                "previous",
                CreateSupplItemsResourceLink(resourceParameters, ResourceType.PreviousPage),
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

        private string? CreateSupplItemsResourceLink(SupplyItemResourceParameters resourceParameters, ResourceType type)
        {
            if (type == ResourceType.PreviousPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber - 1,
                };
                return Url.Link("GetSupplyItems", parameters);
            }

            if (type == ResourceType.NextPage)
            {
                var parameters = resourceParameters with
                {
                    PageNumber = resourceParameters.PageNumber + 1,
                };
                return Url.Link("GetSupplyItems", parameters);
            }

            return Url.Link("GetSupplyItems", resourceParameters);
        }
    }
}
