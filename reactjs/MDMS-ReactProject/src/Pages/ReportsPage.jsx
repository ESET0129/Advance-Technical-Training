import { useState, useEffect } from 'react';
import { useLogger } from '../hooks/useLogger';
import { Line, Bar } from 'react-chartjs-2';
import toast from 'react-hot-toast';

// Import our reusable components and styles
import Pagination from '../component/Pagination';
import '../styles/Dashboard.css';
import '../styles/Table.css';
import '../styles/ManagementPage.css';

// Import Icons
import { FaSearch, FaChevronLeft, FaChevronRight, FaFileExport } from 'react-icons/fa';

// --- Mock Data for this Page ---

const lineChartData = {
  labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
  datasets: [{
    label: 'Energy Usage (kWh)',
    data: [320, 350, 240, 310, 380, 420, 300],
    fill: false,
    borderColor: 'var(--color-primary)',
    tension: 0.4,
  }],
};

const barChartData = {
  labels: ['Mangalore', 'Bejai', 'Pumpwell', 'PVS', 'Kotekar'],
  datasets: [{
    label: 'Zone Consumption',
    data: [68, 39, 88, 52, 75],
    backgroundColor: 'rgba(106, 27, 154, 0.6)', // Purple
    borderColor: 'rgba(106, 27, 154, 1)',
    borderRadius: 10,
    borderWidth: 1,
  }],
};

const reportTableData = [
  { id: 123, date: '2025-10-07T07:15:13Z', user: 'abc', consumption: '24kWh', status: 'Active' },
  { id: 124, date: '2025-10-07T07:15:13Z', user: 'xyz', consumption: '16kWh', status: 'De-Activated' },
  { id: 125, date: '2025-10-07T07:15:13Z', user: 'test1', consumption: '30kWh', status: 'Active' },
  { id: 126, date: '2025-10-07T07:15:13Z', user: 'test2', consumption: '22kWh', status: 'Active' },
  { id: 127, date: '2025-10-07T07:15:13Z', user: 'demo', consumption: '19kWh', status: 'Active' },
  { id: 128, date: '2025-10-07T07:15:13Z', user: 'abc', consumption: '24kWh', status: 'Active' },
  { id: 129, date: '2025-10-07T07:15:13Z', user: 'xyz', consumption: '16kWh', status: 'De-Activated' },
  { id: 130, date: '2025-10-07T07:15:13Z', user: 'test1', consumption: '30kWh', status: 'Active' },
  { id: 131, date: '2025-10-07T07:15:13Z', user: 'test2', consumption: '22kWh', status: 'Active' },
  { id: 132, date: '2025-10-07T07:15:13Z', user: 'demo', consumption: '19kWh', status: 'Active' },
  { id: 133, date: '2025-10-07T07:15:13Z', user: 'abc', consumption: '24kWh', status: 'Active' },
  { id: 134, date: '2025-10-07T07:15:13Z', user: 'xyz', consumption: '16kWh', status: 'De-Activated' },
  { id: 135, date: '2025-10-07T07:15:13Z', user: 'test1', consumption: '30kWh', status: 'Active' },
  { id: 136, date: '2025-10-07T07:15:13Z', user: 'test2', consumption: '22kWh', status: 'Active' },
  { id: 137, date: '2025-10-07T07:15:13Z', user: 'demo', consumption: '19kWh', status: 'Active' },
  { id: 138, date: '2025-10-07T07:15:13Z', user: 'abc', consumption: '24kWh', status: 'Active' },
  { id: 139, date: '2025-10-07T07:15:13Z', user: 'xyz', consumption: '16kWh', status: 'De-Activated' },
  { id: 140, date: '2025-10-07T07:15:13Z', user: 'test1', consumption: '30kWh', status: 'Active' },
  { id: 141, date: '2025-10-07T07:15:13Z', user: 'test2', consumption: '22kWh', status: 'Active' },
  { id: 142, date: '2025-10-07T07:15:13Z', user: 'demo', consumption: '19kWh', status: 'Active' },
  { id: 143, date: '2025-10-07T07:15:13Z', user: 'abc', consumption: '24kWh', status: 'Active' },
  { id: 144, date: '2025-10-07T07:15:13Z', user: 'xyz', consumption: '16kWh', status: 'De-Activated' },
  { id: 145, date: '2025-10-07T07:15:13Z', user: 'test1', consumption: '30kWh', status: 'Active' },
  { id: 146, date: '2025-10-07T07:15:13Z', user: 'test2', consumption: '22kWh', status: 'Active' },
  { id: 147, date: '2025-10-07T07:15:13Z', user: 'demo', consumption: '19kWh', status: 'Active' },
  { id: 148, date: '2025-10-07T07:15:13Z', user: 'abc', consumption: '24kWh', status: 'Active' },
  { id: 149, date: '2025-10-07T07:15:13Z', user: 'xyz', consumption: '16kWh', status: 'De-Activated' },
  { id: 150, date: '2025-10-07T07:15:13Z', user: 'test1', consumption: '30kWh', status: 'Active' }
];

