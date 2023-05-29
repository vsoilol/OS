import { createSlice } from "@reduxjs/toolkit";
import { employeesApi } from "../../app/serivices/employees";
import { RootState } from "../../app/store";
import {Employee} from "../../models/Employee";

interface InitialState {
  employees: Employee[] | null;
}

const initialState: InitialState = {
  employees: null,
};

const slice = createSlice({
  name: "todo",
  initialState,
  reducers: {
    logout: () => initialState,
  },
});

export default slice.reducer;

export const selectEmployees = (state: RootState) =>
  state.employees.employees;
