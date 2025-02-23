export interface NetworkStatsDto {
    isRAT: Record<string, VersionStats>;
  }
  
  export interface VersionStats {
    rsrp: Record<string, SignalQualityStats>;
    grandTotal: number;
  }
  
  export interface SignalQualityStats {
    snr: Record<string, number>;
  }
  