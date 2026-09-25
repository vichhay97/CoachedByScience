import { useState, useEffect } from "react";

interface MuscleGroup {
  id: number;
  name: string;
}

interface Exercise {
  id: number;
  name: string;
  description: string;
  muscleGroups: MuscleGroup[];
}

function App() {
  const [exercises, setExercises] = useState<Exercise[]>([]);

  useEffect(() => {
    fetch("http://localhost:5028/api/exercises")
      .then((response) => response.json())
      .then((data) => setExercises(data));
  }, []);

  return (
    <div>
      <h1>Exercises</h1>
      <ul>
        {exercises.map((exercise) => (
          <li key={exercise.id}>
            <strong>{exercise.name}</strong>: {exercise.description}
            <br />
            <em>Targets: {exercise.muscleGroups.map((mg) => mg.name).join(", ")}</em>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;