import Dexie from 'dexie';

export const db = new Dexie('MDMSDatabase');

db.version(1).stores({
  logs: '++id, timestamp, level, message, context',
});