import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroupDirective, NgForm, Validators,FormGroup } from '@angular/forms';
import { TaskManagementService } from '../shared/task-management.service';
import { UserCredential } from '../shared/user-credential.model';


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

  constructor(public service: TaskManagementService) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
    emailFormControl: new FormControl('',[Validators.required,Validators.email]),
    passwordFormControl: new FormControl('',[Validators.required])
    })
  }

  onSubmit(form:FormGroup){
    //console.log(this.loginForm);
    let credential = new UserCredential();
    credential.email = form.value['emailFormControl'];
    credential.password = form.value['passwordFormControl'];

    this.service.checkCredential(credential);
  }
  //matcher = new MyErrorStateMatcher();

}
