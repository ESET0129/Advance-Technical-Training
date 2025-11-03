import { useState, useEffect } from 'react';
import { useLogger } from '../hooks/useLogger';
import { Line } from 'react-chartjs-2';

import '../styles/Dashboard.css';
import '../styles/Table.css';

// --- Mock Data ---
const mockChartData = {
  daily: {
    labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
    datasets: [
      {
        label: 'Previous',
        data: [250, 180, 420, 310, 410, 380, 290],
        borderColor: 'rgb(255, 159, 64)',
        backgroundColor: 'rgba(255, 159, 64, 0.2)',
        tension: 0.4,
      },
      {
        label: 'Current',
        data: [300, 210, 390, 380, 280, 430, 350],
        borderColor: 'rgb(192, 75, 192)',
        backgroundColor: 'rgba(192, 75, 192, 0.2)',
        tension: 0.4,
      },
    ],
  },
  weekly: {
    labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
    datasets: [
      {
        label: 'Previous',
        data: [1800, 1900, 1750, 2000],
        borderColor: 'rgb(255, 159, 64)',
        tension: 0.4,
      },
      {
        label: 'Current',
        data: [1950, 1850, 1800, 2100],
        borderColor: 'rgb(192, 75, 192)',
        tension: 0.4,
      },
    ],
  },
  monthly: {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    datasets: [
      {
        label: 'Previous',
        data: [8000, 8200, 7900, 8100, 8300, 8050],
        borderColor: 'rgb(255, 159, 64)',
        tension: 0.4,
      },
      {
        label: 'Current',
        data: [8100, 8300, 7800, 8200, 8400, 8150],
        borderColor: 'rgb(192, 75, 192)',
        tension: 0.4,
      },
    ],
  },
};

const mockTableData = [
  { date: '01 Sep 2025', reading: '25 kWh', difference: '25 kWh', notes: 'hello world' },
  { date: '02 Sep 2025', reading: '52 kWh', difference: '27 kWh', notes: '' },
  { date: '03 Sep 2025', reading: '78 kWh', difference: '26 kWh', notes: '' },
  { date: '04 Sep 2025', reading: '105 kWh', difference: '27 kWh', notes: '' },
];

const chartOptions = {
  responsive: true,
  maintainAspectRatio: false,
  plugins: {
    legend: {
      display: false,
    },
    tooltip: {
      mode: 'index',
      intersect: false,
    },
  },
  scales: {
    y: {
      beginAtZero: true,
    },
  },
};

export default function MeterDataPage() {
  const { logInfo } = useLogger();
  const [chartPeriod, setChartPeriod] = useState('daily');

  useEffect(() => {
    logInfo('Page visit: Meter Data');
  }, [logInfo]);

  return (
    <div className="page-container">
      <h1 style={{ fontSize: '1.75rem', fontWeight: 600, color: '#333' }}>
        Select Date Range
      </h1>

      <div className="chart-container">
        <div className="chart-header">
          <div></div>
          
          <div className="toggle-buttons">
            <button
              className={`toggle-btn ${chartPeriod === 'daily' ? 'active' : ''}`}
              onClick={() => setChartPeriod('daily')}
            >
              Day
            </button>
            <button
              className={`toggle-btn ${chartPeriod === 'weekly' ? 'active' : ''}`}
              onClick={() => setChartPeriod('weekly')}
            >
              Week
            </button>
            <button
              className={`toggle-btn ${chartPeriod === 'monthly' ? 'active' : ''}`}
              onClick={() => setChartPeriod('monthly')}
            >
              Month
            </button>
          </div>
        </div>

        <div className="chart-wrapper-with-legend">
          <div className="chart-content" style={{ height: '350px' }}>
            <Line options={chartOptions} data={mockChartData[chartPeriod]} />
          </div>
          
          <div className="custom-legend">
            <div className="legend-item">
              <span className="legend-color-box" style={{ backgroundColor: 'rgb(255, 159, 64)' }}></span>
              Previous
            </div>
            <div className="legend-item">
              <span className="legend-color-box" style={{ backgroundColor: 'rgb(192, 75, 192)' }}></span>
              Current
            </div>
          </div>
        </div>
      </div>

      <div className="table-wrapper">
        <table className="data-table">
          <thead>
            <tr>
              <th>Date</th>
              <th>Reading</th>
              <th>Difference</th>
              <th>Notes</th>
            </tr>
          </thead>
          <tbody>
            {mockTableData.map((row) => (
              <tr key={row.date}>
                <td>{row.date}</td>
                <td>{row.reading}</td>
                <td>{row.difference}</td>
                <td>{row.notes}</td>
              </tr>
            ))}
            <tr><td>&nbsp;</td><td></td><td></td><td></td></tr>
            <tr><td>&nbsp;</td><td></td><td></td><td></td></tr>
          </tbody>
        </table>
      </div>
    </div>
  );
}