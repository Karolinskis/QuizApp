import React from "react";
import { Box, Typography } from "@mui/material";

const Footer = () => {
  return (
    <Box
      component="footer"
      sx={{
        py: 2,
        px: 2,
        mt: "auto",
        backgroundColor: (theme) =>
          theme.palette.mode === "light"
            ? theme.palette.grey[200]
            : theme.palette.grey[800],
      }}
    >
      <Typography variant="body2" color="textSecondary" align="center">
        Quiz App {new Date().getFullYear()}
      </Typography>
      <Typography variant="body2" color="textSecondary" align="center">
        Made with <span style={{ color: "red" }}>❤</span> by Karolis
      </Typography>
    </Box>
  );
};

export default Footer;
