using ContentWriter.Application.DTOs;
using ContentWriter.Application.Providers;
using ContentWriter.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContentWriter.Api.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/generate")]
public class GenerateController : ControllerBase
{
    private readonly IContentGenerationOrchestrator _orchestrator;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<GenerateController> _logger;

    public GenerateController(
        IContentGenerationOrchestrator orchestrator,
        IServiceScopeFactory scopeFactory,
        ILogger<GenerateController> logger)
    {
        _orchestrator = orchestrator;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    [HttpPost("pillar/plan")]
    public Task<IActionResult> GeneratePillarPlan(Guid projectId, CancellationToken cancellationToken) =>
        RunStep(projectId, _orchestrator.GeneratePillarPlanAsync(projectId, cancellationToken), "pillar-plan");

    [HttpPost("pillar/body")]
    public Task<IActionResult> GeneratePillarBody(Guid projectId, CancellationToken cancellationToken) =>
        RunStep(projectId, _orchestrator.GeneratePillarBodyAsync(projectId, cancellationToken), "pillar-body");

    [HttpPost("pillar")]
    public Task<IActionResult> GeneratePillar(Guid projectId, CancellationToken cancellationToken) =>
        RunStep(projectId, _orchestrator.GeneratePillarAsync(projectId, cancellationToken), "pillar");

    /// <summary>
    /// Tool page generation (up to 5 pages × 1,500-2,500 words) far exceeds the edge proxy's
    /// request timeout, so it runs detached from the request lifetime. Poll the project detail
    /// endpoint for the resulting ToolPost rows.
    /// </summary>
    [HttpPost("tools")]
    public IActionResult GenerateToolPages(Guid projectId)
    {
        _ = Task.Run(async () =>
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var orchestrator = scope.ServiceProvider.GetRequiredService<IContentGenerationOrchestrator>();
            try
            {
                await orchestrator.GenerateToolPagesAsync(projectId, CancellationToken.None);
                _logger.LogInformation("Background tool page generation completed for project {ProjectId}", projectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Background tool page generation failed for project {ProjectId}", projectId);
            }
        });

        return Accepted(new { projectId, step = "tools", status = "started" });
    }

    [HttpPost("blog")]
    public Task<IActionResult> GenerateBlog(Guid projectId, CancellationToken cancellationToken) =>
        RunStep(projectId, _orchestrator.GenerateBlogAsync(projectId, cancellationToken), "blog");

    [HttpPost("social")]
    public Task<IActionResult> GenerateSocial(Guid projectId, CancellationToken cancellationToken) =>
        RunStep(projectId, _orchestrator.GenerateSocialAsync(projectId, cancellationToken), "social");

    [HttpPost("email-cold-outreach")]
    public Task<IActionResult> GenerateColdOutreach(Guid projectId, CancellationToken cancellationToken) =>
        RunStep(projectId, _orchestrator.GenerateColdOutreachAsync(projectId, cancellationToken), "email-cold-outreach");

    [HttpPost("image-prompts")]
    public Task<IActionResult> GenerateImagePrompts(Guid projectId, CancellationToken cancellationToken) =>
        RunStep(projectId, _orchestrator.GenerateImagePromptsAsync(projectId, cancellationToken), "image-prompts");

    [HttpPost]
    public Task<IActionResult> GenerateAll(Guid projectId, CancellationToken cancellationToken) =>
        RunStep(projectId, _orchestrator.GenerateAllAsync(projectId, cancellationToken), "all");

    private async Task<IActionResult> RunStep(Guid projectId, Task<GeneratedContentSet> action, string step)
    {
        try
        {
            var result = await action;
            return Ok(result);
        }
        catch (ContentGenerationException ex)
        {
            _logger.LogWarning(ex, "Content generation step {Step} failed for project {ProjectId}", step, projectId);
            var title = ex.Message.Contains("timed out", StringComparison.OrdinalIgnoreCase)
                ? "Generation timed out"
                : "Content generation failed";
            return Problem(ex.Message, statusCode: 502, title: title);
        }
    }
}
