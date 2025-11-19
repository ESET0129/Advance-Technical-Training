import { Routes, Route } from 'react-router-dom';
import LoginPage from './pages/Login';
import DashboardPage from './pages/Dashboard';
import ForgotPasswordPage from './pages/ForgotPassword';
import ResetPasswordPage from './pages/ResetPassword';
import BillsPage from './pages/BillsPage';
import BillDetailsPage from './pages/BillDetailsPage';
import MeterDataPage from './pages/MeterDataPage';
import AlertsPage from './pages/AlertsPage';
import ProfilePage from './pages/ProfilePage';
import LogsPage from './pages/LogsPage';

import MainLayout from './component/MainLayout';
import AuthLayout from './component/AuthLayout';
import ProtectedRoute from './component/ProtectedRoute';
import PublicRoute from './component/PublicRoute';

import ErrorBoundary from './component/ErrorBoundary';

// Utility Pages
import NotFoundPage from './Pages/utility/NotFoundPage';
import ServerErrorPage from './pages/utility/ServerErrorPage';
import AccessDeniedPage from './pages/utility/AccessDeniedPage';
import MaintenancePage from './pages/utility/MaintenancePage';

// Zone Placeholder Pages
import MeterManagementPage from './Pages/MeterManagementPage';
import UserManagementPage from './Pages/UserManagementPage';
import ReportsPage from './Pages/ReportsPage';
import SettingsPage from './Pages/SettingsPage';

import ZoneManagementPage from './Pages/ZoneManagementPage';
import UserRoleManagementPage from './Pages/UserRoleManagementPage';
import AuditLogsPage from './Pages/AuditLogsPage';
import EnterpriseSettingsPage from './Pages/EnterpriseSettingsPage';


// Simulate a maintenance mode flag
const IN_MAINTENANCE_MODE = false;

function App() {
  if (IN_MAINTENANCE_MODE) {
    return <MaintenancePage />;
  }

  return (
    <ErrorBoundary>
      <Routes>
        
        {/* Public-only routes */}
        <Route element={<PublicRoute />}>
          <Route element={<AuthLayout />}>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/forgot-password" element={<ForgotPasswordPage />} />
            <Route path="/reset-password" element={<ResetPasswordPage />} />
          </Route>
        </Route>

        {/* Protected Routes */}
        <Route element={<ProtectedRoute />}>
          <Route element={<MainLayout />}>
            {/* Common */}
            <Route path="/" element={<DashboardPage />} />
            <Route path="/dashboard" element={<DashboardPage />} />
            
            {/* End User */}
            <Route path="/bills" element={<BillsPage />} />
            <Route path="/bills/:billId" element={<BillDetailsPage />} />
            <Route path="/meter-data" element={<MeterDataPage />} />
            <Route path="/alerts" element={<AlertsPage />} />
            <Route path="/profile" element={<ProfilePage />} />
            <Route path="/logs" element={<LogsPage />} />
            
            {/* Zone User */}
            <Route path="/meter-management" element={<MeterManagementPage />} />
            <Route path="/user-management" element={<UserManagementPage />} />
            <Route path="/reports" element={<ReportsPage />} />
            <Route path="/settings" element={<SettingsPage />} />
            {/* Enterprise User */}
            
            <Route path="/zone-management" element={<ZoneManagementPage />} />
            <Route path="/user-role-management" element={<UserRoleManagementPage />} />
            <Route path="/audit-logs" element={<AuditLogsPage />} />
            <Route path="/setting-configuration" element={<EnterpriseSettingsPage />} />
            {/* Utility */}
            <Route path="/access-denied" element={<AccessDeniedPage />} />
          </Route>
        </Route>
        
        {/* Catch-all 404 */}
        <Route path="*" element={<NotFoundPage />} />
        
      </Routes>
    </ErrorBoundary>
  );
}

export default App;