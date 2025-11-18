import { ICITY } from './ICITY';
import { IWORKOUT } from './IWORKOUT';

export interface IUSER {
  id: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
  cities: ICITY[];
  workouts: IWORKOUT[];
}
