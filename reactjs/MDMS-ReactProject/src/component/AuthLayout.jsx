import { Outlet, Link } from 'react-router-dom';
import '../styles/Auth.css';

const ThemeToggle = () => {
  return <div className="theme-toggle"></div>;
};

const LanguageSelector = () => {
  return (
    <select className="lang-select" defaultValue="en">
      <option value="en">en</option>
      <option value="es">es</option>
    </select>
  );
};

export default function AuthLayout() {
  return (
    <div className="auth-layout">
      <header className="auth-header">
        <Link to="/login" className="auth-logo">
          MDMS
        </Link>
        <div className="auth-controls">
          <ThemeToggle />
          <LanguageSelector />
        </div>
      </header>
      <main className="auth-main">
        <Outlet />
      </main>
    </div>
  );
}