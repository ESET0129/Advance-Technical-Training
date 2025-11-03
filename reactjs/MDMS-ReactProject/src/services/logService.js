import { db } from './dbService';

export const logService = {
  addLog: async (level, message, context = {}) => {
    try {
      const logEntry = {
        timestamp: new Date(),
        level,
        message,
        context: JSON.stringify(context),
      };
      await db.logs.add(logEntry);
    } catch (error) {
      console.error('Failed to write to IndexedDB:', error);
    }
  },

  getLogs: async () => {
    try {
      return await db.logs.toArray();
    } catch (error) {
      console.error('Failed to read from IndexedDB:', error);
      return [];
    }
  },

  clearLogs: async () => {
    try {
      await db.logs.clear();
    } catch (error) {
      console.error('Failed to clear IndexedDB:', error);
    }
  },
};