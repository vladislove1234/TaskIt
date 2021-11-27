import React from 'react';
import {useSelector, useDispatch} from 'react-redux';
import {ActionCreator} from '../../redux/action-creator';

import './teams-list.scss';

const TeamsList = () => {
  const teams = useSelector(({teams}) => {
    return teams.teams.map(({id, name}) => ({id, name}));
  });
  const activeTeam = useSelector(({teams}) => teams.activeTeam);

  const dispatch = useDispatch();
  const onTeamClick = (event, id) => {
    event.preventDefault();
    dispatch(ActionCreator.selectTeam(id));
  };


  return (
    <div className="teams">
      <h4 className="teams__header">teams</h4>

      <ul className="teams__list">
        {
          teams.map(({id, name}) => (
            <li
              key={id}
              tabIndex={1}
              onClick={(event) => onTeamClick(event, id)}
              className={`teams__item 
                ${id === activeTeam && `teams__item--active`}`}
            >
              <span className="teams__item-text">{name}</span>
              <div className="teams__item-buttons-wrapper">
                <button className="teams__item-button">tasks</button>
                <button className="teams__item-button">members</button>
                <button className="teams__item-button">chat</button>
              </div>
            </li>
          ))
        }

        <li className="teams__item teams__item--add-team">
          <span className="teams__item-text">+ add team</span>
        </li>
      </ul>
    </div>
  );
};

export default TeamsList;
