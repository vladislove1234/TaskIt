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

      <section className="quote">
        <img src="./img/main-quote.svg" alt="Quote" />

        <div>
          <p className="quote__text">
            our life is short <br /> it <span>shines</span> and fades.
          </p>
          <p className="quote__author">&copy; Mykhailo Kotsiubynsky</p>
        </div>
      </section>

      <section className="about">
        <div className="about__text">
          TaskIT - your personal time manager. We guarantee visible
          changes even after the first day of using! Here you can add
          your mates to do tasks together, create different teams and
          mark tasks in your own calendar. <Link to="/signup">try it now</Link>
        </div>

        <img src="./img/about.svg" alt="about" />
      </section>
    </>
  );
};

export default MainPage;
