import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BorneDTO } from '../models/BorneDTO';
import { borneService } from '../services/borneService';
import { DisponibiliteDTO } from '../models/DisponibiliteDTO';
 
@Component({
  selector: 'app-favoris',
  templateUrl: './favoris.component.html',
  styleUrls: ['./favoris.component.css']
})
export class FavorisComponent implements OnInit {
  borneId: number |undefined;
  borneData: BorneDTO | undefined;
  userIsConnected: boolean | undefined;
  favoris: BorneDTO[] = [];
  favorisId: number | undefined; 
  disponibilitesMap: { [borneId: number]: DisponibiliteDTO[] } = {};
  disponibiliteId :  number | undefined;
  mesBornes: BorneDTO[] = [];
  constructor(public http:HttpClient, public borneService: borneService, public route : ActivatedRoute, public router:Router){ }
 
  ngOnInit() {
    this.userIsConnected = sessionStorage.getItem("token") != null
    this.getMesFavoris();
    this.route.snapshot.paramMap.get('favorisId');
    
  }
 
  async getMesFavoris() {
   console.log('test1')
   try {
    this.favoris = await this.borneService.getMesFavoris();
    for (let borne of this.favoris) {
      this.voirDisponibilite(borne.borneId);
    }
    for (let borne of this.favoris) {
      const moyenneNote = await this.borneService.getMoyenneNote(borne.borneId).toPromise();
      borne.moyenneNote = moyenneNote !== null ? moyenneNote : 0;
     
    }}
    
    catch (error) {
      console.error('Error', error);
      console.log('test 3')
    }
  }
 
  async supprimerFavoris(favorisId: number) {
    try {
      await this.borneService.supprimerFavoris(favorisId);
      alert('Favori supprimé de votre liste');
      this.getMesFavoris(); 
    } catch (error) {
      console.error('Error deleting favorite:', error);
    }
  }
  async voirDisponibilite(borneId: number) {
    try {
      const disponibilites = await this.borneService.GetDisponibilite(borneId);
      this.disponibilitesMap[borneId] = disponibilites;
    } catch (error) {
      console.error('Error fetching disponibilités:', error);
    }
  }
}