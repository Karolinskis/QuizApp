import React from "react";
import { Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer";

import QuizPage from "./pages/QuizPage";
import HighScoresPage from "./pages/HighScoresPage";
import NotFoundPage from "./pages/NotFoundPage";

import { Box } from "@mui/material";

function App() {
  return (
    <Box display="flex" flexDirection="column" minHeight="100vh">
      <Navbar />
      <Box
        component="main"
        display="flex"
        flexDirection="column"
        flexGrow={1}
        sx={{ padding: 3 }}
      >
        <Routes>
          <Route path="/" element={<QuizPage />} />
          <Route path="/high-scores" element={<HighScoresPage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </Box>
      <Footer />
    </Box>
  );
}

export default App;
