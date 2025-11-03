import { useState } from 'react';
import { Link } from 'react-router-dom';
import toast from 'react-hot-toast';

export default function ForgotPasswordPage() {
  const [email, setEmail] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading(true);
    
    console.log('Sending reset link to:', email);
    
    setTimeout(() => {
      toast.success('Reset link sent to your email!');
      setLoading(false);
    }, 1500);
  };

  return (
    <form className="auth-form" onSubmit={handleSubmit}>
      <h2>Forgot password</h2>
      <input
        type="email"
        placeholder="email"
        className="auth-input"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
      />
      
      <button type="submit" className="auth-button" disabled={loading}>
        {loading ? 'Sending...' : 'send reset link'}
      </button>

      <div className="auth-text-link">
        <Link to="/login" className="auth-link">
          login
        </Link>
      </div>
    </form>
  );
}