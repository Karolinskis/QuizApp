import React from "react";
import { TextField, Box } from "@mui/material";

interface RegistrationStepProps {
  email: string;
  setEmail: (email: string) => void;
}

const RegistrationStep: React.FC<RegistrationStepProps> = ({
  email,
  setEmail,
}) => {
  return (
    <Box mb={4}>
      <TextField
        label="Enter your email"
        type="email"
        fullWidth
        margin="normal"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
    </Box>
  );
};

export default RegistrationStep;
