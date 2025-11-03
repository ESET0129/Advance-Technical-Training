import apiClient from './apiClient';

export const authService = {
  login: async (email, password) => {
    try {
      const response = await apiClient.get('/users', {
        params: {
          email: email,
          password: password,
        },
      });

      if (response.data.length === 1) {
        return response.data[0]; // Return the user object
      } else {
        throw new Error('Invalid email or password');
      }
    } catch (error) {
      console.error('Login error:', error.message);
      throw new Error(error.response?.data?.message || 'Login failed');
    }
  },
};