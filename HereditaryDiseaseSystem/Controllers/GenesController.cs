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
            var genes = await _context.Genes
                .Include(g => g.GeneDiseases)
                .Include(g => g.GenePhenotypes)
                .ToListAsync();

            return View(genes);
        }

        // GET: Genes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gene = await _context.Genes
                .Include(g => g.GeneDiseases)
                    .ThenInclude(gd => gd.Disease)
                .Include(g => g.GenePhenotypes)
                    .ThenInclude(gp => gp.Phenotype)
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
            ViewBag.Diseases = _context.Diseases.ToList();
            ViewBag.Phenotypes = _context.Phenotypes.ToList();

            return View();
        }

        // POST: Genes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            Gene gene,
            int[] selectedDiseases,
            int[] selectedPhenotypes)
        {
            if (ModelState.IsValid)
            {
                // Save GeneDisease relations
                foreach (var diseaseId in selectedDiseases)
                {
                    gene.GeneDiseases.Add(new GeneDisease
                    {
                        DiseaseId = diseaseId
                    });
                }

                // Save GenePhenotype relations
                foreach (var phenotypeId in selectedPhenotypes)
                {
                    gene.GenePhenotypes.Add(new GenePhenotype
                    {
                        PhenotypeId = phenotypeId
                    });
                }

                _context.Genes.Add(gene);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Diseases = _context.Diseases.ToList();
            ViewBag.Phenotypes = _context.Phenotypes.ToList();

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

            var gene = await _context.Genes
                .Include(g => g.GeneDiseases)
                .Include(g => g.GenePhenotypes)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gene == null)
            {
                return NotFound();
            }

            ViewBag.Diseases = _context.Diseases.ToList();
            ViewBag.Phenotypes = _context.Phenotypes.ToList();

            return View(gene);
        }

        // POST: Genes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(
            int id,
            Gene gene,
            int[] selectedDiseases,
            int[] selectedPhenotypes)
        {
            if (id != gene.Id)
            {
                return NotFound();
            }

            var existingGene = await _context.Genes
                .Include(g => g.GeneDiseases)
                .Include(g => g.GenePhenotypes)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (existingGene == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update basic fields
                    existingGene.Symbol = gene.Symbol;
                    existingGene.ChromosomeLocation = gene.ChromosomeLocation;

                    // Remove old relationships
                    existingGene.GeneDiseases.Clear();
                    existingGene.GenePhenotypes.Clear();

                    // Add new diseases
                    foreach (var diseaseId in selectedDiseases)
                    {
                        existingGene.GeneDiseases.Add(new GeneDisease
                        {
                            GeneId = existingGene.Id,
                            DiseaseId = diseaseId
                        });
                    }

                    // Add new phenotypes
                    foreach (var phenotypeId in selectedPhenotypes)
                    {
                        existingGene.GenePhenotypes.Add(new GenePhenotype
                        {
                            GeneId = existingGene.Id,
                            PhenotypeId = phenotypeId
                        });
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneExists(gene.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
            }

            ViewBag.Diseases = _context.Diseases.ToList();
            ViewBag.Phenotypes = _context.Phenotypes.ToList();

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
                .Include(g => g.GeneDiseases)
                    .ThenInclude(gd => gd.Disease)
                .Include(g => g.GenePhenotypes)
                    .ThenInclude(gp => gp.Phenotype)
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
            var gene = await _context.Genes
                .Include(g => g.GeneDiseases)
                .Include(g => g.GenePhenotypes)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gene != null)
            {
                // Remove relationships first
                _context.GeneDiseases.RemoveRange(gene.GeneDiseases);

                _context.GenePhenotypes.RemoveRange(gene.GenePhenotypes);

                // Remove gene
                _context.Genes.Remove(gene);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GeneExists(int id)
        {
            return _context.Genes.Any(e => e.Id == id);
        }
    }
}
