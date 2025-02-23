import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './components/nav-bar/nav-bar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, NavbarComponent],
  template: `
    <app-navbar></app-navbar> <!-- Navbar Always Present -->
    <router-outlet></router-outlet> <!-- Dynamic Content -->
  `
})
export class AppComponent { }
