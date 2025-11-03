import { useState, useEffect } from 'react';
import { useLogger } from '../hooks/useLogger';
import '../styles/Profile.css';

import ProfileTab from '../components/ProfileTab';
import SecurityTab from '../components/SecurityTab';
import NotificationTab from '../components/NotificationTab';

export default function ProfilePage() {
  const { logInfo } = useLogger();
  const [activeTab, setActiveTab] = useState('profile');

  useEffect(() => {
    logInfo(`Page visit: Profile & Settings (Tab: ${activeTab})`);
  }, [logInfo, activeTab]);

  const renderTabContent = () => {
    switch (activeTab) {
      case 'profile':
        return <ProfileTab />;
      case 'security':
        return <SecurityTab />;
      case 'notification':
        return <NotificationTab />;
      default:
        return <ProfileTab />;
    }
  };

  return (
    <div className="page-container">
      <h1>Profile & Settings</h1>

      <nav className="tabs-nav">
        <button
          className={`tab-btn ${activeTab === 'profile' ? 'active' : ''}`}
          onClick={() => setActiveTab('profile')}
        >
          Profile
        </button>
        <button
          className={`tab-btn ${activeTab === 'security' ? 'active' : ''}`}
          onClick={() => setActiveTab('security')}
        >
          Security
        </button>
        <button
          className={`tab-btn ${activeTab === 'notification' ? 'active' : ''}`}
          onClick={() => setActiveTab('notification')}
        >
          Notification
        </button>
      </nav>

      <div className="tab-content">
        {renderTabContent()}
      </div>
    </div>
  );
}