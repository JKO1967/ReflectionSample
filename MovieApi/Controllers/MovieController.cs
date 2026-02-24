using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieApi.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Administrator")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieRepository _repository; 

    public MovieController(ILogger<MovieController> logger, IMovieRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    // GET: api/<MovieController>
    [HttpGet]
  //  [AllowAnonymous]
    public IActionResult GetAllMovies()
    {
        _logger.LogInformation("Movie Get wurde aufgerufen");
        return Ok(_repository.GetAll());
    }

    // GET api/<MovieController>/5
    [HttpGet("{id}", Name ="SingleMovie")]
    public IActionResult Get(int id)
    {
        if (!_repository.MovieExists(id))
        {
            return NotFound();
        }
        return Ok(_repository.GetMovieById(id));    
    }

    // GET api/<MovieController>/gettitle?DasLebendesBrian
    [HttpGet("gettitle")]
    public IActionResult GetByTitle(string title)
    {
        if (!_repository.MovieExists(title))
        {
            return NotFound();
        }
        return Ok(_repository.GetMovieByName(title));
    }

    // POST api/<MovieController>
    [HttpPost]   
    public IActionResult Post([FromBody] Movie value)
    {
        var movie = _repository.AddMovie(value);
        return CreatedAtRoute("SingleMovie", new { id = movie.Id }, movie);
    }

    // PUT api/<MovieController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Movie value)
    {
        if (!_repository.MovieExists(id))
        {
            return NotFound();
        }
        _repository.UpdateMovie(value);
        return NoContent();
    }

    // DELETE api/<MovieController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_repository.MovieExists(id))
        {
            return NotFound();
        }

        _repository.DeleteMovie(id);

        return NoContent();
    }
}
