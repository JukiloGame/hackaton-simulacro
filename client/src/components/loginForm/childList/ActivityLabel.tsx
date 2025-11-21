import React, { useState } from "react";
import { useFetchData } from "../../../hooks/useFetchData/useFetchData";
import { childApiInstance } from "../../../api/apiInstance";

interface Activity {
  // Aceptamos varias formas (según el sample JSON del backend): `Id`/`id` y `Type`/`name`.
  id: number;
  type: string;
  description: string;
}

interface Props {
  childId: number;
}

const ActivityLabel = ({ childId }: Props): React.ReactElement => {
  const { data: activities, isLoading, error } = useFetchData<Activity[]>(
    "/activities"
  );
  const [selected, setSelected] = useState<number | null>(null);
  const [isAssigning, setIsAssigning] = useState(false);

//TODO: Llamar al endpoint del backend para asignar la actividad al niño
  const handleAssign = async () => {
    if (!selected) return;
    setIsAssigning(true);
    try {
      // Intentamos asignar la actividad al niño. Ajusta la ruta si tu API usa otra.
      await childApiInstance.post(`/children/${childId}/activities`, {
        activityId: selected,
      });
      console.log(`Actividad ${selected} asignada al niño ${childId}`);
      // Aquí podrías añadir un callback para actualizar la UI del padre.
    } catch (err) {
      console.error("No se pudo asignar la actividad", err);
    } finally {
      setIsAssigning(false);
    }
  };

  if (isLoading) return <span className="text-sm text-gray-500">Cargando...</span>;
  if (error !== null) return <span className="text-sm text-red-500">Error</span>;
  if (!activities || activities.length === 0)
    return <span className="text-sm text-gray-500">Sin actividades</span>;

  return (
    <div className="flex items-center gap-2">
      <label className="text-sm text-gray-600">Actividad:</label>
      <select
        value={selected ?? ""}
        onChange={(e) => setSelected(e.target.value === "" ? null : Number(e.target.value))}
        className="border rounded-md px-2 py-1 text-sm"
      >
        <option value="">Selecciona...</option>
        {activities.map((a) => {
          const id = a.id;
          const label = a.type ?? `Actividad ${id}`;
          return (
            <option key={id} value={id}>
              {label}
            </option>
          );
        })}
      </select>
//TODO: Modificar Model/Child para que incluya id de actividades asignadas y comentarios
      <button
        onClick={handleAssign}
        disabled={!selected || isAssigning}
        className="ml-2 bg-orange-500 text-white px-3 py-1 rounded-md text-sm disabled:opacity-50"
      >
        {isAssigning ? "Asignando..." : "Asignar"}
      </button>
    </div>
  );
};

export default ActivityLabel;
