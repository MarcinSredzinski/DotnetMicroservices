using Domain.Library.Clients;
using Domain.Library.Models;
using Microsoft.Extensions.Logging;

namespace Business.Library;

public class SuperComplexBusinessLogic
{
    private readonly GitHubClient _gitHubClient;
    private readonly ILogger<SuperComplexBusinessLogic> _logger;

    public SuperComplexBusinessLogic(GitHubClient gitHubClient, ILogger<SuperComplexBusinessLogic> logger)
    {
        _gitHubClient = gitHubClient;
        _logger = logger;
    }

    public async Task<IEnumerable<GitHubBranch>> GetBranchesSortedAlphabetically()
    {
        var branches = await _gitHubClient.GetAspNetCoreDocsBranchesAsync();
        if (branches is not null && branches.Any())
        {
            _logger.LogInformation("{0} - got github branches successfully", nameof(SuperComplexBusinessLogic));
            return branches.OrderBy(b => b.Name);
        }
        _logger.LogError("{0} - was not able to get any gitHub branches", nameof(SuperComplexBusinessLogic));
        return await Task.FromResult<IEnumerable<GitHubBranch>>(null);
    }
}