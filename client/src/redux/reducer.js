import {combineReducers} from 'redux';

import appReducer from './reducers/app';
import userReducer from './reducers/user';
import teamsReducer from './reducers/teams';

export const reducer = combineReducers({
  app: appReducer,
  user: userReducer,
  teams: teamsReducer,
});
