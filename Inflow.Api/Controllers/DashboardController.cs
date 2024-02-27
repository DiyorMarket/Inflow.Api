using Inflow.Domain.DTOs.Dashboard;
using Inflow.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inflow.Controllers;

[Route("api/dashboard")]
[ApiController]
//[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public ActionResult<DashboardDto> GetDashboard() => Ok(_dashboardService.GetDashboard());
}
