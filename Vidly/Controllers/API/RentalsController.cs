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
            string customerName = null,
            string movieName = null)
        {
            var rentalsQuery = _context.Rentals
                    .Include(r => r.Customer)
                    .Include(r => r.Movie);

            if (!String.IsNullOrWhiteSpace(customerName))
                rentalsQuery = rentalsQuery.Where(c => c.Customer.Name.Contains(customerName));

            if (!String.IsNullOrWhiteSpace(movieName))
                rentalsQuery = rentalsQuery.Where(c => c.Movie.Name.Contains(movieName));

            _context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

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

        [HttpPut]
        public IHttpActionResult MarkAsReturned(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var rentalInDb = _context.Rentals.Include(r => r.Customer).Include(r => r.Movie).SingleOrDefault(c => c.Id == id);

            if (rentalInDb == null)
                return NotFound();

            //retornar al stock
            var movie = _context.Movies.SingleOrDefault(m => m.Id == rentalInDb.Movie.Id);
            movie.NumberAvailable++;

            rentalInDb.DateReturned = DateTime.Now;

            _context.SaveChanges();

            return Ok();
        }
    }
}

