import axios from 'axios';

export default axios.create({
  baseUrl: `/api`,
  timeout: 1000 * 10, // 10 seconds
  withCredentials: true,
});
