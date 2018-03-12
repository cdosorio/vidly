using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/rentals
        public IHttpActionResult GetRental( 
            string queryCustomerName = null,
            string queryMovieName = null)
        {
            var rentalsQuery = _context.Rentals
                    .Include(r => r.Customer)
                    .Include(r => r.Movie);

            if (!String.IsNullOrWhiteSpace(queryCustomerName))
                rentalsQuery = rentalsQuery.Where(c => c.Customer.Name.Contains(queryCustomerName));

            if (!String.IsNullOrWhiteSpace(queryMovieName))
                rentalsQuery = rentalsQuery.Where(c => c.Movie.Name.Contains(queryMovieName));

            var rentalDtos = rentalsQuery
                   .ToList()
                   .Select(Mapper.Map<Rental, RentalDto>);

            return Ok(rentalDtos);
        }

        [HttpPost]
        public IHttpActionResult CreateRental(ListRentalDto listRentalDto)
        {

            if (listRentalDto.MovieIds.Count == 0)
                return BadRequest("No movies Ids has been given");

            var customer = _context.Customers.SingleOrDefault(
                        c => c.Id == listRentalDto.CustomerId);

            if (customer == null)
                return BadRequest("CustomerId is not valid");
            
            var movies = _context.Movies.Where(
                        m => listRentalDto.MovieIds.Contains(m.Id));

            if (movies.Count() != listRentalDto.MovieIds.Count)
                return BadRequest("One or more MovieIds are invalid");


            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);                
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}

