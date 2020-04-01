import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  decodedToken: any;
  constructor(private authService: AuthService, private alertify: AlertifyService) {}
  
  ngOnInit() {
  }

  login(){
    this.authService.login(this.model)
      .subscribe(next => {
       this.alertify.success('Logged in');
      },
      error => {
        this.alertify.error(error);
      });
  }

  loggedIn(){
    const loggedInres = this.authService.loggedIn();
    this.decodedToken = this.authService.decodedToken;
    return loggedInres;
  }

  logOut(){
    localStorage.removeItem('token');
    this.decodedToken = null;
    this.alertify.message('Logged out');
  }

}
