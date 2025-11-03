import { useState } from 'react';
import { useAuthStore } from '../store/authStore';
import apiClient from '../services/apiClient';
import toast from 'react-hot-toast';
import { FaUser } from 'react-icons/fa';

export default function SecurityTab() {
  const { user } = useAuthStore();
  const [passwords, setPasswords] = useState({
    currentPassword: '',
    newPassword: '',
    confirmPassword: '',
  });
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    setPasswords({ ...passwords, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (passwords.newPassword !== passwords.confirmPassword) {
      toast.error('New passwords do not match!');
      return;
    }

    setLoading(true);
    
    try {
      if (user.password !== passwords.currentPassword) {
        throw new Error('Current password is incorrect');
      }
      
      const response = await apiClient.patch(`/users/${user.id}`, {
        password: passwords.newPassword,
      });

      toast.success('Password updated successfully!');
      setPasswords({ currentPassword: '', newPassword: '', confirmPassword: '' });
      
    } catch (error) {
      toast.error(error.message || 'Failed to update password.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <form className="profile-form-container" onSubmit={handleSubmit}>
      <div className="profile-avatar">
        <FaUser />
      </div>

      <div className="form-field">
        <label htmlFor="currentPassword">current password</label>
        <input
          type="password"
          id="currentPassword"
          name="currentPassword"
          value={passwords.currentPassword}
          onChange={handleChange}
        />
      </div>

      <div className="form-field">
        <label htmlFor="newPassword">new password</label>
        <input
          type="password"
          id="newPassword"
          name="newPassword"
          value={passwords.newPassword}
          onChange={handleChange}
        />
      </div>

      <div className="form-field">
        <label htmlFor="confirmPassword">confirm password</label>
        <input
          type="password"
          id="confirmPassword"
          name="confirmPassword"
          value={passwords.confirmPassword}
          onChange={handleChange}
        />
      </div>
      
      <button type="submit" className="form-submit-btn" disabled={loading}>
        {loading ? 'Saving...' : 'Save and continue'}
      </button>
    </form>
  );
}