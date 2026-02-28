import React, { useState } from 'react';

import type { Student, CreateStudentRequest, UpdateStudentRequest } from '../types/student';

interface StudentFormProps {
    editingStudent: Student | null;
    onSubmit: (data: CreateStudentRequest | UpdateStudentRequest) => Promise<void>;
    onCancel: () => void;
    isLoading: boolean;
}

const emptyForm = {
    id: '',
    namaDepan: '',
    namaBelakang: '',
    tanggalLahir: '',
};

const getInitialForm = (editingStudent: Student | null) => {
    if (!editingStudent) return emptyForm;
    return {
        id: editingStudent.id,
        namaDepan: editingStudent.namaDepan,
        namaBelakang: editingStudent.namaBelakang ?? '',
        tanggalLahir: editingStudent.tanggalLahir.split('T')[0],
    };
};

const StudentForm: React.FC<StudentFormProps> = ({
    editingStudent,
    onSubmit,
    onCancel,
    isLoading,
}) => {
    const [form, setForm] = useState(() => getInitialForm(editingStudent));
    const [errors, setErrors] = useState<Partial<typeof emptyForm>>({});

    const validate = (): boolean => {
        const newErrors: Partial<typeof emptyForm> = {};
        if (!form.id.trim()) newErrors.id = 'NIM harus diisi.';
        else if (form.id.length < 10 || form.id.length > 20)
            newErrors.id = 'NIM harus 10–20 karakter.';
        if (!form.namaDepan.trim()) newErrors.namaDepan = 'Nama depan harus diisi.';
        if (!form.tanggalLahir) newErrors.tanggalLahir = 'Tanggal lahir harus diisi.';
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setForm(prev => ({ ...prev, [name]: value }));
        if (errors[name as keyof typeof emptyForm]) {
            setErrors(prev => ({ ...prev, [name]: undefined }));
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!validate()) return;
        await onSubmit({
            id: form.id.trim(),
            namaDepan: form.namaDepan.trim(),
            namaBelakang: form.namaBelakang.trim() || undefined,
            tanggalLahir: form.tanggalLahir,
        });
    };

    const isEdit = !!editingStudent;

    return (
        <div className="form-overlay" onClick={onCancel}>
            <div className="form-box" onClick={e => e.stopPropagation()}>
                <h2 className="form-title">
                    {isEdit ? 'Edit Data Mahasiswa' : 'Tambah Mahasiswa Baru'}
                </h2>
                <form onSubmit={handleSubmit} noValidate>
                    <div className="form-group">
                        <label htmlFor="id">NIM</label>
                        <input
                            id="id"
                            name="id"
                            type="text"
                            placeholder="Nomor Induk Mahasiswa"
                            value={form.id}
                            onChange={handleChange}
                            className={errors.id ? 'input-error' : ''}
                            maxLength={20}
                        />
                        {errors.id && <span className="error-msg">{errors.id}</span>}
                    </div>

                    <div className="form-group">
                        <label htmlFor="namaDepan">Nama Depan</label>
                        <input
                            id="namaDepan"
                            name="namaDepan"
                            type="text"
                            placeholder="Nama depan"
                            value={form.namaDepan}
                            onChange={handleChange}
                            className={errors.namaDepan ? 'input-error' : ''}
                            maxLength={100}
                        />
                        {errors.namaDepan && <span className="error-msg">{errors.namaDepan}</span>}
                    </div>

                    <div className="form-group">
                        <label htmlFor="namaBelakang">
                            Nama Belakang <span className="optional">(opsional)</span>
                        </label>
                        <input
                            id="namaBelakang"
                            name="namaBelakang"
                            type="text"
                            placeholder="Nama belakang"
                            value={form.namaBelakang}
                            onChange={handleChange}
                            maxLength={100}
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="tanggalLahir">Tanggal Lahir</label>
                        <input
                            id="tanggalLahir"
                            name="tanggalLahir"
                            type="date"
                            value={form.tanggalLahir}
                            onChange={handleChange}
                            className={errors.tanggalLahir ? 'input-error' : ''}
                        />
                        {errors.tanggalLahir && <span className="error-msg">{errors.tanggalLahir}</span>}
                    </div>

                    <div className="form-actions">
                        <button
                            type="button"
                            className="btn btn-secondary"
                            onClick={onCancel}
                            disabled={isLoading}
                        >
                            Batal
                        </button>
                        <button
                            type="submit"
                            className="btn btn-primary"
                            disabled={isLoading}
                        >
                            {isLoading ? 'Menyimpan...' : isEdit ? 'Simpan Perubahan' : 'Tambah'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default StudentForm;
