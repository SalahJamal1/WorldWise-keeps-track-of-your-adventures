'use client';
import AppNav from '@/app/_components/AppNav';
import Spinner from '@/app/_components/Spinner';
import { useAuth } from '@/app/_hooks/useAuth';
import { GetWorkouts } from '@/app/_lib/apiWorkouts';
import { useEffect } from 'react';
import { WorkoutItem } from '../../_components/WorkoutItem';

export default function Page() {
  const { dispatch, loading, workouts } = useAuth()!;

  useEffect(() => {
    const controller = new AbortController();
    (async () => {
      dispatch({ type: 'LOADER' });
      try {
        const res = await GetWorkouts(controller.signal);
        dispatch({ type: 'LOAD_WORKOUTS', payload: res.data });
      } catch (err: any) {
        if (err.name === 'CanceledError') return;
        console.log(err);
      }
    })();
    return () => controller.abort();
  }, []);

  if (loading) return <Spinner />;
  return (
    <div className="flex flex-col items-center">
      <AppNav />
      {!workouts?.length ? (
        <p className="mt-10 text-xl text-center mx-20">
          👋 Add your first workout by clicking on the map
        </p>
      ) : (
        <ul className="mt-10 w-full h-[60vh] overflow-x-hidden overflow-y-scroll">
          {workouts?.map((workout, i) => {
            const { type } = workout;
            return <WorkoutItem workout={workout} type={type} key={i} />;
          })}
        </ul>
      )}
    </div>
  );
}
