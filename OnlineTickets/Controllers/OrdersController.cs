using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineTickets.Entities;
using OnlineTickets.Persistance;
using OnlineTickets.Models.OrderModels;
using Mapster;

namespace OnlineTickets.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : Controller
{

    private readonly OnlineTicketsDbContext _context;
    public OrdersController(OnlineTicketsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = _context.Orders.AsNoTracking().ProjectToType<OrderViewModel>();
        return Ok(orders);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByOrderId(Guid id)
    {

        var orderById = await _context.Orders.AsNoTracking()
            .Where(o => o.Id == id)
            .ProjectToType<OrderViewModel>() 
            .FirstOrDefaultAsync();

        if (orderById == null)
        {
            return NotFound();
        }

        return Ok(orderById);
    }


    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateModel orderModel, Guid userId)
    {
        var userById = await _context.Users.AsNoTracking()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (userById == null)
        {
            return NotFound();
        }

        Order order = orderModel.Adapt<Order>();
        order.Id = Guid.NewGuid();
        order.UserId = userById.Id;
        order.TotalPrice = 0;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return Ok();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(OrderUpdateModel orderModel)
    {

        var orderById = await _context.Orders.AsNoTracking()
            .Where(o => o.Id == orderModel.Id)
            .FirstOrDefaultAsync();

        if (orderById == null)
        {
            return NotFound();
        }

        Order order = orderModel.Adapt<Order>();
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return Ok();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var orderById = await _context.Orders.AsNoTracking()
                           .Where(o => o.Id == id)
                           .FirstOrDefaultAsync();

        if (orderById == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(orderById);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
