using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebAPI.Data;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FAQsController(
    PortfolioDbContext _context) : PortfolioBaseController
{
    /// <summary>
    /// Gets all FAQ's that are site applicable.
    /// </summary>
    /// <returns>All site-ready FAQs</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FAQ>>> GetFAQsAsync()
    {
        await base.DoTestsAsync();

        return await _context.FAQs
            .Where(faq => faq.IsSiteReady)
            .OrderByDescending(faq => faq.UpVotes)
            .ToListAsync();
    }

    /// <summary>
    /// Gets the to 10 FAQ's based on UpVotes.
    /// </summary>
    /// <returns>List of top 10 FAQs</returns>
    [HttpGet("top-ten")]
    public async Task<ActionResult<IEnumerable<FAQ>>> GetTopTenFAQsAsync()
    {
        await base.DoTestsAsync();

        return await _context.FAQs
            .Where(faq => faq.IsSiteReady)
            .OrderByDescending(faq => faq.UpVotes)
            .Take(10)
            .ToListAsync();
    }

    /// <summary>
    /// Gets the FAQ specified by id.
    /// </summary>
    /// <param name="id">DB ID of the FAQ</param>
    /// <returns>FAQ matching the ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FAQ>> GetFAQAsync(int id)
    {
        await base.DoTestsAsync();

        FAQ? faq = await _context.FAQs
            .FindAsync(id);

        if (faq == null) { return NotFound(); }

        return faq;
    }

    /// <summary>
    /// Patches a FAQ by updating the upvotes or downvotes as specified.
    /// </summary>
    /// <param name="id">DB ID of the FAQ</param>
    /// <param name="voteType">"up" or "down"</param>
    [HttpPatch("{id:int}/vote/{voteType}")]
    public async Task<IActionResult> VoteAsync(int id, string voteType)
    {
        await base.DoTestsAsync();

        if (voteType != "up" && voteType != "down")
        {
            return BadRequest($"Expected either '/{id}/vote/up' or '/{id}/vote/down");
        }

        FAQ? faq = await _context.FAQs
            .FindAsync(id);

        if (faq == null) { return NotFound(); }

        if (voteType == "up") { faq.UpVotes += 1; }
        else { faq.DownVotes += 1; }

        await _context.SaveChangesAsync();

        return NoContent();
    }
}