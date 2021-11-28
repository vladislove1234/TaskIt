import React, {useState, useEffect} from 'react';

import {WORK_TIME, REST_TIME} from '../../utils/consts';

import './pomodoro.scss';

const Pomodoro = () => {
  const [updater, setUpdater] = useState(new Date());
  const [state, setState] = useState(`work`);
  const [time, setTime] = useState({
    seconds: +localStorage.getItem(`timer`),
    formatedTime: ``,
  }); // 25 minues

  useEffect(() => {
    const int = setInterval(() => {
      setUpdater(new Date());

      setTime((prevState) => {
        const seconds = prevState.seconds - 1;

        if (seconds < 0) {
          setState((pauseState) => pauseState === `work` ? `rest` : `work`);
          return setTimer({
            seconds: REST_TIME,
            formatedTime: `05:00`,
          });
        }

        const secs = seconds % 60;
        const mins = Math.floor(seconds / 60);

        const formatedSeconds = `${secs < 10 ? `0` : ``}${secs}`;
        const formatedMinutes = `${mins < 10 ? `0` : ``}${mins}`;
        const formatedTime = `${formatedMinutes}:${formatedSeconds}`;

        localStorage.setItem(`timer`, seconds);

        return {...prevState, formatedTime, seconds};
      });
    }, 1000);

    return () => {
      clearInterval(int);
    };
  }, [updater]);

  return (
    <div className="pomodoro">
      <div className="pomodoro__wrapper">
        <img src="./img/clock.svg" alt="Timer" className="pomodoro__image" />
        <p className="pomodoro__time">{time.formatedTime}</p>
      </div>
    </div>
  );
};

export default Pomodoro;
