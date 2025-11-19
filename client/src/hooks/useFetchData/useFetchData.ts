import { useEffect, useState } from "react";
import { childApiInstance } from "../../api/apiInstance";

export const useFetchData = <T>(url: string) => {
  const [data, setData] = useState<T | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await childApiInstance.get(url);
        setData(response.data);
      } catch {
        setError("An error happened");
      } finally {
        setIsLoading(false);
      }
    };
    fetchData();
  }, [url]);

  return { data, isLoading, error };
};