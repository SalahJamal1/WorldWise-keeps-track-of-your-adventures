'use client';
import AppNav from '@/app/_components/AppNav';
import CityItem from '@/app/_components/CityItem';
import Spinner from '@/app/_components/Spinner';
import { useAuth } from '@/app/_hooks/useAuth';
import { GetCities } from '@/app/_lib/apiCity';

import { useEffect } from 'react';

export default function Page() {
  const { dispatch, loading, cities } = useAuth()!;

  useEffect(() => {
    const controller = new AbortController();
    (async () => {
      dispatch({ type: 'LOADER' });
      try {
        const res = await GetCities(controller.signal);
        dispatch({ type: 'LOAD_CITIES', payload: res.data });
      } catch (err: any) {
        if (err.name === 'CanceledError') return;

        console.log(err);
      }
    })();
    return () => controller.abort();
  }, []);
  if (loading) return <Spinner />;

  return (
    <div className="flex flex-col items-center w-full">
      <AppNav />
      {!cities?.length ? (
        <p className="mt-10 text-xl text-center mx-20">
          👋 Add your first city by clicking on a city on the map
        </p>
      ) : (
        <ul className="mt-10 w-full h-[50vh] overflow-y-scroll">
          {cities?.map((city, i) => (
            <CityItem city={city} key={i} />
          ))}
        </ul>
      )}
    </div>
  );
}
