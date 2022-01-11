import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = "https://localhost:5001/api/account/";
  //declare an observable
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable()

  constructor(private http: HttpClient) { }

  login(model: any){
    return this.http.post(this.baseUrl + "login", model).pipe(
      map((res: User) => {
        this.setUserLocally(res);
      })
    )
  }

  //
  register(model: any){
    return this.http.post(this.baseUrl + "register", model).pipe(
      map((res: User) => {
        this.setUserLocally(res);
      })
    )
  }

  //observable
  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem("user");
    this.currentUserSource.next(null);
  }

  setUserLocally(userdata: any){
    if(userdata){
      localStorage.setItem("user", JSON.stringify(userdata));
      //set Observable
      this.currentUserSource.next(userdata);
    }
  }
}