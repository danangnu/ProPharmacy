import { FileVersion } from './fileVersion';

export interface User {
    id: number;
    email: string;
    lastName: string;
    firstName: string;
    versionCreated: FileVersion[];
}