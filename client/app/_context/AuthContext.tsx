'use client';
import {
  createContext,
  ReactNode,
  useEffect,
  useMemo,
  useReducer,
} from 'react';
import { refresh } from '../_lib/apiAuth';
import { IUSER } from '../utils/IUSER';
import { ICITY } from '../utils/ICITY';
import { IWORKOUT } from '../utils/IWORKOUT';

type Props = {
  children: ReactNode;
};

interface ISTATE {
  auth: boolean;
  user: IUSER;
  loader: boolean;
  loading: boolean;
  cities: ICITY[];
  city: ICITY;
  workouts: IWORKOUT[];
}
const initialState: ISTATE = {
  auth: typeof window !== 'undefined' && !!localStorage.getItem('jwt'),
  loader: false,
  loading: false,
  user: {} as IUSER,
  cities: [],
  workouts: [],
  city: {} as ICITY,
};

type IACTION =
  | { type: 'USER_LOGIN'; payload: IUSER }
  | { type: 'USER_LOGOUT' }
  | { type: 'USER_PENDING' }
  | { type: 'USER_REFRESH'; payload: IUSER }
  | { type: 'LOADER' }
  | { type: 'FINALLY' }
  | { type: 'LOAD_CITIES'; payload: ICITY[] }
  | { type: 'LOAD_CITY'; payload: ICITY }
  | { type: 'LOAD_WORKOUTS'; payload: IWORKOUT[] };

const reducer = (state: ISTATE, action: IACTION): ISTATE => {
  switch (action.type) {
    case 'USER_LOGIN':
      return { ...state, user: action.payload, auth: true, loader: false };
    case 'USER_REFRESH':
      return {
        ...state,
        user: action.payload,
        auth: true,
        loader: false,
        loading: false,
      };
    case 'USER_LOGOUT':
      localStorage.removeItem('jwt');
      return { ...state, user: {} as IUSER, auth: false, loader: false };
    case 'USER_PENDING':
      return { ...state, loader: true };
    case 'LOADER':
      return { ...state, loading: true };
    case 'FINALLY':
      return { ...state, loading: false };
    case 'LOAD_CITIES':
      return { ...state, loading: false, cities: action.payload };
    case 'LOAD_CITY':
      return { ...state, loading: false, city: action.payload };
    case 'LOAD_WORKOUTS':
      return { ...state, loading: false, workouts: action.payload };

    default:
      return state;
  }
};

export interface ICONTEXT extends ISTATE {
  dispatch: (action: IACTION) => void;
}
export const AuthProvider = createContext<null | ICONTEXT>(null);
export default function AuthContext({ children }: Props) {
  const [{ auth, loading, user, loader, cities, city, workouts }, dispatch] =
    useReducer(reducer, initialState);

  useEffect(() => {
    const token = localStorage.getItem('jwt');
    if (token) {
      (async () => {
        dispatch({ type: 'USER_PENDING' });
        try {
          const res = await refresh();
          localStorage.setItem('jwt', res?.data?.accessToken);
          dispatch({ type: 'USER_REFRESH', payload: res?.data?.user });
        } catch (err) {
          console.log(err);
          dispatch({ type: 'USER_LOGOUT' });
        }
      })();
    }
  }, []);

  const value = useMemo(() => {
    return {
      auth,
      cities,
      city,
      workouts,
      user,
      dispatch,
      loading,
      loader,
    };
  }, [auth, cities, city, workouts, user, dispatch, loading, loader]);
  return (
    <AuthProvider.Provider value={value}>{children}</AuthProvider.Provider>
  );
}
