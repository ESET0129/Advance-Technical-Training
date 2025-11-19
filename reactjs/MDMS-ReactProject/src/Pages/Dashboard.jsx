import { useState, useEffect } from 'react';
import { useAuthStore } from '../store/authStore';
import { useLogger } from '../hooks/useLogger';
import { Line } from 'react-chartjs-2';

import StatCard from '../component/StatCard';
import '../styles/Dashboard.css';
import apiClient from '../services/apiClient';

import toast from 'react-hot-toast';

import { FaChartBar, FaBroadcastTower } from 'react-icons/fa'; 
import '../styles/Enterprise.css'; 
import MapComponent from '../component/MapComponent';
import AlertsFeed from '../component/AlertsFeed';

import { 
  FaBolt, 
  FaFileInvoiceDollar, 
  FaExclamationTriangle, 
  FaHistory,
  FaRegFileAlt,
  FaRegListAlt,
  FaRegBell,
  FaWaveSquare,
  FaChartLine,
  FaExclamationCircle,
  FaPlusCircle,
  FaFileExport
} from 'react-icons/fa';

// --- Mock Data ---
const mockEndUserStats = {
  consumption: '256 kWh',
  billDue: '₹1,230 Due on 12 Oct',
  billLabel: 'This Month\'s Bill',
  pending: '₹120 Pending',
  pendingLabel: 'Outstanding Balance',
  lastPayment: 'Paid ₹1,200 on 10 Sep',
  lastPaymentLabel: 'Last Payment',
};

const chartLabels = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];

const mockChartData = {
  daily: {
    labels: chartLabels,
    datasets: [{
      label: 'Consumption (kWh)',
      data: [65, 59, 80, 81, 56, 55, 40],
      fill: false,
      borderColor: '#1976d2',
      tension: 0.4,
    }],
  },
  weekly: {
    labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
    datasets: [{
      label: 'Consumption (kWh)',
      data: [450, 480, 500, 420],
      fill: false,
      borderColor: '#1976d2',
      tension: 0.4,
    }],
  },
  monthly: {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    datasets: [{
      label: 'Consumption (kWh)',
      data: [1800, 1900, 1750, 2000, 1850, 1950],
      fill: false,
      borderColor: '#1976d2',
      tension: 0.4,
    }],
  },
};

