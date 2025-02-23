import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { provideCharts, withDefaultRegisterables } from 'ng2-charts';
import { AppComponent } from './app/app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter, Routes } from '@angular/router';
import { FileUploadComponent } from './app/components/file-upload/file-upload.component';
import { DashboardComponent } from './app/components/dashboard/dashboard.component';

// Define routes
const routes: Routes = [
  { path: '', component: FileUploadComponent }, // Home Page (Default)
  { path: 'summarize', component: DashboardComponent } // Summarize Page
];

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    provideCharts(withDefaultRegisterables()),
    provideAnimationsAsync(),
    provideRouter(routes) // Enable Routing
  ]
}).catch((err) => console.error(err));
