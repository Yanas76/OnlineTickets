using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineTickets.Entities;
using OnlineTickets.Persistance;
using OnlineTickets.Models.TicketModels;
using Mapster;
using System.Text.Json.Serialization;
using System.Text.Json;
using OnlineTickets.Models.OrderModels;

namespace OnlineTickets.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TicketsController : Controller
{

    private readonly OnlineTicketsDbContext _context;
    public TicketsController(OnlineTicketsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = _context.Tickets.AsNoTracking().ProjectToType<TicketViewModel>();
        return Ok(tickets);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByTicketId(Guid id)
    {

        var ticketById = await _context.Tickets.AsNoTracking()
            .Where(c => c.Id == id)
            .ProjectToType<TicketViewModel>()
            .FirstOrDefaultAsync();

        if (ticketById == null)
        {
            return NotFound();
        }

        return Ok(ticketById);
    }


    [HttpPost]
    public async Task<IActionResult> Create(TicketCreateModel ticketModel, Guid orderId, Guid filmId)
    {
        var orderById = await _context.Orders.AsNoTracking()
           .Where(c => c.Id == orderId)
           .FirstOrDefaultAsync();

        var filmById = await _context.Films.AsNoTracking()
           .Where(c => c.Id == filmId)
           .FirstOrDefaultAsync();

        if (orderById == null || filmById == null)
        { 
            return NotFound();
        }

        Ticket ticket = ticketModel.Adapt<Ticket>();
        ticket.Id = Guid.NewGuid();
        ticket.OrderId = orderById.Id;
        ticket.FilmId = filmById.Id;

        orderById.TotalPrice += ticket.Price;
       
        _context.Orders.Update(orderById);
        _context.Tickets.Add(ticket);     
        await _context.SaveChangesAsync();
        return Ok();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(TicketUpdateModel ticketModel)
    {

        var ticketById = await _context.Tickets.AsNoTracking()
           .Where(c => c.Id == ticketModel.Id)
           .FirstOrDefaultAsync();

        if (ticketById == null)
        {
            return NotFound();
        }

        var orderById = await _context.Orders.AsNoTracking()
          .Where(c => c.Id == ticketById.OrderId)
          .FirstOrDefaultAsync();

        orderById.TotalPrice = orderById.TotalPrice - ticketById.Price + ticketModel.Price;

        Ticket ticket = ticketModel.Adapt<Ticket>();
        ticket.FilmId = ticketById.FilmId;
        ticket.OrderId = ticketById.OrderId;

        _context.Orders.Update(orderById);
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
        return Ok();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ticketById = await _context.Tickets.AsNoTracking()
                           .Where(c => c.Id == id)
                           .FirstOrDefaultAsync();

        if (ticketById == null)
        {
            return NotFound();
        }

        var orderById = await _context.Orders.AsNoTracking()
         .Where(c => c.Id == ticketById.OrderId)
         .FirstOrDefaultAsync();

        orderById.TotalPrice = orderById.TotalPrice - ticketById.Price;

        _context.Tickets.Remove(ticketById);
        _context.Orders.Update(orderById);
        await _context.SaveChangesAsync();
        return Ok();

    }
}