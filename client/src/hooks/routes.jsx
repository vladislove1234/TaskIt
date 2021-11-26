import React from 'react';
import {Route, Routes} from 'react-router-dom';

import MainPage from '../pages/main-page';
import LoginPage from '../pages/login-page';
import SignupPage from '../pages/signup-page';

export const useRoutes = (isAuth) => {
  if (isAuth) {
    return (
      <Routes />
    );
  }

  return (
    <Routes>
      <Route path="/" element={<MainPage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/signup" element={<SignupPage />} />
    </Routes>
  );
};
