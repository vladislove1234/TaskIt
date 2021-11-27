import {combineReducers} from 'redux';

import userReducer from './reducers/user';
import teamsReducer from './reducers/teams';

export const reducer = combineReducers({
  user: userReducer,
  teams: teamsReducer,
});
