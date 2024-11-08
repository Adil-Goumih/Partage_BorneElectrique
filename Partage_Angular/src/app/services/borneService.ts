import { Adresse, Borne } from '../models/borne';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, lastValueFrom } from 'rxjs';
import { Inscription } from '../models/inscription';
import { Connection } from '../models/connection';
import { rechercheAdresse } from '../models/Adresse';
import { BorneDTO } from '../models/BorneDTO';
import { Router } from '@angular/router';
import { Favoris } from '../models/favoris';
import { DisponibiliteDTO } from '../models/DisponibiliteDTO';
 
 
 
 
const GOOGLE_KEY : string= "AIzaSyBBQs5E0hsoebOjW1EZAjSabi16bSRuxu4";
 
export interface EvaluationDTO {
  BorneId: number;
  minimumNote?: number;
  Commentaire: string;
}
 
@Injectable({
  providedIn: 'root'
})
export class borneService {
 
  private apiUrl          = 'http://localhost:5026';
  private apiUrlRecherche = 'http://localhost:5026/api/Bornes';
  private apiUrlDespo     = 'http://localhost:5026/api/Disponibilite';
 
 
  constructor(public route : Router,public http : HttpClient) { }  
 
 
  async postBorne(borne: Borne): Promise<Observable<Borne>>{
    //La connexion est établie par l'intercepteur
    return await lastValueFrom(this.http.post<any>(this.apiUrl + "/api/Bornes/PostBorne", borne));
 
  }
 
  async inscriptionMembre(Nom: string, Prenom: string, Pseudo: string, Courriel: string, MotDePasse: string, MotDePasseConfirm: string): Promise<void> {
    let register = new Inscription(Nom, Prenom, Pseudo, Courriel, MotDePasse, MotDePasseConfirm);
    console.log(register);
    let x = await lastValueFrom(this.http.post<Inscription>(this.apiUrl + "/api/Users/Inscription", register));  
   
    }
 
    async getConnetion(connectionCourriel: string, connetionPassword: string) {
      let connetionDTO = new Connection(connectionCourriel, connetionPassword);
   
      console.log(connetionDTO);
     
      try {
        let response = await lastValueFrom(this.http.post<any>(this.apiUrl + "/api/Users/Connection", connetionDTO));
        console.log('Réponse de la connexion :', response);
        if (response && response.token) {
          sessionStorage.setItem("token", response.token);
          sessionStorage.setItem("userId", response.userId); // Stocker userId
          sessionStorage.setItem("connetionCourriel", connectionCourriel);
          this.route.navigate(["/home"]);
      } else {
          throw new Error('Réponse de connexion invalide.');
      }
      } catch (error) {
        if (error instanceof HttpErrorResponse && error.status === 400) {
          alert(error.error.message);
        } else {
          console.error('An unexpected error occurred:', error);
          alert('veuillez verifier si votre Api est démarré:');
        }
      }
    }
    getUserId(): string | null {
      return sessionStorage.getItem("userId");

    }
 searchBornes(address: string, typeConnecteur: string, puissance?: number, minimumNote?: number, favoris?: boolean): Observable<BorneDTO[]> {
  
  let token = sessionStorage.getItem("token");
  let httpoptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    }),
    params: new HttpParams()
  };
 
  if (address) httpoptions.params = httpoptions.params.set('address', address);
  if (typeConnecteur) httpoptions.params = httpoptions.params.set('typeConnecteur', typeConnecteur);
  if (puissance !== undefined) httpoptions.params = httpoptions.params.set('puissance', puissance.toString());
  if (minimumNote !== undefined) httpoptions.params = httpoptions.params.set('minimumNote', minimumNote.toString());
  if (favoris !== undefined) httpoptions.params = httpoptions.params.set('favoris', favoris.toString());
 
  return this.http.get<BorneDTO[]>(`${this.apiUrlRecherche}/SearchByAddress`, httpoptions)
}
getBorneByID(borneId: number) {
  //La connexion est établie par l'intercepteur
  return this.http.get<BorneDTO>(`${this.apiUrlRecherche}/GetBorne/`+ borneId);
}
 
async ajouterFavoris(borneId: number): Promise<Observable<any>> {
  //La connexion est établie par l'intercepteur
  return await lastValueFrom(this.http.post<any>(`${this.apiUrlRecherche}/AjouterFavoris/${borneId}`, {}));
 
}
 
async getMesFavoris() : Promise<BorneDTO[]>{
//La connexion est établie par l'intercepteur
  return await lastValueFrom(this.http.get<BorneDTO[]>(`${this.apiUrlRecherche}/GetMesFavoris`));
}
 
getMoyenneNote(borneId: number): Observable<number> {
  return this.http.get<number>(`${this.apiUrlRecherche}/GetMoyenneNote/${borneId}`);
}

async supprimerFavoris(favorisId: number) {
  //La connexion est établie par l'intercepteur
  return await lastValueFrom(this.http.put<any>(`${this.apiUrlRecherche}/SupprimerFavoris/${favorisId}`, {}));
 
}
 
async ajouterEvaluation(evaluationDto: EvaluationDTO): Promise<Observable<any>> {
  const userId = this.getUserId(); // Récupérez l'ID de l'utilisateur
  console.log('ID utilisateur récupéré :', userId);
  if (!userId) {
    throw new Error('Utilisateur non authentifié.');
  }

  const evaluationDtoWithUserId = {
    ...evaluationDto,
    UserId: userId // Ajoutez l'ID de l'utilisateur
  };

  try {
    return await lastValueFrom(this.http.post<any>(`${this.apiUrlRecherche}/AjouterEvaluation`, evaluationDtoWithUserId));
  } catch (error) {
    console.error('Erreur lors de l\'appel à l\'API pour ajouter l\'évaluation :', error);
    throw error;
  }
}

async GetEvaluations(borneId: number) {
  //La connexion est établie par l'intercepteur
  return await lastValueFrom(this.http.get<any>(`${this.apiUrlRecherche}/GetEvaluations/${borneId}`));
}
 

 ajouterDisponibilite(disponibilite: DisponibiliteDTO): Observable<any> {
  return this.http.post<any>(`${this.apiUrlDespo}`, disponibilite);
}

async GetDisponibilite(borneId: number) {
  return await lastValueFrom(this.http.get<DisponibiliteDTO[]>(`${this.apiUrlDespo}/${borneId}`));
}
deleteDisponibilite(disponibiliteId: number): Promise<void> {
  return this.http.delete<void>(`${this.apiUrlDespo}/${disponibiliteId}`).toPromise();
}
updateDisponibilite(disponibiliteId: number, disponibiliteDTO: DisponibiliteDTO): Observable<void> {
  return this.http.put<void>(`${this.apiUrlDespo}/${disponibiliteId}`, disponibiliteDTO);
}

async getMyBorne() : Promise<BorneDTO[]>{
  
    return await lastValueFrom(this.http.get<BorneDTO[]>(`${this.apiUrlRecherche}/GetMyBornes`));
  }



  async supprimerBorne(borneId: number) {
    
    return await lastValueFrom(this.http.put<any>(`${this.apiUrlRecherche}/DeleteBorne/${borneId}`, {}));
   
  }
}