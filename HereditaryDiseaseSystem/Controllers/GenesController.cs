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

        public async Task<IActionResult> Index()
        {
            var genes = await _context.Genes
                .Include(g => g.GeneDiseases)
                .Include(g => g.GenePhenotypes)
                .ToListAsync();

            return View(genes);
        }

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

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Diseases = _context.Diseases.ToList();
            ViewBag.Phenotypes = _context.Phenotypes.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Gene gene, int[] selectedDiseases, int[] selectedPhenotypes)
        {
            bool geneExists = await _context.Genes
                .AnyAsync(g => g.Symbol == gene.Symbol);

            if (geneExists)
            {
                ModelState.AddModelError(
                    "Symbol",
                    "Gene with this symbol already exists.");
            }

            if (ModelState.IsValid)
            {
                foreach (var diseaseId in selectedDiseases)
                {
                    gene.GeneDiseases.Add(new GeneDisease
                    {
                        DiseaseId = diseaseId
                    });
                }

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

            bool duplicateGene = await _context.Genes
                .AnyAsync(g => g.Symbol == gene.Symbol && g.Id != gene.Id);

            if (duplicateGene)
            {
                ModelState.AddModelError(
                    "Symbol",
                    "Another gene with this symbol already exists.");
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
                    existingGene.Symbol = gene.Symbol;
                    existingGene.ChromosomeLocation = gene.ChromosomeLocation;

                    existingGene.GeneDiseases.Clear();
                    existingGene.GenePhenotypes.Clear();

                    foreach (var diseaseId in selectedDiseases)
                    {
                        existingGene.GeneDiseases.Add(new GeneDisease
                        {
                            GeneId = existingGene.Id,
                            DiseaseId = diseaseId
                        });
                    }

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
                _context.GeneDiseases.RemoveRange(gene.GeneDiseases);

                _context.GenePhenotypes.RemoveRange(gene.GenePhenotypes);

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
