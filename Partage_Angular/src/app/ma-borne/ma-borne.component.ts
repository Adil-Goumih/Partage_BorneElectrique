import { Component, OnInit } from '@angular/core';
import { Borne } from '../models/borne';
import { borneService } from '../services/borneService';
import { BorneDTO } from '../models/BorneDTO';
import { DisponibiliteDTO } from '../models/DisponibiliteDTO';

@Component({
  selector: 'app-ma-borne',
  templateUrl: './ma-borne.component.html',
  styleUrls: ['./ma-borne.component.css']
})
export class MaBorneComponent implements OnInit {
  mesBornes: BorneDTO[] = [];
  userIsConnected: boolean | undefined;
  borneId: number | undefined;
  disponibilites: DisponibiliteDTO[] = [];
  disponibilitesMap: { [borneId: number]: DisponibiliteDTO[] } = {};
  disponibiliteId :  number | undefined;
  editingDisponibiliteId: number | null = null
  

  constructor(private borneService: borneService) { }


  ngOnInit(): void {
    this.userIsConnected = sessionStorage.getItem("token") != null
    this.getMesBornes();
    
    }

    async getMesBornes() {
      try {
        this.mesBornes = await this.borneService.getMyBorne();
        console.log(this.mesBornes);
       
        // Fetch disponibilites for each borne
        for (let borne of this.mesBornes) {
          this.voirDisponibilite(borne.borneId);
        }
        for (let borne of this.mesBornes) {
          const moyenneNote = await this.borneService.getMoyenneNote(borne.borneId).toPromise();
          borne.moyenneNote = moyenneNote !== null ? moyenneNote : 0;
        }
      } catch (error) {
        console.error('Erreur lors de la récupération des bornes:', error);
      }
    }
    async supprimerBorne(borneId: number) {
      try {
        await this.borneService.supprimerBorne(borneId);
        alert('Borne supprimé de votre liste');
        this.getMesBornes(); 
      } catch (error) {
        console.error('Erreur de supprission de la borne:', error);
      }
    }
    async voirDisponibilite(borneId: number) {
      try {
        const disponibilites = await this.borneService.GetDisponibilite(borneId);
        this.disponibilitesMap[borneId] = disponibilites; // Store the disponibilites for the specific borneId
      } catch (error) {
        console.error('Erreur lors de l\'affichage des disponibilités :', error);
      }
    }
    toggleEdit(disponibiliteId: number) {
      this.editingDisponibiliteId = this.editingDisponibiliteId === disponibiliteId ? null : disponibiliteId;
    }
 
    isEditing(disponibiliteId: number): boolean {
      return this.editingDisponibiliteId === disponibiliteId;
    }
 
    async saveDisponibilite(disponibilite: DisponibiliteDTO) {
      try {
        await this.borneService.updateDisponibilite(disponibilite.disponibiliteId, disponibilite).toPromise();
        alert('Disponibilité mise à jour avec succès');
        this.editingDisponibiliteId = null; // Arrêter le mode édition
      } catch (error) {
        console.error('Erreur lors de la mise à jour de la disponibilité:', error);
      }
    }
    async deleteDisponibilite(disponibiliteId: number, borneId: number) {
      try {
        await this.borneService.deleteDisponibilite(disponibiliteId);
        alert('Disponibilité supprimée');
       
        this.disponibilitesMap[borneId] = this.disponibilitesMap[borneId].filter(d => d.disponibiliteId !== disponibiliteId);
      } catch (error) {
        console.error('Erreur lors de la suppression de la disponibilité :', error);
      }
    }
  }


