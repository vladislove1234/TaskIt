import React from 'react';
import PropTypes from 'prop-types';

import './user-avatar.scss';

const UserAvatar = ({online = false, size = 50}) => {
  const sizeProp = typeof size === `number` ? `${size}px` : size;

  return (
    <div 
      style={{
        width: sizeProp,
        height: sizeProp,
      }}
      className={`user-avatar ${online && `user-avatar--online`}`} 
    />
  );
};

UserAvatar.propTypes = {
  size: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
  online: PropTypes.bool,
};

export default UserAvatar;
