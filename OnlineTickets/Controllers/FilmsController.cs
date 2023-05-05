using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineTickets.Entities;
using OnlineTickets.Persistance;
using OnlineTickets.Models.FilmModels;
using Mapster;

namespace OnlineTickets.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilmsController : Controller
{

    private readonly OnlineTicketsDbContext _context;
    public FilmsController(OnlineTicketsDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var films = _context.Films.AsNoTracking().ProjectToType<FilmViewModel>();
        return Ok(films);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByFilmId(Guid id)
    {

        var filmById = await _context.Films.AsNoTracking()
            .Where(f => f.Id == id)
            .ProjectToType<FilmViewModel>()
            .FirstOrDefaultAsync();

        if (filmById == null)
        {
            return NotFound();
        }

        return Ok(filmById);
    }


    [HttpPost]
    public async Task<IActionResult> Create(FilmCreateModel filmModel, Guid cinemaId)
    {

        var cinemaById = await _context.Cinemas.AsNoTracking()
            .Where(c => c.Id == cinemaId)
            .FirstOrDefaultAsync();

        if (cinemaById == null)
        {
            return NotFound();
        }

        Film film = filmModel.Adapt<Film>();
        film.Id = Guid.NewGuid();
        film.CinemaId = cinemaById.Id;

        _context.Films.Add(film);
        await _context.SaveChangesAsync();
        return Ok();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(FilmUpdateModel filmModel)
    {

        var filmById = await _context.Films.AsNoTracking()
            .Where(f => f.Id == filmModel.Id)
            .FirstOrDefaultAsync();

        if (filmById == null)
        {
            return NotFound();
        }

        Film film = filmModel.Adapt<Film>();
        film.CinemaId = filmById.CinemaId;
        _context.Films.Update(film);
        await _context.SaveChangesAsync();
        return Ok();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var filmById = await _context.Films.AsNoTracking()
                           .Where(f => f.Id == id)
                           .FirstOrDefaultAsync();

        if (filmById == null)
        {
            return NotFound();
        }

        _context.Films.Remove(filmById);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
