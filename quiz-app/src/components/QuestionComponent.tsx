import React from "react";
import {
  Typography,
  FormControl,
  FormLabel,
  RadioGroup,
  FormControlLabel,
  Radio,
  Checkbox,
  TextField,
  Box,
} from "@mui/material";
import { Question } from "../types";

interface QuestionComponentProps {
  question: Question;
  answer: string[];
  setAnswer: (questionId: number, answer: string[]) => void;
}

const QuestionComponent: React.FC<QuestionComponentProps> = ({
  question,
  answer,
  setAnswer,
}) => {
  const handleRadioChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setAnswer(question.id, [e.target.value]);
  };

  const handleCheckboxChange =
    (option: string) => (e: React.ChangeEvent<HTMLInputElement>) => {
      const selected = answer || [];
      setAnswer(
        question.id,
        e.target.checked
          ? [...selected, option]
          : selected.filter((a) => a !== option)
      );
    };

  const handleTextboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setAnswer(question.id, [e.target.value]);
  };

  return (
    <Box mb={4}>
      <Typography variant="h6" component="p">
        {question.text}
      </Typography>
      {question.type === "RadioButton" && (
        <FormControl component="fieldset">
          <FormLabel component="legend">Select one:</FormLabel>
          <RadioGroup
            name={question.id.toString()}
            onChange={handleRadioChange}
          >
            {question.options.map((option, index) => (
              <FormControlLabel
                key={index}
                value={option}
                control={<Radio />}
                label={option}
              />
            ))}
          </RadioGroup>
        </FormControl>
      )}
      {question.type === "Checkbox" && (
        <FormControl component="fieldset">
          <FormLabel component="legend">Select all that apply:</FormLabel>
          {question.options.map((option, index) => (
            <FormControlLabel
              key={index}
              control={
                <Checkbox
                  checked={answer.includes(option)}
                  onChange={handleCheckboxChange(option)}
                />
              }
              label={option}
            />
          ))}
        </FormControl>
      )}
      {question.type === "Textbox" && (
        <TextField
          label="Your answer"
          fullWidth
          margin="normal"
          value={answer[0] || ""}
          onChange={handleTextboxChange}
        />
      )}
    </Box>
  );
};

export default QuestionComponent;
