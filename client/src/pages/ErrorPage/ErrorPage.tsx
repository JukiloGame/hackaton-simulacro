import { useNavigate } from "react-router-dom";
//import "./ErrorPage.css";

export const ErrorPage = () => {
  const navigate = useNavigate();

  return (
    <section>
      <div className="flex flex-col justify-center align-middle text-center ">
        <h1 className="mb-2 text-8xl">404</h1>
        <p className="mb-4 text-2xl">Ups... parece que nos hemos desviado del sendero. Esta página no existe o se ha perdido entre los caminos.</p>
        <div className="gap-2 flex justify-center">
          <button onClick={() => navigate("/")} className="text-white border rounded error-btn text-base bg-blue-400 cursor-pointer transition-colors duration-300 hover:bg-blue-500">
            Volver al inicio
          </button>
        </div>
      </div>
    </section>
  );
};