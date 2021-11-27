import {USER_LOGIN, USER_LOGOUT} from './types';
import {TEAMS_SELECT_TEAM, TEAMS_ADD_TEAMS} from './types';

import api from '../utils/api';
import {generateHeaders} from '../utils/utils';

const userActions = {
  login: (loginData) => (dispatch) => {
    api
      .get(`/team/get_teams`, generateHeaders(loginData.token))
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
    paylaod: teams,
  }),

  selectTeam: (id) => ({
    type: TEAMS_SELECT_TEAM,
    payload: id,
  }),
};

export const ActionCreator = {
  ...userActions,
  ...teamsActions,
};
