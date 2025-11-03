import { Outlet, useNavigate, NavLink } from 'react-router-dom';
import { useAuthStore } from '../store/authStore';
import { useTranslation } from 'react-i18next'; 
import ThemeToggle from './ThemeToggle'; 
import LanguageSelector from './LanguageSelector'; 

export default function MainLayout() {
  const { user, logout } = useAuthStore();
  const navigate = useNavigate();
  const { t } = useTranslation();

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
    <div style={{ display: 'flex', height: '100vh', background: 'var(--color-background)' }}>
      
      {/* Sidebar */}
      <nav style={{ 
        width: '200px', 
        background: 'var(--color-background-secondary)', 
        padding: '1rem',
        borderRight: '1px solid var(--color-border)',
        color: 'var(--color-text-primary)'
      }}>
        <h3 style={{color: 'var(--color-primary)'}}>MDMS</h3>
        <p>{t('dashboard.welcome', { name: user?.name })}</p>
        <p><small>Role: {user?.role}</small></p>

        <ul style={{ listStyle: 'none', padding: 0, marginTop: '2rem' }}>
          {/* ... (Your NavLink list remains the same, activeStyle will handle colors) ... */}
          <li>
            <NavLink to="/dashboard" style={activeStyle}>
              {t('sidebar.dashboard')}
            </NavLink>
          </li>
          {user?.role === 'EndUser' && (
            <>
              <li><NavLink to="/bills" style={activeStyle}>{t('sidebar.bills')}</NavLink></li>
              <li><NavLink to="/meter-data" style={activeStyle}>{t('sidebar.meterData')}</NavLink></li>
              <li><NavLink to="/alerts" style={activeStyle}>{t('sidebar.alerts')}</NavLink></li>
              <li><NavLink to="/profile" style={activeStyle}>{t('sidebar.profile')}</NavLink></li>
              <li><NavLink to="/logs" style={activeStyle}>{t('sidebar.logs')}</NavLink></li>
            </>
          )}
          
          {/* --- End User Links --- */}

          {/* {user?.role === 'EndUser' && (
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
          )} */}

          {/* --- Zone Level Links --- */}
{user?.role === 'ZoneLevel' && (
            <>
              <li><NavLink to="/meter-management" style={activeStyle}>{t('sidebar.meterManagement')}</NavLink></li>
              <li><NavLink to="/user-management" style={activeStyle}>{t('sidebar.userManagement')}</NavLink></li>
              <li><NavLink to="/reports" style={activeStyle}>{t('sidebar.reports')}</NavLink></li>
              <li><NavLink to="/settings" style={activeStyle}>{t('sidebar.settings')}</NavLink></li>
            </>
          )}
        </ul>


          {/* {user?.role === 'ZoneLevel' && (
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
            /*}

          
          {/* --- Enterprise Only Links --- */}
        <ul>
          {user?.role === 'EnterpriseLevel' && (
            <li>
              <NavLink to="/zones" style={activeStyle}>
                Zone Management
              </NavLink>
            </li>
          )}
        </ul> 

        <button onClick={handleLogout} style={{ position: 'absolute', bottom: '1rem', width: 'calc(100% - 2rem)' }}>
          {t('logout')}
        </button>
      </nav>
      
      {/* Main Content */}
      <main style={{ flex: 1, padding: '2rem', overflowY: 'auto', background: 'var(--color-background)' }}>
        {/* 7. Add ThemeToggle and LanguageSelector to the header */}
        <header style={{display: 'flex', justifyContent: 'flex-end', gap: '1rem', marginBottom: '1rem'}}>
          <ThemeToggle />
          <LanguageSelector />
        </header>
        <Outlet /> 
      </main>
    </div>
  );
}