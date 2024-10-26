import { Injectable } from '@angular/core';
import { UserCredential } from './user-credential.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TaskManagementService {
  private readonly httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
  private readonly rootUrl: "https://localhost:44301/api";

  constructor(private http:HttpClient) { }

  checkCredential(userCredential: UserCredential){
    console.log("form API", userCredential);
    return this.http.post(this.rootUrl+'/checkcredential',userCredential,this.httpOptions);
  }
}
