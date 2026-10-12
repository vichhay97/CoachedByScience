import { useState, type SubmitEvent } from "react";
import "./ExerciseForm.css";

interface ExerciseFormProps {
    onAddExercise: (name: string, description: string) => Promise<boolean>;
}

function ExerciseForm({ onAddExercise }: ExerciseFormProps) {
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);

    async function handleSubmit(e: SubmitEvent<HTMLFormElement>) {
        e.preventDefault();
        setIsSubmitting(true);
        try {
            const success = await onAddExercise(name, description);
            if (success) {
                setName("");
                setDescription("");
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsSubmitting(false);
        }
    }

    return (
        <form className="exercise-form" onSubmit={handleSubmit}>
            <input placeholder="Exercise Name" value={name} onChange={(e) => setName(e.target.value)} />
            <textarea placeholder="Description" value={description} onChange={(e) => setDescription(e.target.value)} />
            <button type="submit" disabled={isSubmitting}>Submit</button>
        </form>
    );
}

export default ExerciseForm;