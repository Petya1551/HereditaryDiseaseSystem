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
    public class PhenotypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhenotypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Phenotypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Phenotypes.ToListAsync());
        }

        // GET: Phenotypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phenotype = await _context.Phenotypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phenotype == null)
            {
                return NotFound();
            }

            return View(phenotype);
        }

        // GET: Phenotypes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Phenotypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Phenotype phenotype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phenotype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phenotype);
        }

        // GET: Phenotypes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phenotype = await _context.Phenotypes.FindAsync(id);
            if (phenotype == null)
            {
                return NotFound();
            }
            return View(phenotype);
        }

        // POST: Phenotypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Phenotype phenotype)
        {
            if (id != phenotype.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phenotype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhenotypeExists(phenotype.Id))
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
            return View(phenotype);
        }

        // GET: Phenotypes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phenotype = await _context.Phenotypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phenotype == null)
            {
                return NotFound();
            }

            return View(phenotype);
        }

        // POST: Phenotypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phenotype = await _context.Phenotypes.FindAsync(id);
            if (phenotype != null)
            {
                _context.Phenotypes.Remove(phenotype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhenotypeExists(int id)
        {
            return _context.Phenotypes.Any(e => e.Id == id);
        }
    }
}
