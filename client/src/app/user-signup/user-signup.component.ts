import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-signup',
  templateUrl: './user-signup.component.html',
  styleUrls: ['./user-signup.component.scss']
})
export class UserSignupComponent implements OnInit {
  signUpForm!: FormGroup;
  hide = true;

  constructor() { }

  ngOnInit(): void {

    this.signUpForm = new FormGroup({
    nameFormControl: new FormControl('',[Validators.required]),
    emailFormControl: new FormControl('',[Validators.required,Validators.email]),
    passwordFormControl: new FormControl('',[Validators.required, Validators.min(8)]),
    cPasswordFormControl: new FormControl('',[Validators.required])

    })
  }

  get passwordInput(){
    return this.signUpForm.get('passwordFormControl')!.value;
  }

  onSubmit(){
    console.log(this.signUpForm);
  }

}
