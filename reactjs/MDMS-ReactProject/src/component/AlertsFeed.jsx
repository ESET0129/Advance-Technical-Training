import { FaExpand } from 'react-icons/fa';

export default function AlertsFeed({ alerts }) {
  return (
    <div className="alerts-feed-container">
      <div className="alerts-feed-header">
        <h3>Recent Alerts</h3>
        <button><FaExpand /></button>
      </div>
      <div className="alerts-feed-list">
        {alerts.map(alert => (
          <div key={alert.id} className="alert-feed-item">
            <h4>{alert.title}</h4>
            <p>{alert.text}</p>
          </div>
        ))}
      </div>
    </div>
  );
}