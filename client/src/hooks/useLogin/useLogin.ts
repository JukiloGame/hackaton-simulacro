import type { loginData } from './../../types/loginData';
import { useState } from "react";
import { childApiInstance } from "../../api/apiInstance";
import { useNavigate } from 'react-router-dom';

const URL = "user/login"

export const useLogin = () => {
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const loginFunction = async (loginData?: loginData) => {
    setError(null);
    try {
      await childApiInstance.post(URL, loginData);
      navigate(-1);
    } catch {
      setError("An error happened");
    }
  };

  return { loginFunction, error };
};