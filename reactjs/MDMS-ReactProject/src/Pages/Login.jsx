import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuthStore } from '../store/authStore';
import { authService } from '../services/authService';
import toast from 'react-hot-toast';
import { useLogger } from '../hooks/useLogger';
import { useTranslation } from 'react-i18next';

export default function LoginPage() {
  const { t } = useTranslation();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [rememberMe, setRememberMe] = useState(false);
  const [loading, setLoading] = useState(false);
  
  const { logInfo, logError } = useLogger();
  
  const { login } = useAuthStore();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    
    logInfo('Login attempt', { email: email });

    try {
      const userData = await authService.login(email, password);
      login(userData);
      toast.success('Logged in successfully!');
      
      logInfo('Login success', { userId: userData.id, email: userData.email });
      
      navigate('/');
    } catch (error) {
      toast.error(error.message || 'Invalid credentials');
      
      logError('Login failed', error, { email: email });
      
    } finally {
      setLoading(false);
    }
  };

  return (
    <form className="auth-form" onSubmit={handleSubmit}>
      {/* <h2>Login Form</h2>
      <input
        id="email"
        type="email"
        placeholder="email"
        className="auth-input"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
      /> */}
      <h2>{t('login.title')}</h2>
      <input
        id="email"
        type="email"

        placeholder={t('login.email')}
        className="auth-input"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
      />
      

      <input
        id="password"
        type="password"
        placeholder={t('login.password')}
        className="auth-input"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        required
      />

      <div className="auth-options">
        <label className="auth-checkbox-label">
          <input
            type="checkbox"
            checked={rememberMe}
            onChange={(e) => setRememberMe(e.target.checked)}
          />
          {t('login.rememberMe')}
        </label>


        <Link to="/forgot-password" className="auth-link">
          {t('login.forgotPassword')}
        </Link>
      </div>

      <button type="submit" className="auth-button" disabled={loading}>
        {loading ? 'Logging in...' : t('login.button')}
      </button>

      {/* Test Users Info */}
      <div style={{marginTop: '2rem', fontSize: '0.8rem', background: 'var(--color-background-secondary)', padding: '1rem', borderRadius: '8px'}}>
        <p><strong>{t('login.testUsers')}</strong></p>
        <ul style={{ margin: 0, paddingLeft: '1.2rem' }}>
          <li>user@example.com (EndUser)</li>
          <li>zone@example.com (ZoneLevel)</li>
          <li>admin@example.com (EnterpriseLevel)</li>
        </ul>
        <p style={{marginTop: '0.5rem'}}>{t('login.passwordLabel')} <strong>password123</strong></p>
      </div>
    </form>
  );
}