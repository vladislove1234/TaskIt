import {USER_LOGIN, USER_LOGOUT} from './types';

const userActions = {
  login: (loginData) => ({
    type: USER_LOGIN,
    payload: loginData,
  }),

  logout: () => ({
    type: USER_LOGOUT,
  }),
};

export const ActionCreator = {
  ...userActions,
};
