import { FaClock } from 'react-icons/fa';
import '../styles/Dashboard.css';

export default function StatCard({ title, value, label, icon: Icon = FaClock }) {
  return (
    <div className="stat-card">
      <div className="stat-card-icon">
        <Icon />
      </div>
      <div className="stat-card-info">
        <span className="stat-card-value">{value}</span>
        <span className="stat-card-title">{title}</span>
        <span className="stat-card-label">{label}</span>
      </div>
    </div>
  );
}