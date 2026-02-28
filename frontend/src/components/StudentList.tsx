import type { Student } from '../types/student';

interface StudentListProps {
    readonly students: ReadonlyArray<Student>;
    readonly isLoading: boolean;
    readonly onEdit: (student: Student) => void;
    readonly onDelete: (student: Student) => void;
}

const namaLengkap = (s: Student): string =>
    s.namaBelakang ? `${s.namaDepan} ${s.namaBelakang}` : s.namaDepan;

const StudentList = ({ students, isLoading, onEdit, onDelete }: StudentListProps) => {
    if (isLoading) {
        return (
            <div className="state-box">
                <div className="spinner" />
                <p>Memuat data...</p>
            </div>
        );
    }

    if (students.length === 0) {
        return (
            <div className="state-box empty">
                <svg xmlns="http://www.w3.org/2000/svg" width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round"><path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M23 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/></svg>
                <p>Belum ada data mahasiswa.</p>
            </div>
        );
    }

    return (
        <div className="table-wrapper">
            <table className="student-table">
                <thead>
                    <tr>
                        <th>NIM</th>
                        <th>Nama Lengkap</th>
                        <th>Usia</th>
                        <th>Aksi</th>
                    </tr>
                </thead>
                <tbody>
                    {students.map(s => (
                        <tr key={s.id}>
                            <td className="td-nim">{s.id}</td>
                            <td>{namaLengkap(s)}</td>
                            <td>{s.usia} tahun</td>
                            <td className="td-actions">
                                <button
                                    className="btn btn-sm btn-edit"
                                    onClick={() => onEdit(s)}
                                    title="Edit"
                                >
                                    Edit
                                </button>
                                <button
                                    className="btn btn-sm btn-danger"
                                    onClick={() => onDelete(s)}
                                    title="Hapus"
                                >
                                    Hapus
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default StudentList;
