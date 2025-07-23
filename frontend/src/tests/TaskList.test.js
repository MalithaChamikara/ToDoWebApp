import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import { Provider } from "react-redux";
import * as reactRedux from "react-redux";
import { store } from "../store/store";
import TaskList from "../components/TaskList";

// Mock react-redux hooks
jest.mock("react-redux", () => ({
  ...jest.requireActual("react-redux"),
  useDispatch: jest.fn(),
}));

describe("TaskList", () => {
  const mockDispatch = jest.fn();

  beforeEach(() => {
    // Reset the mock before each test
    mockDispatch.mockClear();
    reactRedux.useDispatch.mockReturnValue(mockDispatch);
  });

  it("renders tasks correctly", () => {
    const tasks = [
      { id: "1", title: "Task 1", description: "Desc 1" },
      { id: "2", title: "Task 2", description: "Desc 2" },
    ];
    const mockStore = {
      ...store,
      getState: () => ({
        tasks: { tasks, status: "succeeded", error: null },
      }),
    };

    render(
      <Provider store={mockStore}>
        <TaskList />
      </Provider>
    );

    expect(screen.getByText("Task 1")).toBeInTheDocument();
    expect(screen.getByText("Desc 1")).toBeInTheDocument();
    expect(screen.getByText("Task 2")).toBeInTheDocument();
    expect(screen.getByText("Desc 2")).toBeInTheDocument();
  });

  it("dispatches completeTask action when Done button is clicked", () => {
    const tasks = [{ id: "1", title: "Task 1", description: "Desc 1" }];
    const mockStore = {
      ...store,
      getState: () => ({
        tasks: { tasks, status: "succeeded", error: null },
      }),
    };

    render(
      <Provider store={mockStore}>
        <TaskList />
      </Provider>
    );

    fireEvent.click(screen.getByText(/done/i));

    expect(mockDispatch).toHaveBeenCalledWith(
      expect.objectContaining({
        type: "tasks/completeTask/pending",
        meta: expect.any(Object),
        payload: "1",
      })
    );
  });
});
