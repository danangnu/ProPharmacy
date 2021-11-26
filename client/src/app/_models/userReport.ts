import { FileVersion } from './fileVersion';

export interface UserReport {
  id: number;
  reportName: string;
  created: Date;
  filesVersion: FileVersion[];
}
