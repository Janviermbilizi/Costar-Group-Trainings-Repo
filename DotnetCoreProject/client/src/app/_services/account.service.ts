import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = "https://localhost:5001/api/";
  //declare an observable
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable()

  constructor(private http: HttpClient) { }

  login(model: any){
    return this.http.post(this.baseUrl + "account/login", model).pipe(
      map((res: User) => {
        const user = res;
        if(user){
          localStorage.setItem("user", JSON.stringify(user));
          console.log(user);
          //set Observable
          this.currentUserSource.next(user);
        }
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
}