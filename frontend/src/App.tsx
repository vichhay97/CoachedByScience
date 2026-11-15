import { useState, useEffect } from "react";
import type { Exercise } from "./types";
import ExerciseForm from "./ExerciseForm";
import ExerciseList from "./ExerciseList";

function App() {
  const [exercises, setExercises] = useState<Exercise[]>([]);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  useEffect(() => {
    fetch("http://localhost:5028/api/exercises")
      .then((response) => response.json())
      .then((data) => setExercises(data));
  }, []);

  async function handleAddExercise(name: string, description: string): Promise<boolean> {
    setErrorMessage(null);
    
    try {
      const response = await fetch("http://localhost:5028/api/exercises", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, description })
      });

      if (!response.ok) {
        if (response.status === 400)
        {
          const error: string = await response.json();
          console.error(response.status, error);
          setErrorMessage(error);
          return false;
        }
        else
        {
          console.error(response.status);
          setErrorMessage("Something went wrong on the server, please try again.");
          return false;
        }
        
      }

      const created: Exercise = await response.json();
      setExercises((prev) => [...prev, created]);
      return true;
    } catch (error) {
      console.error("Failed to add exercise: ", error);
      setErrorMessage("Couldn't reach server, please try again.");
      return false;
    }
  }

  return (
    <div>
      <h1>Exercises</h1>
      <ExerciseForm onAddExercise={handleAddExercise} />
      {errorMessage && <p role="alert">{errorMessage}</p>}
      <ExerciseList exercises={exercises} />
    </div>
  );
}

export default App;