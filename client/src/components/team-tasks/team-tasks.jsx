import React, {useState} from 'react';
import {useDispatch, useSelector} from 'react-redux';

import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';

import SearchMember from '../search-member';

import {POINTS} from '../../utils/consts';
import {generateHeaders} from '../../utils/utils';
import api from '../../utils/api';

import {ActionCreator} from '../../redux/action-creator';

import './team-tasks.scss';


const TeamTasks = () => {
  const dispatch = useDispatch();
  const teamId = useSelector(({teams}) => teams.activeTeam?.id);
  const token = useSelector(({user}) => user.token);

  const tasks = useSelector(({teams}) => teams.activeTeam?.tasks || []);

  const tasksObject = [[], [], []];
  tasks.forEach((task) => tasksObject[task.state].push(task));
  const [todo, inProcces, done] = tasksObject;

  const [showAddTask, setShowAddTask] = useState(false);
  const [form, setForm] = useState({
    deadline: new Date(),
    Content: ``,
    Name: ``,
    Price: 1,
    RolesId: [],
    PerformersId: [],
  });

  const onPointBtnClick = (event, type) => {
    event.preventDefault();
    const index = POINTS.indexOf(form.Price) || 0;

    if (type === `more`) {
      return setForm((prevState) => ({
        ...prevState,
        Price: POINTS?.[index + 1] || POINTS[index],
      }));
    }

    return setForm((prevState) => ({
      ...prevState,
      Price: POINTS?.[index - 1] || POINTS[index],
    }));
  };

  const onDateChange = (date) => {
    setForm((prevState) => ({
      ...prevState,
      deadline: date,
    }));
  };

  const onInputChange = (event) => {
    event.preventDefault();

    const {name, value} = event.target;
    setForm((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const onFormSubmit = (event) => {
    event.preventDefault();

    api
      .put(`/team/${teamId}/addTask`, form, generateHeaders(token))
      .then(({data}) => {
        console.log(data);
        dispatch(ActionCreator.addTask(data));
        setShowAddTask(false);
      })
      .catch((error) => console.log(error));
  };

  return (
    <>
      {
        showAddTask && <div className="create-task__wrapper">
          <div className="create-task">
            <header className="create-task__header">
              <h4 className="create-task__title">add task</h4>
            </header>
            <form className="create-task__content" onSubmit={onFormSubmit}>
              <input
                type="text"
                name="Name"
                value={form.Name}
                placeholder="task name"
                onChange={onInputChange}
                className="create-task__input-title"
              />

              <textarea
                rows="10"
                name="Content"
                onChange={onInputChange}
                value={form.Content}
                placeholder="task placeholder"
                className="create-task__input-description"
              ></textarea>

              <div className="create-task__row">
                <SearchMember />

                <div className="create-task__calendar">
                  <div className="create-task__calendar-header">
                    <h5 className="create-task__calendar-title">
                      add deadline
                    </h5>
                  </div>

                  <div className="create-task__calendar-content">
                    <Calendar
                      onChange={onDateChange}
                      minDate={new Date()}
                      value={form.Date}
                    />
                  </div>

                </div>

                <div className="create-task__points">
                  <div className="create-task__calendar-header">
                    <h5 className="create-task__calendar-title">
                      add points
                    </h5>
                  </div>
                  <div className="create-task__point-content">
                    <button
                      onClick={(event) => onPointBtnClick(event, `less`)}
                      className="create-task__point-btn"
                    >
                      <img src="./img/less.svg" alt="less" />
                    </button>

                    <p className="create-task__point">{form.Price}</p>

                    <button
                      onClick={(event) => onPointBtnClick(event, `more`)}
                      className="create-task__point-btn"
                    >
                      <img src="./img/more.svg" alt="more" />
                    </button>
                  </div>
                </div>
              </div>

              <button
                className="create-task__btn"
                type="submit"
              >create</button>
            </form>
          </div>
        </div>
      }

      <div className="tabs__tasks-wrapper">
        <div className="tabs__tasks tabs__tab--todo">
          <div className="tabs__tasks-header">
            <h4 className="tabs__tasks-header-title">to do</h4>
          </div>

          <ul className="tabs__tasks-list">
            {
              todo.map(({name, price, id}) => (
                <li className="tabs__tasks-item" key={id}>
                  <span className="tabs__tasks-title">{name}</span>
                  <span className="tabs__tasks-points">{price}</span>
                </li>
              ))
            }
            <li
              onClick={() => setShowAddTask(true)}
              className="tabs__tasks-item tabs__tasks-item--add"
            >
              <span className="tabs__tasks-title">+ add task</span>
            </li>
          </ul>
        </div>

        <div className="tabs__tasks tabs__tab--in-procces">
          <div className="tabs__tasks-header">
            <h4 className="tabs__tasks-header-title">in procces</h4>
          </div>

          <ul className="tabs__tasks-list">
            {
              inProcces.map(({name, price, id}) => (
                <li className="tabs__tasks-item" key={id}>
                  <span className="tabs__tasks-title">{name}</span>
                  <span className="tabs__tasks-points">{price}</span>
                </li>
              ))
            }
          </ul>
        </div>

        <div className="tabs__tasks tabs__tab--in-done">
          <div className="tabs__tasks-header">
            <h4 className="tabs__tasks-header-title">done</h4>
          </div>

          <ul className="tabs__tasks-list">
            {
              done.map(({name, price, id}) => (
                <li className="tabs__tasks-item" key={id}>
                  <span className="tabs__tasks-title">{name}</span>
                  <span className="tabs__tasks-points">{price}</span>
                </li>
              ))
            }
          </ul>
        </div>
      </div>
    </>
  );
};

export default TeamTasks;
