import { Outlet, useNavigate, NavLink } from 'react-router-dom';
import { useAuthStore } from '../store/authStore';

export default function MainLayout() {
  const { user, logout } = useAuthStore();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  // This style function is used by NavLink to show the active page
  const activeStyle = ({ isActive }) => ({
    color: isActive ? 'blue' : 'black',
    display: 'block',
    padding: '0.5rem 0'
  });

  return (
    <div style={{ display: 'flex', height: '100vh' }}>
      
      {/* Sidebar */}
      <nav style={{ width: '200px', background: '#f4f4f4', padding: '1rem' }}>
        <h3>MDMS</h3>
        <p>Welcome, {user?.name}</p>
        <p><small>Role: {user?.role}</small></p>

        <ul style={{ listStyle: 'none', padding: 0, marginTop: '2rem' }}>
          <li>
            <NavLink to="/dashboard" style={activeStyle}>
              Dashboard
            </NavLink>
          </li>
          
          {/* --- End User Links --- */}
          {user?.role === 'EndUser' && (
            <>
              <li>
                <NavLink to="/bills" style={activeStyle}>
                  Bills & Payments
                </NavLink>
              </li>
              <li>
                <NavLink to="/meter-data" style={activeStyle}>
                  Meter Data
                </NavLink>
              </li>
              <li>
                <NavLink to="/alerts" style={activeStyle}>
                  Alerts & Notifications
                </NavLink>
              </li>
              <li>
                <NavLink to="/profile" style={activeStyle}>
                  Profile & Settings
                </NavLink>
              </li>
              <li>
                <NavLink to="/logs" style={activeStyle}>
                  Logs
                </NavLink>
              </li>
            </>
          )}

          {/* --- Zone Level Links --- */}
          {user?.role === 'ZoneLevel' && (
            <>
              <li>
                <NavLink to="/meter-management" style={activeStyle}>
                  Meter management
                </NavLink>
              </li>
              <li>
                <NavLink to="/user-management" style={activeStyle}>
                  User management
                </NavLink>
              </li>
              <li>
                <NavLink to="/reports" style={activeStyle}>
                  Reports & Analytics
                </NavLink>
              </li>
              <li>
                <NavLink to="/settings" style={activeStyle}>
                  Setting & Notifications
                </NavLink>
              </li>
            </>
          )}
          
          {/* --- Enterprise Only Links --- */}
          {user?.role === 'EnterpriseLevel' && (
            <li>
              <NavLink to="/zones" style={activeStyle}>
                Zone Management
              </NavLink>
            </li>
          )}
        </ul>

        <button onClick={handleLogout} style={{ position: 'absolute', bottom: '1rem', width: 'calc(100% - 2rem)' }}>
          Logout
        </button>
      </nav>
      
      {/* Main Content */}
      <main style={{ flex: 1, padding: '2rem', overflowY: 'auto', background: '#fcfcfa' }}>
        <Outlet /> 
      </main>
    </div>
  );
}