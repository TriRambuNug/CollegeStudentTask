import axios from "axios";

import type { Student, CreateStudentRequest, UpdateStudentRequest } from "../types/student";

const BASE_API = 'http://localhost:5185/api/student';
const api = axios.create({ baseURL: BASE_API });

export const studentService = {
    getAll : (): Promise<Student[]> => 
        api.get<Student[]>('').then((r) => r.data),

    /** GET /api/students/:id */
  getById: (id: string): Promise<Student> =>
    api.get<Student>(`/${encodeURIComponent(id)}`).then((r) => r.data),

  /** POST /api/students */
  create: (data: CreateStudentRequest): Promise<Student> =>
    api.post<Student>('', data).then((r) => r.data),

  /** PUT /api/students/:id */
  update: (id: string, data: UpdateStudentRequest): Promise<Student> =>
    api.put<Student>(`/${encodeURIComponent(id)}`, data).then((r) => r.data),

  /** DELETE /api/students/:id */
  remove: (id: string): Promise<void> =>
    api.delete(`/${encodeURIComponent(id)}`).then(() => undefined),
};