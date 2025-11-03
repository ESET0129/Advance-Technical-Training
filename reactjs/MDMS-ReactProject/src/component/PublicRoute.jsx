import { Navigate, Outlet } from 'react-router-dom';
import { useAuthStore } from '../store/authStore';

export default function PublicRoute() {
  const { user } = useAuthStore();

  if (user) {
    // User is authenticated, so redirect to dashboard
    return <Navigate to="/" replace />;
  }
  
  // User is not authenticated, so render the public-only page
  return <Outlet />;
}