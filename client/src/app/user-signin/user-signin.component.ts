import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroupDirective, NgForm, Validators,FormGroup } from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';



@Component({
  selector: 'app-user-signin',
  templateUrl: './user-signin.component.html',
  styleUrls: ['./user-signin.component.scss'],
})

// export class MyErrorStateMatcher implements ErrorStateMatcher {
// emailFormControl: any;
// matcher: any;
// loginForm!: FormGroup;
//   isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
//     const isSubmitted = form && form.submitted;
//     return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
//   }
// }
export class UserSigninComponent {

  loginForm!: FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
    emailFormControl: new FormControl('',[Validators.required,Validators.email]),
    password: new FormControl('',[Validators.required])
    })
  }

  onSubmit(){
    console.log(this.loginForm);
  }
  //matcher = new MyErrorStateMatcher();

}
