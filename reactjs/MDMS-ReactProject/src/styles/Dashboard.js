/* src/styles/Dashboard.css */

/* --- Page Header --- */
.page-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 1.5rem;
}

.page-title {
  font-size: 1.75rem;
  font-weight: 600;
  color: #333;
}

.page-subtitle {
  font-size: 1rem;
  color: #777;
  margin-top: 0.25rem;
}

.page-meta {
  text-align: right;
  font-size: 0.875rem;
  color: #555;
}

/* --- Stat Card --- */
.stats-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
  gap: 1.5rem;
  margin-bottom: 2rem;
}

.stat-card {
  display: flex;
  align-items: center;
  gap: 1.5rem;
  background: #ffffff;
  padding: 1.5rem;
  border-radius: 8px;
  border: 1px solid #e0e0e0;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.03);
}

.stat-card-icon {
  font-size: 1.75rem;
  color: #1976d2;
}

.stat-card-info {
  display: flex;
  flex-direction: column;
}

.stat-card-value {
  font-size: 1.5rem;
  font-weight: 600;
  color: #333;
}

.stat-card-title {
  font-size: 1rem;
  color: #333;
  font-weight: 500;
}

.stat-card-label {
  font-size: 0.875rem;
  color: #777;
}

/* --- Chart Wrapper --- */
.chart-container {
  background: #ffffff;
  padding: 1.5rem;
  border-radius: 8px;
  border: 1px solid #e0e0e0;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.03);
  margin-bottom: 2rem;
}

.chart-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1.5rem;
}

.chart-title {
  font-size: 1.25rem;
  font-weight: 600;
  color: #333;
}

.toggle-buttons {
  display: flex;
  background: #f4f4f4;
  border-radius: 20px;
}

.toggle-btn {
  padding: 0.5rem 1rem;
  font-size: 0.875rem;
  font-weight: 500;
  border: none;
  background: transparent;
  color: #777;
  cursor: pointer;
  border-radius: 20px;
  transition: all 0.2s ease;
}

.toggle-btn.active {
  background: #1976d2;
  color: #ffffff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

/* --- Quick Actions --- */
.quick-actions-container {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.quick-actions-title {
  font-size: 1.25rem;
  font-weight: 600;
  color: #333;
}

.actions-row {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
}

.action-btn {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.6rem 1rem;
  font-size: 0.875rem;
  font-weight: 500;
  background: #ffffff;
  border: 1px solid #ccc;
  border-radius: 20px;
  cursor: pointer;
  transition: all 0.2s ease;
}

.action-btn:hover {
  background: #f7f7f7;
  border-color: #aaa;
}

/* --- For MeterDataPage Legend --- */
.chart-wrapper-with-legend {
  display: flex;
  flex-direction: row;
  gap: 2rem;
}

.chart-wrapper-with-legend .chart-content {
  flex: 1; /* Chart takes up available space */
}

.custom-legend {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  padding-top: 1rem;
}

.legend-item {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  font-size: 0.9rem;
  font-weight: 500;
  color: #555;
}

.legend-color-box {
  width: 16px;
  height: 16px;
  border-radius: 4px;
}