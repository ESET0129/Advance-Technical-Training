import React, { useState, useEffect } from 'react';
import toast from 'react-hot-toast';
import apiClient from '../services/apiClient';
import '../styles/Profile.css'; // Reuse form styles

export default function EnterpriseSettingsTab() {
  const [settings, setSettings] = useState({
    dataRetentionPeriod: 30,
    autoLogoutTimer: 30,
    auditLogRetention: 30,
    timezone: 'UTC+0',
    defaultLanguage: 'English',
    currencyFormat: 'INR',
  });
  const [loading, setLoading] = useState(true);

  // Fetch current settings
  useEffect(() => {
    const fetchSettings = async () => {
      try {
        setLoading(true);
        const response = await apiClient.get('/enterpriseSettings');
        setSettings(response.data);
      } catch (error) {
        toast.error('Failed to load settings');
      } finally {
        setLoading(false);
      }
    };
    fetchSettings();
  }, []);

  const handleChange = (e) => {
    setSettings({ ...settings, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      await apiClient.put('/enterpriseSettings', settings);
      toast.success('Settings saved successfully!');
    } catch (error) {
      toast.error('Failed to save settings.');
    } finally {
      setLoading(false);
    }
  };

  if (loading && !settings) {
    return <p>Loading settings...</p>;
  }

  return (
    <form onSubmit={handleSubmit} style={{ maxWidth: '900px', margin: '0 auto' }}>
      {/* Policies & Rules */}
      <h3 style={{ fontSize: '1.25rem', fontWeight: 600, marginBottom: '0.5rem' }}>Policies & Rules</h3>
      <p style={{ color: 'var(--color-text-secondary)', marginBottom: '1.5rem' }}>
        Define enterprise-wide operational constraints and retention policies.
      </p>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem' }}>
        <div className="form-field">
          <label htmlFor="dataRetentionPeriod">Data Retention Period (in days)</label>
          <input
            type="number"
            id="dataRetentionPeriod"
            name="dataRetentionPeriod"
            value={settings.dataRetentionPeriod}
            onChange={handleChange}
          />
        </div>
        <div className="form-field">
          <label htmlFor="autoLogoutTimer">Auto Logout Timer (minutes)</label>
          <input
            type="number"
            id="autoLogoutTimer"
            name="autoLogoutTimer"
            value={settings.autoLogoutTimer}
            onChange={handleChange}
          />
        </div>
        <div className="form-field">
          <label htmlFor="auditLogRetention">Audit Log Retention (in days)</label>
          <input
            type="number"
            id="auditLogRetention"
            name="auditLogRetention"
            value={settings.auditLogRetention}
            onChange={handleChange}
          />
        </div>
      </div>

      {/* Localization */}
      <h3 style={{ fontSize: '1.25rem', fontWeight: 600, marginBottom: '0.5rem', marginTop: '2.5rem' }}>Localization</h3>
      <p style={{ color: 'var(--color-text-secondary)', marginBottom: '1.5rem' }}>
        Set the enterprise's regional preferences and user experience defaults.
      </p>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr 1fr', gap: '1.5rem' }}>
        <div className="form-field">
          <label htmlFor="timezone">Timezone</label>
          <select
            id="timezone"
            name="timezone"
            value={settings.timezone}
            onChange={handleChange}
          >
            <option value="UTC+0">UTC+0</option>
            <option value="UTC+5:30">UTC+5:30 (India)</option>
            <option value="UTC-5">UTC-5 (Eastern Time)</option>
            <option value="UTC-8">UTC-8 (Pacific Time)</option>
          </select>
        </div>
        <div className="form-field">
          <label htmlFor="defaultLanguage">Default Language</label>
          <input
            type="text"
            id="defaultLanguage"
            name="defaultLanguage"
            value={settings.defaultLanguage}
            onChange={handleChange}
          />
        </div>
        <div className="form-field">
          <label htmlFor="currencyFormat">Currency Format</label>
          <input
            type="text"
            id="currencyFormat"
            name="currencyFormat"
            value={settings.currencyFormat}
            onChange={handleChange}
          />
        </div>
      </div>

      <div style={{ maxWidth: '450px', margin: '2rem auto 0' }}>
        <button type="submit" className="form-submit-btn" disabled={loading}>
          {loading ? 'Saving...' : 'Save the changes'}
        </button>
      </div>
    </form>
  );
}