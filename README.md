# QuizApp

A web application that allows users to take quizzes and view high scores. The application is built using React, TypeScript, and Vite for the frontend, and ASP.NET Core for the backend. The backend uses an in-memory database to store quiz questions and user submissions.

## Calculation Rules

- **Radio buttons**: if answer is correct (+100 points).
- **Checkbox**: ( 100 / good answers) \* correctly checked. No decimal points, rounded up.
- **Textbox**: Only if text is identical match (+100 points). Should be case insensitive.

## Running the Solution

### Frontend

1. Navigate to the `quiz-app` directory:

   ```sh
   cd quiz-app
   ```

2. Install the dependencies:

   ```sh
    npm install
   ```

3. Start the development server:
   ```sh
    npm run dev
   ```

The frontend will be accessible at `http://localhost:5173`.

### Backend

1. Navigate to the `QuizApp` directory:

   ```sh
   cd QuizAppBackend/QuizApp
   ```

2. Run the application:

   ```sh
   dotnet restore
   ```

3. Run the backend application:

   ```sh
   dotnet run
   ```

The backend will be accessible at `http://localhost:5106`.

### Running the unit tests

1. Navigate to the `QuizAppBackend` directory:

   ```sh
   cd QuizAppBackend
   ```

2. Run the unit tests:

   ```sh
    dotnet test
   ```
