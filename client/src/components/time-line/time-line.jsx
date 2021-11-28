import React, {useState, useEffect} from 'react';

import './time-line.scss';

const TimeLine = () => {
  const [date, setDate] = useState(new Date());

  const minutesFromStart = date.getHours() * 60 + date.getMinutes();
  const minutesInDay = 24 * 60;
  const percentage = minutesFromStart * 100 / minutesInDay;

  const timeLineStyle = {
    transform: `translate(-${percentage * (2 / 3) + (100 / 6)}%, -50%)`,
  };

  useEffect(() => {
    const interval = setInterval(() => {
      setDate(new Date());
    }, 1000 * 60);

    return () => {
      clearInterval(interval);
    };
  }, []);

  const events = [{
    start: 15 * 60,
    end: 18 * 60,
    color: `#A4B6F6`,
  }, {
    start: 18 * 60 + 30,
    end: 23 * 60,
    color: `#f9a75c`,
  }];

  return (
    <div className="timeline__wrapper">
      <div className="timeline">
        <div className="timeline__hours-wrapper">
          <div className="timeline__hours" style={timeLineStyle}>
            {
              events.map((event) => {
                const {start, end, color} = event;
                const time = end - start;
                const width = `${time * 100 / (36 * 60)}%`;
                const startPerc = start * 100 / minutesInDay;
                const left = `${startPerc * (2 / 3) + (100 / 6)}%`;

                return (
                  <div
                    style={{
                      width, left,
                      background: color,
                    }}
                    key={start + color}
                    className="timeline__event"
                  />
                );
              })
            }

            <span className="timeline__hour">18:00</span>
            <span className="timeline__hour">19:00</span>
            <span className="timeline__hour">20:00</span>
            <span className="timeline__hour">21:00</span>
            <span className="timeline__hour">22:00</span>
            <span className="timeline__hour">23:00</span>
            <span className="timeline__hour">00:00</span>
            <span className="timeline__hour">01:00</span>
            <span className="timeline__hour">02:00</span>
            <span className="timeline__hour">03:00</span>
            <span className="timeline__hour">04:00</span>
            <span className="timeline__hour">05:00</span>
            <span className="timeline__hour">06:00</span>
            <span className="timeline__hour">07:00</span>
            <span className="timeline__hour">08:00</span>
            <span className="timeline__hour">09:00</span>
            <span className="timeline__hour">10:00</span>
            <span className="timeline__hour">11:00</span>
            <span className="timeline__hour">12:00</span>
            <span className="timeline__hour">13:00</span>
            <span className="timeline__hour">14:00</span>
            <span className="timeline__hour">15:00</span>
            <span className="timeline__hour">16:00</span>
            <span className="timeline__hour">17:00</span>
            <span className="timeline__hour">18:00</span>
            <span className="timeline__hour">19:00</span>
            <span className="timeline__hour">20:00</span>
            <span className="timeline__hour">21:00</span>
            <span className="timeline__hour">22:00</span>
            <span className="timeline__hour">23:00</span>
            <span className="timeline__hour">00:00</span>
            <span className="timeline__hour">01:00</span>
            <span className="timeline__hour">02:00</span>
            <span className="timeline__hour">03:00</span>
            <span className="timeline__hour">04:00</span>
            <span className="timeline__hour">05:00</span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TimeLine;
