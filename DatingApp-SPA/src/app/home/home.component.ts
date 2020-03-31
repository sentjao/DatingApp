import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  values: any;
  registerMode = false;
  constructor(private http: HttpClient) { }

  ngOnInit() {
  }


  registerToggle(){
    console.log('register mode true');
    this.registerMode = true;
  }

  cancelRegisterMode(value){
    console.log('register mode false');
    this.registerMode = value;
  }



}
