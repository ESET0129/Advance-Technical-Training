import { useState } from 'react';
import '../styles/Profile.css'; // We'll reuse these styles

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

export default function ZoneNotificationTab() {
  const [prefs, setPrefs] = useState({
    email: true,
    sms: false,
    push: true,
  });

  const handleChange = (e) => {
    setPrefs({ ...prefs, [e.target.name]: e.target.checked });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Logic to save settings would go here
    console.log('Saving preferences:', prefs);
    toast.success('Preferences updated!');
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
      
      <button type="submit" className="form-submit-btn">
        Save and continue
      </button>
    </form>
  );
}