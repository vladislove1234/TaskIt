import {USER_LOGIN, USER_LOGOUT} from './types';
import {TEAMS_SELECT_TEAM} from './types';

const userActions = {
  login: (loginData) => ({
    type: USER_LOGIN,
    payload: loginData,
  }),

  logout: () => ({
    type: USER_LOGOUT,
  }),
};

const teamsActions = {
  selectTeam: (id) => ({
    type: TEAMS_SELECT_TEAM,
    payload: id,
  }),
};

export const ActionCreator = {
  ...userActions,
  ...teamsActions,
};
