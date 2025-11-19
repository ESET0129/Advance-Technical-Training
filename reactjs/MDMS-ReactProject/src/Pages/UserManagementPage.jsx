import { useState, useEffect } from 'react';
import { useAuthStore } from '../store/authStore';
import { useLogger } from '../hooks/useLogger';
import apiClient from '../services/apiClient';
import toast from 'react-hot-toast';

// Import components & styles
import ActionMenu from '../component/ActionMenu';
import Pagination from '../component/Pagination';
import InviteUserModal from '../component/InviteUserModal.jsx';// <-- NEW
import '../styles/Table.css';
import '../styles/ManagementPage.css'; // <-- NEW
import '../styles/Modal.css';

// Import Icons
import { 
  FaEye, 
  FaPencilAlt, 
  FaKey,
  FaUserPlus,
  FaSearch
} from 'react-icons/fa';

const USERS_PER_PAGE = 10;

export default function UserManagementPage() {
  const { user } = useAuthStore();
  const { logInfo, logError } = useLogger();
  
  // Data State
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);

  // Pagination State
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  
  // Search State
  const [searchQuery, setSearchQuery] = useState("");
  
  // Modal State
  const [isInviteModalOpen, setIsInviteModalOpen] = useState(false);

  useEffect(() => {
    logInfo('Page visit: User Management', { page: currentPage, search: searchQuery });

    const fetchData = async () => {
      if (!user?.zoneId) return;
      try {
        setLoading(true);
        
        // Add search query to params
        const params = { 
          zoneId: user.zoneId,
          role: 'EndUser',
          _page: currentPage,
          _limit: USERS_PER_PAGE,
        };
        
        if (searchQuery) {
          params.q = searchQuery; // json-server uses 'q' for full-text search
        }

        const usersRes = await apiClient.get('/users', { params });
        
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
    
    // Use a timer to delay search to avoid spamming the API
    const delayDebounceFn = setTimeout(() => {
      fetchData();
    }, 500); // Wait 500ms after user stops typing

    return () => clearTimeout(delayDebounceFn); // Clear timer on cleanup
    
  }, [user, logInfo, logError, currentPage, searchQuery]);

  const handleUserInvited = (newUser) => {
    // Add new user to the list in real-time
    setUsers([newUser, ...users]);
    // You might want to also refetch to ensure pagination is correct
  };

  // --- Action Menu Items ---
  const getActionItems = (targetUser) => [
    {
      label: 'View',
      icon: <FaEye />,
      onClick: () => toast.success(`Viewing user ${targetUser.name}`),
    },
    {
      label: 'Edit', // Per your idea, this *could* navigate to /settings/:userId
      icon: <FaPencilAlt />,
      onClick: () => toast.info(`"Edit" modal for ${targetUser.name} would open here.`),
    },
    {
      label: 'Reset password',
      icon: <FaKey />,
      onClick: () => toast.success(`Password reset link sent to ${targetUser.email}`),
    },
  ];

  return (
    <div className="page-container">
      <div className="page-header-controls">
        <h1 className="page-title">User Management</h1>
        <div className="search-and-actions">
          <div className="search-bar">
            <FaSearch />
            <input 
              type="text" 
              placeholder="Search by name, email..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
            />
          </div>
          <button 
            className="btn btn-primary" 
            onClick={() => setIsInviteModalOpen(true)}
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
              <th>ID</th>
              <th>Name</th>
              <th>Email</th>
              <th>Role</th>
              <th>Zone</th>
              <th>Status</th>
              <th>More Actions</th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr><td colSpan="7">Loading users...</td></tr>
            ) : (
              users.map((u) => (
                <tr key={u.id}>
                  <td>{u.id}</td>
                  <td>{u.name}</td>
                  <td>{u.email}</td>
                  <td>{u.role}</td>
                  <td>{u.zoneId}</td>
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

      {isInviteModalOpen && (
        <InviteUserModal
          onClose={() => setIsInviteModalOpen(false)}
          onUserInvited={handleUserInvited}
        />
      )}
    </div>
  );
}