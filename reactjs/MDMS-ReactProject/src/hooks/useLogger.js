import { useCallback } from 'react';
import { logService } from '../services/logService';

/**
 * Custom hook to provide easy-to-use logging functions.
 */
export const useLogger = () => {
  const logInfo = useCallback((message, context = {}) => {
    logService.addLog('info', message, context);
  }, []);

  const logWarn = useCallback((message, context = {}) => {
    logService.addLog('warn', message, context);
  }, []);

  const logError = useCallback((message, error, context = {}) => {
    const errorContext = {
      ...context,
      errorMessage: error.message,
      stack: error.stack,
    };
    logService.addLog('error', message, errorContext);
  }, []);

  return { logInfo, logWarn, logError };
};