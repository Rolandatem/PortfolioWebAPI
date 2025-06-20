using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.DTOs;

namespace PortfolioWebAPI.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class TagController(
    IMapper _mapper,
    PortfolioDbContext _context) : PortfolioBaseController
{
    #region "GET"
    [HttpGet("sitefiltertagtypes")]
    public async Task<ActionResult<IEnumerable<SiteFilterTagTypeDTO>>> GetSiteFilterTagTypesAsync()
    {
        await base.DoTestsAsync();

        return Ok(await _context.SiteFilterTagTypes
            .ProjectTo<SiteFilterTagTypeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync());
    }
    #endregion
}