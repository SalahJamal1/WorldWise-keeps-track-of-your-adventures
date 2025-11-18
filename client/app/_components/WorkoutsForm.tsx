'use client';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';
import { useAuth } from '../_hooks/useAuth';
import { IWORKOUT } from '../utils/IWORKOUT';

import { usePosition } from '../_hooks/usePosition';
import { apiCity } from '../_lib/apiCity';
import { convertToEmoji } from '../utils/helper';
import { SubmitHandler, useForm } from 'react-hook-form';
import { AddWorkouts, GetWorkouts } from '../_lib/apiWorkouts';

export default function WorkoutsForm() {
  const { dispatch } = useAuth()!;
  const router = useRouter();
  const inputStyle =
    'bg-light--3 w-full rounded-[5px] py-1 px-2 outline-none text-black focus:bg-light--2 duration-300 transition-all';
  const labelInput = 'font-semibold float-left';
  const { register, handleSubmit, setValue } = useForm<IWORKOUT>({
    defaultValues: {
      type: 'running',
      date: new Date(),
      duration: '',
      distance: '',
      cityName: '',
      coords: [0, 0],
      emoji: '',
      cadence: '',
      pace: 0,
    },
  });
  const { lat, lng } = usePosition();

  useEffect(() => {
    const controller = new AbortController();
    if (lat && lng) {
      (async () => {
        try {
          const res = await apiCity(lat, lng, controller.signal);
          setValue('cityName', res.data.locality || res.data.city);
          setValue('emoji', convertToEmoji(res?.data?.countryCode));
        } catch (err: any) {
          if (err.name === 'CanceledError') return;
          console.log(err);
        }
      })();
    }
    return () => controller.abort();
  }, [lat, lng]);

  const onSubmit: SubmitHandler<IWORKOUT> = async value => {
    const newWorkout: IWORKOUT = {
      ...value,
      pace:
        value.type === 'running'
          ? +value.duration / +value.distance
          : +value.distance / (+value.duration / 60),
      coords: [+lat!, +lng!],
      type: value.type === 'running' ? 0 : 1,
    };

    dispatch({ type: 'LOADER' });
    try {
      const res = await AddWorkouts(newWorkout);
      const data = await GetWorkouts();
      dispatch({ type: 'LOAD_WORKOUTS', payload: data.data });
    } catch (err) {
      console.log(err);
    }
    router.push('/apps/workouts');
  };
  return (
    <form
      className="grid grid-cols-2  bg-dark--2 rounded-[5px]  gap-x-12 gap-y-4 mt-4"
      onSubmit={handleSubmit(onSubmit)}
    >
      <div className="space-y-2">
        <label className="font-semibold block">Type</label>
        <select
          id=""
          {...register('type')}
          className="block w-full bg-light--3 rounded-[5px] py-1 px-2 outline-none text-black focus:bg-light--2 duration-300 transition-all"
        >
          <option value="running">Running</option>
          <option value="cycling">Cycling</option>
        </select>
      </div>
      <div className="space-y-2">
        <label className={labelInput}>Distance</label>

        <input
          type="text"
          required
          className={inputStyle}
          {...register('distance')}
          placeholder="km"
        />
      </div>
      <div className="space-y-2">
        <label className={labelInput}>Duration</label>

        <input
          required
          className={inputStyle}
          {...register('duration')}
          placeholder="min"
        />
      </div>
      <div className="space-y-2">
        <label className={labelInput}>Cadence</label>

        <input
          required
          className={inputStyle}
          {...register('cadence')}
          placeholder="step/min"
        />
      </div>

      <div className="flex items-center justify-between col-span-2">
        <button className="bg-brand--2 rounded-[7px] border-none px-5 py-2 cursor-pointer hover:opacity-90 transition">
          Add
        </button>
        <button
          onClick={e => {
            e.preventDefault();
            router.push('/apps/workouts');
          }}
          className="border px-2.5 py-1.5 rounded-[7px] cursor-pointer font-semibold"
        >
          ← Back
        </button>
      </div>
    </form>
  );
}
