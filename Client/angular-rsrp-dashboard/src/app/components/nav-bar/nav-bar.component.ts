// navbar.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    RouterModule,
  ],
  template: `
    <mat-toolbar class="navbar">
      <img src="assets/img/Oppo-Logo.png" alt="Oppo Logo" class="logo" />
      <span class="spacer"></span>
      <h1 class="title">BIG DATA ANALYSIS: InterRAT Transitions</h1>
      <span class="spacer"></span>
      <button mat-icon-button routerLink="/" class="nav-button">
        <mat-icon>home</mat-icon>
      </button>
      <button mat-button routerLink="/summarize" class="nav-button">
        Summarize
      </button>
      <button mat-button routerLink="/compare" class="nav-button">
        Compare
      </button>
    </mat-toolbar>
  `,
  styles: [
    `
      .navbar {
        background-color: #002d00; /* Dark olive green */
        color: white;
        display: flex;
        align-items: center;
        padding: 0 16px;
      }
      .logo {
        height: 75px;
        margin-right: 16px;
      }
      .title {
       
        font-size: 1.6rem;
        text-align: center;
        white-space: normal;
        line-height: 1.2;
      }
      .spacer {
        flex: 1 1 auto;
      }
      .nav-button {
        color: white !important;
        font-weight: bold;
      }
      .nav-button:hover {
        background-color: rgba(255, 255, 255, 0.1);
      }
    `,
  ],
})
export class NavbarComponent {}
