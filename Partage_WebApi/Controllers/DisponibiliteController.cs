using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Partage.Data;
using Partage.DTOs;
using Partage.Migrations;
using Partage.Models;
using Partage.Services;

namespace Partage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DisponibiliteController : ControllerBase
    {
        private readonly PartageDbContext _context;

        public DisponibiliteController(PartageDbContext context)
        {
            _context = context;
        }

        // Méthode pour obtenir l'ID de la borne connectée
        private int GetConnectedBorneId()
        {
            // Par exemple, extraire l'ID de la borne à partir des revendications de l'utilisateur connecté
            // Vous devez adapter ceci en fonction de votre logique d'authentification
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var borne = _context.Bornes.FirstOrDefault(b => b.UserId == userId);
            return borne?.BorneId ?? 0; // Retourne 0 si aucune borne n'est trouvée
        }

        // GET: api/Disponibilite
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisponibiliteDTO>>> GetDisponibilites()
        {
            int borneId = GetConnectedBorneId();

            var disponibilites = await _context.Disponibilites
                .Where(d => d.BorneId == borneId)
                .Select(d => new DisponibiliteDTO
                {
                    DisponibiliteId = d.DisponibiliteId,
                    DateDeLaSemaine = d.DateDeLaSemaine,
                    HeureDebut = d.HeureDebut,
                    HeureFin = d.HeureFin,
                    BorneId = d.BorneId
                }).ToListAsync();

            if (disponibilites == null || !disponibilites.Any())
            {
                return NotFound(new { message = "Aucune disponibilité trouvée pour cette borne." });
            }

            return Ok(disponibilites);
        }

        // GET: api/Disponibilite/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DisponibiliteDTO>> GetDisponibilite(int id)
        {
            int borneId = GetConnectedBorneId();

            var disponibilite = await _context.Disponibilites
                .Where(d => d.BorneId == id)
                .Select(d => new DisponibiliteDTO
                {
                    DisponibiliteId = d.DisponibiliteId,
                    DateDeLaSemaine = d.DateDeLaSemaine,
                    HeureDebut = d.HeureDebut,
                    HeureFin = d.HeureFin,
                    BorneId = d.BorneId
                }).ToListAsync();
            //.FirstOrDefaultAsync()
            if (disponibilite == null)
            {
                return NotFound();
            }

            return Ok(disponibilite);
        }

        // POST: api/Disponibilite
        [HttpPost]
        public async Task<ActionResult<Disponibilite>> PostDisponibilite(DisponibiliteDTO disponibiliteDTO)
        {
            int borneId = GetConnectedBorneId();

            TimeSpan heureDebut;
            TimeSpan heureFin;

            try
            {
                heureDebut = TimeSpan.Parse(disponibiliteDTO.HeureDebut);
                heureFin = TimeSpan.Parse(disponibiliteDTO.HeureFin);
            }
            catch (FormatException ex)
            {
                return BadRequest("Format des heures invalide. Veuillez utiliser le format 'HH:mm:ss'.");
            }

            var disponibilite = new Disponibilite
            {
                BorneId = disponibiliteDTO.BorneId,
                DateDeLaSemaine = disponibiliteDTO.DateDeLaSemaine,
                HeureDebut = heureDebut.ToString(),
                HeureFin = heureFin.ToString(),
            };

            _context.Disponibilites.Add(disponibilite);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDisponibilite), new { id = disponibilite.DisponibiliteId }, disponibilite);
        }

        // PUT: api/Disponibilite/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisponibilite(int id, DisponibiliteDTO disponibiliteDTO)
        {
            int borneId = GetConnectedBorneId();

            if (id != disponibiliteDTO.DisponibiliteId)
            {
                return BadRequest();
            }

            var disponibilite = await _context.Disponibilites
                .Where(d => d.DisponibiliteId == id && d.BorneId == borneId)
                .FirstOrDefaultAsync();

            if (disponibilite == null)
            {
                return NotFound();
            }

            disponibilite.DateDeLaSemaine = disponibiliteDTO.DateDeLaSemaine;
            disponibilite.HeureDebut = disponibiliteDTO.HeureDebut;
            disponibilite.HeureFin = disponibiliteDTO.HeureFin;

            _context.Entry(disponibilite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisponibiliteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Disponibilite/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisponibilite(int id)
        {
            int borneId = GetConnectedBorneId();

            var disponibilite = await _context.Disponibilites
                .Where(d => d.DisponibiliteId == id && d.BorneId == borneId)
                .FirstOrDefaultAsync();

            if (disponibilite == null)
            {
                return NotFound();
            }

            _context.Disponibilites.Remove(disponibilite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DisponibiliteExists(int id)
        {
            int borneId = GetConnectedBorneId();
            return _context.Disponibilites.Any(e => e.DisponibiliteId == id && e.BorneId == borneId);
        }
    }


}

