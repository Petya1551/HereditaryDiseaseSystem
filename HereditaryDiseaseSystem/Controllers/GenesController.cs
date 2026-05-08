using HereditaryDiseaseSystem.Data;
using HereditaryDiseaseSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HereditaryDiseaseSystem.Controllers
{
    [Authorize]
    public class GenesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Genes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genes.ToListAsync());
        }

        // GET: Genes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gene = await _context.Genes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gene == null)
            {
                return NotFound();
            }

            return View(gene);
        }

        // GET: Genes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Symbol,ChromosomeLocation")] Gene gene)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gene);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gene);
        }

        // GET: Genes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gene = await _context.Genes.FindAsync(id);
            if (gene == null)
            {
                return NotFound();
            }
            return View(gene);
        }

        // POST: Genes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Symbol,ChromosomeLocation")] Gene gene)
        {
            if (id != gene.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gene);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneExists(gene.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gene);
        }

        // GET: Genes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gene = await _context.Genes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gene == null)
            {
                return NotFound();
            }

            return View(gene);
        }

        // POST: Genes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gene = await _context.Genes.FindAsync(id);
            if (gene != null)
            {
                _context.Genes.Remove(gene);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneExists(int id)
        {
            return _context.Genes.Any(e => e.Id == id);
        }
    }
}
