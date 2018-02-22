using AutoMapper;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        public IHttpActionResult CreateRental(RentalDto rentalDto)
        {

            if (rentalDto.MovieIds.Count == 0)
                return BadRequest("No movies Ids has been given");

            var customer = _context.Customers.SingleOrDefault(
                        c => c.Id == rentalDto.CustomerId);

            if (customer == null)
                return BadRequest("CustomerId is not valid");
            
            var movies = _context.Movies.Where(
                        m => rentalDto.MovieIds.Contains(m.Id));

            if (movies.Count() != rentalDto.MovieIds.Count)
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

