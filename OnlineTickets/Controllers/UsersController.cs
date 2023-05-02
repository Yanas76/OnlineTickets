using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineTickets.Entities;
using OnlineTickets.Persistance;
using OnlineTickets.Models.UserModels;
using Mapster;

namespace OnlineTickets.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private readonly OnlineTicketsDbContext _context;

    public UsersController(OnlineTicketsDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = _context.Users.AsNoTracking().ProjectToType<UserViewModel>();
        return Ok(users);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByUserId(Guid id)
    {

        var userById = await _context.Users.AsNoTracking()
            .Where(u => u.Id == id)
            .ProjectToType<UserViewModel>()
            .FirstOrDefaultAsync();

        if (userById == null)
        {
            return NotFound();
        }

        return Ok(userById);
    }


    [HttpPost]
    public async Task<IActionResult> Create(UserCreateModel userModel)
    {

        User user = userModel.Adapt<User>();    
        user.Id = Guid.NewGuid();
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UserUpdateModel userModel)
    {

        var userById = await _context.Users.AsNoTracking()
            .Where(u => u.Id == userModel.Id)
            .FirstOrDefaultAsync();

        if (userById == null)
        {
            return NotFound();
        }

        User user = userModel.Adapt<User>();
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userById = await _context.Users.AsNoTracking()
                           .Where(u => u.Id == id)
                           .FirstOrDefaultAsync();

        if (userById == null)
        {
            return NotFound();
        }

        _context.Users.Remove(userById);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
