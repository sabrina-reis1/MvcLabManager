using Microsoft.AspNetCore.Mvc;
using MvcLabManager.Models;

namespace MvcLabManager.Controllers;

public class ComputerController : Controller
{
    private readonly LabManagerContext _context;

    public ComputerController(LabManagerContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View(_context.Computers);

    public IActionResult Show(int id)
    {
        Computer computer = _context.Computers.Find(id);

        if(computer == null)
        {
            return NotFound(); // RedirectToAction("Index");
        }

        return View(computer);
    }

    public IActionResult Create([FromForm] int id, [FromForm] string ram, [FromForm] string processor)
    {
        if(_context.Computers.Find(id) == null)
        {
            Computer computer = new Computer(id,ram,processor);
            _context.Computers.Add(computer);
            _context.SaveChanges();
            return View("Cadastro");
        }
        else
        {
           return Content("Um computador com esse ID já foi cadastrado, por favor insira outro ID.");
        }
    }

    public IActionResult Cadastro()
    {
        return View();
    }

     public IActionResult Delete(int id){

        Computer maquina = _context.Computers.Find(id);
        _context.Computers.Remove(maquina);
        _context.SaveChanges();

        return View();
    }

    public IActionResult Update(int id, [FromForm] string ram, [FromForm] string processor )
    {
        Computer computer = _context.Computers.Find(id);

        if(computer == null)
        {
            return Content("O computador não existe");
        }
        else
        {
            computer.Id = id;
            computer.Ram = ram;
            computer.Processor = processor;
            _context.Computers.Update(computer);
            _context.SaveChanges();
            return Content("Computador atualizado com sucesso!");
        }

    }
}