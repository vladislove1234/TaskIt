import React from 'react';

import {Link} from 'react-router-dom';

import UserInfo from '../../components/user-info';
import TeamsList from '../../components/teams-list';


import './app-page.scss';

const AppPage = () => {
  return (
    <section className="app">

      <aside className="app__aside">
        <div className="app__logo">
          <Link className="app__logo-wrapper" to="/">
            <img src="./img/logo.svg" alt="TaskIt" />
          </Link>
          <h3 className="app__promo">
            your <span className="app__promo--accent">best</span> time manager
          </h3>
        </div>

        <section className="sidebar">
          <UserInfo />
          <TeamsList />
        </section>
      </aside>

    </section>
  );
};

export default AppPage;
