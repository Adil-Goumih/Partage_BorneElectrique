import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { BorneComponent } from './borne/borne.component';
import { HomeComponent } from './home/home.component';
import { InscriptionComponent } from './inscription/inscription.component';
import { ConnectionComponent } from './connection/connection.component';
import { RechercheComponent } from './Recherche/Recherche.component';
import { DetailsComponent } from './details/details.component';
import { FavorisComponent } from './favoris/favoris.component';
import { DisponibiliteComponent } from './disponibilite/disponibilite.component';
import { MaBorneComponent } from './ma-borne/ma-borne.component';
import { AfficheDisponibiliteComponent } from './afficheDisponibilite/afficheDisponibilite.component';



const routes: Routes = [
 
  {path:"", component:HomeComponent}, 
  {path:"home", component:HomeComponent}, 
  {path:"borne", component:BorneComponent}, 
  {path:"inscription", component:InscriptionComponent}, 
  {path:"connection", component:ConnectionComponent}, 
  {path:"recherche", component:RechercheComponent}, 
  { path: 'details/:borneId', component: DetailsComponent },
  { path: 'favoris', component: FavorisComponent },
  { path: 'disponibilite', component: DisponibiliteComponent },
  { path: 'ma-borne', component: MaBorneComponent },
  { path: 'disponibilite/:id', component: DisponibiliteComponent },
  { path: 'afficheDisponibilite', component: AfficheDisponibiliteComponent },
  { path: 'afficheDisponibilite/:id', component: AfficheDisponibiliteComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
  
 }
