import {USER_LOGIN, USER_LOGOUT} from '../types';

const initialState = {
  name: ``,
  token: ``,
  surname: ``,
  isAuth: false,
};

const userStorageName = `user`;

export default (state = initialState, action) => {
  switch (action.type) {
  case USER_LOGIN:
    localStorage.setItem(userStorageName, JSON.stringify(action.payload));

    return {
      ...state,
      ...action.payload,
      isAuth: true,
    };

  case USER_LOGOUT:
    localStorage.removeItem(userStorageName);

    return {
      ...state,
      name: ``,
      token: ``,
      surname: ``,
      isAuth: false,
    };
  }

  return state;
};
