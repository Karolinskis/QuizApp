import React from "react";
import {
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Button,
} from "@mui/material";

interface ScoreModalProps {
  open: boolean;
  score: number;
  onClose: () => void;
  onViewHighScores: () => void;
}

const ScoreModal: React.FC<ScoreModalProps> = ({
  open,
  score,
  onClose,
  onViewHighScores,
}) => {
  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Your Score</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Your score is: {score}
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="primary">
          Close
        </Button>
        <Button onClick={onViewHighScores} color="primary" autoFocus>
          View High Scores
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ScoreModal;