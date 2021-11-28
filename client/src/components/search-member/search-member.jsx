import React from 'react';
import PropTypes from 'prop-types';

import UserAvatar from '../user-avatar';

import './search-member.scss';

const SearchMember = ({className = ``, ...props}) => {
  return (
    <div {...props} className={`search-member ${className}`}>
      <div className="search-member__header" />
      <div className="search-member__content">
        <input
          type="text"
          name="name"
          placeholder="username"
          className="search-member__search"
        />

        <ul className="search-member__members-list">
          <li className="search-member__member">
            <UserAvatar width={55} />
            <span className="search-member__name">marckusia</span>
            <button className="search-member__add">+</button>
          </li>

          <li className="search-member__member">
            <UserAvatar width={55} />
            <span className="search-member__name">marckusia</span>
            <button className="search-member__add">+</button>
          </li>

          <li className="search-member__member">
            <UserAvatar width={55} />
            <span className="search-member__name">marckusia</span>
            <button className="search-member__add">+</button>
          </li>
        </ul>
      </div>
    </div>
  );
};

SearchMember.propTypes = {
  className: PropTypes.string,
};

export default SearchMember;
