import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { RootLayout } from "./layouts/RootLayout";
import { ErrorPage } from "./pages/ErrorPage/ErrorPage";
import { Home } from "./pages/Home/Home";
import { Login } from "./pages/Login/Login";
import { AuthProvider } from "./context/AuthProvider";
//port About from "./pages/About";

 const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    errorElement: <ErrorPage />,
    children: [
      { index: true, element: <Home />},
      { path: "login", element: <Login /> },
    ],
  },
]); 


export default function App() {
  return (
    <AuthProvider>
      <RouterProvider router={router} />;
    </AuthProvider>
  )
}
