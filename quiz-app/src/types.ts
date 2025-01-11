export interface Question {
  id: number;
  text: string;
  type: "RadioButton" | "Checkbox" | "Textbox";
  options: string[];
}

export interface QuizSubmission {
  email: string;
  answers: {
    questionId: number;
    answerValue: string[];
  }[];
}
