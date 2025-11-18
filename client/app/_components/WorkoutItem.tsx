'use client';
import { useAuth } from '../_hooks/useAuth';
import { IWORKOUT } from '../utils/IWORKOUT';
import { usePathname, useRouter } from 'next/navigation';
import { deleteWorkouts, GetWorkouts } from '../_lib/apiWorkouts';

type Props = {
  workout: IWORKOUT;
  type: string | number;
};
export function WorkoutItem({ workout, type }: Props) {
  const { dispatch } = useAuth()!;

  const handelDeleteWorkout = async (value: number) => {
    dispatch({ type: 'LOADER' });
    try {
      const res = await deleteWorkouts(value);
      const data = await GetWorkouts();
      dispatch({ type: 'LOAD_WORKOUTS', payload: data.data });
    } catch (err) {
      console.log(err);
    }
  };
  const pathname = usePathname();
  const router = useRouter();
  const handelClick = (value: { lat: string; lng: string }) => {
    const params = new URLSearchParams();
    params.set('lat', value.lat);
    params.set('lng', value.lng);
    router.replace(`${pathname}?${params}`);
  };
  return (
    <li
      className={`grid grid-cols-4 grid-rows-2 px-4 py-2 bg-dark--2 rounded-[5px] mb-4 mx-5 border-l-4 workouts--${type} gap-x-4 gap-y-2 cursor-pointer relative`}
      onClick={() =>
        handelClick({
          lat: workout.coords[0].toString(),
          lng: workout.coords[1].toString(),
        })
      }
    >
      <h2 className="text-base font-medium flex-wrap flex items-center gap-1 col-span-4">
        <span className="text-3xl">{workout.emoji}</span>
        <span className="capitalize">{workout.type}</span> in{' '}
        <span> {workout.cityName}</span>
        on{' '}
        {new Date(workout.date).toLocaleString('en-us', {
          month: 'long',
          day: '2-digit',
        })}
      </h2>
      <div className={`flex gap-1 `}>
        <span>{type === 'running' ? '🏃‍♂️' : '🚴‍♀️'}</span>
        <span>{workout.distance}</span>
        <span>km</span>
      </div>
      <div className={`flex gap-1`}>
        <span>⏱</span>
        <span>{workout.duration}</span>
        <span>min</span>
      </div>
      <div className={`flex gap-1 uppercase`}>
        <span>⚡️</span>
        <span>{workout.pace?.toFixed(2)}</span>
        <span>{type === 'running' ? 'min/km' : 'km/h'}</span>
      </div>
      <div className={`flex gap-1 justify-end uppercase`}>
        <span>{type === 'running' ? '🦶🏼' : '⛰'}</span>
        <span>{workout.cadence}</span>
        <span>{type === 'running' ? 'spm' : 'm'}</span>
      </div>
      <button
        onClick={e => {
          e.preventDefault();
          handelDeleteWorkout(workout.id!);
        }}
        className="bg-[#242a2e] rounded-full text-xl w-6 h-6 flex items-center justify-center hover:bg-brand--1 duration-150 transition-all cursor-pointer absolute right-2 top-2"
      >
        &times;
      </button>
    </li>
  );
}
