import React from 'react';
import {Link} from 'react-router-dom';

import './login-page.scss';

const LoginPage = () => {
  return (
    <section className="login">
      <div className="login__modal login__modal--login">
        <div className="login__modal-header">
          <Link className="login__modal-logo" to="/">
            <span className="visually-hidden">TaskIt</span>
            <img src="../img/logo.svg" alt="TaskIt" />
          </Link>
        </div>
        <div className="login__modal-content login__modal-content--login">
          <form
            onSubmit={() => {}}
            className="login__modal-form"
          >
            <input
              type="text"
              name="username"
              placeholder="username"
              className="login__modal-input"
            />

            <input
              type="password"
              name="password"
              placeholder="password"
              className="login__modal-input"
            />

            <button
              type="submit"
              className="login__modal-submit"
            >log in</button>

            <p className="login__modal-login-text">
              do not have an account? <Link to="/signup">sign up</Link>.
            </p>
          </form>

          <div className="login__modal-text-wrapper">
            <p className="login__modal-text">
              our life is short <br />
              it <span className="login__modal-text--accent">shines</span> and
              fades.
            </p>

            <p className="login__modal-copyright">
              &copy; Mykhailo Kotsiubynsky
            </p>
          </div>
        </div>
      </div>
    </section>
  );
};

export default LoginPage;
