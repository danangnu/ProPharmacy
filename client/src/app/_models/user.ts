import { FileVersion } from './fileVersion';

export interface User {
    id: number;
    email: string;
    name: string;
    versionCreated: FileVersion[];
}
