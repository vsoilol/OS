import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import {Paths} from "./paths";
import {Employees} from "./pages/employees";
import {EditEmployee} from "./pages/edit-employee";
import {Employee} from "./pages/employee";
import {AddEmployee} from "./pages/add-employee";
import {Status} from "./pages/status";
import {Provider} from "react-redux";
import {store} from "./app/store";
import {ConfigProvider, theme} from "antd";
import "./index.css";

const router = createBrowserRouter([
    {
        path: Paths.home,
        element: <Employees/>,
    },
    {
        path: Paths.employeeAdd,
        element: <AddEmployee/>,
    },
    {
        path: `${Paths.employee}/:id`,
        element: <Employee/>,
    },
    {
        path: `${Paths.employeeEdit}/:id`,
        element: <EditEmployee/>,
    },
    {
        path: `${Paths.status}/:status`,
        element: <Status/>,
    },
]);

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);
root.render(
    <Provider store={store}>
        <ConfigProvider
            theme={{
                algorithm: theme.darkAlgorithm,
            }}
        >
            <RouterProvider router={router}/>
        </ConfigProvider>
    </Provider>
);
