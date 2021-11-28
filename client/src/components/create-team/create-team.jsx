import React, {useState} from 'react';
import {useSelector, useDispatch} from 'react-redux';

import api from '../../utils/api';
import {generateHeaders} from '../../utils/utils';

import {ActionCreator} from '../../redux/action-creator';

import SearchMember from '../search-member/search-member';

import './create-team.scss';

const CreateTeam = () => {
  const token = useSelector(({user}) => user.token);
  const dispatch = useDispatch();

  const [form, setForm] = useState({
    Name: ``,
    UsersId: [],
  });

  const onInputChange = (event) => {
    event.preventDefault();

    const {name, value} = event.target;
    setForm((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const onSubmit = (event) => {
    event.preventDefault();
    api
      .post(`/team/addTeam`, form, generateHeaders(token))
      .then(({data: team}) => {
        dispatch(ActionCreator.addTeam(team));
        dispatch(ActionCreator.selectTeam(team.id, token));
        dispatch(ActionCreator.setAppWindow(`team_tasks`));
      })
      .catch(({response}) => console.log(response.data));
  };

  return (
    <section className="create-team">
      <div className="create-team__header">
        <h4 className="create-team__title">add team</h4>
      </div>

      <form className="create-team__container" onSubmit={onSubmit}>
        <input
          required
          type="text"
          name="Name"
          minLength={2}
          maxLength={30}
          value={form.Name}
          placeholder="team name"
          onChange={onInputChange}
          className="create-team__title-input"
        />

        <SearchMember
          className="create-team__members"
          selectHandler={(UsersId) => {
            setForm((prevState) => ({...prevState, UsersId}));
          }}
        />

        <button
          type="submit"
          className="create-team__create"
        >create</button>
      </form>
    </section>
  );
};

export default CreateTeam;
