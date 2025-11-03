import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';

export const useAuthStore = create(
  persist(
    (set) => ({
      user: null,
      
      login: (userData) => {
        set({ user: userData });
      },
      
      logout: () => {
        set({ user: null });
      },
      
      isAuthenticated: () => !!set.user,
      
      getUserRole: () => set.user?.role,
    }),
    {
      name: 'auth-storage',
      storage: createJSONStorage(() => localStorage),
    }
  )
);