import React from 'react';
import {useSelector, useDispatch} from 'react-redux';

import {ActionCreator} from '../../redux/action-creator';

import UserAvatar from '../user-avatar';

import './user-info.scss';

const UserInfo = () => {
  const username = useSelector(({user}) => user.username);
  const dispatch = useDispatch();

  const onLogOutClick = (event) => {
    event.preventDefault();
    dispatch(ActionCreator.logout());
  };

  return (
    <div className="user-info">
      <UserAvatar online size={55} />

      <div className="user-info__name-wrapper">
        <p className="user-info__name">{username}</p>
        <button className="user-info__logout" onClick={onLogOutClick}>
          <img src="./img/logout.svg" alt="Log out" />
          log out
        </button>
      </div>
    </div>
  );
};

export default UserInfo;
