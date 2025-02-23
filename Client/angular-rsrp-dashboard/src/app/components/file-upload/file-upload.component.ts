import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-file-upload',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatProgressBarModule],
  template: `
    <div class="file-upload-container">
      <div 
        class="drop-zone" 
        (dragover)="onDragOver($event)" 
        (dragleave)="onDragLeave($event)" 
        (drop)="onDrop($event)"
      >
        <p>Drag and drop your .xlsx file here</p>
        <p>or</p>
        <input type="file" (change)="onFileSelected($event)" accept=".xlsx" #fileInput>
        <button mat-raised-button color="primary" (click)="fileInput.click()">Select File</button>
      </div>
      <p *ngIf="selectedFile">Selected file: {{ selectedFile.name }}</p>
      <button mat-raised-button color="accent" (click)="onSubmit()" [disabled]="!selectedFile || processing">
        Process File
      </button>
      <mat-progress-bar *ngIf="processing" mode="indeterminate"></mat-progress-bar>
      <p *ngIf="processingMessage">{{ processingMessage }}</p>
      <button *ngIf="processingSuccessful" mat-raised-button color="primary" (click)="goToSummary()">
        Summary
      </button>
    </div>
  `,
  styles: [`
    .file-upload-container {
      max-width: 500px;
      margin: 20px auto;
      padding: 20px;
      border: 2px dashed #ccc;
      border-radius: 10px;
      text-align: center;
    }
    .drop-zone {
      padding: 20px;
      border: 2px dashed #aaa;
      border-radius: 5px;
      margin-bottom: 20px;
      cursor: pointer;
    }
    .drop-zone.dragover {
      background-color: #f0f0f0;
    }
    input[type="file"] {
      display: none;
    }
    button {
      margin-top: 10px;
    }
  `]
})

export class FileUploadComponent {
  selectedFile: File | null = null;
  processing = false;
  processingMessage = '';
  processingSuccessful = false;

  constructor(private http: HttpClient, private router: Router) {}

  onDragOver(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    if (event.target instanceof HTMLElement) {
      event.target.classList.add('dragover');
    }
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    if (event.target instanceof HTMLElement) {
      event.target.classList.remove('dragover');
    }
  }

  onDrop(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    if (event.target instanceof HTMLElement) {
      event.target.classList.remove('dragover');
    }
    const files = event.dataTransfer?.files;
    if (files && files.length > 0) {
      this.selectedFile = files[0];
    }
  }

  onFileSelected(event: Event) {
    const element = event.target as HTMLInputElement;
    const files = element.files;
    if (files && files.length > 0) {
      this.selectedFile = files[0];
    }
  }

  onSubmit() {
    if (!this.selectedFile) {
      this.processingMessage = 'Please select a file first.';
      return;
    }

    this.processing = true;
    this.processingMessage = 'Processing file...';
    this.processingSuccessful = false;

    // Simulating the process of saving the file locally and running the Python script
    setTimeout(() => {
      this.http.post('http://localhost:3000/process-file', { filename: this.selectedFile?.name })
        .subscribe(
          (response: any) => {
            this.processingMessage = response.message;
            this.processing = false;
            this.processingSuccessful = true; // Set to true on successful processing
          },
          (error) => {
            this.processingMessage = 'Error processing file. Please try again.';
            this.processing = false;
            this.processingSuccessful = false;
          }
        );
    }, 2000); // Simulating processing time
  }

  goToSummary() {
    this.router.navigate(['/summarize']);
  }
}