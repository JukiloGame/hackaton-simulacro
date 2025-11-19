import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { RootLayout } from "./layouts/RootLayout";
import { ErrorPage } from "./pages/ErrorPage/ErrorPage";
import { Home } from "./pages/Home/Home";
//port About from "./pages/About";

 const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    errorElement: <ErrorPage />,
    children: [
      { index: true, element: <Home />},
      //{ path: "about", element: <About /> },
    ],
  },
]); 


export default function App() {
  return <RouterProvider router={router} />;
  /* return (
    <h1 className="flex justify-evenly">Hello World</h1>
  ); */
}
