import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { type loginData } from "../../types/loginData";
import { useLogin } from "../../hooks/useLogin/useLogin";

export const LoginForm = () => {

  const [showPassword, setShowPassword] = useState(false);
    const [login, setLogin] = useState<loginData>();
    const [errorMsg, setErrorMsg] = useState<string>("");
    const { loginFunction, error } = useLogin();
    const navigate = useNavigate();
  
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
      const { name, value } = e.target;
      setLogin({...login,[name]: value});
    };

    const handleSubmit = async (e: React.FormEvent) => {
      e.preventDefault();
      await loginFunction(login);
      if(error){
        setErrorMsg(error);
      }
    }
  
    return (
      
        <div className="w-full max-w-sm bg-white p-6 rounded-xl shadow">
          <h2 className="text-2xl font-semibold text-center mb-6">
            Iniciar sesión
          </h2>
  
          <form className="space-y-4">
            <div>
              <label className="block text-sm font-medium mb-1">Correo</label>
              <input
                type="email"
                name="email"
                value={login?.email}
                onChange={e => handleChange(e)}
                placeholder="tu@ejemplo.com"
                className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:ring-indigo-300"
              />
            </div>
  
            <div>
              <label className="block text-sm font-medium mb-1">Contraseña</label>
  
              <div className="relative">
                <input
                  type={showPassword ? "text" : "password"}
                  name="password"
                  value={login?.password}
                  onChange={e => handleChange(e)}
                  placeholder="••••••••"
                  className="w-full border border-gray-300 rounded px-3 py-2 pr-12 focus:outline-none focus:ring focus:ring-indigo-300"
                />
  
                <button
                  type="button"
                  onClick={() => setShowPassword(!showPassword)}
                  className="absolute right-2 top-1/2 -translate-y-1/2 text-sm text-gray-500 hover:text-gray-700"
                >
                  {showPassword ? "Ocultar" : "Mostrar"}
                </button>
              </div>
            </div>
            
            <button
              type="submit"
              className="w-full bg-indigo-600 text-white py-2 rounded hover:bg-indigo-700 transition-colors"
              onClick={(e) => handleSubmit(e)}
            >
              Entrar
            </button>
            <p className="text-red-500 text-2xl mt-2">{errorMsg}</p>
          </form>
        </div>
    );
}