import { useCallback, useEffect, useState } from 'react';
import toast, { Toaster } from 'react-hot-toast';
import './App.css';
import ConfirmDialog from './components/ConfirmDialog';
import StudentForm from './components/StudentForm';
import StudentList from './components/StudentList';
import { studentService } from './services/studentService';
import type { Student, CreateStudentRequest, UpdateStudentRequest } from './types/student';

function App() {
    const [students, setStudents] = useState<Student[]>([]);
    const [isListLoading, setIsListLoading] = useState(true);
    const [isFormLoading, setIsFormLoading] = useState(false);
    const [showForm, setShowForm] = useState(false);
    const [editingStudent, setEditingStudent] = useState<Student | null>(null);
    const [deletingStudent, setDeletingStudent] = useState<Student | null>(null);

    const loadStudents = useCallback(async () => {
        setIsListLoading(true);
        try {
            const data = await studentService.getAll();
            setStudents(data);
        } catch {
            toast.error('Gagal memuat data mahasiswa.');
        } finally {
            setIsListLoading(false);
        }
    }, []);

    useEffect(() => { loadStudents(); }, [loadStudents]);

    const handleOpenCreate = () => {
        setEditingStudent(null);
        setShowForm(true);
    };

    const handleOpenEdit = (student: Student) => {
        setEditingStudent(student);
        setShowForm(true);
    };

    const handleCloseForm = () => {
        setShowForm(false);
        setEditingStudent(null);
    };

    const handleSubmit = async (data: CreateStudentRequest | UpdateStudentRequest) => {
        setIsFormLoading(true);
        try {
            if (editingStudent) {
                await studentService.update(editingStudent.id, data as UpdateStudentRequest);
                toast.success('Data mahasiswa berhasil diperbarui.');
            } else {
                await studentService.create(data as CreateStudentRequest);
                toast.success('Mahasiswa berhasil ditambahkan.');
            }
            handleCloseForm();
            await loadStudents();
        } catch (err: unknown) {
            const msg = (err as { response?: { data?: { message?: string } } })
                ?.response?.data?.message;
            toast.error(msg ?? 'Terjadi kesalahan. Silakan coba lagi.');
        } finally {
            setIsFormLoading(false);
        }
    };

    const handleDeleteRequest = (student: Student) => {
        setDeletingStudent(student);
    };

    const handleDeleteConfirm = async () => {
        if (!deletingStudent) return;
        try {
            await studentService.remove(deletingStudent.id);
            toast.success(`Mahasiswa ${deletingStudent.namaDepan} berhasil dihapus.`);
            setDeletingStudent(null);
            await loadStudents();
        } catch {
            toast.error('Gagal menghapus data mahasiswa.');
        }
    };

    return (
        <div className="app">
            <Toaster position="top-right" />

            <header className="app-header">
                <div className="header-content">
                    <h1>Data Mahasiswa</h1>
                    <button className="btn btn-primary" onClick={handleOpenCreate}>
                        + Tambah Mahasiswa
                    </button>
                </div>
            </header>

            <main className="app-main">
                <div className="card">
                    <div className="card-header">
                        <span className="student-count">
                            Total: <strong>{students.length}</strong> mahasiswa
                        </span>
                    </div>
                    <StudentList
                        students={students}
                        isLoading={isListLoading}
                        onEdit={handleOpenEdit}
                        onDelete={handleDeleteRequest}
                    />
                </div>
            </main>

            {showForm && (
                <StudentForm
                    key={editingStudent?.id ?? 'create'}
                    editingStudent={editingStudent}
                    onSubmit={handleSubmit}
                    onCancel={handleCloseForm}
                    isLoading={isFormLoading}
                />
            )}

            <ConfirmDialog
                isOpen={!!deletingStudent}
                title="Hapus Mahasiswa"
                message={`Apakah Anda yakin ingin menghapus data ${deletingStudent?.namaDepan ?? ''}? Tindakan ini tidak dapat dibatalkan.`}
                onConfirm={handleDeleteConfirm}
                onCancel={() => setDeletingStudent(null)}
            />
        </div>
    );
}

export default App;
