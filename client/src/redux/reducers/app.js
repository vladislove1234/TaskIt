import {APP_SET_WINDOW} from '../types';

const initialState = {
  window: undefined,
};

export default (state = initialState, action) => {
  switch (action.type) {
  case APP_SET_WINDOW:
    return {
      ...state,
      window: action.payload,
    };
  }

  return state;
};
