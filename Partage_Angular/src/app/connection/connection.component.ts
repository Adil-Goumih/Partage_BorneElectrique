import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { borneService } from '../services/borneService';

@Component({
  selector: 'app-connection',
  templateUrl: './connection.component.html',
  styleUrls: ['./connection.component.css']
})
export class ConnectionComponent implements OnInit {

  connectionCourriel : string = "";
  connectionMotDePasse : string = "";
  userIsConnected : boolean = false;

  constructor(public route : Router, public http : HttpClient, public borneService : borneService) { }

  ngOnInit() {

    this.userIsConnected = sessionStorage.getItem("token") != null    
    
  }

  async connection(){    
    if (!this.connectionCourriel || !this.connectionMotDePasse) {
      alert('Veuillez remplir tous les champs.');
      return;
    }
    try {
      await this.borneService.getConnetion(this.connectionCourriel, this.connectionMotDePasse);
      console.log('Token et userId stockés après connexion');
      console.log('Token:', sessionStorage.getItem("token"));
      console.log('userId:', sessionStorage.getItem("userId"));
      window.location.reload(); 
    } catch (error) {
      console.error('Erreur lors de la connexion :', error);
    }
}
}
