import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BorneComponent } from './borne/borne.component';
import { HomeComponent } from './home/home.component';
import { InscriptionComponent } from './inscription/inscription.component';
import { ConnectionComponent } from './connection/connection.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { RechercheComponent } from './Recherche/Recherche.component';
import { DetailsComponent } from './details/details.component';
import { FavorisComponent } from './favoris/favoris.component';
import { AuthInterceptor } from './auth.interceptor';
import { EtoileComponent } from './Etoile/Etoile.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { DisponibiliteComponent } from './disponibilite/disponibilite.component';
import { MaBorneComponent } from './ma-borne/ma-borne.component';
import { AfficheDisponibiliteComponent } from './afficheDisponibilite/afficheDisponibilite.component';


@NgModule({
  declarations: [										
    AppComponent,
    BorneComponent,
      HomeComponent,
      InscriptionComponent,
      ConnectionComponent,
      RechercheComponent,
      DetailsComponent,
      FavorisComponent,
      EtoileComponent,
      DisponibiliteComponent,
      MaBorneComponent,
      AfficheDisponibiliteComponent
   ],
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCardModule,
    AppRoutingModule,

    FormsModule,
    
    GoogleMapsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true}
    ],
    bootstrap: [AppComponent]
  })
  export class AppModule { }