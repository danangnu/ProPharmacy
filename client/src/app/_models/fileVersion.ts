import { Docs } from './docs';

export interface FileVersion {
    id: number;
    versionName: string;
    created: Date;
    documents: Docs[];
}
