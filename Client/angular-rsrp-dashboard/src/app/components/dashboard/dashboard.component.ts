import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from '../../services/data.service';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { NetworkStatsDto, VersionStats } from '../../models/network-stats.model';
import { ChartComponent } from '../chart/chart.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatDialogModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  dataLte: NetworkStatsDto | null = null;  // 4G Data
  dataNr: NetworkStatsDto | null = null;   // 5G Data
  displayedColumns: string[] = ['rsrp', 'poorSNR', 'goodSNR', 'veryGoodSNR', 'total'];

  constructor(private dataService: DataService, private dialog: MatDialog) {}

  ngOnInit(): void {
    // Fetch 4G data
    this.dataService.getDataLte().subscribe(response => {
      this.dataLte = response;
    });

    // Fetch 5G data
    this.dataService.getDataNR().subscribe(response => {
      this.dataNr = response;
    });
  }

  getRsrpEntries(device: VersionStats) {
    return Object.entries(device.rsrp).map(([rsrp, details]) => ({
      rsrp,
      poorSNR: details.snr['Poor SNR'] || 0,
      goodSNR: details.snr['Good SNR'] || 0,
      veryGoodSNR: details.snr['Very Good SNR'] || 0,
      total: details.snr['total'] || 0
    }));
  }

  openChart(device: VersionStats) {
    this.dialog.open(ChartComponent, {
      data: device,
      width: '600px',
      height: '400px'
    });
  }
}
