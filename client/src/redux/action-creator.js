import {USER_LOGIN, USER_LOGOUT} from './types';
import {
  TEAMS_SELECT_TEAM,
  TEAMS_ADD_TEAMS,
  TEAMS_SELECT_WINDOW,
  TEAMS_ADD_TEAM,
} from './types';
import {APP_SET_WINDOW} from './types';

import api from '../utils/api';
import {generateHeaders} from '../utils/utils';

const userActions = {
  login: (loginData) => (dispatch) => {
    api
      .get(`/team/getTeams`, generateHeaders(loginData.token))
      .then(({data}) => dispatch(ActionCreator.addTeams(data)))
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

  selectTeam: (id) => ({
    type: TEAMS_SELECT_TEAM,
    payload: id,
  }),

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
