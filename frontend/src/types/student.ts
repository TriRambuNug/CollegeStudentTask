export interface Student {
    id: string;
    namaDepan: string;
    namaBelakang?: string;
    tanggalLahir: string;
    usia: number;
}

export interface CreateStudentRequest {
    id: string;
    namaDepan: string;
    namaBelakang?: string;
    tanggalLahir: string;
}

export interface UpdateStudentRequest {
    id: string;
    namaDepan: string;
    namaBelakang?: string;
    tanggalLahir: string;
}
