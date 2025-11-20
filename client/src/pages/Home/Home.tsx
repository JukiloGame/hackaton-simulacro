import { useEffect } from "react";
import { useFetchData } from "../../hooks/useFetchData/useFetchData";
import { ChildList } from "../../components/loginForm/childList/childList";

export const Home = (): React.ReactElement => {
  return (
    <div>
      <ChildList />
    </div>
  );
};
