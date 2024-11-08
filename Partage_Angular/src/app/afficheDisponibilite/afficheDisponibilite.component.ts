import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { EvaluationDTO } from '../services/borneService';
import { BorneDTO } from '../models/BorneDTO';
import { DisponibiliteDTO } from '../models/DisponibiliteDTO';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { borneService } from '../services/borneService';

@Component({
  selector: 'app-afficheDisponibilite',
  templateUrl: './afficheDisponibilite.component.html',
  styleUrls: ['./afficheDisponibilite.component.css']
})
export class AfficheDisponibiliteComponent implements OnInit {

  borneId: number = 0;
  borneData: BorneDTO | undefined;
  userIsConnected: boolean | undefined;
  borneRating: number = 0;
  comment: string = '';
  userId :string = '';
 
  disponibilites: DisponibiliteDTO[] = [];
  
  
  constructor(private cdRef: ChangeDetectorRef, public http:HttpClient, public borneService: borneService, public route : ActivatedRoute, public router:Router) {}

  ngOnInit() {
    // Récupération de l'ID de la borne à partir des paramètres de la route
    this.borneId = +this.route.snapshot.paramMap.get('id')!;
    this.voirDisponibilite(this.borneId);
  }

  async voirDisponibilite(borneId: number) {
    try {
      this.disponibilites = await this.borneService.GetDisponibilite(borneId);
      console.log('Disponibilités:', this.disponibilites);
    } catch (error) {
      console.error('Erreur lors de l\'affichage des disponibilités :', error);
    }
  }
  

}
