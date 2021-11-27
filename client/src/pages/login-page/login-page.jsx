import React, {useState} from 'react';
import {Link} from 'react-router-dom';
import {useDispatch} from 'react-redux';

import {ActionCreator} from '../../redux/action-creator';

import api from '../../utils/api';

import './login-page.scss';

const LoginPage = () => {
  const dispatch = useDispatch();

  const [error, setError] = useState(``);
  const [state, setState] = useState({
    username: ``,
    password: ``,
  });

  const onInputChange = (event) => {
    event.preventDefault();

    if (error) {
      setError(``);
    }

    const {name, value} = event.target;

    setState((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const onSubmit = (event) => {
    event.preventDefault();

    api.post(`/user/authenticate`, state)
      .then(({data}) => dispatch(ActionCreator.login(data)))
      .catch(({response}) => setError(response.data.message));
  };

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
            onSubmit={onSubmit}
            className="login__modal-form"
          >
            <input
              required
              type="text"
              name="username"
              placeholder="username"
              onChange={onInputChange}
              className="login__modal-input"
            />

            <input
              required
              type="password"
              name="password"
              placeholder="password"
              onChange={onInputChange}
              className="login__modal-input"
            />

            {error && <p className="login__modal-error">{error}</p>}

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
