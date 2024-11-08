import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { borneService } from '../services/borneService';

@Component({
  selector: 'app-inscription',
  templateUrl: './inscription.component.html',
  styleUrls: ['./inscription.component.css']
})
export class InscriptionComponent implements OnInit {

  inscriptionNom : string = "";
  inscriptionPrenom : string = ""; 
  inscriptionUserName : string = ""; 
  inscriptionCourriel : string = "";
  inscriptionMotDePasse : string = ""; 
  inscriptionMotDePasseConfirm : string = "";

  constructor(public route : Router, public http : HttpClient, public borneService : borneService) { }

  ngOnInit() {
  }

  async inscriptionMembre(){
   
    if( !this.inscriptionNom  || !this.inscriptionPrenom  || !this.inscriptionUserName || !this.inscriptionCourriel  || !this.inscriptionMotDePasse  || !this.inscriptionMotDePasseConfirm )
    {
      alert('Veuillez remplir tous les champs.');
      return
    }
   
    if(!this.inscriptionMotDePasse  || !this.inscriptionMotDePasseConfirm || this.inscriptionMotDePasse != this.inscriptionMotDePasseConfirm )
    {
      alert('Les mots de passe ne correspondent pas');
      return
    }
   
    if( this.inscriptionMotDePasse == this.inscriptionMotDePasseConfirm )
    {
      this.borneService.inscriptionMembre(this.inscriptionNom, this.inscriptionPrenom,this.inscriptionUserName, this.inscriptionCourriel, this.inscriptionMotDePasse, this.inscriptionMotDePasseConfirm)
      alert('Inscription ruéssi');
      this.route.navigate(["/connection"])
    }
  }



}
