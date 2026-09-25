import { useState, useEffect } from "react";
import type { Exercise } from "./types";
import ExerciseList from "./ExerciseList";

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
      <ExerciseList exercises={exercises} />
    </div>
  );
}

export default App;