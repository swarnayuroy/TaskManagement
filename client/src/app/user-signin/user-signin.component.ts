import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroupDirective, NgForm, Validators,FormGroup } from '@angular/forms';
import { TaskManagementService } from '../shared/task-management.service';
import { UserCredentialDTO } from '../shared/user-credential.model';
import { Router } from '@angular/router';


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

  public loginForm!: FormGroup;
  public hide : boolean=false;

  constructor(private service: TaskManagementService,
    private router:Router
  ) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
    emailFormControl: new FormControl('',[Validators.required,Validators.email]),
    passwordFormControl: new FormControl('',[Validators.required])
    })
  }

  onSubmit(form:FormGroup){
    let credential = new UserCredentialDTO();
    credential.Email = form.value['emailFormControl'];
    credential.Password = form.value['passwordFormControl'];
    this.service.checkCredential(credential).subscribe(response =>{
      console.log(response);
      if(response){
        this.router.navigate(['/taskDashboard']);
      }
    });
  }
  //matcher = new MyErrorStateMatcher();

  togglePassword() {
    this.hide = !this.hide;
  }
}
