import React from 'react';
import PropTypes from 'prop-types';

import './user-avatar.scss';

const UserAvatar = ({online = false, size = 50, color}) => {
  const sizeProp = typeof size === `number` ? `${size}px` : size;

  const style = {
    width: sizeProp,
    height: sizeProp,
  };

  if (color) {
    style.backgroundColor = color;
  }

  return (
    <div
      style={style}
      className={`user-avatar ${online && `user-avatar--online`}`}
    />
  );
};

UserAvatar.propTypes = {
  size: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
  online: PropTypes.bool,
  color: PropTypes.string,
};

export default UserAvatar;
