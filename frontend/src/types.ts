export interface MuscleGroup {
    id: number;
    name: string;
}

export interface Exercise {
    id: number;
    name: string;
    description: string;
    muscleGroups: MuscleGroup[];
}