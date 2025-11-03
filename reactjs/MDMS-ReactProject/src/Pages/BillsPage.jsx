import { useLogger } from '../hooks/useLogger';
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { useAuthStore } from '../store/authStore';
import apiClient from '../services/apiClient';
import '../styles/Table.css';

// Mock Pagination Component (for UI only)
const Pagination = () => (
  <div className="pagination">
    <button className="pagination-btn" disabled>← Previous</button>
    <span className="pagination-item active">1</span>
    <span className="pagination-item">2</span>
    <span className="pagination-item">3</span>
    <span className="pagination-dots">...</span>
    <span className="pagination-item">67</span>
    <span className="pagination-item">68</span>
    <button className="pagination-btn">Next →</button>
  </div>
);

export default function BillsPage() {
  const { logInfo, logError } = useLogger();
  const { user } = useAuthStore();
  const [bills, setBills] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    logInfo('Page visit: Bills & Payments');
    
    const fetchBills = async () => {
      try {
        setLoading(true);
        const response = await apiClient.get('/bills', {
          params: { userId: user.id }
        });
        setBills(response.data);
      } catch (error) {
        logError('Failed to fetch bills', error, { userId: user.id });
      } finally {
        setLoading(false);
      }
    };

    if (user?.id) {
      fetchBills();
    }
  }, [logInfo, user, logError]); // Added logError

  return (
    <div className="page-container">
      <div className="page-header">
        <h1>My Bills</h1>
      </div>

      <div className="table-wrapper">
        <table className="data-table">
          <thead>
            <tr>
              <th>Month</th>
              <th>Amount</th>
              <th>Due Date</th>
              <th>Status</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr><td colSpan="5">Loading bills...</td></tr>
            ) : (
              bills.map((bill) => (
                <tr key={bill.id}>
                  <td>{bill.month}</td>
                  <td>₹{bill.amount.toFixed(2)}</td>
                  <td>{bill.dueDate}</td>
                  <td>{bill.status}</td>
                  <td>
                    <Link to={`/bills/${bill.id}`} className="table-action-link">
                      View / Pay
                    </Link>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <Pagination />
      
      <p><strong>Note:</strong> All bills are generated on the 1st of each month</p>
    </div>
  );
}