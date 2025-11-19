import { useState, useEffect, useMemo } from 'react';
import { useAuthStore } from '../store/authStore';
import { useLogger } from '../hooks/useLogger';
import apiClient from '../services/apiClient';
import toast from 'react-hot-toast';
import { Line } from 'react-chartjs-2'; // Import Line chart

// Import components & styles
import ActionMenu from '../component/ActionMenu';
import Pagination from '../component/Pagination';
import EditMeterModal from '../component/EditMeterModal';
import '../styles/Table.css';
import '../styles/Dashboard.css'; // For chart and buttons
import '../styles/ManagementPage.css'; // For search bars
import '../styles/Modal.css'; 

// Import Icons
import { 
  FaEye, 
  FaPencilAlt, 
  FaToggleOn, 
  FaFileCsv, 
  FaFileExport, 
  FaExclamationTriangle,
  FaSearch,
  FaTrash,
  FaChevronLeft, 
  FaChevronRight
} from 'react-icons/fa';

const METERS_PER_PAGE = 10;

// ZONE-LEVEL VIEW

const ZoneMeterManagement = () => {
  const { user } = useAuthStore();
  const { logInfo, logError } = useLogger();
  
  const [meters, setMeters] = useState([]);
  const [ownerMap, setOwnerMap] = useState({});
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [editingMeter, setEditingMeter] = useState(null);

  useEffect(() => {
    logInfo('Page visit: Zone Meter Management', { page: currentPage });

    const fetchData = async () => {
      if (!user?.zoneId) return;
      try {
        setLoading(true);
        
        const metersRes = await apiClient.get('/meters', {
          params: { 
            zoneId: user.zoneId,
            _page: currentPage,
            _limit: METERS_PER_PAGE
          }
        });
        
        const totalCount = metersRes.headers['x-total-count'];
        setTotalPages(Math.ceil(totalCount / METERS_PER_PAGE));
        
        const usersRes = await apiClient.get('/users');
        const userMap = usersRes.data.reduce((acc, user) => {
          acc[user.id] = user.name;
          return acc;
        }, {});

        setMeters(metersRes.data);
        setOwnerMap(userMap);
        
      } catch (error) {
        logError('Failed to fetch meter data', error);
        toast.error('Could not load meter data.');
      } finally {
        setLoading(false);
      }
    };
    
    fetchData();
  }, [user, logInfo, logError, currentPage]);

  const handleEdit = (meter) => {
    setEditingMeter(meter);
    setIsEditModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsEditModalOpen(false);
    setEditingMeter(null);
  };

  const handleSaveMeter = (updatedMeter) => {
    setMeters(meters.map(m => m.id === updatedMeter.id ? updatedMeter : m));
  };

  const getActionItems = (meter) => [
    { label: 'View', icon: <FaEye />, onClick: () => toast.success(`Viewing meter ${meter.id}`) },
    { label: 'Edit', icon: <FaPencilAlt />, onClick: () => handleEdit(meter) },
    { label: meter.status === 'Active' ? 'De-Activate' : 'Activate', icon: <FaToggleOn />, onClick: () => toast.success(`Toggled status for meter ${meter.id}`) },
  ];

  return (
    <div className="page-container">
      <h1 className="page-title" style={{ marginBottom: '1.5rem' }}>
        Meter Management (Zone: {user.zoneId})
      </h1>
      
      <div className="table-wrapper">
        <table className="data-table">
          <thead>
            <tr>
              <th>Meter ID</th>
              <th>Zone</th>
              <th>Owner</th>
              <th>Status</th>
              <th>Last Reading</th>
              <th>More Actions</th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr><td colSpan="6">Loading meters...</td></tr>
            ) : (
              meters.map((meter) => (
                <tr key={meter.id}>
                  <td>{meter.id}</td>
                  <td>{meter.zoneId}</td>
                  <td>{ownerMap[meter.userId] || 'N/A'}</td>
                  <td>{meter.status}</td>
                  <td>{new Date(meter.lastReadingTimestamp).toLocaleString()}</td>
                  <td><ActionMenu items={getActionItems(meter)} /></td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <Pagination 
        currentPage={currentPage}
        totalPages={totalPages}
        onPageChange={setCurrentPage}
      />

      <div className="bulk-operations" style={{ marginTop: '1rem' }}>
        <h2 style={{ fontSize: '1.25rem', fontWeight: 600, marginBottom: '1rem' }}>
          Bulk operations
        </h2>
        <div className="actions-row">
          <button className="action-btn" onClick={() => toast.info('Opening import wizard...')}><FaFileCsv /> Import CSV</button>
          <button className="action-btn" onClick={() => toast.success('Exporting CSV...')}><FaFileExport /> Export CSV</button>
          <button className="action-btn" onClick={() => toast.error('De-activation modal would open here.')}><FaExclamationTriangle /> De-Activate meters</button>
        </div>
      </div>

      {isEditModalOpen && (
        <EditMeterModal
          meter={editingMeter}
          onClose={handleCloseModal}
          onSave={handleSaveMeter}
        />
      )}
    </div>
  );
};


//  ENTERPRISE-LEVEL VIEW 

const EnterpriseMeterManagement = () => {
  const { logInfo, logError } = useLogger();

  // Data State
  const [meters, setMeters] = useState([]);
  const [ownerMap, setOwnerMap] = useState({});
  const [loading, setLoading] = useState(true);
  const [chartData, setChartData] = useState(null);

  // Filter State
  const [zoneQuery, setZoneQuery] = useState('');
  const [ownerQuery, setOwnerQuery] = useState('');
  
  // Pagination State
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  
  // Chart State
  const [currentYear, setCurrentYear] = useState(2025);

  // Fetch Table Data (Meters + Users)
  useEffect(() => {
    logInfo('Page visit: Global Meter Management', { page: currentPage, zoneQuery });

    const fetchTableData = async () => {
      try {
        setLoading(true);
        
        // Build API query params
        const params = {
          _page: currentPage,
          _limit: METERS_PER_PAGE,
          _sort: 'id'
        };
        if (zoneQuery) {
          params.zoneId_like = zoneQuery; // 'zoneId_like' for partial search
        }
        
        const [metersRes, usersRes] = await Promise.all([
          apiClient.get('/meters', { params }),
          apiClient.get('/users') // Get all users for owner mapping
        ]);
        
        const totalCount = metersRes.headers['x-total-count'];
        setTotalPages(Math.ceil(totalCount / METERS_PER_PAGE));
        
        const userMap = usersRes.data.reduce((acc, user) => {
          acc[user.id] = user.name;
          return acc;
        }, {});
        
        setMeters(metersRes.data);
        setOwnerMap(userMap);
        
      } catch (error) {
        logError('Failed to fetch global meter data', error);
        toast.error('Could not load meter data.');
      } finally {
        setLoading(false);
      }
    };
    
    // Debounce search to avoid spamming API
    const delayDebounceFn = setTimeout(fetchTableData, 500);
    return () => clearTimeout(delayDebounceFn);
    
  }, [logInfo, logError, currentPage, zoneQuery]);

  // Fetch Chart Data
  useEffect(() => {
    const fetchChartData = async () => {
      try {
        // 1. Fetch the PARENT object, not the child
        const res = await apiClient.get(`/energyUsageTrend`);
        
        // 2. Select the correct year's data from the response
        const yearData = res.data[currentYear];
        
        // 3. Set the chart data using that year's data
        setChartData({
          labels: yearData.labels,
          datasets: [{
            label: 'Energy Usage',
            data: yearData.data,
            borderColor: 'var(--color-primary)',
            tension: 0.4,
            fill: false // 'fill: false' is correct for a line chart
          }]
        });
      } catch (error) {
        logError('Failed to fetch chart data', error);
        setChartData(null);
      }
    };
    fetchChartData();
  }, [currentYear, logError]);

  // Client-side filter for Owner (since we can't search by owner name in API easily)
  const filteredMeters = useMemo(() => {
    if (!ownerQuery) {
      return meters; // No filter
    }
    return meters.filter(meter => {
      const ownerName = ownerMap[meter.userId] || '';
      return ownerName.toLowerCase().includes(ownerQuery.toLowerCase());
    });
  }, [meters, ownerQuery, ownerMap]);

  // Action Menu Items for Enterprise
  const getActionItems = (meter) => [
    { label: 'View', icon: <FaEye />, onClick: () => toast.success(`Viewing meter ${meter.id}`) },
    { label: 'Edit', icon: <FaPencilAlt />, onClick: () => toast.info(`Editing meter ${meter.id}`) },
    { label: 'Delete', icon: <FaTrash />, onClick: () => toast.error(`Deleting meter ${meter.id}`) },
  ];
  
  const chartOptions = { /* ... (same as dashboard) ... */ };

  return (
    <div className="page-container">
      <h1 className="page-title">Global Meter Management</h1>
      
      {/* --- Filters --- */}
      <div className="search-and-actions" style={{ marginBottom: '1.5rem' }}>
        <div className="search-bar">
          <FaSearch />
          <input 
            type="text" 
            placeholder="Search by Zone (e.g., Z-NORTH)"
            value={zoneQuery}
            onChange={(e) => setZoneQuery(e.target.value)}
          />
        </div>
        <div className="search-bar">
          <FaSearch />
          <input 
            type="text" 
            placeholder="Search by Owner (e.g., John)"
            value={ownerQuery}
            onChange={(e) => setOwnerQuery(e.target.value)}
          />
        </div>
      </div>
      
      {/* --- Table --- */}
      <div className="table-wrapper">
        <table className="data-table">
          {/* ... (thead is same as Zone view) ... */}
          <thead>
            <tr>
              <th>Meter ID</th>
              <th>Zone</th>
              <th>Owner</th>
              <th>Status</th>
              <th>Last Reading</th>
              <th>More Actions</th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr><td colSpan="6">Loading meters...</td></tr>
            ) : (
              filteredMeters.map((meter) => (
                <tr key={meter.id}>
                  <td>{meter.id}</td>
                  <td>{meter.zoneId}</td>
                  <td>{ownerMap[meter.userId] || 'N/A'}</td>
                  <td>{meter.status}</td>
                  <td>{new Date(meter.lastReadingTimestamp).toLocaleString()}</td>
                  <td><ActionMenu items={getActionItems(meter)} /></td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <Pagination 
        currentPage={currentPage}
        totalPages={totalPages}
        onPageChange={setCurrentPage}
      />
      
      {/* --- Chart --- */}
      <div className="chart-container" style={{ marginTop: '2rem' }}>
        <div className="chart-header">
          <h2 className="chart-title">Each zones Trend of energy usage over time</h2>
          <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
            <button className="pagination-btn" onClick={() => setCurrentYear(y => y - 1)}>
              <FaChevronLeft />
            </button>
            <span style={{color: 'var(--color-text-primary)'}}>{currentYear}</span>
            <button className="pagination-btn" onClick={() => setCurrentYear(y => y + 1)}>
              <FaChevronRight />
            </button>
          </div>
        </div>
        <div className="chart-content" style={{ height: '300px' }}>
          {chartData ? <Line options={chartOptions} data={chartData} /> : <p>Loading chart data...</p>}
        </div>
      </div>
    </div>
  );
};


//  MAIN COMPONENT 

export default function MeterManagementPage() {
  const { user } = useAuthStore();
  
  if (!user) {
    return <p>Loading...</p>; // Or a loader component
  }

  // Render the correct component based on user role
  switch (user.role) {
    case 'ZoneLevel':
      return <ZoneMeterManagement />;
    case 'EnterpriseLevel':
      return <EnterpriseMeterManagement />;
    default:
      // EndUsers shouldn't be here, redirect or show access denied
      return <div>Access Denied.</div>; 
  }
}