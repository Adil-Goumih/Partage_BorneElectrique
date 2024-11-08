import { ChangeDetectorRef, Component, FactoryProvider, OnInit } from '@angular/core';
import { BorneDTO } from '../models/BorneDTO';
import { borneService } from '../services/borneService';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Favoris } from '../models/favoris';
import { EvaluationDTO } from '../models/EvaluationDTO';
import { DisponibiliteDTO } from '../models/DisponibiliteDTO';


@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  borneId: number = 0;
  borneData: BorneDTO | undefined;
  userIsConnected: boolean | undefined;
  borneRating: number = 0;
  comment: string = '';
  userId :string = '';
  evaluations: EvaluationDTO[] = [];
  disponibilites: DisponibiliteDTO[] = [];
  
  
  constructor(private cdRef: ChangeDetectorRef, public http:HttpClient, public borneService: borneService, public route : ActivatedRoute, public router:Router) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras?.state) {
      this.borneData = navigation.extras.state?.['data'];
      if (this.borneData) {
        this.borneId = this.borneData.borneId; // Assurez-vous que borneId est bien défini
        
      }
    }

   }
  ngOnInit() {
    // Récupérer les données passées via l'état de la navigation 
    this.userIsConnected = sessionStorage.getItem("token") != null  
    console.log('Borne ' + this.borneId );
    this.getBorneById();
    this.getEvaluations();
  }

  getBorneById() {
    if (this.borneId) {
      this.borneService.getBorneByID(this.borneId).subscribe(
        async (borne: BorneDTO) => {
          this.borneData = borne;
          const moyenneNote = await this.borneService.getMoyenneNote(borne.borneId).toPromise();
        this.borneData.moyenneNote = moyenneNote !== null ? moyenneNote : 0;
          
          console.log(this.borneData); // Affichage des détails de la borne dans la console
        },
        (error) => {
          console.error('Erreur lors de la récupération des détails de la borne : ', error);
        }
      );
    }}

    ajouterFavoris(id : number) {  
      
      if (id)
      {
        this.borneService.ajouterFavoris(id)
        try {
            console.log('Borne ' + id + ' ajoutée aux favoris');
          alert('Borne ajoutée aux Favoris avec succès!');
            }

           catch (error) {
            console.error('Erreur lors de l\'ajout de la borne aux favoris :', error);
          }
      
      }
    } 

   async voirDisponibilite(borneId: number) {
  try {
    this.disponibilites = await this.borneService.GetDisponibilite(borneId);
    console.log('Disponibilités:', this.disponibilites);
  } catch (error) {
    console.error('Erreur lors de l\'affichage des disponibilités :', error);
  }
}


    async submitEvaluation() {
     
      if (this.borneRating <= 0 || this.borneRating > 5) {
        alert('Veuillez sélectionner une note valide.');
        return;
      }
      if (!this.borneId) {
        alert('ID de la borne est manquant.');
        return;
      }
    
      const evaluationDto: EvaluationDTO = {
        
        BorneId: this.borneId,
        Note: this.borneRating,
        Commentaire: this.comment || ''
      };
      alert('Évaluation envoyée avec succès!');
      window.location.reload();
      console.log('Données envoyées :', evaluationDto);
      const userId = sessionStorage.getItem("userId"); // Récupération du userId
    console.log('ID utilisateur récupéré :', userId); // Débogage
    if (!userId) {
      alert('Utilisateur non authentifié.');
      return;
  }
  const evaluationDtoWithUserId = {
    ...evaluationDto,
    UserId: userId
};
console.log('Données envoyées :', evaluationDtoWithUserId);


(await this.borneService.ajouterEvaluation(evaluationDtoWithUserId)).subscribe(
  () => {
      alert('Évaluation ajoutée avec succès!');
      this.borneRating = 0; // Réinitialiser la note
      this.comment = ''; // Réinitialiser le commentaire
  },
  (error) => {
      console.error('Erreur lors de l\'ajout de l\'évaluation :', error);
      console.log('Données envoyées :', evaluationDtoWithUserId);
  }
);
     
    }
    updateRating(rating: number) {
      this.borneRating = rating;
    }
    updateComment(comment: string) {
      this.comment = comment;
    }  

    getEvaluations() {
      if (this.borneId) {
        this.borneService.GetEvaluations(this.borneId).then(
          (evaluations: any[]) => {
            
            this.evaluations = evaluations.map(evaluation => ({
              Note: evaluation.note,
              Commentaire: evaluation.commentaire,
              BorneId: evaluation.borneId,
              UserId: evaluation.userId
            }));
            console.log('Evaluations récupérées:', this.evaluations);
          },
          (error) => {
            console.error('Erreur lors de la récupération des évaluations :', error);
          }
        );
      }
    }
}
