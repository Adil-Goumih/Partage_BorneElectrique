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
    public class BornesController : ControllerBase
    {
        private readonly PartageDbContext _context;
        private readonly GeocodingService _geocodingService;

        public BornesController(PartageDbContext context, GeocodingService geocodingService)
        {
            _context = context;
            _geocodingService = geocodingService;
        }

        // GET: api/Bornes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borne>>> GetBorne()
        {
            if (_context.Bornes == null)
            {
                return NotFound();
            }  
            return await _context.Bornes.ToListAsync();
        }

        // GET: api/Bornes/5
        [HttpGet("GetBorne/{id}")]
        public async Task<ActionResult<BorneDTO>> GetBorne(int id)
        {
          if (_context.Bornes == null)
          {
              return NotFound();
          }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            User? user = await _context.Users.FindAsync(userId);

            Borne borne = await _context.Bornes.FindAsync(id);

            if (borne == null)
            {
                return NotFound();
            }


            var borneDTO = new BorneDTO
            {
                BorneId = borne.BorneId,
                TypeConnecteur = borne.TypeConnecteur,
                Puissance = borne.Puissance,
                NumCivique = borne.Adresse.NumCivique,
                NomRue = borne.Adresse.NomRue,
                Ville = borne.Adresse.Ville,
                Province = borne.Adresse.Province,
                CodePostal = borne.Adresse.CodePostal
        // Ajoutez toutes les autres propriétés nécessaires ici
    };


            return borneDTO;
        }

        [Authorize]
        [HttpGet("GetMyBornes")]
        public async Task<ActionResult<IEnumerable<BorneDTO>>> GetMyBornes()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _context.Users
                .Include(u => u.Bornes)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || !user.Bornes.Any())
            {
                return NotFound("Aucune borne trouvée pour l'utilisateur connecté.");
            }

            var bornesDTO = user.Bornes.Select(borne => new BorneDTO
            {
                BorneId = borne.BorneId,
                TypeConnecteur = borne.TypeConnecteur,
                Puissance = borne.Puissance,
                NumCivique = borne.Adresse.NumCivique,
                NomRue = borne.Adresse.NomRue,
                Ville = borne.Adresse.Ville,
                Province = borne.Adresse.Province,
                CodePostal = borne.Adresse.CodePostal
            }).ToList();

            return Ok(bornesDTO);
        }









        // PUT: api/Bornes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorne(int id, Borne borne)
        {
            if (id != borne.BorneId)
            {
                return BadRequest();
            }

            _context.Entry(borne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorneExists(id))
                {
                    return NotFound();
                }else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bornes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostBorne")]
        public async Task<ActionResult<Borne>> PostBorne(Borne borne)
        {


          if (_context.Bornes == null)
          {
              return Problem("Entity set 'PartageDbContext.Borne'  is null.");
          }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            User? user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                 borne.User= user;
                 user.Bornes.Add(borne);
                await _geocodingService.UpdateAdresseWithCoordinates(borne.Adresse);
                _context.Bornes.Add(borne);
                 await _context.SaveChangesAsync();
                 return CreatedAtAction("GetBorne", new { id = borne.BorneId }, borne);
            }

            return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Utilisateur non trouvé"});


        }

        // DELETE: api/Bornes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorne(int id)
        {
            if (_context.Bornes == null)
            {
                return NotFound();
            }
            var borne = await _context.Bornes.FindAsync(id);
            if (borne == null)
            {
                return NotFound();
            }

            _context.Bornes.Remove(borne);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BorneExists(int id)
        {
            return (_context.Bornes?.Any(e => e.BorneId == id)).GetValueOrDefault();
        }

        [Authorize]
        [HttpGet("SearchByAddress")]
        public async Task<ActionResult<IEnumerable<BorneDTO>>> SearchByAddress([FromQuery] string? address = null, 
                                                                               [FromQuery] string? typeConnecteur = null,                                                                                                                    
                                                                               [FromQuery] int? puissance = null,                                                                                                                    
                                                                               [FromQuery] bool? favoris = null,                                                                                                                   
                                                                               [FromQuery] int? minimumNote = null)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            User? user = await _context.Users.FindAsync(userId);

            try
            {
                double? latitude = null;
                double? longitude = null;

                if (!string.IsNullOrEmpty(address))
                {
                    var coordinates = _geocodingService.GetCoordinates(address);
                    latitude = coordinates.Latitude;
                    longitude = coordinates.Longitude;
                }

                var bornesQuery = _context.Bornes
                                 .Include(b => b.Adresse)
                                 .Include(b => b.ListFavoris)
                                 .Include(b => b.Evaluations)
                                 .AsQueryable();

                if (!string.IsNullOrEmpty(typeConnecteur))
                {
                    bornesQuery = bornesQuery.Where(b => b.TypeConnecteur == typeConnecteur);
                }

                if (puissance.HasValue)
                {
                    bornesQuery = bornesQuery.Where(b => b.Puissance == puissance.Value);
                }

                if (favoris.HasValue)
                {
                    
                    if (userId == null)
                    {
                        return Unauthorized();
                    }

                    if (favoris.Value)
                    {
                        bornesQuery = bornesQuery.Where(b => b.ListFavoris.Any(f => f.UserId == userId && f.IsFavoris == true));
                    }
                    else
                    {
                        bornesQuery = bornesQuery.Where(b => !b.ListFavoris.Any(f => f.UserId == userId && f.IsFavoris == true));
                    }
                }

                if (minimumNote.HasValue)
                {
                    bornesQuery = bornesQuery.Where(b => b.Evaluations.Any() && b.Evaluations.Average(e => e.Note) >= minimumNote.Value);
                }

                var bornes = await bornesQuery.ToListAsync();

                if (latitude.HasValue && longitude.HasValue)
                {
                    double radiusInKm = 50000.0;
                    bornes = bornes.Where(b => GetDistance(
                                                           latitude.Value,
                                                           longitude.Value,
                                                           b.Adresse.Latitude,
                                                           b.Adresse.Longitude) <= radiusInKm)
                                 .OrderBy(b => GetDistance(
                                                           latitude.Value,
                                                           longitude.Value,
                                                           b.Adresse.Latitude,
                                                           b.Adresse.Longitude)).ToList();
                }

                var borneDtos = bornes.Select(b => new BorneDTO
                {
                    BorneId = b.BorneId,
                    TypeConnecteur = b.TypeConnecteur,
                    Puissance = b.Puissance,
                    NumCivique = b.Adresse.NumCivique,
                    NomRue = b.Adresse.NomRue,
                    Ville = b.Adresse.Ville,
                    Province = b.Adresse.Province,
                    CodePostal = b.Adresse.CodePostal,
                    Evaluations = b.Evaluations.Select(e => new EvaluationDTO
                    {
                        UserId = e.UserId,
                        BorneId = e.BorneId,
                        Note = e.Note,
                        Commentaire = e.Commentaire
                    }).ToList()
                }).ToList();

                if (borneDtos == null || !borneDtos.Any())

                {
                    return NotFound();
                }

                return Ok(borneDtos);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }

        }

        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371;
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a =
                    Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c;

            return distance;
        }

        private double ToRadians(double angle)
        {
            return angle * Math.PI / 180;
        }

        [Authorize]
        [HttpPost("AjouterFavoris/{id}")]
        public async Task<IActionResult> AjouterFavoris(int id)
        {

           
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var borne = await _context.Bornes.FindAsync(id);
            if (borne == null)
            {
                return NotFound();
            }
            
        var favoris = await _context.Favoris
                .FirstOrDefaultAsync(f => f.UserId == userId && f.BorneId == id);

            if (favoris != null)
            {
                favoris.IsFavoris = true;
                _context.Favoris.Update(favoris);
            }
            else
            {
                favoris = new Models.Favoris
                {
                    UserId = userId,
                    BorneId = id,
                    IsFavoris = true
                };
                _context.Favoris.Add(favoris);
            }


            await _context.SaveChangesAsync();

            return Ok(favoris);



        }


        [Authorize]

        [HttpPut("SupprimerFavoris/{id}")]

        public async Task<IActionResult> SupprimerFavoris(int id)

        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var favoris = await _context.Favoris
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FavorisId == id);

            if (favoris == null)
            {
                return NotFound("Favori non trouvé.");
            }

            favoris.IsFavoris = false;

            _context.Favoris.Update(favoris);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //GetFavoris
        [Authorize]
        [HttpGet("GetMesFavoris")]
        public async Task<ActionResult<IEnumerable<BorneDTO>>> GetMesFavoris()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            List<BorneDTO> favoris = await _context.Favoris

               .Where(f => f.UserId == userId && f.IsFavoris)

               .Include(f => f.Borne)

               .Select(f => new BorneDTO

               {

                   BorneId = f.Borne.BorneId,
                   FavorisId = f.FavorisId,
                   TypeConnecteur = f.Borne.TypeConnecteur,
                   Puissance = f.Borne.Puissance,
                   NumCivique = f.Borne.Adresse.NumCivique,
                   NomRue = f.Borne.Adresse.NomRue,
                   Ville = f.Borne.Adresse.Ville,
                   Province = f.Borne.Adresse.Province,
                   CodePostal = f.Borne.Adresse.CodePostal,
                   Evaluations = f.Borne.Evaluations.Select(e => new EvaluationDTO
                   {


                   }).ToList()

               }).ToListAsync();

            return Ok(favoris);
        }

        [Authorize]
        [HttpPost("AjouterEvaluation")]
        public async Task<ActionResult> AjouterEvaluation(EvaluationDTO evaluationDto)
        {
            if (evaluationDto == null)
            {
                return BadRequest("EvaluationDTO ne peut pas être nul.");
            }

            // Assurez-vous que l'ID utilisateur est présent
            if (string.IsNullOrEmpty(evaluationDto.UserId))
            {
                return BadRequest("UserId est requis.");
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return Unauthorized();
                }

                // Validation de la note
                if (evaluationDto.Note < 0 || evaluationDto.Note > 5)
                {
                    return BadRequest(new { Message = "La note doit être comprise entre 0 et 5." });
                }

                var evaluation = new Evaluation
                {
                    BorneId = evaluationDto.BorneId, 
                    UserId = userId,
                    Note = evaluationDto.Note,
                    Commentaire = evaluationDto.Commentaire,
                    Date = DateTime.UtcNow
                };

                _context.Evaluations.Add(evaluation);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("GetMoyenneNote/{borneId}")]
        public async Task<double> GetMoyenneNote(int borneId)
        {
            return await _context.Evaluations.Where(e => e.BorneId == borneId).AverageAsync(e => e.Note);
        }

        [Authorize]
        [HttpGet("GetEvaluations/{borneId}")]
        public async Task<ActionResult<IEnumerable<EvaluationDTO>>> GetEvaluations(int borneId)
        {
            var evaluations = await _context.Evaluations
                                   .Where(e => e.BorneId == borneId)
                                   .Select(e => new EvaluationDTO
                                   {
                                       BorneId = e.BorneId,
                                       UserId = e.UserId,
                                       Note = e.Note,
                                       Commentaire = e.Commentaire
                                   }).ToListAsync();

            if (evaluations == null || !evaluations.Any())
            {
                return NotFound();
            }

            return Ok(evaluations);
        }



    }
}
