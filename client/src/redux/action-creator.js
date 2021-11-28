import {USER_LOGIN, USER_LOGOUT} from './types';
import {
  TEAMS_SELECT_TEAM,
  TEAMS_ADD_TEAMS,
  TEAMS_SELECT_WINDOW,
  TEAMS_ADD_TEAM,
  TEAMS_SET_TASKS,
  TEAMS_ADD_TASK,
} from './types';
import {APP_SET_WINDOW} from './types';

import api from '../utils/api';
import {generateHeaders} from '../utils/utils';

const userActions = {
  login: (loginData) => (dispatch) => {
    api
      .get(`/team/getTeams`, generateHeaders(loginData.token))
      .then(({data}) => {
        const teams = data.map((team) => ({...team, id: team.teamId}));
        dispatch(ActionCreator.addTeams(teams));
      })
      .catch(({response}) => console.log(response.data.message));

    dispatch({
      type: USER_LOGIN,
      payload: loginData,
    });
  },

  logout: () => ({
    type: USER_LOGOUT,
  }),
};

const teamsActions = {
  addTeams: (teams) => ({
    type: TEAMS_ADD_TEAMS,
    payload: teams,
  }),

  addTeam: (team) => ({
    type: TEAMS_ADD_TEAM,
    payload: team,
  }),

  selectTeam: (id, token) => (dispatch) => {
    api
      .get(`/team/${id}/getTeam`, generateHeaders(token))
      .then(({data}) => {
        dispatch({
          type: TEAMS_SELECT_TEAM,
          payload: data,
        });
      });

    api
      .get(`/team/${id}/getTasks`, generateHeaders(token))
      .then(({data}) => {
        dispatch({
          type: TEAMS_SET_TASKS,
          payload: data,
        });
      });
  },

  addTask: (task) => {
    task.id = task.taskId;

    return {
      type: TEAMS_ADD_TASK,
      payload: task,
    };
  },

  selectWindow: (window) => ({
    type: TEAMS_SELECT_WINDOW,
    payload: window,
  }),
};

const appActions = {
  setAppWindow: (window) => ({
    type: APP_SET_WINDOW,
    payload: window,
  }),
};

export const ActionCreator = {
  ...appActions,
  ...userActions,
  ...teamsActions,
};
