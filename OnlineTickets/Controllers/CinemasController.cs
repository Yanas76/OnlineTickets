using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTickets.Entities;
using OnlineTickets.Persistance;
using OnlineTickets.Models.CinemaModels;
using Mapster;
using OnlineTickets.Models.FilmModels;

namespace OnlineTickets.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CinemasController : Controller
{
    private readonly OnlineTicketsDbContext _context;

    public CinemasController(OnlineTicketsDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cinemas = _context.Cinemas.AsNoTracking().ProjectToType<CinemaViewModel>();
        return Ok(cinemas);
    }


    [HttpGet("{id}", Name = "GetByCinemaId")]
    public async Task<IActionResult> GetByCinemaId(Guid id)
    {

        var cinemaById = await _context.Cinemas.AsNoTracking()
            .Where(c => c.Id == id)
            .ProjectToType<CinemaViewModel>()
            .FirstOrDefaultAsync();

        if (cinemaById == null)
        {
            return NotFound();
        }

        return Ok(cinemaById);
    }

    [HttpGet("films/{id}", Name = "GetFilmsByCinemaId")]
    public async Task<IActionResult> GetFilmsByCinemaId(Guid id)
    {

        var cinemaById = await _context.Cinemas.AsNoTracking()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (cinemaById == null)
        {
            return NotFound();
        }

        var filmsById = _context.Films.AsNoTracking()
            .Where(c => c.CinemaId == id)
            .ProjectToType<FilmViewModel>();

        return Ok(filmsById);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CinemaCreateModel cinemaModel)
    {
        Cinema cinema = cinemaModel.Adapt<Cinema>();
        cinema.Id = Guid.NewGuid();
        _context.Cinemas.Add(cinema);
        await _context.SaveChangesAsync();
        return Ok(cinema);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(CinemaUpdateModel cinemaModel)
    {

        var cinemaById = await _context.Cinemas.AsNoTracking()
            .Where(c => c.Id == cinemaModel.Id)
            .FirstOrDefaultAsync();

        if (cinemaById == null)
        {
            return NotFound();
        }

        Cinema cinema = cinemaModel.Adapt<Cinema>();  
        _context.Cinemas.Update(cinema);
        await _context.SaveChangesAsync();
        return Ok(cinema);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cinemaById = await _context.Cinemas.AsNoTracking()
                           .Where(c => c.Id == id)
                           .FirstOrDefaultAsync();

        if (cinemaById == null)
        {
            return NotFound();
        }

        _context.Cinemas.Remove(cinemaById);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
