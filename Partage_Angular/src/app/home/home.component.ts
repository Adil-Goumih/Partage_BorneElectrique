import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',

  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userIsConnected : boolean = false;
  constructor() { }

  async ngOnInit() {

    this.userIsConnected = sessionStorage.getItem("token") != null;

  }


}
