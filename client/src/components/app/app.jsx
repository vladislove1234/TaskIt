import React, {useEffect} from 'react';
import {BrowserRouter as Router} from 'react-router-dom';
import {useSelector, useDispatch} from 'react-redux';

import {ActionCreator} from '../../redux/action-creator';

import {useRoutes} from '../../hooks/routes';

// import {
//   HubConnectionBuilder,
//   HttpTransportType,
//   LogLevel,
// } from '@microsoft/signalr';


import 'normalize.css';
import './app.scss';

const App = () => {
  const isAuth = useSelector(({user}) => user.isAuth);
  const dispatch = useDispatch();

  const router = useRoutes(isAuth);

  useEffect(() => {
    // const connection = new HubConnectionBuilder()
    //   .configureLogging(LogLevel.Debug)
    //   .withUrl(`https://localhost:5050/chat`, {
    //     skipNegotiation: true,
    //     transport: HttpTransportType.WebSockets,
    //   })
    //   .build();

    // dispatch(ActionCreator.initMessages(connection));
    const userJSON = localStorage.getItem(`user`);

    if (userJSON) {
      try {
        const user = JSON.parse(userJSON);
        dispatch(ActionCreator.login(user));
      } catch (e) {
        console.log(`logging error`, e);
      }
    }
  }, []);

  return (
    <Router>
      {router}
    </Router>
  );
};

export default App;
