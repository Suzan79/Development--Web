// Type declaration for the objects recieved from the response 
export type Movie = {
    Id: number,
    Title: string,
    Actors: Actor[]
};
export type Actor = {
    Id: number,
    Name: string,
    Gender: "Male" | "Female",
    Birth: Date
};
