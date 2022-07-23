using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public MoviesController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Movies endpoint which return list of MovieDto
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet(Name = "GetMovies")]
        public IActionResult GetMovies()
        {
            try
            {
                var claims = User.Claims;

                var movies = _repository.Movie.GetAllMovies(trackChanges: false);

                var moviesDto = _mapper.Map<IEnumerable<MovieDto>>(movies);

                return Ok(moviesDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get single move for given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}", Name = "GetMovieById")]
        public IActionResult GetMovieById(Guid id)
        {
            try
            {
                var claims = User.Claims;

                var movie = _repository.Movie.GetMovieById(id,trackChanges: false);

                var movieDto = _mapper.Map<MovieDto>(movie);

                return Ok(movieDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// create new movie
        /// </summary>
        /// <param name="movieViewModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult PostMovie(MovieViewModel movieViewModel)
        {
            try
            {
                var claims = User.Claims;
                var movie = _mapper.Map<Movie>(movieViewModel);
                _repository.Movie.CreateMovie(movie);
                _repository.Save();
                return CreatedAtRoute("GetMovieById", routeValues: new { id = movie.Id }, value: movie);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// update movie details for given movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieViewModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public IActionResult PutMovie(Guid id, MovieViewModel movieViewModel)
        {
            try
            {
                var existingMovie = _repository.Movie.GetMovieById(id, false);

                if (existingMovie == null)
                    return BadRequest("invalid id");

                var claims = User.Claims;

                var movie = _mapper.Map(movieViewModel,existingMovie);

                _repository.Movie.UpdateMovie(movie);
                _repository.Save();
                return Ok(movie); ;
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Delete movie for given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(Guid id)
        {
            try
            {
                var existingMovie = _repository.Movie.GetMovieById(id, false);

                if (existingMovie == null)
                    return BadRequest("invalid id");

                _repository.Movie.DeleteMovie(existingMovie);
                _repository.Save();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Privacy Policy page
        /// </summary>
        /// <returns></returns>
        [HttpGet("Privacy")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Privacy()
        {
            var claims = User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToList();

            return Ok(claims);
        }
    }
}
