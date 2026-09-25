import type { Exercise } from "./types";

interface ExerciseCardProps {
    exercise: Exercise;
}

function ExerciseCard({ exercise }: ExerciseCardProps) {
    return (
        <li>
            <strong>{exercise.name}</strong>: {exercise.description}
            <br />
            <em>Targets: {exercise.muscleGroups.map((mg) => mg.name).join(", ")}</em>
        </li>
    );
}

export default ExerciseCard;