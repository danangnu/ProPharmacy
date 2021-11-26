import { UserReport } from './userReport';

export interface User {
  id: number;
  email: string;
  name: string;
  reportCreated: UserReport[];
}
