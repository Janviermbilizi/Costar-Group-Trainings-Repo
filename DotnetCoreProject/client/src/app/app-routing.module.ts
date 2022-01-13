import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberFinderComponent } from './members/member-finder/member-finder.component';
import { MemberProfileComponent } from './members/member-profile/member-profile.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: "", component: HomeComponent},
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      {path: "members", component: MemberFinderComponent},
      {path: "messages", component: MessagesComponent, canActivate: [AuthGuard]},
      {path: "members/:id", component: MemberProfileComponent, canActivate: [AuthGuard]},
    ]
  },
  {path: "**", component: HomeComponent, pathMatch: "full"},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
