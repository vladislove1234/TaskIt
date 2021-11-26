import React from 'react';
import {Link} from 'react-router-dom';

import './main-page.scss';


const MainPage = () => {
  return (
    <section className="main">
      MainPage

      <Link to="/login">Log in</Link>
      <Link to="/signup">Sign up</Link>
    </section>
  );
};

export default MainPage;
