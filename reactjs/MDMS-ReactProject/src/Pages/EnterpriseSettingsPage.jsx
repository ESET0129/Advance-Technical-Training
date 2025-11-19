import React, { useState, useEffect } from 'react';
import { useLogger } from '../hooks/useLogger';
import '../styles/Profile.css'; // We reuse the tab styles

// Import the new tab components
import EnterpriseSettingsTab from '../component/EnterpriseSettingsTab';
import EnterpriseNotificationTab from '../component/EnterpriseNotificationTab';

export default function EnterpriseSettingsPage() {
  const { logInfo } = useLogger();
  const [activeTab, setActiveTab] = useState('settings');

  useEffect(() => {
    logInfo(`Page visit: Setting & Configuration (Tab: ${activeTab})`);
  }, [logInfo, activeTab]);

  const renderTabContent = () => {
    switch (activeTab) {
      case 'settings':
        return <EnterpriseSettingsTab />;
      case 'notification':
        return <EnterpriseNotificationTab />;
      default:
        return <EnterpriseSettingsTab />;
    }
  };

  return (
    <div className="page-container">
      <h1>Setting and Configuration</h1>
      <p style={{marginTop: '-1.5rem', marginBottom: '2rem', color: 'var(--color-text-secondary)'}}>
        Manage organization-wide configurations and integrations
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