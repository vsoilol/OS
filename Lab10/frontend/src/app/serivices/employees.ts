import { api } from "./api";
import {Employee} from "../../models/Employee";

export const employeesApi = api.injectEndpoints({
  endpoints: (builder) => ({
    getAllEmployees: builder.query<Employee[], void>({
      query: () => ({
        url: "/employee",
        method: "GET",
      }),
    }),
    getEmployee: builder.query<Employee, number>({
      query: (id) => ({
        url: `/employee/${id}`,
        method: "GET",
      }),
    }),
    editEmployee: builder.mutation<void, Employee>({
      query: (employee) => ({
        url: `/employee`,
        method: "PUT",
        body: employee,
      }),
    }),
    removeEmployee: builder.mutation<void, number>({
      query: (id) => ({
        url: `/employee/${id}`,
        method: "DELETE",
        body: { id },
      }),
    }),
    addEmployee: builder.mutation<void, Employee>({
      query: (employee) => ({
        url: "/employee",
        method: "POST",
        body: employee,
      }),
    }),
  }),
});

export const {
  useGetAllEmployeesQuery,
  useGetEmployeeQuery,
  useEditEmployeeMutation,
  useRemoveEmployeeMutation,
  useAddEmployeeMutation,
} = employeesApi;

export const {
  endpoints: {
    getAllEmployees,
    getEmployee,
    editEmployee,
    removeEmployee,
    addEmployee,
  },
} = employeesApi;
