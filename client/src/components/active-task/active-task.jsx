import React from 'react';
import {useSelector} from 'react-redux';
import {useDispatch} from 'react-redux';
import {ActionCreator} from '../../redux/action-creator';
import api from '../../utils/api';
import {generateHeaders} from '../../utils/utils';
import UserAvatar from '../user-avatar';

import './active-task.scss';

const mapState = [`to do`, `in procces`, `done`];

const ActiveTask = ({task}) => {
  const {price, name, state, content, performers, deadline, taskId: id} = task;
  const dispatch = useDispatch();

  const activeTeamId = useSelector(({teams}) => teams.activeTeam?.id);
  const userId = useSelector(({user}) => user.id);
  const token = useSelector(({user}) => user.token);

  const onExit = (event) => {
    event.preventDefault();
    dispatch(ActionCreator.setActiveTask(null));
  };

  const onTakeClick = (event) => {
    event.preventDefault();
    api
      .put(`/team/${activeTeamId}/takeTask`, {
        taskId: id,
        memberid: userId,
        endTime: deadline,
        startTime: new Date(),
        color: `#${Math.floor(Math.random() * 16777215).toString(16)}`,
      }, generateHeaders(token))
      .then(({data}) => {
        console.log(data);
        
        api
          .patch(
            `/team/${activeTeamId}/updateTask?taskId=${id}&taskState=1`,
            {},
            generateHeaders(token),
          )
          .then(({data}) => console.log(data))
          .catch((error) => console.log(error));
      })
      .catch((error) => console.log(error));
  };


  return (
    <div className="active-task">
      <div className="active-task__header">
        <button className="active-task__exit" onClick={onExit}>
          <svg width="14" height="24" viewBox="0 0 7 12" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M6 11L1 6L6 1" stroke="white"/>
          </svg>
        </button>

        <h4 className="action-task__title">
          <b>{mapState[state]}</b> | {name}
        </h4>

        <div className="active-task__points">
          <span>{price}</span> points
        </div>
      </div>

      <div className="active-task__content">
        <div className="active-task__content-text">
          <h4 className="active-task__content-text-header">description</h4>
          <p className="active-task__content-text-description">"{content}"</p>
        </div>

        <div className="active-task__perfomers">
          <h4 className="active-task__content-text-header">perfomers</h4>
          {
            !performers.length ?
              <li
                className="team-members__item
                team-members__item--add team-members__item--task"
              >
                <UserAvatar size={100} color="#878787" />
                <div className="team-member__text">none</div>
              </li> :
              performers.map((performer) => (
                <li
                  key={performer.id}
                  className="team-members__item
                team-members__item--add team-members__item--task"
                >
                  <UserAvatar size={100} />
                  <div className="team-member__text">{performer.name}</div>
                </li>
              ))
          }
        </div>

        <div className="active-task__deadline">
          <h4 className="active-task__content-text-header">deadline</h4>
          {new Date(deadline).toLocaleDateString()}
        </div>

        {
          !performers.length &&
           <button
             className="active-task__take"
             onClick={onTakeClick}
           >take the task</button>
        }
      </div>
    </div>
  );
};

export default ActiveTask;
