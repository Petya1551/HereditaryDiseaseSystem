using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HereditaryDiseaseSystem.Models;

namespace HereditaryDiseaseSystem.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@admin.com";
            var adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        public static async Task SeedGeneticsDataAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.EnsureCreatedAsync();

            if (!context.Diseases.Any())
            {
                context.Diseases.AddRange(
                    new Disease { Name = "Cystic Fibrosis", Description = "Lung and digestive disorder." },
                    new Disease { Name = "Huntington Disease", Description = "Neurodegenerative disorder." },
                    new Disease { Name = "Sickle Cell Anemia", Description = "Hemoglobin disorder." },
                    new Disease { Name = "Duchenne Muscular Dystrophy", Description = "Muscle degeneration disease." },
                    new Disease { Name = "Tay-Sachs Disease", Description = "Fatal neurodegenerative disease." },
                    new Disease { Name = "Hemophilia A", Description = "Clotting disorder." },
                    new Disease { Name = "Hemophilia B", Description = "Clotting disorder." },
                    new Disease { Name = "Marfan Syndrome", Description = "Connective tissue disorder." },
                    new Disease { Name = "Phenylketonuria", Description = "Metabolic disorder." },
                    new Disease { Name = "Albinism", Description = "Pigmentation disorder." },
                    new Disease { Name = "Wilson Disease", Description = "Copper accumulation disorder." },
                    new Disease { Name = "Charcot-Marie-Tooth Disease", Description = "Peripheral neuropathy." },
                    new Disease { Name = "Rett Syndrome", Description = "Neurodevelopmental disorder." },
                    new Disease { Name = "Fragile X Syndrome", Description = "Genetic intellectual disability." },
                    new Disease { Name = "Polycystic Kidney Disease", Description = "Kidney cyst disorder." },
                    new Disease { Name = "Beta Thalassemia", Description = "Hemoglobin disorder." },
                    new Disease { Name = "Alpha Thalassemia", Description = "Hemoglobin disorder." },
                    new Disease { Name = "G6PD Deficiency", Description = "Enzyme deficiency." },
                    new Disease { Name = "Spinal Muscular Atrophy", Description = "Motor neuron disease." },
                    new Disease { Name = "Familial Hypercholesterolemia", Description = "High cholesterol." },
                    new Disease { Name = "Neurofibromatosis Type 1", Description = "Tumor disorder." },
                    new Disease { Name = "Neurofibromatosis Type 2", Description = "Nerve tumor disorder." },
                    new Disease { Name = "Osteogenesis Imperfecta", Description = "Brittle bones." },
                    new Disease { Name = "Ehlers-Danlos Syndrome", Description = "Connective tissue disorder." },
                    new Disease { Name = "Maple Syrup Urine Disease", Description = "Metabolic disorder." },
                    new Disease { Name = "Gaucher Disease", Description = "Lipid storage disorder." },
                    new Disease { Name = "Fabry Disease", Description = "X-linked disorder." },
                    new Disease { Name = "Pompe Disease", Description = "Glycogen storage disorder." },
                    new Disease { Name = "Retinoblastoma", Description = "Eye cancer." },
                    new Disease { Name = "Achondroplasia", Description = "Dwarfism disorder." }
                );

                context.SaveChanges();
            }

            if (!context.Phenotypes.Any())
            {
                context.Phenotypes.AddRange(
                    new Phenotype { Name = "Muscle Weakness", Description = "Reduced strength." },
                    new Phenotype { Name = "Blue Sclera", Description = "Blue eye tint." },
                    new Phenotype { Name = "Short Stature", Description = "Low height." },
                    new Phenotype { Name = "Intellectual Disability", Description = "Cognitive impairment." },
                    new Phenotype { Name = "Seizures", Description = "Brain activity disorder." },
                    new Phenotype { Name = "Joint Hypermobility", Description = "Loose joints." },
                    new Phenotype { Name = "Bone Fragility", Description = "Weak bones." },
                    new Phenotype { Name = "Delayed Growth", Description = "Slow development." },
                    new Phenotype { Name = "Hearing Loss", Description = "Reduced hearing." },
                    new Phenotype { Name = "Vision Impairment", Description = "Poor vision." },
                    new Phenotype { Name = "Skin Hypopigmentation", Description = "Low pigment." },
                    new Phenotype { Name = "Facial Dysmorphism", Description = "Facial abnormalities." },
                    new Phenotype { Name = "Enlarged Liver", Description = "Hepatomegaly." },
                    new Phenotype { Name = "Enlarged Spleen", Description = "Splenomegaly." },
                    new Phenotype { Name = "Chronic Fatigue", Description = "Tiredness." },
                    new Phenotype { Name = "Heart Defects", Description = "Heart problems." },
                    new Phenotype { Name = "Skeletal Deformities", Description = "Bone deformities." },
                    new Phenotype { Name = "Delayed Speech", Description = "Speech delay." },
                    new Phenotype { Name = "Tremors", Description = "Shaking." },
                    new Phenotype { Name = "Ataxia", Description = "Coordination loss." },
                    new Phenotype { Name = "Neuropathy", Description = "Nerve damage." },
                    new Phenotype { Name = "Anemia Symptoms", Description = "Low blood oxygen." },
                    new Phenotype { Name = "Kidney Dysfunction", Description = "Kidney issues." },
                    new Phenotype { Name = "Liver Dysfunction", Description = "Liver issues." },
                    new Phenotype { Name = "Skin Lesions", Description = "Skin abnormalities." },
                    new Phenotype { Name = "Photophobia", Description = "Light sensitivity." },
                    new Phenotype { Name = "Muscle Atrophy", Description = "Muscle loss." },
                    new Phenotype { Name = "Growth Retardation", Description = "Growth delay." },
                    new Phenotype { Name = "Cognitive Decline", Description = "Memory loss." },
                    new Phenotype { Name = "Abnormal Gait", Description = "Walking disorder." }
                );

                context.SaveChanges();
            }

            if (!context.Genes.Any())
            {
                context.Genes.AddRange(
                    new Gene { Symbol = "CFTR", ChromosomeLocation = "7q31.2" },
                    new Gene { Symbol = "HTT", ChromosomeLocation = "4p16.3" },
                    new Gene { Symbol = "HBB", ChromosomeLocation = "11p15.5" },
                    new Gene { Symbol = "DMD", ChromosomeLocation = "Xp21.2" },
                    new Gene { Symbol = "HEXA", ChromosomeLocation = "15q23" },
                    new Gene { Symbol = "F8", ChromosomeLocation = "Xq28" },
                    new Gene { Symbol = "F9", ChromosomeLocation = "Xq27.1" },
                    new Gene { Symbol = "FBN1", ChromosomeLocation = "15q21.1" },
                    new Gene { Symbol = "PAH", ChromosomeLocation = "12q23.2" },
                    new Gene { Symbol = "TYR", ChromosomeLocation = "11q14.3" },
                    new Gene { Symbol = "ATP7B", ChromosomeLocation = "13q14.3" },
                    new Gene { Symbol = "PMP22", ChromosomeLocation = "17p12" },
                    new Gene { Symbol = "MECP2", ChromosomeLocation = "Xq28" },
                    new Gene { Symbol = "FMR1", ChromosomeLocation = "Xq27.3" },
                    new Gene { Symbol = "PKD1", ChromosomeLocation = "16p13.3" },
                    new Gene { Symbol = "PKD2", ChromosomeLocation = "4q21" },
                    new Gene { Symbol = "HBA1", ChromosomeLocation = "16p13.3" },
                    new Gene { Symbol = "HBA2", ChromosomeLocation = "16p13.3" },
                    new Gene { Symbol = "G6PD", ChromosomeLocation = "Xq28" },
                    new Gene { Symbol = "SMN1", ChromosomeLocation = "5q13.2" },
                    new Gene { Symbol = "LDLR", ChromosomeLocation = "19p13.2" },
                    new Gene { Symbol = "NF1", ChromosomeLocation = "17q11.2" },
                    new Gene { Symbol = "NF2", ChromosomeLocation = "22q12.2" },
                    new Gene { Symbol = "COL1A1", ChromosomeLocation = "17q21.33" },
                    new Gene { Symbol = "COL1A2", ChromosomeLocation = "7q21.3" },
                    new Gene { Symbol = "GAA", ChromosomeLocation = "17q25.3" },
                    new Gene { Symbol = "GBA", ChromosomeLocation = "1q22" },
                    new Gene { Symbol = "GLA", ChromosomeLocation = "Xq22.1" },
                    new Gene { Symbol = "RB1", ChromosomeLocation = "13q14.2" },
                    new Gene { Symbol = "SHOX", ChromosomeLocation = "Xp22.33 / Yp11.3" }
                );

                context.SaveChanges();
            }

            if (!context.GeneDiseases.Any() && !context.GenePhenotypes.Any())
            {
                var genes = context.Genes.ToList();
                var diseases = context.Diseases.ToList();
                var phenotypes = context.Phenotypes.ToList();

                Gene G(string s) => genes.First(x => x.Symbol == s);
                Disease D(string s) => diseases.First(x => x.Name == s);
                Phenotype P(string s) => phenotypes.First(x => x.Name == s);

                context.GeneDiseases.AddRange(
                    new GeneDisease { GeneId = G("CFTR").Id, DiseaseId = D("Cystic Fibrosis").Id },
                    new GeneDisease { GeneId = G("HTT").Id, DiseaseId = D("Huntington Disease").Id },
                    new GeneDisease { GeneId = G("HBB").Id, DiseaseId = D("Sickle Cell Anemia").Id },
                    new GeneDisease { GeneId = G("HBB").Id, DiseaseId = D("Beta Thalassemia").Id },
                    new GeneDisease { GeneId = G("DMD").Id, DiseaseId = D("Duchenne Muscular Dystrophy").Id },
                    new GeneDisease { GeneId = G("HEXA").Id, DiseaseId = D("Tay-Sachs Disease").Id },
                    new GeneDisease { GeneId = G("F8").Id, DiseaseId = D("Hemophilia A").Id },
                    new GeneDisease { GeneId = G("F9").Id, DiseaseId = D("Hemophilia B").Id },
                    new GeneDisease { GeneId = G("FBN1").Id, DiseaseId = D("Marfan Syndrome").Id },
                    new GeneDisease { GeneId = G("PAH").Id, DiseaseId = D("Phenylketonuria").Id },
                    new GeneDisease { GeneId = G("TYR").Id, DiseaseId = D("Albinism").Id },
                    new GeneDisease { GeneId = G("ATP7B").Id, DiseaseId = D("Wilson Disease").Id },
                    new GeneDisease { GeneId = G("PMP22").Id, DiseaseId = D("Charcot-Marie-Tooth Disease").Id },
                    new GeneDisease { GeneId = G("MECP2").Id, DiseaseId = D("Rett Syndrome").Id },
                    new GeneDisease { GeneId = G("FMR1").Id, DiseaseId = D("Fragile X Syndrome").Id },
                    new GeneDisease { GeneId = G("PKD1").Id, DiseaseId = D("Polycystic Kidney Disease").Id },
                    new GeneDisease { GeneId = G("PKD2").Id, DiseaseId = D("Polycystic Kidney Disease").Id },
                    new GeneDisease { GeneId = G("HBA1").Id, DiseaseId = D("Alpha Thalassemia").Id },
                    new GeneDisease { GeneId = G("HBA2").Id, DiseaseId = D("Alpha Thalassemia").Id },
                    new GeneDisease { GeneId = G("G6PD").Id, DiseaseId = D("G6PD Deficiency").Id },
                    new GeneDisease { GeneId = G("SMN1").Id, DiseaseId = D("Spinal Muscular Atrophy").Id },
                    new GeneDisease { GeneId = G("LDLR").Id, DiseaseId = D("Familial Hypercholesterolemia").Id },
                    new GeneDisease { GeneId = G("NF1").Id, DiseaseId = D("Neurofibromatosis Type 1").Id },
                    new GeneDisease { GeneId = G("NF2").Id, DiseaseId = D("Neurofibromatosis Type 2").Id },
                    new GeneDisease { GeneId = G("COL1A1").Id, DiseaseId = D("Osteogenesis Imperfecta").Id },
                    new GeneDisease { GeneId = G("COL1A2").Id, DiseaseId = D("Osteogenesis Imperfecta").Id },
                    new GeneDisease { GeneId = G("GAA").Id, DiseaseId = D("Pompe Disease").Id },
                    new GeneDisease { GeneId = G("GBA").Id, DiseaseId = D("Gaucher Disease").Id },
                    new GeneDisease { GeneId = G("GLA").Id, DiseaseId = D("Fabry Disease").Id },
                    new GeneDisease { GeneId = G("RB1").Id, DiseaseId = D("Retinoblastoma").Id },
                    new GeneDisease { GeneId = G("SHOX").Id, DiseaseId = D("Achondroplasia").Id }
                );

                context.GenePhenotypes.AddRange(
                    new GenePhenotype { GeneId = G("CFTR").Id, PhenotypeId = P("Chronic Fatigue").Id },
                    new GenePhenotype { GeneId = G("CFTR").Id, PhenotypeId = P("Growth Retardation").Id }
                );

                context.SaveChanges();
            }
        }
    }
}