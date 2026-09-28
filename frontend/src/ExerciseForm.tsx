import { useState } from "react";
import "./ExerciseForm.css";

function ExerciseForm() {
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");

    return (
        <form className="exercise-form" onSubmit={(e) => {
            e.preventDefault();
            console.log(name, description);
        }}
        >
            <input placeholder="Exercise Name" value={name} onChange={(e) => setName(e.target.value)} />
            <textarea placeholder="Description" value={description} onChange={(e) => setDescription(e.target.value)} />
            <button type="submit">Submit</button>
        </form>
    );
}

export default ExerciseForm;