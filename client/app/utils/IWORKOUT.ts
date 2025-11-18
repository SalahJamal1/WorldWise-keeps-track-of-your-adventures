export interface IWORKOUT {
  id?: number;
  type: string | number;
  date: Date;
  distance: number | string;
  duration: number | string;
  pace: number;
  cadence: number | string;
  cityName: string;
  emoji: string;
  coords: [number, number];
}
