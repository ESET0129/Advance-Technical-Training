import { useLogger } from '../hooks/useLogger';
import { useEffect, useState } from 'react';
import { logService } from '../services/logService';

export default function LogsPage() {
  const { logInfo } = useLogger();
  const [logs, setLogs] = useState([]);

  useEffect(() => {
    logInfo('Page visit: Logs');
    const fetchLogs = async () => {
      const allLogs = await logService.getLogs();
      setLogs(allLogs.reverse());
    };
    fetchLogs();
  }, [logInfo]);

  return (
    <div>
      <h1>Local Logs (IndexedDB)</h1>
      <p>This page displays all logs captured in your local browser storage.</p>
      <pre style={{ 
        background: '#f4f4f4', 
        border: '1px solid #ccc', 
        padding: '1rem', 
        maxHeight: '60vh', 
        overflow: 'auto',
        whiteSpace: 'pre-wrap',
        wordWrap: 'break-word'
      }}>
        {logs.length > 0 
          ? JSON.stringify(logs, null, 2) 
          : 'No logs found.'
        }
      </pre>
    </div>
  );
}