const lineChartOptions = {
  responsive: true,
  plugins: { legend: { display: false } },
  scales: { y: { beginAtZero: true } },
};

const barChartOptions = {
  responsive: true,
  plugins: { legend: { display: false } },
  scales: { y: { beginAtZero: true, max: 100 } },
};

const REPORTS_PER_PAGE = 10;

export default function ReportsPage() {
  const { logInfo } = useLogger();
  const [currentYear, setCurrentYear] = useState(2025);
  const [currentPage, setCurrentPage] = useState(1);
  
  const totalPages = Math.ceil(reportTableData.length / REPORTS_PER_PAGE);
  const startIndex = (currentPage - 1) * REPORTS_PER_PAGE;
  const endIndex = startIndex + REPORTS_PER_PAGE;
  const currentTableData = reportTableData.slice(startIndex, endIndex);
  
  useEffect(() => {
    logInfo('Page visit: Reports & Analytics', { page: currentPage });
  }, [logInfo, currentPage]);

  return (
    <div className="page-container">
      <h1 className="page-title">Reports and Analytics</h1>
      
      {/* --- THIS IS THE MISSING CODE --- */}
      <div className="chart-container">
        <h2 className="chart-title">Trend of energy usage over time</h2>
        <div className="chart-content" style={{ height: '350px' }}>
          <Line options={lineChartOptions} data={lineChartData} />
        </div>
      </div>
      {/* --- END OF MISSING CODE --- */}

      {/* --- THIS IS THE OTHER MISSING CODE --- */}
      <div className="chart-container">
        <div className="chart-header">
          <h2 className="chart-title">Compare zone consumption</h2>
          <div className="search-and-actions">
            <div className="search-bar">
              <FaSearch />
              <input type="text" placeholder="Search..." defaultValue="abc" />
            </div>
            <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
              <button className="pagination-btn" onClick={() => setCurrentYear(y => y - 1)}>
                <FaChevronLeft />
              </button>
              <span>{currentYear}</span>
              <button className="pagination-btn" onClick={() => setCurrentYear(y => y + 1)}>
                <FaChevronRight />
              </button>
            </div>
          </div>
        </div>
        <div className="chart-content" style={{ height: '250px' }}>
          <Bar options={barChartOptions} data={barChartData} />
        </div>
      </div>
      {/* --- END OF MISSING CODE --- */}

      {/* --- Reports Table --- */}
      <div className="page-header-controls" style={{ marginTop: '2rem' }}>
        <h2 className="page-title">Reports</h2>
        <div className="actions-row">
          <button className="action-btn" onClick={() => toast.success('Exporting CSV...')}>
            <FaFileExport /> Export as CSV
          </button>
          <button className="action-btn" onClick={() => toast.info('PDF export coming soon!')}>
            <FaFileExport /> Export as PDF
          </button>
        </div>
      </div>

      <div className="table-wrapper">
        <table className="data-table">
          <thead>
            <tr>
              <th>Meter ID</th>
              <th>Dates</th>
              <th>User name</th>
              <th>Consumption</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {currentTableData.map((row, index) => (
              <tr key={index}>
                <td>{row.id}</td>
                <td>{new Date(row.date).toLocaleString()}</td>
                <td>{row.user}</td>
                <td>{row.consumption}</td>
                <td>{row.status}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <Pagination 
        currentPage={currentPage}
        totalPages={totalPages}
        onPageChange={setCurrentPage}
      />
    </div>
  );
}