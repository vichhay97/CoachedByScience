import { useState, useEffect } from "react";
import type { Exercise } from "./types";
import ExerciseForm from "./ExerciseForm";
import ExerciseList from "./ExerciseList";

function App() {
  const [exercises, setExercises] = useState<Exercise[]>([]);
  const [submitErrorMessage, setSubmitErrorMessage] = useState<string | null>(null);
  const [loadErrorMessage, setLoadErrorMessage] = useState<string | null>(null);

  useEffect(() => {
    async function loadExercises() {
      setLoadErrorMessage(null);
      const loadFailedMessage = "Couldn't load exercises, please try again.";
      try {
        const response = await fetch("http://localhost:5028/api/exercises");

        if (!response.ok) {
          console.error(response.status);
          setLoadErrorMessage(loadFailedMessage);
          return;
        }
        const data = await response.json();
        setExercises(data);
      } catch (error) {
        console.error("Failed to fetch exercises: ", error);
        setLoadErrorMessage(loadFailedMessage);
      }
    }

    loadExercises();

  }, []);

  async function handleAddExercise(name: string, description: string): Promise<boolean> {
    setSubmitErrorMessage(null);

    try {
      const response = await fetch("http://localhost:5028/api/exercises", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, description })
      });

      if (!response.ok) {
        if (response.status === 400) {
          const error: string = await response.json();
          console.error(response.status, error);
          setSubmitErrorMessage(error);
          return false;
        }
        else {
          console.error(response.status);
          setSubmitErrorMessage("Something went wrong on the server, please try again.");
          return false;
        }

      }

      const created: Exercise = await response.json();
      setExercises((prev) => [...prev, created]);
      return true;
    } catch (error) {
      console.error("Failed to add exercise: ", error);
      setSubmitErrorMessage("Couldn't reach server, please try again.");
      return false;
    }
  }

  return (
    <div>
      <h1>Exercises</h1>
      <ExerciseForm onAddExercise={handleAddExercise} />
      {submitErrorMessage && <p role="alert">{submitErrorMessage}</p>}
      <ExerciseList exercises={exercises} />
      {loadErrorMessage && <p role="alert">{loadErrorMessage}</p>}
    </div>
  );
}

export default App;