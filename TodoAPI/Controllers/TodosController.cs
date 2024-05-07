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
                      // IEnumerable to get collection object JSON, ARRAY, String
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
    
      //Delete delete
      [HttpDelete("{id:int}")]

      public async Task<IActionResult> Delete(int id)
      {
          var TodoList = await _context.TodoLists.FindAsync(id);
            if(TodoList == null)
            {
              return NotFound();
            }

            _context.Remove(TodoList);

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
              return Ok("To do item deleted Successfully");
            }

            return BadRequest("ERROR: Unable to delete to do item");
      }

      //Get a single to do item

      [HttpGet("{id:int}")]
                        //Return object Action Result Model
      public async Task<ActionResult<TodoList>> GetTodoList(int id)
      {
          var todoList = await _context.TodoLists.FindAsync(id);
          if(todoList == null)
          {
            return NotFound("ERROR: To do item not found");
          }
          return Ok(todoList);
      }

      //Update PUT

      [HttpPut("{id:int}")]
                      //don't need to pass in object
      public async Task<IActionResult> EditTodoList(int id, TodoList todoList)
      {
        var TodoFromDb = await _context.TodoLists.FindAsync(id);

        if(TodoFromDb == null)
        {
          return BadRequest("ERROR: To do item not found");
        }

        TodoFromDb.Name = todoList.Name;
        TodoFromDb.Description = todoList.Description;

        var result = await _context.SaveChangesAsync();

        if(result > 0)
        {
          return Ok("To do item edited Successfully");
        }
        return BadRequest("ERROR: Unable to update item");
      }


    }

      
