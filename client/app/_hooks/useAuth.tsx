'use client';
import { useContext } from 'react';
import { ICONTEXT, AuthProvider } from '../_context/AuthContext';

export const useAuth = (): ICONTEXT | null => {
  const context = useContext(AuthProvider);
  if (context === undefined) throw new Error('Filed to load context');
  return context;
};
