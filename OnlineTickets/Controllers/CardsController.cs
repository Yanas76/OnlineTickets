using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineTickets.Entities;
using OnlineTickets.Persistance;
using OnlineTickets.Models.CardModels;
using Mapster;

namespace OnlineTickets.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CardsController : Controller
{
    private readonly OnlineTicketsDbContext _context;

    public CardsController(OnlineTicketsDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cards = _context.Cards.AsNoTracking().ProjectToType<CardViewModel>(); 
        return Ok(cards);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByCardId(Guid id)
    {

        var cardById = await _context.Cards.AsNoTracking()
            .Where(c => c.Id == id)
            .ProjectToType<CardViewModel>()
            .FirstOrDefaultAsync();         

        if(cardById == null)
        {
            return NotFound();
        }
            
        return Ok(cardById);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CardCreateModel cardModel)
    {
        Card card = cardModel.Adapt<Card>();
        card.Id = Guid.NewGuid();  
        _context.Cards.Add(card);
        await _context.SaveChangesAsync();
        return Ok(card);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(CardUpdateModel cardModel)
    {

        var cardById = await _context.Cards.AsNoTracking()
            .Where(c => c.Id == cardModel.Id)
            .FirstOrDefaultAsync();

        if( cardById == null)
        {
            return NotFound();
        }

        Card card = cardModel.Adapt<Card>();
        _context.Cards.Update(card);
        await _context.SaveChangesAsync();  
        return Ok(card);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cardById = await _context.Cards.AsNoTracking()
                           .Where(c => c.Id == id)
                           .FirstOrDefaultAsync();

        if (cardById == null)
        {
            return NotFound();
        }

        _context.Cards.Remove(cardById);
        await _context.SaveChangesAsync();
        return Ok();
    }

}
