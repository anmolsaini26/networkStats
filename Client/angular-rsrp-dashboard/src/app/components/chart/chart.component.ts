import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseChartDirective } from 'ng2-charts';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';
import { VersionStats } from '../../models/network-stats.model';

@Component({
  selector: 'app-chart',
  standalone: true,
  imports: [CommonModule, BaseChartDirective],
  template: `
    <div class="chart-container">
      <canvas baseChart
        [data]="barChartData"
        [options]="barChartOptions"
        [type]="barChartType">
      </canvas>
    </div>
  `,
  styles: [`
    .chart-container {
      display: block;
      width: 100%;
      height: 100%;
    }
  `]
})
export class ChartComponent implements OnInit {
  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    scales: {
      x: {},
      y: { min: 0 }
    },
    plugins: {
      legend: { display: true }
    }
  };
  public barChartType: ChartType = 'bar';
  public barChartData: ChartData<'bar'> = {
    labels: [],
    datasets: []
  };

  constructor(@Inject(MAT_DIALOG_DATA) public data: VersionStats) {}

  ngOnInit(): void {
    this.prepareChartData();
  }

  prepareChartData() {
    const labels: string[] = [];
    const poorSNRData: number[] = [];
    const goodSNRData: number[] = [];
    const veryGoodSNRData: number[] = [];

    Object.entries(this.data.rsrp).forEach(([rsrp, details]) => {
      labels.push(rsrp);
      poorSNRData.push(details.snr['Poor SNR'] || 0);
      goodSNRData.push(details.snr['Good SNR'] || 0);
      veryGoodSNRData.push(details.snr['Very Good SNR'] || 0);
    });

    this.barChartData = {
      labels: labels,
      datasets: [
        { data: poorSNRData, label: 'Poor SNR' },
        { data: goodSNRData, label: 'Good SNR' },
        { data: veryGoodSNRData, label: 'Very Good SNR' }
      ]
    };
  }
}
