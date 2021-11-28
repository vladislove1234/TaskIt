import React, {useState} from 'react';

import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';

import SearchMember from '../search-member';

import './team-tasks.scss';

const TeamTasks = () => {
  const [showAddTask, setShowAddTask] = useState(false);
  const [value, onChange] = useState(new Date());
  const [form, setForm] = useState({
    Name: ``,
    Description: ``,
    Point: 0,
  });

  return (
    <>
      {
        showAddTask && <div className="create-task__wrapper">
          <div className="create-task">
            <header className="create-task__header">
              <h4 className="create-task__title">add task</h4>
            </header>
            <form className="create-task__content">
              <input
                type="text"
                name="Name"
                placeholder="task name"
              />

              <textarea name="Description" rows="10"></textarea>

              <div>
                <SearchMember />

                <Calendar
                  onChange={onChange}
                  value={value}
                />
              </div>
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
            <li className="tabs__tasks-item">
              <span className="tabs__tasks-title">task1</span>
              <span className="tabs__tasks-points">7</span>
            </li>

            <li className="tabs__tasks-item">
              <span className="tabs__tasks-title">task2</span>
              <span className="tabs__tasks-points">11</span>
            </li>

            <li className="tabs__tasks-item">
              <span className="tabs__tasks-title">task3</span>
              <span className="tabs__tasks-points">1</span>
            </li>

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
            <li className="tabs__tasks-item">
              <span className="tabs__tasks-title">task1</span>
              <span className="tabs__tasks-points">7</span>
            </li>

            <li className="tabs__tasks-item">
              <span className="tabs__tasks-title">task2</span>
              <span className="tabs__tasks-points">vladek 11</span>
            </li>
          </ul>
        </div>

        <div className="tabs__tasks tabs__tab--in-done">
          <div className="tabs__tasks-header">
            <h4 className="tabs__tasks-header-title">done</h4>
          </div>

          <ul className="tabs__tasks-list">
            <li className="tabs__tasks-item">
              <span className="tabs__tasks-title">task1</span>
              <span className="tabs__tasks-points">7</span>
            </li>

            <li className="tabs__tasks-item">
              <span className="tabs__tasks-title">task2</span>
              <span className="tabs__tasks-points">vladek 11</span>
            </li>
          </ul>
        </div>
      </div>
    </>
  );
};

export default TeamTasks;
