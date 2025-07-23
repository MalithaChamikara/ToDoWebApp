import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import { Provider } from "react-redux";
import * as reactRedux from "react-redux";
import { store } from "../store/store";
import TaskForm from "../components/TaskForm";

// Mock react-redux hooks
jest.mock("react-redux", () => ({
  ...jest.requireActual("react-redux"),
  useDispatch: jest.fn(),
}));

describe("TaskForm", () => {
  const mockDispatch = jest.fn();

  beforeEach(() => {
    // Reset the mock before each test
    mockDispatch.mockClear();
    reactRedux.useDispatch.mockReturnValue(mockDispatch);
  });

  it("dispatches createTask action with correct data on submit", () => {
    render(
      <Provider store={store}>
        <TaskForm />
      </Provider>
    );

    // Fill in the form
    fireEvent.change(screen.getByLabelText(/title/i), {
      target: { value: "Test Task" },
    });
    fireEvent.change(screen.getByLabelText(/description/i), {
      target: { value: "Test Description" },
    });

    // Submit the form
    fireEvent.click(screen.getByText(/add/i));

    // Verify dispatch was called with correct action
    expect(mockDispatch).toHaveBeenCalledWith(
      expect.objectContaining({
        type: "tasks/createTask/pending",
        meta: expect.any(Object),
        payload: {
          title: "Test Task",
          description: "Test Description",
        },
      })
    );
  });

  it("does not dispatch if title is empty", () => {
    render(
      <Provider store={store}>
        <TaskForm />
      </Provider>
    );

    // Try to submit without title
    fireEvent.click(screen.getByText(/add/i));

    // Verify dispatch was not called
    expect(mockDispatch).not.toHaveBeenCalled();
  });

  it("clears form after successful submission", () => {
    render(
      <Provider store={store}>
        <TaskForm />
      </Provider>
    );

    // Fill in the form
    const titleInput = screen.getByLabelText(/title/i);
    const descriptionInput = screen.getByLabelText(/description/i);

    fireEvent.change(titleInput, {
      target: { value: "Test Task" },
    });
    fireEvent.change(descriptionInput, {
      target: { value: "Test Description" },
    });

    // Submit the form
    fireEvent.click(screen.getByText(/add/i));

    // Verify inputs are cleared
    expect(titleInput.value).toBe("");
    expect(descriptionInput.value).toBe("");
  });

  it("shows validation error for empty title", () => {
    render(
      <Provider store={store}>
        <TaskForm />
      </Provider>
    );

    // Try to submit without title
    fireEvent.click(screen.getByText(/add/i));

    // Verify error message is shown
    expect(screen.getByText(/title is required/i)).toBeInTheDocument();
  });
});
