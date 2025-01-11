import React, { useEffect, useState } from "react";
import { Question, QuizSubmission } from "../types";
import { getQuestions, submitQuiz } from "../api";
import {
  Container,
  Typography,
  Button,
  Box,
  Stepper,
  Step,
  StepLabel,
} from "@mui/material";
import QuestionComponent from "./QuestionComponent";
import RegistrationStep from "./RegistrationStep";

const QuizPage = () => {
  const [questions, setQuestions] = useState<Question[]>([]);
  const [email, setEmail] = useState("");
  const [answers, setAnswers] = useState<{ [key: number]: string[] }>({});
  const [activeStep, setActiveStep] = useState(0);

  useEffect(() => {
    getQuestions().then((response) => setQuestions(response.data));
  }, []);

  const handleNext = () => {
    if (activeStep === 0 && !validateEmail(email)) {
      return;
    }
    setActiveStep((prevActiveStep) => prevActiveStep + 1);
  };

  const handleBack = () => {
    setActiveStep((prevActiveStep) => prevActiveStep - 1);
  };

  const handleSubmit = async () => {
    const submission: QuizSubmission = {
      email,
      answers: Object.entries(answers).map(([questionId, answer]) => ({
        questionId: parseInt(questionId),
        answerValue: answer,
      })),
    };

    console.log(submission);

    // const response = await submitQuiz(submission);
    // alert(`Your score is: ${response.data.score}`);
  };

  const setAnswer = (questionId: number, answer: string[]) => {
    setAnswers({ ...answers, [questionId]: answer });
  };

  const validateEmail = (email: string) => {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
  };

  return (
    <Container maxWidth="md">
      <Typography
        variant="h4"
        component="h1"
        gutterBottom
        align="center"
        mb={5}
      >
        Quiz
      </Typography>
      <Stepper activeStep={activeStep} alternativeLabel>
        <Step>
          <StepLabel>Registration</StepLabel>
        </Step>
        {questions.map((question, index) => (
          <Step key={index}>
            <StepLabel>{`Question ${index + 1}`}</StepLabel>
          </Step>
        ))}
      </Stepper>
      {activeStep === 0 ? (
        <RegistrationStep email={email} setEmail={setEmail} />
      ) : (
        questions.length > 0 && (
          <QuestionComponent
            question={questions[activeStep - 1]}
            answer={answers[questions[activeStep - 1].id] || []}
            setAnswer={setAnswer}
          />
        )
      )}
      <Box display="flex" justifyContent="space-between">
        <Button
          disabled={activeStep === 0}
          onClick={handleBack}
          variant="contained"
        >
          Back
        </Button>
        {activeStep === questions.length ? (
          <Button variant="contained" color="primary" onClick={handleSubmit}>
            Submit
          </Button>
        ) : (
          <Button
            variant="contained"
            color="primary"
            onClick={handleNext}
            disabled={activeStep === 0 && !validateEmail(email)}
          >
            Next
          </Button>
        )}
      </Box>
    </Container>
  );
};

export default QuizPage;
