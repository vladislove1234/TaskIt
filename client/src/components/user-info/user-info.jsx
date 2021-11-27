import React from 'react';
import {useSelector} from 'react-redux';

import UserAvatar from '../user-avatar';

import './user-info.scss';

const UserInfo = () => {
  const username = useSelector(({user}) => user.username);

  return (
    <div className="user-info">
      <UserAvatar online size={55} />
      <p className="user-info__name">{username}</p>
    </div>
  );
};

export default UserInfo;
