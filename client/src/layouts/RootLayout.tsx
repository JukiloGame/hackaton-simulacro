import { Outlet, Link } from "react-router-dom";

export function RootLayout() {
  return (
    <div>
      <nav>
        <Link to="/">Inicio</Link> | <Link to="/about">Sobre nosotros</Link>
      </nav>
      <main>
        <Outlet /> {/* Renderiza la ruta hija */}
      </main>
    </div>
  );
}
