import React, {useState} from 'react';
import {useSelector} from 'react-redux';

import UserAvatar from '../user-avatar';
import SearchMember from '../search-member';

import './team-members.scss';

const TeamMembers = () => {
  const [showAdd, setShowAdd] = useState(false);
  const teamName = useSelector(({teams}) => {
    return teams.teams.find(({id}) => id === teams.activeTeam).name;
  });

  const members = useSelector(({teams}) => {
    return teams.teams.find(({id}) => id === teams.activeTeam).users;
  });

  return (
    <>
      {
        showAdd && <div
          className="add-members"
          onClick={() => setShowAdd(false)}
        >
          <SearchMember
            className="add-members__window"
            onClick={(event) => event.stopPropagation()}
          />
        </div>
      }


      <div className="team-members">
        <header className="team-members__header">
          <h4 className="team-members__header-title">
            <b>{teamName}</b> | members
          </h4>
        </header>

        <div className="team-members__content">
          <ul className="team-members__list">
            {
              members.map(({username, id}) => (
                <li className="team-members__item" key={id}>
                  <UserAvatar size={100} />
                  <div className="team-member__text">
                    <p className="team-member__name">{username}</p>
                    <p className="team-member__role">???</p>
                  </div>
                </li>
              ))
            }

            <li
              tabIndex={1}
              onClick={() => setShowAdd(true)}
              className="team-members__item team-members__item--add"
            >
              <UserAvatar size={100} color="#E8E8E8" />
              <div className="team-member__text">+ add member</div>
            </li>
          </ul>
        </div>
      </div>
    </>
  );
};

export default TeamMembers;