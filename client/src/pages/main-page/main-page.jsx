import React from 'react';
import {Link} from 'react-router-dom';

import './main-page.scss';


const MainPage = () => {
  return (
    <>
      <header className="header">
        <div>
          <h3 className="header__title">taskIT</h3>
          <p className="header__description">
            manage your <span>time</span> correctly
          </p>
        </div>

        <div className="header-btns">
          <Link to="/signup" className="header__btn header__btn--signup">
            Sign up
          </Link>

          <Link to="/login" className="header__btn header__btn--login">
            Log in
          </Link>
        </div>
      </header>

      <section className="main">


      </section>
    </>
  );
};

export default MainPage;
