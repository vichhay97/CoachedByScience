import type { Exercise } from "./types";
import ExerciseCard from "./ExerciseCard";

interface ExerciseListProps {
    exercises: Exercise[];
}

function ExerciseList({ exercises }: ExerciseListProps) {
    return (
        <ul>
            {exercises.map((exercise) => (
                <ExerciseCard key={exercise.id} exercise={exercise} />
            ))}
        </ul>
    );
}

export default ExerciseList;