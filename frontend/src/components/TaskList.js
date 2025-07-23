import React from "react";
import { useSelector, useDispatch } from "react-redux";
import { Card, CardContent, Typography, Button, Box } from "@mui/material";
import { completeTask } from "../store/tasksSlice";

function TaskList() {
  const tasks = useSelector((state) => state.tasks.tasks);
  const dispatch = useDispatch();

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        gap: 2,
        height: "100%",
        width: "100%",
      }}
    >
      {tasks.map((task) => (
        <Card
          key={task.id}
          sx={{
            backgroundColor: "#f5f5f5",
            borderRadius: 1,
            width: "100%",
            minHeight: "120px",
          }}
        >
          <CardContent>
            <Typography variant="h6" gutterBottom>
              {task.title}
            </Typography>
            <Box
              sx={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                mt: 2,
              }}
            >
              <Typography color="textSecondary" sx={{ flex: 1, mr: 2 }}>
                {task.description || "No description"}
              </Typography>
              <Button
                variant="outlined"
                color="secondary"
                onClick={() => dispatch(completeTask(task.id))}
              >
                Done
              </Button>
            </Box>
          </CardContent>
        </Card>
      ))}
    </Box>
  );
}

export default TaskList;
