export interface RatePoint {
  date: string;
  rate: number;
}
export interface RatesResponse {
  provider: string;
  source: string;
  target: string;
  from: string;
  to: string;
  min: number;
  max: number;
  avg: number;
  points: RatePoint[];
}
