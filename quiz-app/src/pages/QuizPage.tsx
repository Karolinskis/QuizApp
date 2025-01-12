import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
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
import QuestionComponent from "../components/QuestionComponent";
import RegistrationStep from "../components/RegistrationStep";
import ConfirmDialog from "../components/ConfirmDialog";
import ScoreModal from "../components/ScoreModal";

const QuizPage = () => {
  const [questions, setQuestions] = useState<Question[]>([]);
  const [email, setEmail] = useState("");
  const [answers, setAnswers] = useState<{ [key: number]: string[] }>({});
  const [activeStep, setActiveStep] = useState(0);
  const [confirmDialogOpen, setConfirmDialogOpen] = useState(false);
  const [scoreModalOpen, setScoreModalOpen] = useState(false);
  const [score, setScore] = useState(0);
  const navigate = useNavigate();

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

    const response = await submitQuiz(submission);
    const score: number = response.data.score;

    setScore(score);
    setScoreModalOpen(true);
  };

  const setAnswer = (questionId: number, answer: string[]) => {
    setAnswers({ ...answers, [questionId]: answer });
  };

  const validateEmail = (email: string) => {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
  };

  const handleConfirmDialogOpen = () => {
    setConfirmDialogOpen(true);
  };

  const handleConfirmDialogClose = () => {
    setConfirmDialogOpen(false);
  };

  const handleConfirmDialogSubmit = () => {
    setConfirmDialogOpen(false);
    handleSubmit();
  };

  const handleScoreModalClose = () => {
    setScoreModalOpen(false);
    navigate("/high-scores");
  };

  const handleViewHighScores = () => {
    navigate("/high-scores");
  };

  if (questions.length === 0) {
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
        <Typography variant="body1" align="center">
          No questions available. Please try again later.
        </Typography>
      </Container>
    );
  }

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
          <Button
            variant="contained"
            color="primary"
            onClick={handleConfirmDialogOpen}
          >
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

      <ConfirmDialog
        open={confirmDialogOpen}
        onClose={handleConfirmDialogClose}
        onConfirm={handleConfirmDialogSubmit}
      />

      {score !== null && (
        <ScoreModal
          open={scoreModalOpen}
          score={score}
          onClose={handleScoreModalClose}
          onViewHighScores={handleViewHighScores}
        />
      )}
    </Container>
  );
};

export default QuizPage;
