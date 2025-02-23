import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NetworkStatsDto } from '../models/network-stats.model';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrlLTE = 'http://localhost:5220/api/network_stats?is_lte=1'; 
  private apiUrlNR = 'http://localhost:5220/api/network_stats?is_nr_5g=1'; 

  constructor(private http: HttpClient) {}

  getDataLte(): Observable<NetworkStatsDto> {
    return this.http.get<NetworkStatsDto>(this.apiUrlLTE);
  }
  getDataNR(): Observable<NetworkStatsDto> {
    return this.http.get<NetworkStatsDto>(this.apiUrlNR);
  }
}
