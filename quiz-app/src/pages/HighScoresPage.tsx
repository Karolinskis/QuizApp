import { useEffect, useState } from "react";
import { HighScore } from "../types";
import { getHighScores } from "../api";
import {
  Container,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Box,
} from "@mui/material";

const HighScoresPage = () => {
  const [highScores, setHighScores] = useState<HighScore[]>([]);

  useEffect(() => {
    getHighScores(10).then((response) => setHighScores(response.data));
  }, []);

  const getColor = (index: number) => {
    switch (index) {
      case 0:
        return "gold";
      case 1:
        return "silver";
      case 2:
        return "#cd7f32";
      default:
        return "inherit";
    }
  };

  const getPosition = (index: number) => {
    switch (index) {
      case 0:
        return "🥇";
      case 1:
        return "🥈";
      case 2:
        return "🥉";
      default:
        return index + 1;
    }
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
        High Scores
      </Typography>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="center">Position</TableCell>
              <TableCell align="center">Email</TableCell>
              <TableCell align="center">Score</TableCell>
              <TableCell align="center">Date</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {highScores.length === 0 ? (
              <TableRow>
                <TableCell colSpan={4} align="center">
                  No high scores yet. Be the first to take the quiz!
                </TableCell>
              </TableRow>
            ) : (
              highScores.map((entry, index) => (
                <TableRow key={index}>
                  <TableCell align="center">
                    <Box component="span" fontWeight="bold">
                      {getPosition(index)}
                    </Box>
                  </TableCell>
                  <TableCell align="center" style={{ color: getColor(index) }}>
                    {entry.email}
                  </TableCell>
                  <TableCell align="center" style={{ color: getColor(index) }}>
                    {entry.score}
                  </TableCell>
                  <TableCell align="center" style={{ color: getColor(index) }}>
                    {new Date(entry.completedAt).toLocaleString()}
                  </TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
};

export default HighScoresPage;
