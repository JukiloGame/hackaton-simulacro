import { useFetchData } from "../../../hooks/useFetchData/useFetchData";
import LogoNexe from "../../../assets/LogoNexe.jpg";

interface ChildDetails {
  id: number;
  name: string;
  dob: string;
}

export const ChildList = (): React.ReactElement => {
  const { data, isLoading, error } = useFetchData<ChildDetails[]>("/children");
  console.log(data);
  const handleAddChild = () => {
    console.log("Añadir niño: ");
  };
  const handleSelectedChild = (id: number) => {
    console.log("Ver ficha del niño con id: ", id);
  };

  if (isLoading) {
    return (
      <div className="w-full justify-center pu-20 text-gray-600">
        Loading employee...
      </div>
    );
  }

  if (error !== null) {
    return (
      <div className="w-full flex justify-center py-20 text-red-500">
        An error happened
      </div>
    );
  }

  if (!data) {
    return (
      <div className="w-full flex justify-center py-20 text-gray-600">
        Data could not be found
      </div>
    );
  }

  return (
    <div className="w-full px-6 py-8">
      {/* Header */}
      <div className="flex justify-between items-center mb-8">
        <div className="flex items-center gap-3">
          <img
            src={LogoNexe}
            alt="Nexe logo"
            className="h-10 w-10 rounded-full object-cover"
          />
          <h1 className="text-3xl font-semibold text-gray-900">Mis niños</h1>
        </div>

        <button
          onClick={handleAddChild}
          className="flex items-center gap-2 bg-orange-500 text-white px-4 py-2 rounded-lg hover:bg-orange-600 transition"
        >
          <img src={LogoNexe} alt="icon add" className="h-5 w-5 rounded-full" />
          Añadir niño
        </button>
      </div>

      {/* Child list */}
      <div className="flex flex-col gap-4">
        {data?.map((child) => (
          <div
            key={child.id}
            onClick={() => handleSelectedChild(child?.id)}
            className="cursor-pointer border rounded-xl p-4 shadow-sm hover:shadow-md bg-white transition flex justify-between items-center"
          >
            <div>
              <h3 className="text-lg font-medium text-gray-900">
                {child?.name}
              </h3>

              <p className="text-sm text-gray-500">
                Fecha de nacimiento: {""}
                {child?.dob
                  ? new Date(child.dob).toLocaleDateString()
                  : "Sin fecha"}
              </p>
            </div>

            {/* Flecha de navegación -> reemplazada por mini logo */}
            <img
              src={LogoNexe}
              alt="ver ficha"
              className="h-6 w-6 rounded-full opacity-60"
            />
          </div>
        ))}

        {/* Bottom add button */}
        <button
          onClick={handleAddChild}
          className="flex items-center justify-center gap-2 border-2 border-orange-500 text-orange-600 py-3 rounded-lg hover:bg-orange-50 transition"
        >
          <img src={LogoNexe} alt="add" className="h-5 w-5 rounded-full" />
          Añadir niño
        </button>
      </div>
    </div>
  );
};
