import axios from "axios";
import { QuizSubmission, Question, HighScore } from "./types";

const api = axios.create({
  baseURL: "http://localhost:5106/api",
});

export const submitQuiz = (submission: QuizSubmission) =>
  api.post("/quiz/submit", submission);
export const getQuestions = () => api.get<Question[]>("/quiz/questions");

export const getHighScores = (count: number = 10) =>
  api.get<HighScore[]>(`/highscores?count=${count}`);
