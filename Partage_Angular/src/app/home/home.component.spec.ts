/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { HomeComponent } from './home.component';
import { HttpBackend } from '@angular/common/http';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

// Backend
// [HttpGet("search")]
// public async Task<ActionResult<IEnumerable<Borne>>> SearchBornes([FromQuery] string? typeConnecteur, [FromQuery] int? puissance, [FromQuery] string? nomRue, [FromQuery] string? codePostal, [FromQuery] int? numCivique, [FromQuery] string? ville, [FromQuery] string? province)
// {
//     if (_context.Bornes == null)
//     {
//         return NotFound();
//     }

//     var query = _context.Bornes.AsQueryable();

//     if (!string.IsNullOrEmpty(typeConnecteur))
//     {
//         query = query.Where(b => b.TypeConnecteur == typeConnecteur);
//     }
//     if (puissance.HasValue)
//     {
//         query = query.Where(b => b.Puissance == puissance.Value);
//     }
//     if (!string.IsNullOrEmpty(nomRue))
//     {
//         query = query.Where(b => b.Adresse.NomRue == nomRue);
//     }
//     if (!string.IsNullOrEmpty(codePostal))
//     {
//         query = query.Where(b => b.Adresse.CodePostal == codePostal);
//     }
//     if (numCivique.HasValue)
//     {
//         query = query.Where(b => b.Adresse.NumCivique == numCivique.Value);
//     }
//     if (!string.IsNullOrEmpty(ville))
//     {
//         query = query.Where(b => b.Adresse.Ville == ville);
//     }
//     if (!string.IsNullOrEmpty(province))
//     {
//         query = query.Where(b => b.Adresse.Province == province);
//     }

//     return await query.ToListAsync();
// }