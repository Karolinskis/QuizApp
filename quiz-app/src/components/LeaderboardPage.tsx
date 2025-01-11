import React, { useEffect, useState } from "react";
import { QuizSubmission } from "../types";
import { getHighScores } from "../api";

const LeaderboardPage = () => {
  const [highScores, setHighScores] = useState<QuizSubmission[]>([]);

  useEffect(() => {
    getHighScores().then((response) => setHighScores(response.data));
  }, []);

  return (
    <div>
      <h1>High Scores</h1>
      <ul>
        {highScores.map((entry, index) => (
          <li key={index}>
            {`${entry.email} - ${entry.score} - ${entry.completedAt}`}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default LeaderboardPage;
