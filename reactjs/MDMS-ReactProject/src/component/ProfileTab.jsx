import { useState, useEffect } from 'react';
import { useAuthStore } from '../store/authStore';
import apiClient from '../services/apiClient';
import toast from 'react-hot-toast';
import { FaUser, FaEdit } from 'react-icons/fa';

export default function ProfileTab() {
  const { user, login: updateUserInStore } = useAuthStore();
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    mobile: '',
  });
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (user) {
      setFormData({
        name: user.name || '',
        email: user.email || '',
        mobile: user.mobile || '',
      });
    }
  }, [user]);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      const response = await apiClient.patch(`/users/${user.id}`, formData);
      updateUserInStore(response.data);
      toast.success('Profile updated successfully!');
    } catch (error) {
      toast.error('Failed to update profile.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <form className="profile-form-container" onSubmit={handleSubmit}>
      <div className="profile-avatar">
        <FaUser />
        <button type="button" className="avatar-edit-btn" aria-label="Edit avatar">
          <FaEdit />
        </button>
      </div>

      <div className="form-field">
        <label htmlFor="name">name</label>
        <input
          type="text"
          id="name"
          name="name"
          value={formData.name}
          onChange={handleChange}
        />
      </div>

      <div className="form-field">
        <label htmlFor="email">email</label>
        <input
          type="email"
          id="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
        />
      </div>

      <div className="form-field">
        <label htmlFor="mobile">mobile no.</label>
        <input
          type="tel"
          id="mobile"
          name="mobile"
          value={formData.mobile}
          onChange={handleChange}
        />
      </div>
      
      <button type="submit" className="form-submit-btn" disabled={loading}>
        {loading ? 'Saving...' : 'Save and continue'}
      </button>
    </form>
  );
}