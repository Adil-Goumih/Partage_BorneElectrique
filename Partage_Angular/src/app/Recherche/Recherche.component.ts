import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { borneService } from '../services/borneService';
import { BorneDTO } from '../models/BorneDTO';

@Component({
  selector: 'app-Recherche',
  templateUrl: './Recherche.component.html',
  styleUrls: ['./Recherche.component.css']
})
export class RechercheComponent {
  borneId: number | undefined
  address: string = "";
  typeConnecteur: string = "";
  puissance: number | undefined;
  results: BorneDTO[] | undefined;
  minimumNote: number | undefined;
  isFavoris: boolean | undefined;

  typeConnecteurs: string[] = ['','NACS', 'J1772'];
  puissances: (number | undefined)[] = [7, 10, 20, 25, 50, 150, 250];  // Ajouter `null` comme option initiale
  notes: (number | undefined)[] = [1, 2, 3, 4, 5];  // Ajouter `null` comme option initiale
  errorMessage: string = '';
  userIsConnected : boolean = false;

  constructor(private borneService: borneService, public router : Router, public route : ActivatedRoute) {}  

  ngOnInit() {
    
    this.userIsConnected = sessionStorage.getItem("token") != null
    const borneIdParam = this.route.snapshot.paramMap.get('borneId');   
    if(borneIdParam !== null) 
    {
      this.borneId = parseInt(borneIdParam, 10);   
    }else
    {     
      this.borneId = undefined;   
    }   
      console.log(this.borneId); 
        
  }

  onSearch(): void {
    if (true) {
      this.borneService.searchBornes(this.address, this.typeConnecteur, this.puissance, this.minimumNote,  this.isFavoris)
        .subscribe(
          data => {
            if (data && data.length > 0) {  
              this.results = data;
              this.results.forEach(borne => {
                this.borneService.getMoyenneNote(borne.borneId).subscribe(
                  moyenneNote => {
                    borne.moyenneNote = moyenneNote;  // Adiciona a média da nota diretamente
                  },
                  error => {
                    console.error('Erreur lors de la récupération de la moyenne des notes:', error);
                    borne.moyenneNote = null;  // Se houver erro, define como null
                  }
                );
              });
              this.errorMessage = '';  
            } else {
              this.results = [];  
              this.errorMessage = 'Aucune borne trouvée pour les critères de recherche fournis.';
            }
          },
          error => {
            console.error(error);
            if (error.status === 404) {
              this.results = [];  
              this.errorMessage = 'Aucune borne trouvée pour les critères de recherche fournis.';  
            } 
          }
        );
    } else {
      this.errorMessage = 'Veuillez remplir au moins un des champs pour effectuer une recherche.';
    }
  }
  

  isFormValid(): boolean { 
    if(
    (this.address && this.address !== '') ||
    (this.typeConnecteur && this.typeConnecteur.length > 0 && this.typeConnecteur !== '') ||
    (this.puissance && this.puissance > 0 && this.puissance !== null && this.puissance !== undefined) ||
    (this.minimumNote && this.minimumNote > 0) ||
    this.isFavoris){ 
      return false;
    }   
    return true;
  }

  getBorneByID(borneId: number): void {
    // Vérifier que l'ID de la borne est valide
    if (borneId == null) {
      console.error('Borne ID is undefined or null');
      return;
    }
  
    // Récupérer les détails de la borne via le service
    this.borneService.getBorneByID(borneId).subscribe(
      (data: BorneDTO) => {
        // Afficher les détails de la borne
        console.log('Details de la Borne:', data);
  
        // Naviguer vers la page des détails de la borne avec l'état
        this.router.navigate(['/details', borneId], {state: { data },
        });
      },
      error => {
        // Gérer les erreurs de la requête
        console.error('Error:', error);
      }
    );
  }
}
