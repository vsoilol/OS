import {configureStore, ThunkAction, Action} from "@reduxjs/toolkit";
import {api} from "./serivices/api";
import employees from '../features/employees/employeesSlice'

export const store = configureStore({
    reducer: {
        [api.reducerPath]: api.reducer,
        employees
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(api.middleware),
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<ReturnType,
    RootState,
    unknown,
    Action<string>>;
