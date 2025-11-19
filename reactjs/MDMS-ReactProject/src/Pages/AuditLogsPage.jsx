import { useState, useEffect } from 'react';
import { useLogger } from '../hooks/useLogger';
import apiClient from '../services/apiClient';
import toast from 'react-hot-toast';

// Import components & styles
import ActionMenu from '../component/ActionMenu';
import Pagination from '../component/Pagination';
import '../styles/Table.css';
import '../styles/ManagementPage.css';

// Import Icons
import { 
  FaEye, 
  FaPencilAlt, 
  FaTrash,
  FaFileExport,
  FaSearch
} from 'react-icons/fa';

const LOGS_PER_PAGE = 10;

export default function AuditLogsPage() {
  const { logInfo, logError } = useLogger();
  
  // Data State
  const [logs, setLogs] = useState([]);
  const [userMap, setUserMap] = useState({});
  const [loading, setLoading] = useState(true);

  // Pagination State
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  
  // Filter State
  const [filter, setFilter] = useState('');

  useEffect(() => {
    logInfo('Page visit: Audit Logs', { page: currentPage, filter });

    const fetchData = async () => {
      try {
        setLoading(true);
        
        // Build API query params
        const params = { 
          _page: currentPage,
          _limit: LOGS_PER_PAGE,
          _sort: 'id',
          _order: 'desc' // Show newest logs first
        };
        
        if (filter) {
          // This will search all fields for the filter text
          params.q = filter;
        }

        const [logsRes, usersRes] = await Promise.all([
          apiClient.get('/auditLogs', { params }),
          apiClient.get('/users') // Get all users for mapping
        ]);
        
        const totalCount = logsRes.headers['x-total-count'];
        setTotalPages(Math.ceil(totalCount / LOGS_PER_PAGE));
        
        const uMap = usersRes.data.reduce((acc, user) => {
          acc[user.id] = user.name;
          return acc;
        }, {});

        setLogs(logsRes.data);
        setUserMap(uMap);
        
      } catch (error) {
        logError('Failed to fetch audit logs', error);
        toast.error('Could not load audit logs.');
      } finally {
        setLoading(false);
      }
    };
    
    // Debounce search
    const delayDebounceFn = setTimeout(fetchData, 500);
    return () => clearTimeout(delayDebounceFn);
    
  }, [logInfo, logError, currentPage, filter]);

  // --- Action Menu Items ---
  const getActionItems = (log) => [
    {
      label: 'View',
      icon: <FaEye />,
      onClick: () => toast.success(`Viewing log ${log.id}`),
    },
    {
      label: 'Edit',
      icon: <FaPencilAlt />,
      onClick: () => toast.info(`Editing log ${log.id}`),
    },
    {
      label: 'Delete',
      icon: <FaTrash />,
      onClick: () => toast.error(`Deleting log ${log.id}`),
    },
  ];

  return (
    <div className="page-container">
      <div className="page-header-controls">
        <h1 className="page-title">Audit Logs</h1>
        <div className="search-and-actions">
          <button className="action-btn" onClick={() => toast.success('Exporting CSV...')}>
            <FaFileExport /> Export as CSV
          </button>
          <button className="action-btn" onClick={() => toast.info('PDF export coming soon!')}>
            <FaFileExport /> Export as PDF
          </button>
        </div>
      </div>

      {/* --- Filter Bar --- */}
      <div className="search-bar" style={{ marginBottom: '1.5rem', maxWidth: '400px' }}>
        <FaSearch />
        <input 
          type="text" 
          placeholder="Filter by user, resource, status..."
          value={filter}
          onChange={(e) => setFilter(e.target.value)}
        />
      </div>
      
      {/* --- Table --- */}
      <div className="table-wrapper">
        <table className="data-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Timestamp</th>
              <th>User</th>
              <th>Resource</th>
              <th>Status</th>
              <th>More Actions</th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr><td colSpan="6">Loading logs...</td></tr>
            ) : (
              logs.map((log) => (
                <tr key={log.id}>
                  <td>{log.id}</td>
                  <td>{new Date(log.timestamp).toLocaleString()}</td>
                  <td>{userMap[log.userId] || `User ID: ${log.userId}`}</td>
                  <td>{log.resource} ({log.resourceId})</td>
                  <td>{log.status}</td>
                  <td>
                    <ActionMenu items={getActionItems(log)} />
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      {/* --- Pagination --- */}
      <Pagination 
        currentPage={currentPage}
        totalPages={totalPages}
        onPageChange={setCurrentPage}
      />
    </div>
  );
}