import { useState } from "react";
import "./ExerciseForm.css";

interface ExerciseFormProps {
    onAddExercise: (name: string, description: string) => Promise<boolean>;
}

function ExerciseForm({ onAddExercise }: ExerciseFormProps) {
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");

    return (
        <form className="exercise-form" onSubmit={async (e) => {
            e.preventDefault();
            const success = await onAddExercise(name, description);
            if (success) {
                setName("");
                setDescription("");
            }
        }}
        >
            <input placeholder="Exercise Name" value={name} onChange={(e) => setName(e.target.value)} />
            <textarea placeholder="Description" value={description} onChange={(e) => setDescription(e.target.value)} />
            <button type="submit">Submit</button>
        </form>
    );
}

export default ExerciseForm;