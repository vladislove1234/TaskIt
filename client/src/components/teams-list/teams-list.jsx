import React from 'react';
import {useSelector, useDispatch} from 'react-redux';
import {ActionCreator} from '../../redux/action-creator';

import {TEAM_TABS} from '../../utils/consts';

import './teams-list.scss';

const TeamsList = () => {
  const teams = useSelector(({teams}) => {
    return teams.teams.map(({id, name}) => ({id, name}));
  });
  const activeTeam = useSelector(({teams}) => teams.activeTeam?.id);
  const activeTab = useSelector(({app}) => app.window);
  const token = useSelector(({user}) => user.token);

  const dispatch = useDispatch();

  const onTeamClick = (event, id) => {
    event.preventDefault();
    dispatch(ActionCreator.selectTeam(id, token));
    dispatch(ActionCreator.setAppWindow(`team_tasks`));
  };

  const onTabClick = (event, tab) => {
    event.preventDefault();
    dispatch(ActionCreator.setAppWindow(`team_${tab}`));
  };

  const onAddClick = (event) => {
    event.preventDefault();
    dispatch(ActionCreator.setAppWindow(`createTeam`));
  };


  return (
    <div className="teams">
      <h4 className="teams__header">teams</h4>

      <ul className="teams__list">
        {
          teams.map(({id, name}) => (
            <li
              key={id}
              className={`teams__item 
                  ${id === activeTeam && `teams__item--active`}`}
            >
              <span
                tabIndex={1}
                className="teams__item-text"
                onClick={(event) => onTeamClick(event, id)}
              >
                {name}
              </span>

              <div className="teams__item-buttons-wrapper">
                {
                  TEAM_TABS.map((tab) => (
                    <button
                      key={tab + id}
                      onClick={(event) => onTabClick(event, tab)}
                      className={
                        `teams__item-button 
                          ${`team_${tab}` === activeTab &&
                           `teams__item-button--active`}`
                      }
                    >{tab}</button>
                  ))
                }
              </div>
            </li>
          ))
        }

        <li className="teams__item teams__item--add-team">
          <span
            tabIndex={0}
            onClick={onAddClick}
            className="teams__item-text"
          >
            + add team
          </span>
        </li>
      </ul>
    </div>
  );
};

export default TeamsList;
