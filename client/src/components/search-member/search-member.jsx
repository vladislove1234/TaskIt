import React, {useState, useEffect} from 'react';
import PropTypes from 'prop-types';

import UserAvatar from '../user-avatar';

import './search-member.scss';
import api from '../../utils/api';
import {generateHeaders} from '../../utils/utils';
import {useSelector} from 'react-redux';

const SearchMember = ({className = ``, selectHandler = () => {}, ...props}) => {
  const [name, setName] = useState(``);
  const [list, setList] = useState([]);
  const [selected, setSelected] = useState([]);

  const token = useSelector(({user}) => user.token);

  useEffect(() => {
    if (name) {
      api
        .get(`/user/getByName?name=${name}`, generateHeaders(token))
        .then(({data}) => setList(data))
        .catch(({response}) => console.log(response.data));
    } else {
      setList([]);
    }
  }, [name]);

  useEffect(() => {
  }, [selected]);

  const onSelect = (event, id) => {
    event.preventDefault();

    if (selected.includes(id)) {
      return setSelected((prevState) => {
        const index = prevState.indexOf(id);
        return prevState.slice(index, 1);
      });
    }

    setSelected((prevState) => {
      selectHandler([...prevState, id], id);
      return [...prevState, id];
    });
  };

  return (
    <div {...props} className={`search-member ${className}`}>
      <div className="search-member__header" />
      <div className="search-member__content">
        <input
          type="text"
          name="name"
          value={name}
          placeholder="username"
          className="search-member__search"
          onChange={(e) => setName(e.target.value)}
        />

        <ul className="search-member__members-list">
          {
            list.length ?
              list.map(({username, id}) => (
                <li className="search-member__member" key={id}>
                  <UserAvatar width={55} />
                  <span className="search-member__name">{username}</span>
                  <button
                    onClick={(event) => onSelect(event, id)}
                    className="search-member__add"
                  >
                    {selected.includes(id) ? `-` : `+`}
                  </button>
                </li>
              )) :
              <p className="search-member__empty">
                we couldn't find user with this username
              </p>
          }
        </ul>
      </div>
    </div>
  );
};

SearchMember.propTypes = {
  className: PropTypes.string,
};

export default SearchMember;
