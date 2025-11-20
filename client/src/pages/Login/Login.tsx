import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { LoginForm } from "../../components/loginForm/loginForm";

export const Login = () => {
  const navigate = useNavigate();

  return (
    <div className="min-h-screen bg-gray-100 flex items-center justify-center p-6">
      <LoginForm />
    </div>
  );
};