import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  [x: string]: any;
  title = 'MonProjetPartage';
  userIsConnected : boolean = false;


  constructor(private router: Router) {}
  async ngOnInit() {

    this.userIsConnected = sessionStorage.getItem("token") != null;


  }
  logout() {
    // Perform logout actions (clear session, etc.)
    // For demonstration, simply navigate to home route
    this.router.navigate(['/']); // Navigate to the home route
    this.userIsConnected = false; // Update user connection status
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("connetionCourriel");


  }
}
