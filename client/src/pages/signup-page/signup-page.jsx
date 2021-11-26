import React, {useState} from 'react';
import {Link} from 'react-router-dom';

import './signup-page.scss';

const SignupPage = () => {
  const [error, setError] = useState(``);
  const [state, setState] = useState({
    name: ``,
    surname: ``,
    username: ``,
    email: ``,
    password: ``,
    passwordRepeat: ``,
  });

  const onChange = (event) => {
    event.preventDefault();
    if (error) {
      setError(``);
    }

    setState((prevState) => ({
      ...prevState,
      [event.target.name]: event.target.value,
    }));
  };

  const onSubmit = (event) => {
    event.preventDefault();

    if (state.password !== state.passwordRepeat) {
      setError(`passwords are not the same.`);
    }
  };

  return (
    <section className="login">
      <div className="login__modal login__modal--signup">
        <div className="login__modal-header">
          <Link className="login__modal-logo" to="/">
            <span className="visually-hidden">TaskIt</span>
            <img src="../img/logo.svg" alt="TaskIt" />
          </Link>
        </div>
        <div className="login__modal-content">
          <form
            onSubmit={onSubmit}
            className="login__modal-form login__modal-form--signup"
          >
            <div className="login__modal-column-wrapper">
              <div className="login__modal-column">
                <input
                  required
                  type="text"
                  name="name"
                  onChange={onChange}
                  value={state.name}
                  placeholder="name"
                  className="login__modal-input"
                />

                <input
                  required
                  type="text"
                  name="surname"
                  onChange={onChange}
                  value={state.surname}
                  placeholder="surname"
                  className="login__modal-input"
                />

                <input
                  required
                  type="text"
                  name="username"
                  onChange={onChange}
                  value={state.username}
                  placeholder="username"
                  className="login__modal-input"
                />
              </div>

              <div className="login__modal-column">
                <input
                  required
                  type="email"
                  name="email"
                  onChange={onChange}
                  value={state.email}
                  placeholder="email"
                  className="login__modal-input"
                />

                <input
                  required
                  minLength={8}
                  type="password"
                  name="password"
                  onChange={onChange}
                  value={state.password}
                  placeholder="password"
                  className="login__modal-input"
                />

                <input
                  required
                  minLength={8}
                  type="password"
                  name="passwordRepeat"
                  onChange={onChange}
                  value={state.passwordRepeat}
                  placeholder="repeat password"
                  className="login__modal-input"
                />
              </div>
            </div>

            {error && <p className="login__modal-error">{error}</p>}

            <button
              type="submit"
              className="login__modal-submit"
            >sign up</button>

            <p className="login__modal-login-text">
              do you already have an account? <Link to="/login">log in</Link>.
            </p>
          </form>
        </div>
      </div>
    </section>
  );
};

export default SignupPage;
