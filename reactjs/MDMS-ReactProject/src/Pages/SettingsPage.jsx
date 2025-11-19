import { useState, useEffect } from 'react';
import { useLogger } from '../hooks/useLogger';
import '../styles/Profile.css'; // We reuse the tab styles

// Import the new tab components
import ZoneSettingsTab from '../component/ZoneSettingsTab';
import ZoneNotificationTab from '../component/ZoneNotificationTab';

export default function SettingsPage() {
  const { logInfo } = useLogger();
  const [activeTab, setActiveTab] = useState('settings'); // Default to 'settings'

  useEffect(() => {
    logInfo(`Page visit: Setting & Notifications (Tab: ${activeTab})`);
  }, [logInfo, activeTab]);

  const renderTabContent = () => {
    switch (activeTab) {
      case 'settings':
        return <ZoneSettingsTab />;
      case 'notification':
        return <ZoneNotificationTab />;
      default:
        return <ZoneSettingsTab />;
    }
  };

  return (
    <div className="page-container">
      <h1>User Management</h1>
      <p style={{marginTop: '-1.5rem', marginBottom: '2rem', color: 'var(--color-text-secondary)'}}>
        Manage your alert rules and communication preferences.
      </p>

      {/* Tab Navigation */}
      <nav className="tabs-nav">
        <button
          className={`tab-btn ${activeTab === 'settings' ? 'active' : ''}`}
          onClick={() => setActiveTab('settings')}
        >
          Settings
        </button>
        <button
          className={`tab-btn ${activeTab === 'notification' ? 'active' : ''}`}
          onClick={() => setActiveTab('notification')}
        >
          Notification
        </button>
      </nav>

      {/* Tab Content */}
      <div className="tab-content">
        {renderTabContent()}
      </div>
    </div>
  );
}