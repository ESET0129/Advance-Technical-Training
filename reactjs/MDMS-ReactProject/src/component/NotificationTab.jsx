import { useState, useEffect } from 'react';
import { useAuthStore } from '../store/authStore';
import apiClient from '../services/apiClient';
import toast from 'react-hot-toast';

// Reusable Toggle Switch Component
const ToggleSwitch = ({ label, name, checked, onChange }) => (
  <div className="notification-item">
    <label htmlFor={name}>{label}</label>
    <label className="toggle-switch">
      <input
        type="checkbox"
        id={name}
        name={name}
        checked={checked}
        onChange={onChange}
      />
      <span className="toggle-slider"></span>
    </label>
  </div>
);

export default function NotificationTab() {
  const { user, login: updateUserInStore } = useAuthStore();
  const [prefs, setPrefs] = useState({
    email: false,
    sms: false,
    push: false,
  });
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (user?.notificationPreferences) {
      setPrefs(user.notificationPreferences);
    }
  }, [user]);

  const handleChange = (e) => {
    setPrefs({ ...prefs, [e.target.name]: e.target.checked });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      const response = await apiClient.patch(`/users/${user.id}`, {
        notificationPreferences: prefs,
      });
      updateUserInStore(response.data);
      toast.success('Preferences updated!');
    } catch (error) {
      toast.error('Failed to update preferences.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <form className="notification-settings" onSubmit={handleSubmit}>
      <p style={{ fontSize: '1.25rem', marginBottom: '2rem' }}>
        You can get notifications from
      </p>

      <ToggleSwitch
        label="Email"
        name="email"
        checked={prefs.email}
        onChange={handleChange}
      />
      <ToggleSwitch
        label="SMS"
        name="sms"
        checked={prefs.sms}
        onChange={handleChange}
      />
      <ToggleSwitch
        label="Push"
        name="push"
        checked={prefs.push}
        onChange={handleChange}
      />
      
      <button type="submit" className="form-submit-btn" disabled={loading}>
        {loading ? 'Saving...' : 'Save and continue'}
      </button>
    </form>
  );
}