const endUserChartOptions = {
  responsive: true,
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

// --- Dashboard Components ---

const EndUserDashboard = () => {
  const [chartPeriod, setChartPeriod] = useState('daily');
  const { user } = useAuthStore();
  
  return (
    <>
      <div className="page-header">
        <div>
          <h1 className="page-title">Welcome, {user?.name || 'xyz'}</h1>
          <p className="page-subtitle">As of {new Date().toLocaleDateString('en-US', { day: 'numeric', month: 'long', year: 'numeric' })}</p>
          <p className="page-subtitle">Zone: Bangalore North</p>
        </div>
        <div className="page-meta">
          <p>Last synced at 10:45 AM</p>
          <p>Data Source: Smart Meter #1023</p>
        </div>
      </div>
      
      <div className="stats-grid">
        <StatCard title="Current Consumption" value={mockEndUserStats.consumption} icon={FaBolt} />
        <StatCard title={mockEndUserStats.billLabel} value={mockEndUserStats.billDue} icon={FaFileInvoiceDollar} />
        <StatCard title={mockEndUserStats.pendingLabel} value={mockEndUserStats.pending} icon={FaExclamationTriangle} />
        <StatCard title={mockEndUserStats.lastPaymentLabel} value={mockEndUserStats.lastPayment} icon={FaHistory} />
      </div>
      
      <div className="chart-container">
        <div className="chart-header">
          <h2 className="chart-title">Electricity Consumption Overview</h2>
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
        <div className="chart-content" style={{height: '350px'}}>
          <Line options={endUserChartOptions} data={mockChartData[chartPeriod]} />
        </div>
      </div>

      <div className="quick-actions-container">
        <h2 className="quick-actions-title">Quick Actions</h2>
        <div className="actions-row">
          <button className="action-btn"><FaFileInvoiceDollar /> Pay Bill</button>
          <button className="action-btn"><FaRegFileAlt /> View Bill History</button>
          <button className="action-btn"><FaRegListAlt /> View Detailed Usage</button>
          <button className="action-btn"><FaRegBell /> Manage Notifications</button>
        </div>
      </div>
    </>
  );
};

const ZoneDashboard = () => {
  const { user } = useAuthStore();
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);
  const [chartPeriod, setChartPeriod] = useState('week');

  useEffect(() => {
    const fetchZoneStats = async () => {
      if (!user?.zoneId) return;
      try {
        setLoading(true);
        //const response = await apiClient.get(`/zoneStats/${user.zoneId}`);
        const response = await apiClient.get(`/zoneStats`);
        setStats(response.data[user.zoneId]);
      } catch (error) {
        console.error("Failed to fetch zone stats:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchZoneStats();
  }, [user]);

  const chartData = {
    labels: stats?.analytics?.[chartPeriod]?.labels || [],
    datasets: [{
      label: 'Avg Zone Consumption',
      data: stats?.analytics?.[chartPeriod]?.data || [],
      fill: false,
      borderColor: '#c04bc0', // Purple from design
      tension: 0.4,
    }],
  };
  
  const zoneChartOptions = {
    responsive: true,
    plugins: {
      legend: { display: false },
      tooltip: { mode: 'index', intersect: false },
    },
    scales: { y: { beginAtZero: true } },
  };

  if (loading || !stats) {
    return <p>Loading Zone Dashboard...</p>;
  }

  return (
    <>
      <h1 className="page-title" style={{marginBottom: '1.5rem'}}>Zone Dashboard</h1>
      
      <div className="stats-grid">
        <StatCard title="Active meters" value={stats.activeMeters} icon={FaWaveSquare} />
        <StatCard title="Avg usage" value={stats.avgUsage} icon={FaChartLine} />
        <StatCard title="Pending alert" value={stats.pendingAlerts} icon={FaExclamationCircle} />
      </div>

      <div className="chart-container">
        <div className="chart-header">
          <h2 className="chart-title">Analytics Chart</h2>
          <div className="toggle-buttons">
            <button
              className={`toggle-btn ${chartPeriod === 'week' ? 'active' : ''}`}
              onClick={() => setChartPeriod('week')}
            >
              Week
            </button>
            <button
              className={`toggle-btn ${chartPeriod === 'month' ? 'active' : ''}`}
              onClick={() => setChartPeriod('month')}
            >
              Month
            </button>
          </div>
        </div>
        <div className="chart-content" style={{ height: '350px' }}>
          <Line options={zoneChartOptions} data={chartData} />
        </div>
      </div>
      
      <div className="actions-row" style={{ marginTop: '2rem' }}>
        <button 
        className="action-btn" 
        onClick={() => toast('"Add Meter" form would open here!')}
      >
        <FaPlusCircle /> Add meter
      </button>
        <button 
        className="action-btn" 
        onClick={() => toast.success('Generating report...')}
      >
        <FaFileExport /> Generate Report
      </button>
      </div>
    </>
  );
};

const EnterpriseDashboard = () => {
  const [stats, setStats] = useState(null);
  const [zones, setZones] = useState([]);
  const [alerts, setAlerts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        // Fetch all data in parallel
        const [statsRes, zonesRes, alertsRes] = await Promise.all([
          apiClient.get('/enterpriseStats'),
          apiClient.get('/mapZones'),
          apiClient.get('/recentAlerts')
        ]);
        
        setStats(statsRes.data);
        setZones(zonesRes.data);
        setAlerts(alertsRes.data);

      } catch (error) {
        console.error("Failed to fetch enterprise data:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  if (loading || !stats) {
    return <p>Loading Enterprise Dashboard...</p>;
  }

  return (
    <>
      <h1 className="page-title" style={{marginBottom: '1.5rem'}}>Enterprise Dashboard</h1>
      
      <div className="stats-grid">
        <StatCard title="Total zones" value={stats.totalZones} icon={FaChartBar} />
        <StatCard title="Total meters" value={stats.totalMeters} icon={FaWaveSquare} />
        <StatCard title="Critical Alerts" value={stats.criticalAlerts} icon={FaExclamationCircle} />
        <StatCard title="Average Consumption per Zone" value={`${stats.avgConsumptionPerZone}%`} icon={FaBroadcastTower} />
      </div>

      <div className="dashboard-grid">
        <MapComponent zones={zones} />
        <AlertsFeed alerts={alerts} />
      </div>
    </>
  );
};

// --- Main Page Component ---
export default function DashboardPage() {
  const { user } = useAuthStore();
  const { logInfo } = useLogger();

  useEffect(() => {
    logInfo('Page visit: Dashboard', { role: user?.role });
  }, [logInfo, user]);
  
  const renderDashboardByRole = () => {
    switch (user?.role) {
      case 'EndUser':
        return <EndUserDashboard />;
      case 'ZoneLevel':
        return <ZoneDashboard />;
      case 'EnterpriseLevel':
        return <EnterpriseDashboard />;
      default:
        return <p>Loading dashboard...</p>;
    }
  };
  
  return (
    <div>
      {renderDashboardByRole()}
    </div>
  );
}