import axios from "axios";
import { QuizSubmission, Question } from "./types";

const api = axios.create({
  baseURL: "http://localhost:5106/api",
});

export const submitQuiz = (submission: QuizSubmission) =>
  api.post("/quiz", submission);
export const getQuestions = () => api.get<Question[]>("/quiz");

// TODO: Should probably use some type of a custom type
export const getHighScores = () => api.get<QuizSubmission[]>("/highscores");
