import React from "react";
import { AppBar, Toolbar, Typography, Button } from "@mui/material";
import { Link } from "react-router-dom";

const Navbar = () => {
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          Quiz App
        </Typography>
        <Button color="inherit" component={Link} to="/">
          Quiz
        </Button>
        <Button color="inherit" component={Link} to="/high-scores">
          High Scores
        </Button>
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;
