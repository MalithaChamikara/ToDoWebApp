import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Grid, Divider, Container, Typography } from "@mui/material";
import TaskForm from "./components/TaskForm";
import TaskList from "./components/TaskList";
import { fetchTasks } from "./store/tasksSlice";

function App() {
  const dispatch = useDispatch();
  const tasksStatus = useSelector((state) => state.tasks.status);

  useEffect(() => {
    if (tasksStatus === "idle") {
      dispatch(fetchTasks());
    }
  }, [tasksStatus, dispatch]);

  return (
    <Container maxWidth="lg" sx={{ py: 4 }}>
      <Typography variant="h4" component="h1" gutterBottom align="center">
        To-Do App
      </Typography>
      <Grid container spacing={4}>
        <Grid item xs={12} md={3}>
          <TaskForm />
        </Grid>
        <Grid item xs={12} md={9}>
          <Divider orientation="vertical" flexItem sx={{ mr: -2, ml: -2 }} />
          <TaskList />
        </Grid>
      </Grid>
    </Container>
  );
}

export default App;
