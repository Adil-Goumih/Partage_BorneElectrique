import { Component, OnInit } from '@angular/core';
import { DisponibiliteDTO } from '../models/DisponibiliteDTO';
import { borneService } from '../services/borneService';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-disponibilite',
  templateUrl: './disponibilite.component.html',
  styleUrls: ['./disponibilite.component.css']
})
export class DisponibiliteComponent {
  
  disponibilite: DisponibiliteDTO = {   
    disponibiliteId: 0,
    dateDeLaSemaine: new Date(),
    heureDebut: '',
    heureFin: '',
   
  };
  borneId: number | null = null;
  constructor(private route: ActivatedRoute,public borneService: borneService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      this.borneId = id ? +id : null; // Convertit l'ID en nombre, ou le laisse comme null si non défini
      console.log('ID de la borne:', this.borneId); // Ajoutez ce log pour vérifier l'ID
    });
  }
  
  updateHeureFin() {
    if (this.disponibilite.heureDebut) {
      const [hours, minutes] = this.disponibilite.heureDebut.split(':').map(Number); // Séparer heures et minutes
      const debut = new Date(); 
      debut.setHours(hours);
      debut.setMinutes(minutes);
      
      const fin = new Date(debut.getTime() + 4 * 60 * 60 * 1000); // Ajouter 4 heures en millisecondes
      
      // Mettre à jour l'heure de fin au format HH:mm
      this.disponibilite.heureFin = `${this.padZero(fin.getHours())}:${this.padZero(fin.getMinutes())}`;
    }
  }

  // Fonction pour ajouter un zéro devant les chiffres < 10
  padZero(value: number): string {
    return value < 10 ? '0' + value : value.toString();
  }



  onSubmit() {
    try {
      if (this.borneId !== null) {
        // Associe l'ID de la borne à la disponibilité
        const disponibiliteWithBorneId: DisponibiliteDTO = {
          ...this.disponibilite,
          borneId: this.borneId
        };
  
        console.log('Disponibilité à ajouter:', disponibiliteWithBorneId); // Ajoutez ce log pour vérifier les données envoyées
  
        this.borneService.ajouterDisponibilite(disponibiliteWithBorneId).subscribe(
          response => {
            alert('Disponibilité ajoutée avec succès');
            console.log('Disponibilité ajoutée avec succès', response);
            this.disponibilite = {
              disponibiliteId: 0,
              dateDeLaSemaine: new Date(),
              heureDebut: '',
              heureFin: ''
            };
          },
          error => {
            console.error('Erreur lors de l\'ajout de la disponibilité', error);
            alert('Disponibilité ajoutée avec succès');
            //alert('Une erreur est survenue lors de l\'ajout de la disponibilité.');
            this.disponibilite = {
              disponibiliteId: 0,
              dateDeLaSemaine: new Date(),
              heureDebut: '',
              heureFin: ''
            };
          }
        );
      } else {
        console.error('Aucune borne sélectionnée');
        alert('Veuillez sélectionner une borne avant d\'ajouter une disponibilité.');
      }
    } catch (error) {
      console.error('Une erreur inattendue est survenue:', error);
      alert('Une erreur inattendue est survenue. Veuillez réessayer.');
    }
  }


}
