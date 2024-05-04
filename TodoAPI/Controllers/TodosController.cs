using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQLitePCL;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
      
      private readonly  AppDbContext _context;
      public TodosController(AppDbContext context)
      {
       _context = context;
      }

      [HttpGet]

      public async Task<IEnumerable<TodoList>> GetTodoLists()
      {
        var TodoLists = await _context.TodoLists.AsNoTracking().ToListAsync();
        return TodoLists;
      }

      [HttpPost]

      public async Task<IActionResult> Create(TodoList todoList)
      {
          if(!ModelState.IsValid)
          {
            return BadRequest(ModelState);
          }
          await _context.AddAsync(todoList);

              var result = await _context.SaveChangesAsync();

              if(result > 0)
              {
                  return Ok("Added to List Successfully");
              }

              return BadRequest("ERROR: Not Added");
      }
    }
      //Delete delete
    //   [HttpDelete("{id:int}")]

    //   public async Task<IActionResult> Delete(int id)
    //   {
    //     var student = await _context.TodoLists.FindAsync(id);
    //     if(TodoList == null)
    //     {
    //       return NotFound("List item Not Found");
    //     }

    //     _context.Remove(TodoList);

    //     var result = await _context.SaveChangesAsync();

    //     if (result > 0)
    //     {
    //       return Ok("List item deleted Successfully");
    //     }
    //     return BadRequest("Unable to delete Item");
    //   }
    // }

      
