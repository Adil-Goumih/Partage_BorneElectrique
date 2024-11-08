
import { Component, OnInit } from '@angular/core';
import { borneService } from '../services/borneService';
import { Adresse, Borne } from '../models/borne';


@Component({
  selector: 'app-borne',
  templateUrl: './borne.component.html',
  styleUrls: ['./borne.component.css']
})
export class BorneComponent {
      borne: Borne;
      typeConnecteur: string = "";
      puissance: number = 0; 
      nomRue: string = "";
      codePostal: string = "";
      numCivique: number = 0; 
      ville: string = "";
      province: string = "";
      userIsConnected : boolean = false;



  constructor(public borneService: borneService) {
    this.borne = new Borne(0, this.typeConnecteur, this.puissance, 
                 new Adresse(0, this.nomRue, this.codePostal, 
                                this.numCivique, this.ville, 
                                this.province)); 
   }
  
  ngOnInit() {
    
    this.userIsConnected = sessionStorage.getItem("token") != null
  }
  
  async addBorne() {
    console.log(this.borne);
    try {
      let result = await this.borneService.postBorne(this.borne);
      console.log('Borne ajoutée avec succès', result);
     
      alert('Borne ajoutée avec succès');
      this.resetForm();
      
    } catch (error) {
      console.error('Erreur lors de l\'ajout de la borne:', error);
      alert('Erreur lors de l\'ajout de la borne');
    }
  }
  resetForm() {
    this.borne = new Borne(0, "", 0, new Adresse(0, "", "", 0, "", ""));
  }

}