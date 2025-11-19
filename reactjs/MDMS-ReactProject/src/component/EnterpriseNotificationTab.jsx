import React, { useState, useEffect } from 'react';
import toast from 'react-hot-toast';
import apiClient from '../services/apiClient';
import '../styles/Profile.css'; // Reuse toggle styles

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

export default function EnterpriseNotificationTab() {
  const [prefs, setPrefs] = useState({
    email: false,
    sms: false,
    push: false,
  });
  const [loading, setLoading] = useState(false);

  // Fetch current settings
  useEffect(() => {
    const fetchSettings = async () => {
      try {
        const response = await apiClient.get('/enterpriseSettings');
        setPrefs(response.data.notifications || { email: false, sms: false, push: false });
      } catch (error) {
        toast.error('Failed to load notification settings');
      }
    };
    fetchSettings();
  }, []);

  const handleChange = (e) => {
    setPrefs({ ...prefs, [e.target.name]: e.target.checked });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      // We must PATCH here, not PUT, to only update the 'notifications' key
      await apiClient.patch('/enterpriseSettings', {
        notifications: prefs,
      });
      toast.success('Notification preferences updated!');
    } catch (error) {
      toast.error('Failed to update preferences.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <form className="notification-settings" onSubmit={handleSubmit}>
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