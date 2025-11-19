import { useState, useEffect } from 'react';
import { useLogger } from '../hooks/useLogger';
import apiClient from '../services/apiClient';
import toast from 'react-hot-toast';

// Import components & styles
import ActionMenu from '../component/ActionMenu';
import Pagination from '../component/Pagination';
import AddZoneModal from '../component/AddZoneModal';
import '../styles/Table.css';
import '../styles/ManagementPage.css';
import '../styles/Modal.css';

// Import Icons
import { 
  FaEye, 
  FaPencilAlt, 
  FaTrash,
  FaPlus
} from 'react-icons/fa';

const ZONES_PER_PAGE = 10;

export default function ZoneManagementPage() {
  const { logInfo, logError } = useLogger();
  
  // Data State
  const [zones, setZones] = useState([]);
  const [adminMap, setAdminMap] = useState({});
  const [loading, setLoading] = useState(true);

  // Pagination State
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  
  // Modal State
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    logInfo('Page visit: Zone Management', { page: currentPage });

    const fetchData = async () => {
      try {
        setLoading(true);
        
        // Fetch paginated ZONES
        const zonesRes = await apiClient.get('/zones', {
          params: { 
            _page: currentPage,
            _limit: ZONES_PER_PAGE,
            _sort: 'id'
          }
        });
        
        const totalCount = zonesRes.headers['x-total-count'];
        setTotalPages(Math.ceil(totalCount / ZONES_PER_PAGE));
        
        // Fetch all users to map their names
        const usersRes = await apiClient.get('/users');
        const userMap = usersRes.data.reduce((acc, user) => {
          acc[user.id] = user.name;
          return acc;
        }, {});

        setZones(zonesRes.data);
        setAdminMap(userMap);
        
      } catch (error) {
        logError('Failed to fetch zone data', error);
        toast.error('Could not load zone data.');
      } finally {
        setLoading(false);
      }
    };
    
    fetchData();
  }, [logInfo, logError, currentPage]); // Re-fetch when currentPage changes

  const handleZoneAdded = (newZone) => {
    // To see the new zone, we just refetch the current page
    // A more advanced way would be to add it to the state
    // but this is simpler and ensures data is fresh
    setCurrentPage(1); // Go to first page to see new item
  };

  // --- Action Menu Items ---
  const getActionItems = (zone) => [
    {
      label: 'View',
      icon: <FaEye />,
      onClick: () => toast.success(`Viewing zone ${zone.zoneName}`),
    },
    {
      label: 'Edit',
      icon: <FaPencilAlt />,
      onClick: () => toast.info(`Editing zone ${zone.zoneName}`),
    },
    {
      label: 'Delete',
      icon: <FaTrash />,
      onClick: () => toast.error(`Deleting zone ${zone.zoneName}`),
    },
  ];

  return (
    <div className="page-container">
      <div className="page-header-controls">
        <h1 className="page-title">Zone Management</h1>
        <div className="search-and-actions">
          {/* We'll add search later if needed */}
          <button 
            className="btn btn-primary" 
            onClick={() => setIsModalOpen(true)}
          >
            <FaPlus style={{marginRight: '0.5rem'}} />
            Add zone
          </button>
        </div>
      </div>
      
      <div className="table-wrapper">
        <table className="data-table">
          <thead>
            <tr>
              <th>Zone ID</th>
              <th>Zone name</th>
              <th>Admin assigned</th>
              <th>Total Meters</th>
              <th>Status</th>
              <th>More Actions</th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr><td colSpan="6">Loading zones...</td></tr>
            ) : (
              zones.map((zone) => (
                <tr key={zone.id}>
                  <td>{zone.id}</td>
                  <td>{zone.zoneName}</td>
                  <td>{adminMap[zone.adminId] || 'N/A'}</td>
                  <td>{zone.totalMeters}</td>
                  <td>{zone.status}</td>
                  <td>
                    <ActionMenu items={getActionItems(zone)} />
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

      {isModalOpen && (
        <AddZoneModal
          onClose={() => setIsModalOpen(false)}
          onZoneAdded={handleZoneAdded}
        />
      )}
    </div>
  );
}