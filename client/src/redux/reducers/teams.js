import {TEAMS_ADD_TEAMS, TEAMS_SELECT_TEAM} from '../types';

const initialState = {
  teams: [{
    id: 1,
    name: `sontsepoklonnyky`,
  }, {
    id: 2,
    name: `karpaty`,
  }, {
    id: 3,
    name: `team 3`,
  }, {
    id: 4,
    name: `team 4`,
  }],
  activeTeam: 1,
  activeWindow: `tasks`,
};

export default (state = initialState, action) => {
  switch (action.type) {
  case TEAMS_SELECT_TEAM:
    return {
      ...state,
      activeTeam: action.payload,
    };
  
  case TEAMS_ADD_TEAMS:
    const teams = action.payload || [];
  
    return {...state, teams};
  };

  return state;
};
