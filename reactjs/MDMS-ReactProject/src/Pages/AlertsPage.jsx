import { useLogger } from '../hooks/useLogger';
import { useEffect, useState } from 'react';
import { useAuthStore } from '../store/authStore';
import apiClient from '../services/apiClient';
import '../styles/Alerts.css';
import { FaBell } from 'react-icons/fa';

const formatAlertDate = (dateStr) => {
  return new Date(dateStr).toLocaleDateString('en-US', {
    day: 'numeric',
    month: 'long',
    year: 'numeric', // This was the fix
  });
};

const formatAlertTime = (dateStr) => {
  return new Date(dateStr).toLocaleTimeString('en-US', {
    hour: '2-digit',
    minute: '2-digit',
  });
};

export default function AlertsPage() {
  const { logInfo, logError } = useLogger();
  const { user } = useAuthStore();
  
  const [alerts, setAlerts] = useState([]);
  const [selectedAlert, setSelectedAlert] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    logInfo('Page visit: Alerts');
    
    const fetchAlerts = async () => {
      try {
        setLoading(true);
        const response = await apiClient.get('/alerts', {
          params: { userId: user.id }
        });
        setAlerts(response.data);
        if (response.data.length > 0) {
          setSelectedAlert(response.data[0]);
        }
      } catch (error) {
        logError('Failed to fetch alerts', error, { userId: user.id });
      } finally {
        setLoading(false);
      }
    };

    if (user?.id) {
      fetchAlerts();
    }
  }, [logInfo, logError, user]);

  let lastDateHeader = null;

  return (
    <div className="page-container">
      <h1>Alerts & Notifications</h1>

      <div className="alerts-layout">
        
        <div className="alerts-list-container">
          {loading && <p>Loading alerts...</p>}
          {!loading && alerts.map((alert) => {
            const currentDate = formatAlertDate(alert.date);
            let showDateHeader = false;
            
            if (currentDate !== lastDateHeader) {
              showDateHeader = true;
              lastDateHeader = currentDate;
            }
            
            return (
              <div key={alert.id}>
                {showDateHeader && (
                  <h2 className="alert-date-header">{currentDate}</h2>
                )}
                <div
                  className={`alert-card ${selectedAlert?.id === alert.id ? 'active' : ''}`}
                  onClick={() => setSelectedAlert(alert)}
                >
                  <div className="alert-card-icon"><FaBell /></div>
                  <div className="alert-card-content">
                    <h3>{alert.title}</h3>
                    <p>{alert.description}</p>
                  </div>
                </div>
              </div>
            );
          })}
        </div>
        
        <div className="alerts-detail-container">
          {selectedAlert ? (
            <>
              <div className="alert-detail-header">
                <h2>{selectedAlert.title}</h2>
                <div className="alert-detail-timestamp">
                  <div>{formatAlertDate(selectedAlert.date)}</div>
                  <div>{formatAlertTime(selectedAlert.date)}</div>
                </div>
              </div>
              <div className="alert-detail-body">
                {selectedAlert.body.split('\n\n').map((paragraph, index) => (
                  <p key={index}>{paragraph}</p>
                ))}
              </div>
            </>
          ) : (
            <p>Select a notification to read it.</p>
          )}
        </div>
      </div>
    </div>
  );
}
