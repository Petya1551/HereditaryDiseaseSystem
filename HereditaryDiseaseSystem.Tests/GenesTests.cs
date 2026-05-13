using HereditaryDiseaseSystem.Controllers;
using HereditaryDiseaseSystem.Data;
using HereditaryDiseaseSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class GenesTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    private GenesController GetController(ApplicationDbContext context)
    {
        return new GenesController(context);
    }

    [Fact]
    public async Task Create_Gene_ShouldAddGene()
    {
        var context = GetDbContext();
        var controller = GetController(context);

        var gene = new Gene
        {
            Symbol = "BRCA1",
            ChromosomeLocation = "17q21"
        };

        var result = await controller.Create(gene, new int[0], new int[0]);

        Assert.Single(context.Genes);
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public async Task Delete_Gene_ShouldRemoveGene()
    {
        var context = GetDbContext();

        var gene = new Gene
        {
            Symbol = "TP53",
            ChromosomeLocation = "17p13.1"
        };

        context.Genes.Add(gene);
        context.SaveChanges();

        var controller = GetController(context);

        await controller.DeleteConfirmed(gene.Id);

        Assert.Empty(context.Genes);
    }

    [Fact]
    public async Task Details_NullId_ShouldReturnNotFound()
    {
        var controller = GetController(GetDbContext());

        var result = await controller.Details(null);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Edit_Gene_ShouldUpdateValues()
    {
        var context = GetDbContext();

        var gene = new Gene
        {
            Symbol = "OLD",
            ChromosomeLocation = "1q"
        };

        context.Genes.Add(gene);
        context.SaveChanges();

        var controller = GetController(context);

        var updated = new Gene
        {
            Id = gene.Id,
            Symbol = "NEW",
            ChromosomeLocation = "2q"
        };

        await controller.Edit(gene.Id, updated, new int[0], new int[0]);

        var dbGene = context.Genes.First();

        Assert.Equal("NEW", dbGene.Symbol);
        Assert.Equal("2q", dbGene.ChromosomeLocation);
    }

    [Fact]
    public async Task Create_DuplicateSymbol_ShouldNotAddSecondGene()
    {
        var context = GetDbContext();

        context.Genes.Add(new Gene
        {
            Symbol = "BRCA1",
            ChromosomeLocation = "17q"
        });
        context.SaveChanges();

        var controller = GetController(context);

        var gene2 = new Gene
        {
            Symbol = "BRCA1",
            ChromosomeLocation = "18q"
        };

        await controller.Create(gene2, new int[0], new int[0]);

        Assert.Equal(1, context.Genes.Count());
    }
}