import { useState, useEffect } from 'react';
import { useLogger } from '../hooks/useLogger';
import apiClient from '../services/apiClient';
import toast from 'react-hot-toast';
import { Bar } from 'react-chartjs-2'; // <-- Import Bar chart

// Import components & styles
import ActionMenu from '../component/ActionMenu';
import Pagination from '../component/Pagination';
import EnterpriseInviteUserModal from '../component/EnterpriseInviteUserModal';
import '../styles/Table.css';
import '../styles/ManagementPage.css';
import '../styles/Modal.css';

// Import Icons
import { 
  FaEye, 
  FaPencilAlt, 
  FaTrash,
  FaUserPlus,
  FaFileExport,
  FaChevronLeft, 
  FaChevronRight
} from 'react-icons/fa';

const USERS_PER_PAGE = 10;

// Chart options
const barChartOptions = {
  responsive: true,
  plugins: { legend: { display: false } },
  scales: { y: { beginAtZero: true, max: 100 } },
};

export default function UserRoleManagementPage() {
  const { logInfo, logError } = useLogger();
  
  // Data State
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [chartData, setChartData] = useState(null);

  // Pagination State
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  
  // Modal State
  const [isModalOpen, setIsModalOpen] = useState(false);
  
  // Chart State
  const [currentYear, setCurrentYear] = useState(2025);

  // Fetch Table Data
  useEffect(() => {
    logInfo('Page visit: User & Role Management', { page: currentPage });

    const fetchData = async () => {
      try {
        setLoading(true);
        
        // Fetch ALL paginated users
        const usersRes = await apiClient.get('/users', {
          params: { 
            _page: currentPage,
            _limit: USERS_PER_PAGE,
            _sort: 'id'
          }
        });
        
        const totalCount = usersRes.headers['x-total-count'];
        setTotalPages(Math.ceil(totalCount / USERS_PER_PAGE));
        setUsers(usersRes.data);
        
      } catch (error) {
        logError('Failed to fetch user data', error);
        toast.error('Could not load user data.');
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [logInfo, logError, currentPage]); // Re-fetch when currentPage changes
  
  // Fetch Chart Data
  // Fetch Chart Data
  useEffect(() => {
    const fetchChartData = async () => {
      try {
        // 1. Fetch the PARENT object, not the child
        const res = await apiClient.get(`/userStatsByYear`);
        
        // 2. Select the correct year's data from the response
        const yearData = res.data[currentYear];
        
        // 3. Set the chart data using that year's data
        setChartData({
          labels: yearData.labels,
          datasets: [{
            label: 'User Status',
            data: yearData.data,
            backgroundColor: [
              'rgba(106, 27, 154, 0.6)', // Active (Purple)
              'rgba(211, 47, 47, 0.6)'  // De-Activated (Red)
            ],
            borderRadius: 10,
          }]
        });
      } catch (error) {
        logError('Failed to fetch chart data', error);
        setChartData(null);
      }
    };
    fetchChartData();
  }, [currentYear, logError]); // This dependency array is correct

  const handleUserInvited = (newUser) => {
    setUsers([newUser, ...users]);
  };

  // --- Action Menu Items ---
  const getActionItems = (targetUser) => [
    {
      label: 'View',
      icon: <FaEye />,
      onClick: () => toast.success(`Viewing user ${targetUser.name}`),
    },
    {
      label: 'Edit',
      icon: <FaPencilAlt />,
      onClick: () => toast.info(`Editing user ${targetUser.name}`),
    },
    {
      label: 'Delete',
      icon: <FaTrash />,
      onClick: () => toast.error(`Deleting user ${targetUser.name}`),
    },
  ];

  return (
    <div className="page-container">
      <div className="page-header-controls">
        <h1 className="page-title">User & Role Management</h1>
        <div className="search-and-actions">
          <button className="action-btn" onClick={() => toast.success('Exporting CSV...')}>
            <FaFileExport /> Export as CSV
          </button>
          <button 
            className="btn btn-primary" 
            onClick={() => setIsModalOpen(true)}
          >
            <FaUserPlus style={{marginRight: '0.5rem'}} />
            Invite user
          </button>
        </div>
      </div>
      
      <div className="table-wrapper">
        <table className="data-table">
          <thead>
            <tr>
              <th>User ID</th>
              <th>Name</th>
              <th>Email</th>
              <th>Role</th>
              <th>Status</th>
              <th>More Actions</th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr><td colSpan="6">Loading users...</td></tr>
            ) : (
              users.map((u) => (
                <tr key={u.id}>
                  <td>{u.id}</td>
                  <td>{u.name}</td>
                  <td>{u.email}</td>
                  <td>{u.role}</td>
                  <td>{u.status || 'Active'}</td>
                  <td>
                    <ActionMenu items={getActionItems(u)} />
                  </td>
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
          <h2 className="chart-title">Comparison between Active and De-Active users on each year</h2>
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
          {chartData ? <Bar options={barChartOptions} data={chartData} /> : <p>Loading chart data...</p>}
        </div>
      </div>

      {isModalOpen && (
        <EnterpriseInviteUserModal
          onClose={() => setIsModalOpen(false)}
          onUserInvited={handleUserInvited}
        />
      )}
    </div>
  );
}