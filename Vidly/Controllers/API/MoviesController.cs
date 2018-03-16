using AutoMapper;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using Vidly.Models.DataTables;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public DataTableResponse GetMovies(DataTableRequest request)
        {
            var movies = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);


            // Begin Filtering
            IEnumerable<Movie> filteredMovies;

            if (request.Search.Value != "")
            {
                var searchText = request.Search.Value.Trim();
                filteredMovies = movies
                        .Where(m => m.Name.Contains(searchText) || m.Genre.Name.Contains(searchText));
            }
            else
            {
                filteredMovies = movies;
            }
            // End Filtering


            // Begin Sorting
            if (request.Order.Any())
            {
                int sortColumnIndex = request.Order[0].Column;
                string sortDirection = request.Order[0].Dir;

                Func<Movie, string> orderingFunctionString = null;
                Func<Movie, int> orderingFunctionInt = null;
                //Func<Movie, decimal?> orderingFunctionDecimal = null;

                switch (sortColumnIndex)
                {                   
                    case 0:     // MovieName
                        {
                            orderingFunctionString = (c => c.Name);
                            filteredMovies =
                                sortDirection == "asc"
                                    ? filteredMovies.OrderBy(orderingFunctionString)
                                    : filteredMovies.OrderByDescending(orderingFunctionString);
                            break;
                        }
                    case 1:     // GenreName
                        {
                            orderingFunctionString = (c => c.Genre.Name);
                            filteredMovies =
                                sortDirection == "asc"
                                    ? filteredMovies.OrderBy(orderingFunctionString)
                                    : filteredMovies.OrderByDescending(orderingFunctionString);
                            break;
                        }
                    case 2:     // UnitsInStock
                        {
                            orderingFunctionInt = (c => c.NumberInStock);
                            filteredMovies =
                                sortDirection == "asc"
                                    ? filteredMovies.OrderBy(orderingFunctionInt)
                                    : filteredMovies.OrderByDescending(orderingFunctionInt);
                            break;
                        }
                }
            }


            //End Sorting

            

            // Paging Data
            var pagedMovies = filteredMovies.Skip(request.Start).Take(request.Length);

                        
            var movieDtos = pagedMovies
                               .ToList()
                               .Select(Mapper.Map<Movie, MovieDto>);

            return new DataTableResponse
            {
                draw = request.Draw,
                recordsTotal = movies.Count(),
                recordsFiltered = movies.Count(),
                data = movieDtos.ToArray(),
                error = ""
            };
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
