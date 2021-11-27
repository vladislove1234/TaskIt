import {USER_LOGIN, USER_LOGOUT} from '../types';

const initialState = {
  name: `Victor`,
  surname: `Muryn`,
  token: ``,
  username: `hellcaster`,
  email: ``,
  isAuth: true,
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
      email: ``,
      token: ``,
      surname: ``,
      username: ``,
      isAuth: false,
    };
  }

  return state;
};
