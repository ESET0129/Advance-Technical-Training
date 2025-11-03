import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import toast from 'react-hot-toast';

export default function ResetPasswordPage() {
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  
  const email = 'abc@gmail.com'; // Mock email

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading(true);
    
    console.log('Setting new password:', password);
    
    setTimeout(() => {
      toast.success('Password updated successfully!');
      setLoading(false);
      navigate('/login');
    }, 1500);
  };

  return (
    <form className="auth-form" onSubmit={handleSubmit}>
      <h2>Reset password</h2>
      <input
        type="email"
        className="auth-input"
        value={email}
        disabled
      />
      <input
        type="password"
        placeholder="Enter your password"
        className="auth-input"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        required
      />
      <button type="submit" className="auth-button" disabled={loading}>
        {loading ? 'Updating...' : 'update password'}
      </button>
    </form>
  );
}