import { useState, useEffect } from "react";
import type { Exercise } from "./types";
import ExerciseForm from "./ExerciseForm";
import ExerciseList from "./ExerciseList";

function App() {
  const [exercises, setExercises] = useState<Exercise[]>([]);

  useEffect(() => {
    fetch("http://localhost:5028/api/exercises")
      .then((response) => response.json())
      .then((data) => setExercises(data));
  }, []);

  async function handleAddExercise(name: string, description: string) {
    const response = await fetch("http://localhost:5028/api/exercises", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ name, description })
    });

    if (!response.ok) {
      const error = await response.text();
      console.error(response.status, error);
      return;
    }
    
    const created: Exercise = await response.json();
    setExercises((prev) => [...prev, created]);
  }

  return (
    <div>
      <h1>Exercises</h1>
      <ExerciseForm onAddExercise={handleAddExercise} />
      <ExerciseList exercises={exercises} />
    </div>
  );
}

export default App;