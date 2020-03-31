import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

 @Output() cancelOutput = new EventEmitter();

  constructor(private authService: AuthService) { }

  model: any = {};
  ngOnInit() {
  }
  register(){
    this.authService.register(this.model).subscribe(() => {
      console.log('regitration successful');
      }, error => {
        console.error(error);
      }
    );
  }
  cancel() {
    this.cancelOutput.emit(false);
  }

}
