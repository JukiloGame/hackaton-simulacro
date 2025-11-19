import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { RootLayout } from "./layouts/RootLayout";
//port ErrorPage from "./pages/ErrorPage";
//port Home, { loader as homeLoader } from "./pages/Home";
//port About from "./pages/About";

/* const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    errorElement: <ErrorPage />,
    children: [
      { index: true, element: <Home />, loader: homeLoader },
      { path: "about", element: <About /> },
    ],
  },
]); */


export default function App() {
  //return <RouterProvider router={router} />;
  return (
    <h1 className="flex justify-evenly">Hello World</h1>
  );
}
