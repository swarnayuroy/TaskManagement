import { Injectable } from '@angular/core';
import { UserCredential } from './user-credential.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TaskManagementService {

  credential: UserCredential;

  readonly httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};

  readonly rootUrl: "https://localhost:44301/api";

  constructor(private http:HttpClient) { }

  checkCredential(credential: UserCredential){
    return this.http.post(this.rootUrl+'/checkcredential',credential,this.httpOptions);
  }
}
