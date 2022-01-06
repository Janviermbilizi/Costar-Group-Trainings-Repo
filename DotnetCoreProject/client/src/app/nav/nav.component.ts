import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  loggedIn: boolean;
  user: any = {};

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.model).subscribe(res => {
      this.loggedIn = true;
      this.user = res;
      console.log(`Succefully logged in, ${this.user.currentUser}!`);
    }, error => {
      console.log(error);
    })
  }

  logout(){
    this.loggedIn = false;
    this.user = null;
    console.log("Succefully logged out!")
  }
}
