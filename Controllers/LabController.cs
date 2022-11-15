using Microsoft.AspNetCore.Mvc;
using MvcLabManager.Models;

namespace MvcLabManager.Controllers;

public class LabController : Controller
{
    private readonly LabManagerContext _context;

    public LabController(LabManagerContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View(_context.Labs);

    public IActionResult Show(int id)
    {
        Lab lab = _context.Labs.Find(id);

        if(lab == null)
        {
            return RedirectToAction("Index"); 
        }

        return View(lab);
    }

    public IActionResult Create([FromForm] int id, [FromForm] string number, [FromForm] string name, [FromForm] string sector)
    {
        if(_context.Computers.Find(id) == null)
        {
            Lab lab = new Lab(id,number,name,sector);
            _context.Labs.Add(lab);
            _context.SaveChanges();
            return View("Cadastro");
        }
        else
        {
           return Content("Um laboratório com esse ID já foi cadastrado, por favor insira outro ID.");
        }
    }

    public IActionResult Cadastro()
    {
        return View();
    }

     public IActionResult Delete(int id){

        Lab labo = _context.Computers.Find(id);
        _context.Computers.Remove(labo);
        _context.SaveChanges();

        return View();
    }

        public IActionResult Update([FromForm] int id, [FromForm] string number, [FromForm] string name, [FromForm] string sector){
        Lab lab = _context.Labs.Find(id);

        if(lab == null)
        {
            return Content("O laboratório não existe");
        }
        else
        {
            lab.Number = number;
            lab.Name = name;
            lab.Sector = sector;
            _context.Labs.Update(lab);
            _context.SaveChanges();
            return Content("Laboratório atualizado com sucesso");
        }

    }
}