import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { Box, TextField, Button, Typography } from "@mui/material";
import { createTask } from "../store/tasksSlice";

function TaskForm() {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const dispatch = useDispatch();

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!title.trim()) return;
    dispatch(createTask({ title, description }));
    setTitle("");
    setDescription("");
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit}
      sx={{ p: 2, border: "1px solid #e0e0e0", borderRadius: 2 }}
    >
      <Typography variant="h6" gutterBottom>
        Add a Task
      </Typography>
      <TextField
        label="Title"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        fullWidth
        required
        margin="normal"
        variant="outlined"
      />
      <TextField
        label="Description"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        fullWidth
        multiline
        rows={3}
        margin="normal"
        variant="outlined"
      />
      <Button type="submit" variant="contained" color="primary" sx={{ mt: 2,mr: 1 }}>
        Add
      </Button>
    </Box>
  );
}

export default TaskForm;
