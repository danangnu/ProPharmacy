import { Docs } from './docs';
import { PrescriptionSummary } from './prescriptionSummary';

export interface FileVersion {
    id: number;
    versionName: string;
    created: Date;
    documents: Docs[];
    prescSummary: PrescriptionSummary[];
}
