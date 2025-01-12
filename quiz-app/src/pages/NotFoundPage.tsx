import React from "react";
import { Box, Typography, Button } from "@mui/material";
import { Link } from "react-router-dom";

const NotFoundPage = () => {
  return (
    <Box
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="center"
      flexGrow={1}
      textAlign="center"
      bgcolor="#f0f0f0"
      p={3}
    >
      <Typography variant="h1" component="h1" gutterBottom>
        404
      </Typography>
      <Typography variant="h4" component="h2" gutterBottom>
        Oops! Page not found.
      </Typography>
      <Typography variant="body1" gutterBottom>
        It seems you've found a page that doesn't exist.
      </Typography>
      <Typography variant="body1" gutterBottom>
        Maybe try going back to the quiz and test your knowledge instead?
      </Typography>
      <Button variant="contained" color="primary" component={Link} to="/">
        Go to Quiz
      </Button>
    </Box>
  );
};

export default NotFoundPage;
