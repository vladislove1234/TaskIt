import {
  TEAMS_ADD_TEAM,
  TEAMS_ADD_TEAMS,
  TEAMS_SELECT_TEAM,
  TEAMS_SELECT_WINDOW,
  TEAMS_SET_TASKS,
  TEAMS_ADD_TASK,
} from '../types';

const initialState = {
  teams: [],
  activeTeam: null,
  activeTab: ``,
};

export default (state = initialState, action) => {
  switch (action.type) {
  case TEAMS_SELECT_TEAM:
    return {
      ...state,
      activeTab: `tasks`,
      activeTeam: action.payload,
    };

  case TEAMS_ADD_TEAMS:
    return {
      ...state,
      teams: action.payload,
    };

  case TEAMS_SELECT_WINDOW:
    return {
      ...state,
      activeTab: action.payload,
    };

  case TEAMS_ADD_TEAM:
    return {
      ...state,
      teams: [
        action.payload,
        ...state.teams,
      ],
    };

  case TEAMS_SET_TASKS:
    return {
      ...state,
      activeTeam: {
        ...state.activeTeam,
        tasks: action.payload,
      },
    };

  case TEAMS_ADD_TASK:
    return {
      ...state,
      activeTeam: {
        ...state.activeTeam,
        tasks: [
          action.payload,
          ...state.activeTeam.tasks,
        ],
      },
    };
  };

  return state;
};
