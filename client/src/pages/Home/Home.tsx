import { useEffect } from "react"
import { useFetchData } from "../../hooks/useFetchData/useFetchData";

interface ChildDetails {
  id: number;
  name: string;
  Dob: Date;
}

export const Home  = () : React.ReactElement => {
  const { data, isLoading, error } = useFetchData<ChildDetails[]>("/children");

  if (isLoading) {
    return <div>Loading employee...</div>;
  }

  if (error !== null) {
    return <div>An error happened</div>;
  }

  if (!data) {
    return <div>Data could not be found</div>;
  }

  
  

  return (
    <div>
      {data?.map(child => (
        <h1 key={child.id}>{child.name}</h1>
      ))}
    </div>
  )
}