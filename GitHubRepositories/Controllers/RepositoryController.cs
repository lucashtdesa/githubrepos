using GitHubRepositories.Model;
using GitHubRepositories.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubRepositories.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly IRepositoryService _repositoryService;

        public RepositoryController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpGet("/repos/{name}")]
        public async Task<ActionResult<List<Repository>>> GetRepositories(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var repos = await _repositoryService.FindByNameAsync(name);
                if (repos == null)
                {
                    return NotFound();
                }
                return Ok(repos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
