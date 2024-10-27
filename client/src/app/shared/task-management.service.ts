import { Injectable } from '@angular/core';
import { UserCredentialDTO } from './user-credential.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class TaskManagementService {

  constructor(private http:HttpClient) { }

  checkCredential(userCredential: UserCredentialDTO){
    return this.http.post(`/api/checkcredential`, userCredential);
  }
}
